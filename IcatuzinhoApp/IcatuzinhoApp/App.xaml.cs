<<<<<<< HEAD:IcatuzinhoApp/IcatuzinhoApp/App.xaml.cs
﻿using Prism.Unity;
using System.Net.Http;
using Microsoft.Practices.Unity;
using Xamarin;
using SQLite;
=======
﻿
using Prism.Unity;
using Microsoft.Practices.Unity;
using Xamarin;
>>>>>>> parent of 83fb4ec... Atualização de pacotes e refactoring:IcatuzinhoApp/App.xaml.cs

namespace IcatuzinhoApp
{
    public partial class App : PrismApplication
    {
<<<<<<< HEAD:IcatuzinhoApp/IcatuzinhoApp/App.xaml.cs

        public static SQLite.Net.SQLiteConnection conn { get; set; }
        public static HttpClient httpClient { get; set; }
=======
        public static User UserAuthenticated { get; set; }
        public static UnityContainer _container { get; private set; }
>>>>>>> parent of 83fb4ec... Atualização de pacotes e refactoring:IcatuzinhoApp/App.xaml.cs
        public static bool MapLoaded { get; set; }


        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            try
            {
                InitializeComponent();
                _container = IoCconfiguration.Init();
                MapLoaded = false;

                NavigationService.Navigate("LoginPage");
            }
            catch (System.Exception ex)
            {
                Insights.Report(ex);
                throw ex;
            }
        }

        protected override void RegisterTypes()
        {

            try
            {
                // Registrando Serviços e dependências.
                Container.RegisterType<IUserService, UserService>();
                Container.RegisterType<IHttpAccessService, HttpAccessService>();
                Container.RegisterType<IAuthenticationCodeService, AuthenticationCodeService>();
                Container.RegisterType<IAuthenticationService, AuthenticationService>();
                Container.RegisterType<IDriveService, DriverService>();
                Container.RegisterType<ILogExceptionService, LogExceptionService>();
                Container.RegisterType<IScheduleService, ScheduleService>();
                Container.RegisterType<IStationService, StationService>();
                Container.RegisterType<IItineraryService, ItineraryService>();
                Container.RegisterType<ITransactionService, TransactionService>();
                Container.RegisterType<ITravelService, TravelService>();
                Container.RegisterType<IUserService, UserService>();
                Container.RegisterType<IVehicleService, VehicleService>();
                Container.RegisterType<IWeatherService, WeatherService>();

                // Registrando Views para Navegação
                Container.RegisterTypeForNavigation<HomePage>();
				Container.RegisterTypeForNavigation<LoginPage>();
				Container.RegisterTypeForNavigation<SchedulePage>();
				Container.RegisterTypeForNavigation<SelectionPage>();
				Container.RegisterTypeForNavigation<TravelPage>();

                // 3rd Party Controlls
                Container.RegisterInstance(Acr.UserDialogs.UserDialogs.Instance);
                Container.RegisterInstance(Plugin.DeviceInfo.CrossDeviceInfo.Current);
                Container.RegisterInstance(Plugin.Connectivity.CrossConnectivity.Current);
            }
            catch (System.Exception ex)
            {
                Insights.Report(ex);
                throw ex;
            }
        }
    }
}
