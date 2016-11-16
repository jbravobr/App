﻿using System.Net.Http;

using Xamarin;

using Prism.Unity;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public partial class App : PrismApplication
    {
        public static SQLite.SQLiteConnection conn { get; set; }
        public static HttpClient httpClient { get; set; }
        public static bool MapLoaded { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                InitializeComponent();
                NavigationService.NavigateAsync("LoginPage");
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
                Container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
                Container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

                // Registrando Views para Navegação
                Container.RegisterTypeForNavigation<HomePage>();
                Container.RegisterTypeForNavigation<LoginPage>();
                Container.RegisterTypeForNavigation<SchedulePage>();
                Container.RegisterTypeForNavigation<SelectionPage>();
                Container.RegisterTypeForNavigation<TravelPage>();

                // 3rd Party Controls
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