using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.App.Location.Models;

namespace WeatherApi.App.Location
{
    public interface ILocationService
    {
        Task<Address> GetAddress(string address);
    }
}
