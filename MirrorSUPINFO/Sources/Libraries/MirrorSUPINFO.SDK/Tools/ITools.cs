using System;

namespace MirrorSUPINFO.SDK.Tools
{
    public interface ITools
    {
        #region Properties

        INavigationService NavigationService { get; }

        INetworkService NetworkService { get; }
        
        IAuthenticationService AuthenticationService { get; }

        DateTime CurrentDateTime { get; }

        #endregion

        #region Methods

        MirrorModule GetModule(string moduleName);

        void SpeakAsync(string text);
        
        #endregion
    }
}