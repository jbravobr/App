using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public class WeatherService : IWeatherService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        IWeatherRepository _repo;
        DTO<Weather> _utils;

        public WeatherService(IHttpAccessService httpService,
                              ILogExceptionService log,
                              IAuthenticationService auth,
                              IWeatherRepository repo)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
            _repo = repo;
        }

        public async Task GetWeather()
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.WeatherServiceAddress}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<Weather>();
                    var weather = await _utils.ConvertSingleObjectFromJson(data.Content);

                    if (weather != null)
                        Insert(weather);
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
        }

        public bool Insert(Weather entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<Weather> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(Weather entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(Weather entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<Weather> GetAll(Expression<Func<Weather, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<Weather> GetAll()
        {
            return _repo.GetAll();
        }

        public Weather Get(Expression<Func<Weather, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public Weather Get()
        {
            return _repo.Get();
        }

        public Weather GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

