using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.Modules.Models.WeatherModel
{
    public class Wind
    {
        #region Properties

        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Deg { get; set; }

        #endregion       
    }
}
