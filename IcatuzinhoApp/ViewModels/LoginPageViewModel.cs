using Xamarin.Forms;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using System.Linq;
using Acr.UserDialogs;
using Xamarin;
using Prism.Navigation;
using Prism.Commands;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class LoginPageViewModel : BasePageViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool EmailIsEnabled { get; set; }

        public bool PasswordIsEnabled { get; set; }

        readonly IUserService _userService;

        readonly IAuthenticationService _authService;

        readonly IScheduleService _scheduleService;

        readonly IStationService _stationService;

        readonly ITravelService _travelService;

        readonly IWeatherService _weatherService;

        readonly IUserDialogs _userDialogs;

        readonly IItineraryService _itineraryService;

        readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand { get; set; }

        public LoginPageViewModel(IUserService userService,
                              IScheduleService scheduleService,
                              IStationService stationService,
                              ITravelService travelService,
                              IWeatherService weatherService,
                              IItineraryService itineraryService,
                              IAuthenticationService authService,
                              IUserDialogs userDialogs,
                              INavigationService navigationService)
        {
            _userService = userService;
            _scheduleService = scheduleService;
            _stationService = stationService;
            _userDialogs = userDialogs;
            _travelService = travelService;
            _weatherService = weatherService;
            _itineraryService = itineraryService;
            _authService = authService;
            _navigationService = navigationService;

            EmailIsEnabled = true;
            PasswordIsEnabled = true;

            NavigateCommand = new DelegateCommand(Navigate);

            //Logon().ConfigureAwait(true);
        }

        public async Task Logon()
        {
            try
            {
                if (Device.OS == TargetPlatform.Android)
                    _userDialogs.ShowLoading("Verificando conexão");

                if (!await Connectivity.IsNetworkingOK())
                {
                    if (Device.OS == TargetPlatform.Android)
                        _userDialogs.HideLoading();

                    UIFunctions.ShowErrorForConnectivityMessageToUI();
                    EmailIsEnabled = false;
                    PasswordIsEnabled = false;
                }
                else if (await GetAuthenticatedUser())
                {
                    var isRefreshToken = await _authService.RefreshToken();

                    if (isRefreshToken)
                    {
                        if (Device.OS == TargetPlatform.Android)
                            _userDialogs.HideLoading(); // Escondendo o loading da verificação de Rede.

                        if (Device.OS == TargetPlatform.Android)
                            _userDialogs.ShowLoading("Carregando");

                        await _weatherService.GetWeather();

                        Insights.Identify(App.UserAuthenticated.Email,
                                             Insights.Traits.GuestIdentifier,
                                             App.UserAuthenticated.Email);

                        Tracks.TrackLoginInformation();

                        await NavigateCommand.Execute();
                    }
                    else
                    {
                        if (Device.OS == TargetPlatform.Android)
                            _userDialogs.HideLoading(); // Escondendo o loading da verificação de Rede.
                        UIFunctions.ShowErrorMessageToUI("Sua sessão não pode ser renovada, efetue logoff e logue novamente");
                    }
                }
                else
                {
                    if (Device.OS == TargetPlatform.Android)
                        _userDialogs.HideLoading(); // Escondendo o loading da verificação de Rede.
                }
            }
            catch (Exception ex)
            {
                if (Device.OS == TargetPlatform.Android)
                    _userDialogs.HideLoading();

                base.SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public async Task<bool> GetAuthenticatedUser() => await _userService.GetAuthenticatedUser();

        public Command Confirm
        {
            get
            {
                return new Command(async (obj) =>
               {
                   try
                   {
                       _userDialogs.ShowLoading("Carregando");

                       // Com token
                       var userAuthenticated = await _authService.AuthenticationWithFormUrlEncoded(Email, Password, false);

                       if (userAuthenticated)
                       {
                           await _userService.Login(Email, Password);
                           await _stationService.GetAllStations();
                           await _scheduleService.GetAllSchedules();
                           await InsertTravels();
                           await _weatherService.GetWeather();
                           await _itineraryService.GetAllItineraries();
                           await _navigationService.Navigate("SelectionPage", null, true);
                       }
                       else
                       {
                           _userDialogs.HideLoading();
                           UIFunctions.ShowToastErrorMessageToUI("Usuário/Senha inválidos",
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

        public async Task InsertTravels()
        {
            var schedules = _scheduleService.GetAll();

            if (schedules != null && schedules.Any())
            {
                foreach (var schedule in schedules)
                {
                    if (schedule != null)
                        await _travelService.GetTravelByScheduleId(schedule.Id);
                }
            }
        }

        async void Navigate()
        {
            try
            {
                _userDialogs.ShowLoading("Abrindo...");

                await _navigationService.Navigate("SelectionPage", null, true);
            }
            catch (Exception ex)
            {
                UIFunctions.ShowErrorMessageToUI();
                base.SendToInsights(ex);
            }
        }
    }
}

