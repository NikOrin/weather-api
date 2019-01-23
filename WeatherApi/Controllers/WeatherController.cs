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

        private const string LocationNotFound = "Unable to find address. Please make sure this is a valid address";
        private const string WeatherServiceFailure = "Weather service did not return a success response";

        public WeatherController(ILocationService locationService, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _locationService = locationService;
        }

        [HttpGet("{address}")]
        public async Task<ActionResult<WeatherResponseContract>> Get(string address)
        {
            var response = new WeatherResponseContract();
            string errorMessage = null;

            var location = await _locationService.GetAddress(address);

            Forecast forecast = null;
            if (location != null)
                forecast = await _weatherService.GetForecast(location.Longitude, location.Latitude);
            else errorMessage = LocationNotFound;

            if (forecast != null)
                forecast.FormattedAddress = location.FormattedAddress;
            else errorMessage = WeatherServiceFailure;

            response.IsSuccess = forecast != null;
            response.Data = forecast;
            response.ErrorMessage = errorMessage;

            return response;
        }
    }
}
