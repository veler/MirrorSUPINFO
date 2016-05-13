using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.Modules.Models.WeatherModel
{
    public class Coord
    {
        #region Properties

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        #endregion       
    }
}
