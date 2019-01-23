﻿using System.Threading.Tasks;
using WeatherApiProxy.App.Weather.Models;

namespace WeatherApi.App.Weather
{
    public interface IWeatherService
    {
        Task<Forecast> GetForecast(double longitude, double latitude);
    }
}