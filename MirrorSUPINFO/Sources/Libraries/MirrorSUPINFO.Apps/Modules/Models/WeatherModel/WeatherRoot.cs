using System.Collections.Generic;
using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.Modules.Models.WeatherModel
{
    public class WeatherRoot
    {
        #region Properties

        [JsonProperty("coord")]
        public Coord Coordinates { get; set; }

        [JsonProperty("sys")]
        public WeatherModel.System System { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("@base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public Main MainWeather { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Cloud Clouds { get; set; }

        public int dt { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }

        #endregion       
    }
}
