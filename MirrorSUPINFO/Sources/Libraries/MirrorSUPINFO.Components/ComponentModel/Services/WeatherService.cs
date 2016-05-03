using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MirrorSUPINFO.Components.Models;
using Newtonsoft.Json;

namespace MirrorSUPINFO.Components.ComponentModel.Services
{
    internal sealed class WeatherService
    {
        #region Fields

        private const string Token = "6f020331153ad5878279f6a98a72ec51";
        private const string ApiUrl =
           "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&APPID={2}";

        #endregion

        #region Constructor

        public WeatherService(){}

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static async Task<WeatherRoot> GetWeather(double lat, double lon)
        {
            var client = new HttpClient();
            var url = string.Format(ApiUrl, lat, lon, Token);
            var json = await client.GetStringAsync(url);

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<WeatherRoot>(json);
        }

        #endregion

    }
}
