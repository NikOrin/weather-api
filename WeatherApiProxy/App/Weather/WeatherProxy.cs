using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather.Contracts;


namespace WeatherApiProxy.App.Weather
{
    public class WeatherProxy
    {
        private string _baseUri;

        public WeatherProxy(string baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<WeatherResponseContract> GetForecast(string address)
        {
            var client = new HttpClient();

            var response = await client.GetAsync($"{_baseUri}/weather/{address}");

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadAsAsync<WeatherResponseContract>();
        }
    }
}
