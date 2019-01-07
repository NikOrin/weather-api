using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WeatherApi.App.Weather;
using WeatherApiProxy.App.Weather.Contracts;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _weatherService;

        public WeatherController(ILocationService locationService, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _weatherService.LocationService = locationService;
        }

        [HttpGet("{address}")]
        public async Task<ActionResult<WeatherResponseContract>> Get(string address)
        {
            (var forecast, string errorMessage) = await _weatherService.GetForecast(address);

            var response = new WeatherResponseContract();

            response.IsSuccess = forecast != null;
            response.Data = forecast;
            response.ErrorMessage = errorMessage;

            return response;
        }
    }
}
