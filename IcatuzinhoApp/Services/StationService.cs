using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public class StationService : IStationService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        IStationRepository _repo;
        DTO<List<Station>> _utils;

        public StationService(IHttpAccessService httpService,
                              ILogExceptionService log,
                              IAuthenticationService auth,
                              IStationRepository repo)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
            _repo = repo;
        }

        public async Task GetAllStations()
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.StationServiceAddress}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<List<Station>>();
                    var stations = await _utils.ConvertSingleObjectFromJson(data.Content);

                    if (stations != null && stations.Any())
                        Insert(stations);
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

        public bool Insert(Station entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<Station> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(Station entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(Station entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<Station> GetAll(Expression<Func<Station, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<Station> GetAll()
        {
            return _repo.GetAll();
        }

        public Station Get(Expression<Func<Station, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public Station Get()
        {
            return _repo.Get();
        }

        public Station GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

