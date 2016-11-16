using System;
using System.Threading.Tasks;
using Plugin.Connectivity;
namespace IcatuzinhoApp
{
    public static class Connectivity
    {
        public async static Task<bool> IsNetworkingOK()
        {
            if (CrossConnectivity.Current.IsConnected)
                return await CrossConnectivity.Current.IsRemoteReachable("google.com.br");

            return await Task.FromResult(false);
        }
    }
}

