using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IcatuzinhoApp.UITests
{
    public static class Helpers
    {
        public static HttpClient ReturnClient(string accessToken)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.BaseAddress),
                Timeout = TimeSpan.FromSeconds(40)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return httpClient;
        }

        public static async Task<string> GenerateTokenAuthentication()
        {
            var username = "teste@icatuseguros.com.br";
            var password = "Icatu123!";

            var client = new HttpClient
            {
                BaseAddress = new Uri(Constants.BaseAddress),
                Timeout = TimeSpan.FromSeconds(40)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, Constants.FormsAuthentication);

            request.Content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username",username),
                    new KeyValuePair<string, string>("password",password)
            });

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var authenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(jsonString);

                return authenticationToken.AccessToken;
            }

            return string.Empty;
        }
    }
}

