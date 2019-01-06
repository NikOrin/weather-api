using System.Collections.Generic;

namespace WeatherApiProxy.App.Weather.Models
{
    public class Forecast
    {
        public double Latitude;
        public double Longitude;
        public string TimeZone;
        public DataPoint Current;
        public IEnumerable<DataPoint> Hourly;
        public IEnumerable<DataPoint> Daily;
    }
}
