using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IcatuzinhoApp
{
    public class AuthenticationService : IAuthenticationService
    {
        IHttpAccessService _httpClient;
        ILogExceptionService _log;
        IAuthenticationRepository _repo;

        public AuthenticationService(IHttpAccessService httpClient, 
                                     ILogExceptionService log,
                                     IAuthenticationRepository repo)
        {
            _httpClient = httpClient;
            _log = log;
            _repo = repo;
        }

        public async Task<bool> RefreshToken()
        {
            var token = _repo.Get();

            if (token != null)
            {
                try
                {
                    var client = _httpClient.Init(token.AccessToken);

                    var request = new HttpRequestMessage(HttpMethod.Post, $"{Constants.FormsAuthentication}");
                    request.Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "token_refresh"),
                        new KeyValuePair<string, string>("token_refresh",token.AccessToken)
                    });

                    var response = await client.SendAsync(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var authenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(jsonString);

                        Insert(authenticationToken);

                        return await Task.FromResult(true);
                    }
                }
                catch (Exception ex)
                {
                    _log.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return await Task.FromResult(false);
                }
            }

            return await Task.FromResult(false);
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

                    Insert(authenticationToken);

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

        public bool Insert(AuthenticationToken entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<AuthenticationToken> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(AuthenticationToken entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(AuthenticationToken entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<AuthenticationToken> GetAll(Expression<Func<AuthenticationToken, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<AuthenticationToken> GetAll()
        {
            return _repo.GetAll();
        }

        public AuthenticationToken Get(Expression<Func<AuthenticationToken, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public AuthenticationToken Get()
        {
            return _repo.Get();
        }

        public AuthenticationToken GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

