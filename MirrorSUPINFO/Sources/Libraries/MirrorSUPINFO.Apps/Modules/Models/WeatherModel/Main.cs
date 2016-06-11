using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.Modules.Models.WeatherModel
{
    public class Main
    {
        #region Properties

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("temp_min")]
        public double MinimumTemp { get; set; }

        [JsonProperty("temp_max")]
        public double MaximumTemp { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        #endregion      
    }
}
