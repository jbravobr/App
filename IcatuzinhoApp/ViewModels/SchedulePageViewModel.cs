using System;
using PropertyChanged;
using Acr.UserDialogs;
using System.Collections.Generic;
using Microsoft.Practices.Unity;


namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class SchedulePageViewModel : BasePageViewModel
    {
        readonly IScheduleService _scheduleService;
        IUserDialogs _userDialogs { get; set; }

        public IList<Schedule> Schedules { get; set; }

        public SchedulePageViewModel(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
            _userDialogs = App._container.Resolve<IUserDialogs>();

            try
            {
                _userDialogs.ShowLoading("Carregando");

                Schedules = GetAll();

                _userDialogs.HideLoading();
            }
            catch (Exception ex)
            {
                _userDialogs.HideLoading();
                base.SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public IList<Schedule> GetAll()
        {
            var collection = _scheduleService.GetAll();

            foreach (var item in collection)
            {
                item.TimeSchedule = Convert.ToDateTime(item.StartSchedule);
                item.StatusAvatar = DateTime.Now <= item.TimeSchedule ?
                    SetScheduleAvailable(true) :
                    SetScheduleAvailable(false);
                item.StatusDescription = DateTime.Now <= item.TimeSchedule ?
                    "Disponível" :
                    "Indisponível";
            }

            return collection;
        }

        string SetScheduleAvailable(bool available)
        {
            return available ? "online.png" : "offline.png";
        }
    }
}

