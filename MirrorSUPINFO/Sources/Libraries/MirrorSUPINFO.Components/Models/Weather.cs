using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MirrorSUPINFO.Components.Models
{

    public class WeatherRoot
    {
        #region Properties
        [JsonProperty("coord")]
        public Coord Coordinates { get; set; }

        [JsonProperty("sys")]
        public Sys System { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("@base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public Main MainWeather { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        public int dt { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }
        #endregion       
    }
    public class Coord
    {
        #region Properties

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        #endregion       
    }

    public class Sys
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

    public class Weather
    {
        #region Properties
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
        #endregion       
    }

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

    public class Wind
    {
        #region Properties
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Deg { get; set; }
        #endregion       
    }

    public class Clouds
    {
        #region Properties
        [JsonProperty("all")]
        public int All { get; set; }
        #endregion        
    }
    
}
