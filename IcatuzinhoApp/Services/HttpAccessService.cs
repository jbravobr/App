using System;
using System.Net.Http;
using System.Net.Http.Headers;
using ModernHttpClient;

namespace IcatuzinhoApp
{
    public class HttpAccessService : IHttpAccessService
    {
        readonly ILogExceptionService _log;

        public HttpAccessService(ILogExceptionService log)
        {
            _log = log;
        }

        public HttpClient Init(string accessToken = null)
        {
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler())
                {
                    BaseAddress = new Uri($"{Constants.BaseAddress}"),
                    Timeout = TimeSpan.FromSeconds(40)
                };

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrEmpty(accessToken))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                return httpClient;
            }
            catch (TimeoutException tEx)
            {
                _log.SubmitToInsights(tEx);
                UIFunctions.ShowErrorMessageToUI("Ops, algum problema com nossos servidores, por favor tente novamente.");
                return null;
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return null;
            }
        }
    }
}

