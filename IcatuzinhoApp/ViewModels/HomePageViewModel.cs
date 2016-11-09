using System;
using PropertyChanged;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

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

        Travel _travel { get; set; }

        public string TempIco { get; set; }

        readonly ITravelService _travelService;

        readonly IWeatherService _weatherService;

        IUserDialogs _userDialogs { get; set; }

        public HomePageViewModel(ITravelService travelService,
                             IWeatherService weatherService)
        {
            _travelService = travelService;
            _weatherService = weatherService;
            _userDialogs = App._container.Resolve<IUserDialogs>();

            isCheckIn = true;
            isCheckOut = false;

            GetInfos();

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
                        SeatsAvailable = _travel.Vehicle.SeatsAvailable.ToString();

                    SeatsTotal = _travel.Vehicle.SeatsTotal.ToString();
                    Description = _travel.Schedule.Message;

                    var _hours = Convert.ToDateTime(_travel.Schedule.StartSchedule).Hour == 0 ?
                                        ZeroHours :
                                        Convert.ToDateTime(_travel.Schedule.StartSchedule).Hour.ToString();

                    var _minutes = Convert.ToDateTime(_travel.Schedule.StartSchedule).Minute == 0 ?
                                          Minutes :
                                          Convert.ToDateTime(_travel.Schedule.StartSchedule).Minute.ToString();

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

        public void ScheduleGetInfoForUI()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
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

        public Command CheckIn
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

                        _userDialogs.ShowLoading("Carregando");

                        var result = await _travelService.DoCheckIn(_travel.Schedule.Id, App.UserAuthenticated.Id);

                        if (result)
                        {
                            isCheckIn = false;
                            isCheckOut = true;
                            await UpdateSeats();

                            _userDialogs.HideLoading();
                            Tracks.TrackCheckInInformation();
                            UIFunctions.ShowToastSuccessMessageToUI("Checkin efetuado!",
                                                                   Device.OS == TargetPlatform.iOS ?
                                                                   6000 : 3000);
                        }
                        else
                        {
                            _userDialogs.HideLoading();
                            UIFunctions.ShowToastErrorMessageToUI("Erro ao fazer o Checkin, tente novamente",
                                                                   Device.OS == TargetPlatform.iOS ?
                                                                   6000 : 3000);
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
                                UIFunctions.ShowToastSuccessMessageToUI("Checkout efetuado!",
                                                                   Device.OS == TargetPlatform.iOS ?
                                                                   6000 : 3000);
                            }
                            else
                            {
                                _userDialogs.HideLoading();
                                UIFunctions.ShowToastErrorMessageToUI("Erro ao fazer o Checkout, tente novamente",
                                                                   Device.OS == TargetPlatform.iOS ?
                                                                   6000 : 3000);
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
            var result = await _travelService.GetAvailableSeats(_travel.Id);

            if (result > 0)
                SeatsAvailable = result.ToString();
        }

        public Travel GetNextTravel()
        {
            //Expression<Func<Travel, bool>> bySchedule = (x) => x.Schedule.StartSchedule <= DateTime.Now;
            //var travel = await _travelService.GetWithChildrenAsync(bySchedule);
            var travel = _travelService.GetWithChildrenById(1);

            if (travel != null)
                return travel;

            return null;
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

