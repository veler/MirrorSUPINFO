using System;
using MirrorSUPINFO.Components.ComponentModel.Providers.Networking;
using MirrorSUPINFO.SDK;
using MirrorSUPINFO.SDK.Tools;

namespace MirrorSUPINFO.Components.ComponentModel.Services.Module
{
    sealed class Tools : ITools
    {
        #region Properties

        public INavigationService NavigationService { get; }

        public INetworkService NetworkService { get; }

        public IAuthenticationService AuthenticationService { get; }

        public DateTime CurrentDateTime { get; }

        #endregion

        #region Constructors

        public Tools()
        {
            NetworkService = NetworkManager.GetProvider();
            //TODO : NavigationService, AuthenticationService, CurrentDateTime
        }

        #endregion

        #region Methods

        public MirrorModule GetModule(string moduleName)
        {
            return ModuleManager.GetService().GetModules(moduleName);
        }

        public void SpeakAsync(string text)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
