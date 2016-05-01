using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.UI.Core;
using GalaSoft.MvvmLight.Messaging;
using MirrorSUPINFO.Components.ComponentModel.Providers;
using MirrorSUPINFO.SDK;

namespace MirrorSUPINFO.Components.ComponentModel.Services.Module
{
    sealed class ModuleManager
    {
        #region Fields

        private readonly CoreDispatcher _dispatcher;
        private readonly Queue<Task> _periodicTastQueue;

        private bool _periodicTaskInProgress;

        private static ModuleManager _moduleManager;

        #endregion

        #region Properties

        private ObservableCollection<MirrorModule> Modules { get; set; }

        public Tools Tools { get; set; }

        #endregion

        #region Constructors

        public ModuleManager()
        {
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            _periodicTastQueue = new Queue<Task>();

            Initialize();
        }

        #endregion

        #region Methods

        internal static ModuleManager GetService()
        {
            return _moduleManager ?? (_moduleManager = new ModuleManager());
        }

        internal Collection<MirrorModule> GetModules()
        {
            return Modules;
        }

        internal MirrorModule GetModules(string moduleName)
        {
            return Modules.FirstOrDefault(module => module.ModuleName == moduleName);
        }

        internal MirrorModule GetModulesByTitle(string moduleTitle, bool caseInsensitive = false)
        {
            if (caseInsensitive)
            {
                return Modules.FirstOrDefault(module => string.Equals(module.ModuleTitle, moduleTitle, StringComparison.CurrentCultureIgnoreCase));
            }
            return Modules.FirstOrDefault(module => module.ModuleTitle == moduleTitle);
        }

        private void Initialize()
        {
            var assemblyNames = new List<string> { "MirrorSUPINFO.Apps, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" };
            var mirrorModuleType = typeof(MirrorModule);
            Tools = new Tools();

            InitializeNetwork();

            RegisterMessages();

            Modules = new ObservableCollection<MirrorModule>();

            foreach (var assemblyName in assemblyNames)
            {
                var assembly = Assembly.Load(new AssemblyName(assemblyName));

                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.IsSubclassOf(mirrorModuleType))
                    {
                        Modules.Add((MirrorModule)Activator.CreateInstance(type.AsType(), Tools));
                    }
                }
            }
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<NotificationMessage>(this, async (notification) =>
            {
                await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var notificationTarget = notification.Target.ToString();
                    if (notificationTarget == "CallPrimaryPeriodicTask" || notificationTarget == "CallSecondaryPeriodicTask")
                    {
                        Task task;
                        var module = GetModules(notification.Notification);

                        if (notificationTarget == "CallPrimaryPeriodicTask")
                        {
                            task = new Task(() => module.PrimaryPeriodicTask());
                        }
                        else
                        {
                            task = new Task(() => module.SecondaryPeriodicTask());
                        }

                        _periodicTastQueue.Enqueue(task);
                        RunPeriodicTask();
                    }
                });
            });
        }

        private async Task InitializeNetwork()
        {
            try
            {
                var ssid = SettingManager.GetProvider().NetworkSsid;
                var password = SettingManager.GetProvider().NetworkPassword;

                if (string.IsNullOrEmpty(ssid) || !await Tools.NetworkService.WifiIsAvailableAsync())
                {
                    return;
                }

                var networks = await Tools.NetworkService.GetAvailableNetworksAsync();

                if (networks.Count == 0)
                {
                    return;
                }

                var desiredNetwork = networks.FirstOrDefault(network => network.Ssid == ssid);
                if (desiredNetwork == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(password))
                {
                    await Tools.NetworkService.ConnectToNetworkAsync(desiredNetwork, true);
                }
                else
                {
                    await Tools.NetworkService.ConnectToNetworkWithPasswordAsync(desiredNetwork, true, new PasswordCredential { Password = password });
                }
            }
            catch
            {

            }
        }

        private async void RunPeriodicTask()
        {
            if (_periodicTaskInProgress)
            {
                return;
            }

            _periodicTaskInProgress = true;

            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                while (_periodicTastQueue.Count > 0)
                {
                    var task = _periodicTastQueue.Dequeue();
                    task.RunSynchronously();
                }
            });

            _periodicTaskInProgress = false;
        }

        #endregion
    }
}
