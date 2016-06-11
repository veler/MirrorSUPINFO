using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.Modules.Models.WeatherModel
{
    public class System
    {
        #region Properties

        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        #endregion      
    }
}
