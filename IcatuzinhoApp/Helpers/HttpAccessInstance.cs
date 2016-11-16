using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Acr.Settings;

namespace IcatuzinhoApp
{
    public sealed class HttpAccessInstance
    {
        static volatile HttpAccessInstance instance;
        static readonly object syncLock = new object();
        static HttpClient _httpClient;

        public HttpAccessInstance()
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri($"{Constants.BaseAddress}"),
                    Timeout = TimeSpan.FromSeconds(40)
                };

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(Settings.Local.Get<string>("AccessToken")))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Local.Get<string>("AccessToken"));

                _httpClient = httpClient;
            }
            catch (Exception ex)
            {
                LogExceptionHelper.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public static HttpClient GetClient
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                            instance = new HttpAccessInstance();
                    }
                }

                return _httpClient;
            }
        }
    }
}