using Xamarin.Forms;

using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

using Xamarin;

using Prism.Navigation;
using Prism.Commands;
using Acr.UserDialogs;
using PropertyChanged;
using Acr.Settings;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class LoginPageViewModel : BasePageViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailIsEnabled { get; set; }
        public bool PasswordIsEnabled { get; set; }

        readonly IBaseService<User> _userService;
        readonly IBaseService<Schedule> _scheduleService;
        readonly IBaseService<Station> _stationService;
        readonly IBaseService<Travel> _travelService;
        readonly IBaseService<Weather> _weatherService;
        readonly IUserDialogs _userDialogs;
        readonly IBaseService<Itinerary> _itineraryService;
        readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand { get; set; }

        public LoginPageViewModel(IBaseService<User> userService,
                                  IBaseService<Schedule> scheduleService,
                                  IBaseService<Station> stationService,
                                  IBaseService<Travel> travelService,
                                  IBaseService<Weather> weatherService,
                                  IBaseService<Itinerary> itineraryService,
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
            _navigationService = navigationService;

            EmailIsEnabled = true;
            PasswordIsEnabled = true;

            Logon().ConfigureAwait(false);
        }

        public async Task Logon()
        {
            try
            {
                _userDialogs.ShowLoading("Verificando conexão");

                if (!await Connectivity.IsNetworkingOK())
                {
                    _userDialogs.HideLoading();
                    UIFunctions.ShowErrorForConnectivityMessageToUI();
                    EmailIsEnabled = false;
                    PasswordIsEnabled = false;
                }
                else if (GetAuthenticatedUser())
                {
                    _userDialogs.HideLoading();
                    await RegisterLocalAuthenticatedUser();

                    _userDialogs.ShowLoading("Carregando");
                    await _weatherService.Get();

                    Insights.Identify(Settings.Local.Get<string>("UserEmail"),
                                      Insights.Traits.GuestIdentifier,
                                      Settings.Local.Get<string>("UserEmail"));
                    Tracks.TrackLoginInformation();
                    _userDialogs.HideLoading();

                    await NavigateCommand.Execute();
                }
                else
                    _userDialogs.HideLoading();
            }
            catch (Exception ex)
            {
                _userDialogs.HideLoading();
                base.SendToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
            }
        }

        public bool GetAuthenticatedUser() => Settings.Local.Get<bool>("UserLogged");

        public Command Confirm
        {
            get
            {
                return new Command(async (obj) =>
               {
                   try
                   {
                       _userDialogs.ShowLoading("Carregando");

                       var userAuthenticated = await _userService.Login(Email, Password);

                       if (userAuthenticated)
                       {
                           await _stationService.GetAllWithChildren();
                           await _scheduleService.GetAllWithChildren();

                           await InsertTravels();

                           await _weatherService.Get();
                           await _itineraryService.GetAllWithChildren();
                           await RegisterLocalAuthenticatedUser();

                           _userDialogs.HideLoading();
                           await NavigateCommand.Execute();
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

        public async Task RegisterLocalAuthenticatedUser()
        {
            var user = await _userService.GetAll();
            if (user != null && user.Any())
            {
                Settings.Local.Set<string>("UserName", user.FirstOrDefault().Name);
                Settings.Local.Set<string>("UserPassword", Password);
                Settings.Local.Set<string>("UserId", user.FirstOrDefault().Id.ToString());
                Settings.Local.Set<string>("UserEmail", Email);
                Settings.Local.Set<bool>("UserLogged", true);
            }
        }

        public async Task InsertTravels()
        {
            var schedules = await _scheduleService.GetAllWithChildren();

            if (schedules != null && schedules.Any())
            {
                foreach (var schedule in schedules)
                {
                    if (schedule != null)
                    {
                        Expression<Func<Travel, bool>> byScheduleId = (t) => t.Schedule.Id == schedule.Id;
                        await _travelService.GetAllWithChildren(byScheduleId);
                    }
                }
            }
        }

        async void Navigate()
        {
            try
            {
                await _navigationService.NavigateAsync("SelectionPage");
            }
            catch (Exception ex)
            {
                UIFunctions.ShowErrorMessageToUI();
                base.SendToInsights(ex);
            }
        }
    }
}