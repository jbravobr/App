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
        HttpClient _httpClient = Helpers.ReturnClient();

        [Test]
        public async Task ReturnWeatherFromAPI()
        {
            var weather = new Weather();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"icatuzinhoapi/api/weather");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                weather = JsonConvert.DeserializeObject<Weather>(stringJson);
            }

            Assert.Greater(Convert.ToInt32(weather.Temp), 0);
        }

        [Test]
        public async Task ReturnWeatherFromAPIWithAuthenticate()
        {
            var username = "teste@icatuseguros.com.br";
            var password = "Icatu123!";
            var isEncrypted = false;

            var client = new HttpClient
            {
                BaseAddress = new Uri($"{Constants.BaseAddress}"),
                Timeout = TimeSpan.FromSeconds(40)
            };

            //grant_type, refresh_token
            //refresh_token, token

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Constants.FormsAuthentication}");
            request.Content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username",username),
                    new KeyValuePair<string, string>("password",isEncrypted ?
                                                     Crypto.EncryptStringAES(password):
                                                     password)
                });

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var authenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(jsonString);

                if (authenticationToken != null)
                    authenticationToken.SetExpirationTime();

                client = null;
                client = Helpers.ReturnClient(authenticationToken.AccessToken);

                var result = await client.GetAsync($"icatuzinhoapi/api/weather/weatherAuth");

                if (result.IsSuccessStatusCode)
                {
                    var stringJson = await result.Content.ReadAsStringAsync();
                    var weather = JsonConvert.DeserializeObject<Weather>(stringJson);
                    Assert.Greater(Convert.ToInt32(weather.Temp), 0);
                }
            }
        }
    }
}

