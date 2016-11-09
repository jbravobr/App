using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace IcatuzinhoApp
{
    public class UserService : BaseService<User>, IUserService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        DTO<User> _utils;

        public UserService(IHttpAccessService httpService,
                           ILogExceptionService log,
                           IAuthenticationService auth)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
        }

        public async Task<bool> GetAuthenticatedUser()
        {
            var resultDB = false;

            try
            {
                var user = GetAllWithChildren().FirstOrDefault();

                if (user != null)
                    resultDB = await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return await Task.FromResult(resultDB);
            }

            return await Task.FromResult(resultDB);
        }

        public async Task<bool> Login(string email, string password)
        {
            var resultDB = false;

            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.UserServiceAddress}{email}/{password}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<User>();
                    var user = await _utils.ConvertSingleObjectFromJson(data.Content);

                    if (user != null)
                    {
                        var _user = new User { Email = email, Password = Crypto.EncryptStringAES(password), Id = user.Id };
                        resultDB = InsertOrReplaceWithChildren(_user);
                    }

                    return resultDB;
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
                    UIFunctions.ShowErrorMessageToUI();

            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }

            return resultDB;
        }
    }
}

