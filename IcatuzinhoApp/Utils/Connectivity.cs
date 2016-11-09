using System;
using System.Threading.Tasks;
using Plugin.Connectivity;
namespace IcatuzinhoApp
{
    public static class Connectivity
    {
        public async static Task<bool> IsNetworkingOK()
        {
#if DEBUG
            var settings = new DeveloperSettings();
            settings.EnableEmulatorDebug(true);

            return await Task.FromResult(settings.ConnectivityStatus);
#else
            if (CrossConnectivity.Current.IsConnected)
                return await CrossConnectivity.Current.IsReachable("java.sun.com");

            return await Task.FromResult(false);
#endif
        }
    }
}

