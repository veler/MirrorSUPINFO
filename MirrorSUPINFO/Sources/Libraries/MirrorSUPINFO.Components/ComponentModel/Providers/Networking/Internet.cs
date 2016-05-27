using System;
using Windows.Networking.Connectivity;

namespace MirrorSUPINFO.Components.ComponentModel.Providers.Networking
{
    internal static class Internet
    {
        #region Properties

        public static bool IsConnected => NetworkInformation.GetInternetConnectionProfile() != null;

        #endregion  

        #region Events

        public delegate void InternetConnectionChangedHandler(object sender, InternetConnectionChangedEventArgs args);

        public static event InternetConnectionChangedHandler InternetConnectionChanged;

        #endregion

        #region Constructors

        static Internet()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        #endregion

        #region Methods

        private static async void NetworkInformation_NetworkStatusChanged(object sender)
        {
            try
            {
                var networkPresenter = NetworkManager.GetProvider();
                await networkPresenter.GetAvailableNetworksAsync();
                var currentWifiNetwork = networkPresenter.GetCurrentWifiNetwork();

                var arg = new InternetConnectionChangedEventArgs();
                arg.IsConnected = currentWifiNetwork != null;

                if (InternetConnectionChanged != null)
                {
                    InternetConnectionChanged(null, arg);
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }

    internal class InternetConnectionChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }

}
