using MirrorSUPINFO.Components.ComponentModel.Providers.Networking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MirrorSUPINFO.Components.ComponentModel.Services
{
    internal sealed class ClockService
    {
        #region Fields

        private static ClockService _clockService;
        public DateTime Now { get; private set; }

        #endregion

        #region Events

        public delegate void DateTimeChangedHandler(object sender, DateTimeChangedEventArgs args);

        public event DateTimeChangedHandler DateTimeChanged;
        #endregion

        #region Constructor

        public ClockService()
        {
            Internet.InternetConnectionChanged += Internet_InternetConnectionChanged;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            Now = Now.AddSeconds(1);
            if (DateTimeChanged != null)
            {
                var arg = new DateTimeChangedEventArgs();
                arg.currentTime = Now;
                DateTimeChanged(this, arg);
            }
        }

        private async void Internet_InternetConnectionChanged(object sender, InternetConnectionChangedEventArgs args)
        {
            if (args.IsConnected)
            {
                Now = (DateTime) await GetNistTime();
                if (DateTimeChanged != null)
                {
                    var arg = new DateTimeChangedEventArgs();
                    arg.currentTime = Now;
                    DateTimeChanged(this, arg);
                }
                    
            }
        }

        
        #endregion

        #region Methods
        internal static ClockService GetService()
        {
            return _clockService ?? (_clockService = new ClockService());
        }
         
        private async Task<DateTime?> GetNistTime()
        {
            DateTime? dateTime = null;
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://nist.netservicesgroup.com:13/"));
                Debug.WriteLine(request.ToString());
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
         
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string html = await httpResponseMessage.Content.ReadAsStringAsync();
                    string time = Regex.Match(html, @"\d+:\d+:\d+").Value; //HH:mm:ss format
                    string date = "20" + Regex.Match(html, @"\d+-\d+-\d+").Value; //20XX-MM-DD
                    dateTime = DateTime.Parse((date + " " + time));
                    TimeZoneInfo src = TimeZoneInfo.Utc;
                    TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
                    Debug.WriteLine(TimeZoneInfo.ConvertTime((DateTime)dateTime, src, tz));
                }
            }
            catch (Exception e) { }
            return dateTime;
        }
        #endregion 
    }
    
}
