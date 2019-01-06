using DarkSky.Services;
using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather.Contracts;
using WeatherApiProxy.App.Weather.Models;
using DarkSkyForecast = DarkSky.Models.Forecast;
using DarkSkyDataPoint = DarkSky.Models.DataPoint;

namespace WeatherApi.App.Weather
{
    public class WeatherService : IWeatherService
    {
        public ILocationService LocationService { get; set; }
        private string _weatherServiceUri;
        private string _weatherServiceApiKey;

        public WeatherService(string weatherServiceApiKey)
        {
            _weatherServiceApiKey = weatherServiceApiKey;
        }

        public async Task<Forecast> GetForecast(string address)
        {
            var point = LocationService.GetLatLongFromAddress(address);
            
            var service = new DarkSkyService(_weatherServiceApiKey);
            var darkSkyResponse = await service.GetForecast(point.Latitude, point.Longitude);

            if (!darkSkyResponse.IsSuccessStatus) return null;



            return BuildForecast(darkSkyResponse.Response);
        }

        private Forecast BuildForecast(DarkSkyForecast dsForecast)
        {
            var forecast = new Forecast();
            forecast.Current = BuildDataPoint(dsForecast.Currently);

            forecast.Hourly = dsForecast.Hourly.Data.Select(x => BuildDataPoint(x));
            forecast.Daily = dsForecast.Daily.Data.Select(x => BuildDataPoint(x));

            return forecast;
        }

        private DataPoint BuildDataPoint(DarkSkyDataPoint dsDataPoint)
        {
            return new DataPoint
            {
                Time = dsDataPoint.DateTime,
                Summary = dsDataPoint.Summary,
                Temperature = dsDataPoint.Temperature,
                TemperatureHigh = dsDataPoint.TemperatureHigh,
                TemperatureLow = dsDataPoint.TemperatureLow
            };
        }
    }
}
