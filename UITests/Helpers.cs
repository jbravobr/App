using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IcatuzinhoApp.UITests
{
    public static class Helpers
    {
        public static HttpClient ReturnClient()
        {
            var baseAddress = new Uri("http://labdev.labdevmobile.com.br/");

            var httpClient = new HttpClient
            {
                BaseAddress = baseAddress,
                Timeout = TimeSpan.FromSeconds(40)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public static HttpClient ReturnClient(string accessToken)
        {
            try
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("http://labdev.labdevmobile.com.br/"),
                    Timeout = TimeSpan.FromSeconds(40)
                };

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                return httpClient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

