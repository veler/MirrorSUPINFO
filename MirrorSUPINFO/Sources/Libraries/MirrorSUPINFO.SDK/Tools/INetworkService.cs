using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Security.Credentials;

namespace MirrorSUPINFO.SDK.Tools
{
    public interface INetworkService
    {
        #region Methods

        bool IsConnectedToInternet { get; }

        Task<bool> WifiIsAvailableAsync();

        string GetCurrentIpv4Address();

        WiFiAvailableNetwork GetCurrentWifiNetwork();

        Task<IList<WiFiAvailableNetwork>> GetAvailableNetworksAsync();

        Task<bool> ConnectToNetworkAsync(WiFiAvailableNetwork network, bool autoConnect);

        Task<bool> ConnectToNetworkWithPasswordAsync(WiFiAvailableNetwork network, bool autoConnect, PasswordCredential password);

        void DisconnectNetwork(WiFiAvailableNetwork network);

        #endregion

        #region Events

        event EventHandler<bool> InternetConnectionChanged;

        #endregion
    }
}
