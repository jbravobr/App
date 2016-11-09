using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IcatuzinhoApp
{
    public class TravelService : ITravelService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        ITravelRepository _repo;
        DTO<Travel> _utils;

        public TravelService(IHttpAccessService httpService,
                             ILogExceptionService log,
                             IAuthenticationService auth,
                             ITravelRepository repo)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
            _repo = repo;
        }

        public async Task GetTravelByScheduleId(int scheduleId)
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.TravelServiceAddress}{scheduleId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<Travel>();
                    var travel = await _utils.ConvertSingleObjectFromJson(data.Content);

                    if (travel != null)
                        Insert(travel);
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

        public async Task<int?> GetSeatsAvailableByTravel(int scheduleId)
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.TravelServiceAddress}{scheduleId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<Travel>();
                    var travel = await _utils.ConvertSingleObjectFromJson(data.Content);

                    return travel.Vehicle.SeatsAvailable;
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
                    UIFunctions.ShowErrorMessageToUI();

                return null;

            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return null;
            }
        }

        public async Task<bool> DoCheckIn(int scheduleId, int userId)
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.TravelServiceCheckInAddress}{scheduleId}/{userId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<Travel>();
                    var result = await _utils.ConvertSingleObjectFromJsonToBolean(data.Content);

                    return await Task.FromResult(result);
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
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

        public async Task<bool> DoCheckOut(int scheduleId, int userId)
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.TravelServiceCheckOutAddress}{scheduleId}/{userId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<Travel>();
                    var result = await _utils.ConvertSingleObjectFromJsonToBolean(data.Content);

                    return await Task.FromResult(result);
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
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

        public async Task<int> GetAvailableSeats(int travelId)
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.TravelServiceAvailableSeats}{travelId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    var stringJson = await data.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<int>(stringJson);

                    return result;
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
                    UIFunctions.ShowErrorMessageToUI();

                return await Task.FromResult(0);
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return await Task.FromResult(0);
            }
        }

        public bool Insert(Travel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<Travel> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(Travel entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(Travel entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<Travel> GetAll(Expression<Func<Travel, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<Travel> GetAll()
        {
            return _repo.GetAll();
        }

        public Travel Get(Expression<Func<Travel, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public Travel Get()
        {
            return _repo.Get();
        }

        public Travel GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

