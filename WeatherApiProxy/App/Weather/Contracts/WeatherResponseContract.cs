using WeatherApiProxy.App.Weather.Models;

namespace WeatherApiProxy.App.Weather.Contracts
{
    public class WeatherResponseContract
    {
        public bool IsSuccess;
        public string ErrorMessage;
        public Forecast Data;
    }
}
