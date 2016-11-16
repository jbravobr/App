using System;
using PropertyChanged;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class SchedulePageViewModel : BasePageViewModel
    {
        readonly IBaseService<Travel> _travelService;
        readonly IBaseService<Weather> _weatherService;
        readonly IUserDialogs _userDialogs;

        public List<Travel> Travels { get; private set; }
        public bool IsRefreshing { get; private set; }
        public bool IsOval2Setup { get; private set; }
        public string Temp { get; private set; }
        public string TempIco { get; private set; }

        public SchedulePageViewModel(IBaseService<Travel> travelService,
                                     IBaseService<Weather> weatherService)
        {
            _travelService = travelService;
            _weatherService = weatherService;

            Init().ConfigureAwait(false);

            IsRefreshing = false;
            IsOval2Setup = false;
        }

        public async Task Init()
        {
            try
            {
                Travels = await GetAll();
            }
            catch (Exception ex)
            {
                _userDialogs.HideLoading();
                base.SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public async Task<List<Travel>> GetAll()
        {
            var w = await GetWeather();
            TempIco = SetFontAwesomeForTemp(w.Ico);
            Temp = w.Temp;

            var travels = await _travelService.GetAll();

            foreach (var item in travels.OrderBy(c => c.Schedule.StartSchedule.ToLocalTime()))
            {
                if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, item.Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0)
                    SetScheduleAvatar(true, item);
                else
                    SetScheduleAvatar(false, item);

                if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, item.Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0 &&
                    item.Vehicle.SeatsAvailable > 0)
                    SetScheduleStatusDescription(true, item);
                else
                    SetScheduleStatusDescription(false, item);

                item.GetTemp = $"{TempIco} {Temp}º";
            }

            return travels;
        }

        public async Task GetUpdatedSeatsAvailableBySchedule()
        {
            if (Travels == null && !Travels.Any())
                return;

            foreach (var item in Travels)
            {
                try
                {
                    Expression<Func<Travel, bool>> BySchedulelId = (t) => t.Schedule.Id == item.Schedule.Id;
                    item.Vehicle.SeatsAvailable = (await _travelService.GetWithChildren(BySchedulelId, Constants.TravelServiceAvailableSeats)).Vehicle.SeatsAvailable;
                }
                catch (Exception ex)
                {
                    SendToInsights(ex);
                }
            }
        }

        public void ScheduleGetInfoForUI()
        {
            Device.StartTimer(TimeSpan.FromSeconds(25), () =>
                {
                    Task.Factory.StartNew(() =>
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await GetUpdatedSeatsAvailableBySchedule();
                                    ScheduleGetInfoForUI();
                                });
                        });

                    return false;
                });
        }

        public Command Refresh
        {
            get
            {
                return new Command(async (obj) =>
                    {
                        try
                        {
                            IsRefreshing = true;
                            var ids = Travels.Select(x => x.Id).ToList();

                            foreach (var id in ids)
                            {
                                var availableSeats = await _travelService.GetWithChildrenById(id, Constants.TravelServiceAvailableSeats);
                                Travels.First(x => x.Id == id).Vehicle.SeatsAvailable = availableSeats.Vehicle.SeatsAvailable;

                                if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, Travels.First(x => x.Id == id).Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0)
                                    SetScheduleAvatar(true, Travels.First(x => x.Id == id));
                                else
                                    SetScheduleAvatar(false, Travels.First(x => x.Id == id));

                                if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, Travels.First(x => x.Id == id).Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0 &&
                                    Travels.First(x => x.Id == id).Vehicle.SeatsAvailable > 0)
                                    SetScheduleStatusDescription(true, Travels.First(x => x.Id == id));
                                else
                                    SetScheduleStatusDescription(false, Travels.First(x => x.Id == id));
                            }
                        }
                        catch (Exception ex)
                        {
                            SendToInsights(ex);
                            UIFunctions.ShowErrorMessageToUI("Erro ao atualizar as viagens, por favor tente novamente");
                        }
                        finally
                        {
                            IsRefreshing = false;
                        }
                    });
            }
        }

        void SetScheduleAvatar(bool available, Travel item)
        {
            if (available)
            {
                try
                {
                    if (DateTime.Now.ToLocalTime().TimeOfDay <= item.Schedule.StartSchedule.ToLocalTime().TimeOfDay
                        && !IsOval2Setup)
                    {
                        item.Schedule.StatusAvatar = "oval2.png";
                        item.Schedule.StatusDescription = "Embarque Liberado";
                        IsOval2Setup = true;
                    }
                    else
                    {
                        item.Schedule.StatusAvatar = "oval3.png";
                        item.Schedule.StatusDescription = "Embarque Fechado";
                    }

                    return;
                }
                catch (Exception ex)
                {
                    base.SendToInsights(ex);
                }
            }

            try
            {
                item.Schedule.StatusDescription = "Embarque Encerrado";
                item.Schedule.StatusAvatar = "oval1.png";

                return;
            }
            catch (Exception ex)
            {
                base.SendToInsights(ex);
            }
        }

        void SetScheduleStatusDescription(bool available, Travel item)
        {
            if (available)
            {
                try
                {
                    if (IsOval2Setup && DateTime.Now.ToLocalTime().TimeOfDay <= item.Schedule.StartSchedule.ToLocalTime().TimeOfDay)
                        item.Schedule.StatusDescription = "Embarque Liberado";
                    else
                        item.Schedule.StatusDescription = "Embarque fechado";

                    return;
                }
                catch (Exception ex)
                {
                    base.SendToInsights(ex);
                }
            }
            else
            {
                try
                {
                    item.Schedule.StatusDescription = "Embarque encerrado";
                    return;
                }
                catch (Exception ex)
                {
                    base.SendToInsights(ex);
                }
            }
        }

        public async Task<Weather> GetWeather()
        {
            return await _weatherService.Get();
        }

        string SetFontAwesomeForTemp(string ico)
        {
            if (ico.Contains("rain") || ico.Contains("thunder"))
                return $"\uf0e9";

            if (ico.Contains("cloudy"))
                return $"\uf073";

            if (ico.Contains("sunny"))
                return $"\uf185";

            return $"\uf186";
        }
    }
}