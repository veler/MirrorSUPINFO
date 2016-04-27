using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MirrorSUPINFO.Components.ComponentModel.Services
{
    class ClockService
    {
        private async Task<DateTime?> GetNistTime()
        {
            DateTime? dateTime = null;
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://nist.netservicesgroup.com:13/"));
            Debug.WriteLine(request.ToString());
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            //string text = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                string html = await httpResponseMessage.Content.ReadAsStringAsync();
                string time = Regex.Match(html, @"\d+:\d+:\d+").Value; //HH:mm:ss format
                string date = "20" + Regex.Match(html, @"\d+-\d+-\d+").Value; //20XX-MM-DD
                // string date = Regex.Match(html, @">\w+,\s\w+\s\d+,\s\d+<").Value; //dddd, MMMM dd, yyyy
                dateTime = DateTime.Parse((date + " " + time));
                TimeZoneInfo src = TimeZoneInfo.Utc;
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
                Debug.WriteLine(TimeZoneInfo.ConvertTime((DateTime)dateTime, src, tz));
            }
            return dateTime;
        }

    }

}
