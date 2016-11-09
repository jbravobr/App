using System;
using PropertyChanged;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Linq;
using Prism.Navigation;
using Prism.Commands;


namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class HomePageViewModel : BasePageViewModel
    {
        public string CurrentDate { get; set; }

        public string SeatsAvailable { get; set; }

        public string SeatsTotal { get; set; }

        public string Temp { get; set; }

        public string Time { get; set; }

        public string Description { get; set; }

        public bool isCheckIn { get; set; }

        public bool isCheckOut { get; set; }

        public string WeatherFontAwesome { get; set; }

        const string ZeroHours = "00";

        const string Minutes = "00";

        private DateTime[] arrivalTimes { get; set; }

        Travel _travel { get; set; }

        public DelegateCommand NavigateCommand { get; set; }

        public string TempIco { get; set; }

        readonly ITravelService _travelService;

        readonly IWeatherService _weatherService;

        readonly INavigationService _navigationService;

        IUserDialogs _userDialogs { get; set; }

        public HomePageViewModel(ITravelService travelService,
                                 IWeatherService weatherService,
                                 INavigationService navigationService)
        {
            _travelService = travelService;
            _weatherService = weatherService;
            _navigationService = navigationService;

            _userDialogs = App._container.Resolve<IUserDialogs>();

            isCheckIn = true;
            isCheckOut = false;

            GetInfos();
            ScheduleGetInfoForUI();
            ScheduleLocalNotificationForTravel();

            NavigateCommand = new DelegateCommand(Logout);
        }

        public void GetInfos()
        {
            try
            {
                _userDialogs.ShowLoading("Carregando");

                _travel = GetNextTravel();

                if (_travel != null)
                {
                    var currentDay = DateTime.Now.Day < 10 ?
                                             $"0{DateTime.Now.Day.ToString()}" :
                                             DateTime.Now.Day.ToString();

                    var currentMonth = DateTime.Now.Month < 10 ?
                                               $"0{DateTime.Now.Month.ToString()}" :
                                               DateTime.Now.Month.ToString();

                    CurrentDate = $"{currentDay}/{currentMonth}";

                    if (string.IsNullOrEmpty(SeatsAvailable))
                        SeatsAvailable = (_travel.Vehicle.SeatsTotal - _travel.Vehicle.SeatsAvailable).ToString();

                    SeatsTotal = _travel.Vehicle.SeatsTotal.ToString();
                    Description = _travel.Schedule.Message;

                    var _hours = _travel.Schedule.StartSchedule.ToLocalTime().Hour == 0 ?
                                  ZeroHours :
                                        _travel.Schedule.StartSchedule.ToLocalTime().Hour.ToString();

                    var _minutes = _travel.Schedule.StartSchedule.ToLocalTime().Minute == 0 ?
                                    Minutes :
                                          _travel.Schedule.StartSchedule.ToLocalTime().Minute.ToString();

                    Time = $"{_hours}:{_minutes}";

                    var w = GetWeather();
                    TempIco = SetFontAwesomeForTemp(w.Ico);
                    Temp = w.Temp;

                    _userDialogs.HideLoading();
                }
                else
                {
                    _userDialogs.HideLoading();
                    UIFunctions.ShowErrorMessageToUI();
                }
            }
            catch (Exception ex)
            {
                _userDialogs.HideLoading();

                base.SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public void ScheduleLocalNotificationForTravel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Factory.StartNew(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var nextTravelTime = GetNextTravelTime();

                        if (nextTravelTime != null)
                            UIFunctions.ShowNotificationForNextTravel($"{nextTravelTime.Value.Hours}:{nextTravelTime.Value.Minutes}");
                    });
                });

                return false;
            });
        }

        public void ScheduleGetInfoForUI()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Factory.StartNew(() =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        GetInfos();
                        await UpdateSeats();
                        ScheduleGetInfoForUI();
                    });
                });

                return false;
            });
        }

        public Command ShowMenuMoreIOS
        {
            get
            {
                return new Command(() =>
                {
                    var cfg = new ActionSheetConfig();

                    cfg.Add("Sair", async () =>
                    {
                        await NavigateCommand.Execute();
                    });
                    cfg.SetCancel("Cancelar");

                    _userDialogs.ActionSheet(cfg);
                });
            }
        }

        public TimeSpan? GetNextTravelTime()
        {
            var result = _travelService.GetAll()
                                       .FirstOrDefault(t => DateTime.Now.Add(TimeSpan.FromMinutes(5)).TimeOfDay <= t.Schedule.StartSchedule.ToLocalTime().TimeOfDay);

            if (result != null)
                return result.Schedule.StartSchedule.ToLocalTime().TimeOfDay;

            return null;
        }

        public Command ShowMenuMoreAndroid
        {
            get
            {
                return new Command(() =>
                {
                    var cfg = new ActionSheetConfig()
                            .SetTitle("Deseja sair?");

                    cfg.Add("Sim", async () =>
                    {
                        await NavigateCommand.Execute();
                    });
                    cfg.SetCancel("Não");

                    _userDialogs.ActionSheet(cfg);
                });
            }
        }

        public async void Logout()
        {
            App.UserAuthenticated = null;
            await _navigationService.Navigate("LoginPage", null, true, true);
        }

        public Command CheckIn
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (!await Connectivity.IsNetworkingOK())
                        {
                            UIFunctions.ShowErrorForConnectivityMessageToUI();
                            return;
                        }

                        _userDialogs.ShowLoading("Carregando");

                        var result = await _travelService.DoCheckIn(_travel.Schedule.Id, App.UserAuthenticated.Id);

                        if (result)
                        {
                            isCheckIn = false;
                            isCheckOut = true;
                            await UpdateSeats();

                            _userDialogs.HideLoading();
                            Tracks.TrackCheckInInformation();
                        }
                        else
                        {
                            _userDialogs.HideLoading();
                            UIFunctions.ShowErrorMessageToUI("Erro ao fazer o Checkin, tente novamente");
                        }
                    }
                    catch (Exception ex)
                    {
                        //_userDialogs.HideLoading();

                        base.SendToInsights(ex);
                        UIFunctions.ShowErrorMessageToUI();
                    }
                });
            }
        }

        public Command CheckOut
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        if (!await Connectivity.IsNetworkingOK())
                        {
                            UIFunctions.ShowErrorForConnectivityMessageToUI();
                            return;
                        }

                        var confirm = await _userDialogs.ConfirmAsync("Deseja realmente cancelar a sua viagem?",
                                                                      "Checkout", "Sim", "Não");

                        if (confirm)
                        {
                            _userDialogs.ShowLoading("Carregando");

                            var result = await _travelService.DoCheckOut(_travel.Schedule.Id, App.UserAuthenticated.Id);

                            if (result)
                            {
                                isCheckIn = true;
                                isCheckOut = false;
                                await UpdateSeats();

                                _userDialogs.HideLoading();
                                Tracks.TrackCheckOutInformation();
                            }
                            else
                            {
                                _userDialogs.HideLoading();
                                UIFunctions.ShowErrorMessageToUI("Erro ao fazer o Checkout, tente novamente");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _userDialogs.HideLoading();

                        base.SendToInsights(ex);
                        UIFunctions.ShowErrorMessageToUI();
                    }
                });
            }
        }

        public async Task UpdateSeats()
        {
            try
            {
                var result = await _travelService.GetAvailableSeats(_travel.Id);

                if (result > 0)
                    SeatsAvailable = (Convert.ToInt32(SeatsTotal) - result).ToString();

                if (SeatsAvailable == SeatsTotal)
                {
                    isCheckIn = false;
                    isCheckOut = false;
                }
            }
            catch (Exception ex)
            {
                SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI("Ops, houve um erro ao atualizar a disponibilidade de acentos");
            }
        }

        public Travel GetNextTravel()
        {
            var travels = _travelService.GetAll();
            Travel travel;

            travel = travels.Where(c => TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, c.Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0)
                            .OrderBy(c => c.Schedule.StartSchedule.ToLocalTime())
                            .FirstOrDefault();

            if (travel != null)
                return travel;

            return travel ?? travels.First();
        }

        public Weather GetWeather()
        {
            return _weatherService.Get();
        }

        #region Label Text

        public string GetCurrentDate
        {
            get
            {
                return $"\uf073 {CurrentDate}";
            }
        }

        public string GetSeats
        {
            get
            {
                return $"\uf183 {SeatsAvailable}/{SeatsTotal}";
            }
        }

        public string GetTemp
        {
            get
            {
                return $"{TempIco} {Temp}º";
            }
        }

        public string GetTime
        {
            get
            {
                return $"\uf017 {Time}";
            }
        }

        #endregion

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

