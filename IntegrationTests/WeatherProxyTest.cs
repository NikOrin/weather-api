using NUnit.Framework;
using System.Diagnostics;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather;

namespace IntegrationTests
{
    public class WeatherProxyTest
    {
        private WeatherProxy _proxy;

        [SetUp]
        public void Setup()
        {
            _proxy = new WeatherProxy("https://localhost:44312/api");
        }

        [Test]
        public void ProxyDoesNotThrow()
        {
            Assert.DoesNotThrowAsync(() => _proxy.GetForecast("260 Broadway, New York City, NY"));
        }

        [Test]
        public async Task ProxyReturnsData()
        {
            var response = await _proxy.GetForecast("260 Broadway, New York City, NY");

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task ProxyIsSuccessful()
        {
            var response = await _proxy.GetForecast("260 Broadway, New York City, NY");

            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Data);
        }

        [Test]
        public async Task InvalidAddressReturnsNotSuccessful()
        {
            var response = await _proxy.GetForecast("12378461237 asdjhfawiehfwafe");

            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNotNull(response.ErrorMessage);
        }
    }
}