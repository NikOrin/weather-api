using GoogleMaps.LocationServices;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather.Models;

namespace WeatherApi.App.Weather
{
    public interface IWeatherService
    {
        ILocationService LocationService { get; set; }
        Task<Forecast> GetForecast(string address);
    }
}