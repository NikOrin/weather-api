using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.App.Location.Models;
using WeatherApi.App.Location.Models.GoogleGeocode;

namespace WeatherApi.App.Location
{
    public class GoogleGeocodeLocationService : ILocationService
    {
        private const string ApiUrl = @"https://maps.googleapis.com/maps/api/geocode/json?";
        private readonly string _apiKey;

        public GoogleGeocodeLocationService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<Address> GetAddress(string address)
        {
            var client = new HttpClient();

            var response = await client.GetAsync($"{ApiUrl}address={address}&key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var googleReponse = await response.Content.ReadAsAsync<GoogleGeocodeResponse>();

            if (googleReponse.Status != "OK") return null;

            var location = googleReponse.Results.FirstOrDefault();

            return new Address
            {
                Latitude = location.Geometry.Location.Lat,
                Longitude = location.Geometry.Location.Lng,
                FormattedAddress = location.Formatted_Address
            };
        }
    }
}
