using System;
using System.Collections.Generic;

namespace WeatherApiProxy.App.Weather.Models
{
    public class Alert
    {
        public DateTimeOffset Time;
        public string Description;
        public DateTimeOffset ExpirationDate;
        public IEnumerable<string> Regions;
        public string Severity;
        public string Title;
        public Uri Uri;
    }
}
