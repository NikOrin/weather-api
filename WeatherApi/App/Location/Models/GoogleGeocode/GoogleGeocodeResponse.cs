using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.App.Location.Models.GoogleGeocode
{
    public class GoogleGeocodeResponse
    {
        public List<Results> Results;
        public string Status;
    }
}
