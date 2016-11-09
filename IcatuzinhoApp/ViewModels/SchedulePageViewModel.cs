using System;
using PropertyChanged;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class SchedulePageViewModel : BasePageViewModel
    {
        readonly ITravelService _travelService;
        IUserDialogs _userDialogs { get; set; }

        public List<Travel> Travels { get; set; }
        public bool IsRefreshing { get; set; }

        public SchedulePageViewModel(IScheduleService scheduleService,
                                     ITravelService travelService)
        {
            _travelService = travelService;
            Init();

            IsRefreshing = false;
        }

        public void Init()
        {
            try
            {
                Travels = GetAll();
            }
            catch (Exception ex)
            {
                _userDialogs.HideLoading();
                base.SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public List<Travel> GetAll()
        {
            var collection = _travelService.GetAll();

            foreach (var item in collection)
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

            }

            return collection;
        }

        public async Task GetUpdatedSeatsAvailableBySchedule()
        {
            var realm = Realm.GetInstance();

            if (Travels == null && !Travels.Any())
                return;

            foreach (var item in Travels)
            {
                using (var tran = realm.BeginWrite())
                {
                    try
                    {
                        item.Vehicle.SeatsAvailable = (int)await _travelService.GetSeatsAvailableByTravel(item.Schedule.Id);

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        SendToInsights(ex);
                        tran.Rollback();
                    }
                }
            }
        }

        public void ScheduleGetInfoForUI()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
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
                return new Command(async () =>
                {
                    var realm = Realm.GetInstance();

                    try
                    {
                        IsRefreshing = true;
                        var ids = Travels.Select(x => x.Id).ToList();

                        foreach (var id in ids)
                        {
                            var availableSeats = await _travelService.GetAvailableSeats(id);

                            using (var tran = realm.BeginWrite())
                            {
                                try
                                {
                                    Travels.First(x => x.Id == id).Vehicle.SeatsAvailable = availableSeats;

                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    SendToInsights(ex);
                                    tran.Rollback();
                                }
                            }

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
            var realm = Realm.GetInstance();

            if (available)
            {
                using (var tran = realm.BeginWrite())
                {
                    try
                    {
                        item.Schedule.StatusAvatar = "online.png";

                        tran.Commit();
                        return;
                    }
                    catch (Exception ex)
                    {
                        base.SendToInsights(ex);
                        tran.Rollback();
                    }
                }
            }

            using (var tran = realm.BeginWrite())
            {
                try
                {
                    item.Schedule.StatusAvatar = "offline.png";

                    tran.Commit();
                    return;
                }
                catch (Exception ex)
                {
                    base.SendToInsights(ex);
                    tran.Rollback();
                }
            }
        }

        void SetScheduleStatusDescription(bool available, Travel item)
        {
            var realm = Realm.GetInstance();

            if (available)
            {
                using (var tran = realm.BeginWrite())
                {
                    try
                    {
                        item.Schedule.StatusDescription = "Disponível";

                        tran.Commit();
                        return;
                    }
                    catch (Exception ex)
                    {
                        base.SendToInsights(ex);
                        tran.Rollback();
                    }
                }
            }

            using (var tran = realm.BeginWrite())
            {
                try
                {
                    item.Schedule.StatusDescription = "Indisponível";

                    tran.Commit();
                    return;
                }
                catch (Exception ex)
                {
                    base.SendToInsights(ex);
                    tran.Rollback();
                }
            }
        }
    }
}

