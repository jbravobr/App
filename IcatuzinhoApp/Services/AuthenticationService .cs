using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ModernHttpClient;

namespace IcatuzinhoApp
{
    public class AuthenticationService : BaseService<AuthenticationToken>, IAuthenticationService
    {
        IHttpAccessService _httpClient;
        ILogExceptionService _log;

        public AuthenticationService(IHttpAccessService httpClient, ILogExceptionService log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<bool> AuthenticationWithFormUrlEncoded(string username, string password, bool isEncrypted)
        {
            try
            {
                var client = _httpClient.Init();

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

                    base.InsertOrReplaceWithChildren(authenticationToken);

                    return await Task.FromResult(true);
                }

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                    _log.SubmitToInsights(new ArgumentException($"Erro na autorização para o email: {username}"));
                }

                if (response == null)
                    UIFunctions.ShowErrorMessageToUI();

                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return await Task.FromResult(false);
            }
        }
    }
}

