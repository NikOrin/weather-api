using DarkSky.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather.Contracts;
using WeatherApiProxy.App.Weather.Models;
using DarkSkyForecast = DarkSky.Models.Forecast;
using DarkSkyDataPoint = DarkSky.Models.DataPoint;
using DarkSkyAlert = DarkSky.Models.Alert;
using WeatherApi.App.Location;

namespace WeatherApi.App.Weather
{
    public class DarkSkyWeatherService : IWeatherService
    {
        private string _weatherServiceUri;
        private string _weatherServiceApiKey;

        public DarkSkyWeatherService(string weatherServiceApiKey)
        {
            _weatherServiceApiKey = weatherServiceApiKey;
        }

        public async Task<(Forecast, string)> GetForecast(double longitude, double latitude)
        {
            var service = new DarkSkyService(_weatherServiceApiKey);
            var darkSkyResponse = await service.GetForecast(latitude, longitude);

            if (!darkSkyResponse.IsSuccessStatus) return (null, "Weather service did not return a success response");

            return (BuildForecast(darkSkyResponse.Response), null);
        }

        private Forecast BuildForecast(DarkSkyForecast dsForecast)
        {
            var forecast = new Forecast();
            forecast.Current = BuildDataPoint(dsForecast.Currently);

            forecast.Hourly = dsForecast.Hourly.Data.Select(x => BuildDataPoint(x));
            forecast.Daily = dsForecast.Daily.Data.Select(x => BuildDataPoint(x));

            if (dsForecast.Alerts != null)
                forecast.Alerts = dsForecast.Alerts.Select(x => BuildAlert(x));

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
                TemperatureLow = dsDataPoint.TemperatureLow,
                TemperatureFeelsLike = dsDataPoint.ApparentTemperature,
                PrecipitationProbability = dsDataPoint.PrecipProbability
            };
        }

        private Alert BuildAlert(DarkSkyAlert dsAlert)
        {
            return new Alert
            {
                Time = dsAlert.DateTime,
                Description = dsAlert.Description,
                ExpirationDate = dsAlert.ExpiresDateTime,
                Regions = dsAlert.Regions,
                Severity = dsAlert.Severity,
                Title = dsAlert.Title,
                Uri = dsAlert.Uri
            };
        }
    }
}
