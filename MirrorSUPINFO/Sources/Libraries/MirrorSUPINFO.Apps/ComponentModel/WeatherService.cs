using System.Net.Http;
using System.Threading.Tasks;
using MirrorSUPINFO.Apps.Modules.Models.WeatherModel;
using Newtonsoft.Json;

namespace MirrorSUPINFO.Apps.ComponentModel
{
    internal sealed class WeatherService
    {
        #region Fields

        private const string Token = "6f020331153ad5878279f6a98a72ec51";
        private const string ApiUrl =
            "http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&APPID={1}";

        #endregion

        #region Constructor

        public WeatherService()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get weather by location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static async Task<WeatherRoot> GetWeather(string location)
        {
            var client = new HttpClient();
            var url = string.Format(ApiUrl, location, Token);
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
