using MirrorSUPINFO.Components.ComponentModel.Providers.Networking;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MirrorSUPINFO.Components.ComponentModel.Services
{
    internal sealed class ClockService
    {
        #region Fields

        private static ClockService _clockService;

        #endregion

        #region Properties
        
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
            var timer = new DispatcherTimer();
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
                arg.CurrentTime = Now;
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
                    arg.CurrentTime = Now;
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
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://nist.netservicesgroup.com:13/"));
                var httpResponseMessage = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
         
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var html = await httpResponseMessage.Content.ReadAsStringAsync();
                    var time = Regex.Match(html, @"\d+:\d+:\d+").Value; //HH:mm:ss format
                    var date = "20" + Regex.Match(html, @"\d+-\d+-\d+").Value; //20XX-MM-DD
                    dateTime = DateTime.Parse((date + " " + time));
                    var src = TimeZoneInfo.Utc;
                    var tz = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
                    dateTime = TimeZoneInfo.ConvertTime((DateTime)dateTime, src, tz);
                }
            }
            catch (Exception e) { }
            return dateTime;
        }
        #endregion 
    }
    
}
