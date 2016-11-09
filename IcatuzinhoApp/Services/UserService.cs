using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace IcatuzinhoApp
{
    public class UserService : IUserService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        IUserRepository _repo;
        DTO<User> _utils;

        public UserService(IHttpAccessService httpService,
                           ILogExceptionService log,
                           IAuthenticationService auth,
                           IUserRepository repo)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
            _repo = repo;
        }

        public async Task<bool> GetAuthenticatedUser()
        {
            var resultDB = false;

            try
            {
                var user = Get();

                if (user != null)
                {
                    resultDB = await Task.FromResult(true);
                    RegisterLocalAuthenticatedUser();
                }
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
                        resultDB = Insert(_user);
                        RegisterLocalAuthenticatedUser();
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

        private void RegisterLocalAuthenticatedUser()
        {
            var user = Get();

            if (user != null)
                App.UserAuthenticated = user;
        }

        public bool Insert(User entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<User> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(User entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(User entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<User> GetAll(Expression<Func<User, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<User> GetAll()
        {
            return _repo.GetAll();
        }

        public User Get(Expression<Func<User, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public User Get()
        {
            return _repo.Get();
        }

        public User GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

