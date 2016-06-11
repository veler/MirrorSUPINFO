using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.Modules.Models.WeatherModel
{
    public class Cloud
    {
        #region Properties

        [JsonProperty("all")]
        public int All { get; set; }

        #endregion        
    }
}
