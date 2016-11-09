using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public static class IoCconfiguration
    {
        static UnityContainer _container { get; set; }

        public static UnityContainer Init()
        {
            var _c = new UnityContainer();

            _c.RegisterType<ILogExceptionService, LogExceptionService>();

            _c.RegisterInstance(Acr.UserDialogs.UserDialogs.Instance);
            _c.RegisterInstance(Plugin.DeviceInfo.CrossDeviceInfo.Current);
            _c.RegisterInstance(Plugin.Connectivity.CrossConnectivity.Current);

            return _c;
        }
    }
}

