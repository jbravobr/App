using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public class WeatherService : BaseService<Weather>, IWeatherService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        DTO<Weather> _utils;

        public WeatherService(IHttpAccessService httpService,
                              ILogExceptionService log,
                              IAuthenticationService auth)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
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
                        InsertOrReplaceWithChildren(weather);
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
    }
}

