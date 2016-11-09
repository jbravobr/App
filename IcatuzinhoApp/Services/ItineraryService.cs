using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public class ItineraryService : IItineraryService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        IItineraryRepository _repo;
        DTO<Itinerary> _utils;

        public ItineraryService(IHttpAccessService httpService,
                                ILogExceptionService log,
                                IAuthenticationService auth,
                                IItineraryRepository repo)
        {
            _httpService = httpService;
            _log = log;
            _auth = auth;
            _repo = repo;
        }

        public async Task GetAllItineraries()
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.ItineraryServiceAddress}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<Itinerary>();
                    var itineraries = await _utils.ConvertCollectionObjectFromJson(data.Content);

                    if (itineraries != null && itineraries.Any())
                        _repo.Insert(itineraries);
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

        public bool Insert(Itinerary entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<Itinerary> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(Itinerary entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(Itinerary entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<Itinerary> GetAll(Expression<Func<Itinerary, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<Itinerary> GetAll()
        {
            return _repo.GetAll();
        }

        public Itinerary Get(Expression<Func<Itinerary, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public Itinerary Get()
        {
            return _repo.Get();
        }

        public Itinerary GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

