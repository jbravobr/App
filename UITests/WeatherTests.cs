using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class WeatherTests
    {
        HttpClient _httpClient;

        [Test]
        public async Task ReturnWeatherFromAPI()
        {
            var weather = new Weather();
            var token = await Helpers.GenerateTokenAuthentication();

            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync(Constants.WeatherServiceAddress);

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                weather = JsonConvert.DeserializeObject<Weather>(stringJson);
            }

            Assert.Greater(Convert.ToInt32(weather.Temp), 0);
        }
    }
}

