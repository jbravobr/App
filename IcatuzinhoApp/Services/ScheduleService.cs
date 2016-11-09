using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public class ScheduleService : IScheduleService
    {
        IHttpAccessService _httpService;
        ILogExceptionService _log;
        IAuthenticationService _auth;
        IScheduleRepository _repo;
        DTO<List<Schedule>> _utils;

        public ScheduleService(IHttpAccessService httpService,
                               IAuthenticationService auth,
                               IScheduleRepository repo)
        {
            _httpService = httpService;
            _auth = auth;
            _repo = repo;
        }

        public async Task GetAllSchedules()
        {
            try
            {
                var token = _auth.Get();

                var clientCaller = _httpService.Init(token?.AccessToken);
                var data = await clientCaller.GetAsync($"{Constants.ScheduleServiceAddress}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    _utils = new DTO<List<Schedule>>();
                    var schedules = await _utils.ConvertSingleObjectFromJson(data.Content);

                    if (schedules != null && schedules.Any())
                        Insert(schedules);
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

        public bool Insert(Schedule entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<Schedule> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Delete(Schedule entity)
        {
            return _repo.Delete(entity);
        }

        public bool Update(Schedule entity)
        {
            return _repo.Update(entity);
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public List<Schedule> GetAll(Expression<Func<Schedule, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public List<Schedule> GetAll()
        {
            return _repo.GetAll();
        }

        public Schedule Get(Expression<Func<Schedule, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public Schedule Get()
        {
            return _repo.Get();
        }

        public Schedule GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }
    }
}

