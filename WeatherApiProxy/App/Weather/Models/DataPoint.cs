using System;

namespace WeatherApiProxy.App.Weather.Models
{
    public class DataPoint
    {
        public DateTimeOffset Time;
        public string Summary;
        public double? Temperature;
        public double? TemperatureLow;
        public double? TemperatureHigh;
        public double? TemperatureFeelsLike;
        public double? PrecipitationProbability;
    }
}
