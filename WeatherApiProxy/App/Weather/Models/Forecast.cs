using System.Collections.Generic;

namespace WeatherApiProxy.App.Weather.Models
{
    public class Forecast
    {
        public string TimeZone;
        public string FormattedAddress;
        public DataPoint Current;
        public IEnumerable<DataPoint> Hourly;
        public IEnumerable<DataPoint> Daily;
        public IEnumerable<Alert> Alerts;
    }
}
