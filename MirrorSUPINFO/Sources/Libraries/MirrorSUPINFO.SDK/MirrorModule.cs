using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;
using MirrorSUPINFO.SDK.Enums;
using MirrorSUPINFO.SDK.Models;
using MirrorSUPINFO.SDK.Tools;

namespace MirrorSUPINFO.SDK
{
    public abstract class MirrorModule : INotifyPropertyChanged
    {
        #region Fields

        public readonly CoreDispatcher _dispatcher;

        private readonly DispatcherTimer _primaryPeriodicTaskTimer;
        private readonly DispatcherTimer _secondaryPeriodicTaskTimer;

        private BadgeNotification _badgeNotification;

        #endregion

        #region Properties

        public ITools Tools { get; }

        public string ModuleName { get; }

        public ObservableCollection<Notification> Notifications { get; private set; }

        public BadgeNotification BadgeNotification
        {
            get { return _badgeNotification; }
            set
            {
                _badgeNotification = value;
                OnPropertyChanged();
            }
        }

        public ApplicationDataContainer ModuleSettings { get; private set; }

        public StorageFolder ModuleLocalStorage { get; private set; }

        public virtual string BlackIconPath { get; }

        public abstract string ModuleTitle { get; }

        public abstract string IconPath { get; }

        public abstract string VoiceCommandsFilePath { get; }

        public abstract PeriodicTaskTime PrimaryPeriodicTaskTime { get; }

        public abstract PeriodicTaskTime SecondaryPeriodicTaskTime { get; }

        public abstract Type MainPage { get; }

        #endregion

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        protected MirrorModule(ITools tools)
        {
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            Tools = tools;
            ModuleName = GetType().Name;

            if (string.IsNullOrWhiteSpace(IconPath) || !IconPath.ToLower().EndsWith(".xaml"))
            {
                throw new Exception($"The module {ModuleName}'s icon path is invalid.");
            }

            if (MainPage != null && !typeof(ModulePage).IsAssignableFrom(MainPage))
            {
                throw new InvalidCastException($"The MainPage's type should be an {nameof(ModulePage)}.");
            }

            _primaryPeriodicTaskTimer = new DispatcherTimer();
            _secondaryPeriodicTaskTimer = new DispatcherTimer();

            Initialize();
        }

        #endregion

        #region Methods

        private async void Initialize()
        {
            Notifications = new ObservableCollection<Notification>();
            ModuleSettings = ApplicationData.Current.LocalSettings.CreateContainer(ModuleName, ApplicationDataCreateDisposition.Always);
            ModuleLocalStorage = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ModuleName, CreationCollisionOption.OpenIfExists);

            _primaryPeriodicTaskTimer.Interval = GetPeriodicTaskTimeSpan(PrimaryPeriodicTaskTime);
            if (_primaryPeriodicTaskTimer.Interval != TimeSpan.Zero)
            {
                _primaryPeriodicTaskTimer.Tick += PrimaryPeriodicTaskTimer_Tick;
                _primaryPeriodicTaskTimer.Start();
                PrimaryPeriodicTaskTimer_Tick(null, null);
            }

            _secondaryPeriodicTaskTimer.Interval = GetPeriodicTaskTimeSpan(SecondaryPeriodicTaskTime);
            if (_secondaryPeriodicTaskTimer.Interval != TimeSpan.Zero)
            {
                _secondaryPeriodicTaskTimer.Tick += SecondaryPeriodicTaskTimer_Tick;
                _secondaryPeriodicTaskTimer.Start();
                SecondaryPeriodicTaskTimer_Tick(null, null);
            }
        }

        public abstract void PrimaryPeriodicTask();

        public abstract void SecondaryPeriodicTask();

        public abstract void VoiceCommandDetected(string commandName, string text, string[] textSplitted);

        private TimeSpan GetPeriodicTaskTimeSpan(PeriodicTaskTime time)
        {
            switch (time)
            {
                case PeriodicTaskTime.Never:
                    return TimeSpan.Zero;
                case PeriodicTaskTime.OneMinute:
                    return TimeSpan.FromMinutes(1);
                case PeriodicTaskTime.FiveMinutes:
                    return TimeSpan.FromMinutes(5);
                case PeriodicTaskTime.TenMinutes:
                    return TimeSpan.FromMinutes(10);
                case PeriodicTaskTime.ThirtyMinutes:
                    return TimeSpan.FromMinutes(30);
                case PeriodicTaskTime.SixtyMinutes:
                    return TimeSpan.FromMinutes(60);
                default:
                    throw new ArgumentOutOfRangeException(nameof(time), time, null);
            }
        }

        private async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        #endregion

        #region Handled Methods

        private void PrimaryPeriodicTaskTimer_Tick(object sender, object e)
        {
            Messenger.Default.Send(new NotificationMessage(this, "CallPrimaryPeriodicTask", ModuleName));
        }

        private void SecondaryPeriodicTaskTimer_Tick(object sender, object e)
        {
            Messenger.Default.Send(new NotificationMessage(this, "CallSecondaryPeriodicTask", ModuleName));
        }

        #endregion
    }
}
