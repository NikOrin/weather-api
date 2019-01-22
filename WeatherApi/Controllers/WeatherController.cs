using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WeatherApi.App.Location;
using WeatherApi.App.Location.Models;
using WeatherApi.App.Weather;
using WeatherApiProxy.App.Weather.Contracts;
using WeatherApiProxy.App.Weather.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _weatherService;
        private ILocationService _locationService;

        public WeatherController(ILocationService locationService, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _locationService = locationService;
        }

        [HttpGet("{address}")]
        public async Task<ActionResult<WeatherResponseContract>> Get(string address)
        {
            var response = new WeatherResponseContract();
            string errorMessage;

            Address location;
            (location, errorMessage) = await _locationService.GetAddress(address);

            Forecast forecast = null;
            if (location != null)
                (forecast, errorMessage) = await _weatherService.GetForecast(location.Longitude, location.Latitude);

            if (forecast != null)
                forecast.FormattedAddress = location.FormattedAddress;

            response.IsSuccess = forecast != null;
            response.Data = forecast;
            response.ErrorMessage = errorMessage;

            return response;
        }
    }
}
