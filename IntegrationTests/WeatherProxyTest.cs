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
            Assert.DoesNotThrowAsync(() => _proxy.GetForecast("936 paloma pl fullerton ca"));
        }

        [Test]
        public async Task ProxyReturnsData()
        {
            var response = await _proxy.GetForecast("936 paloma pl fullerton ca");

            Assert.IsNotNull(response);
        }

        [Test]
        public async Task ProxyIsSuccessful()
        {
            var response = await _proxy.GetForecast("936 paloma pl fullerton ca");

            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Data);
        }
    }
}