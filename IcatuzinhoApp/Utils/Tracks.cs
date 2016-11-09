using System;
using Xamarin;
using Plugin.DeviceInfo.Abstractions;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public static class Tracks
    {
        const string UnknownUser = "Usuário não capturado";

        public static void SendTrackToInsights(Transaction transaction)
        {
            Insights.Track(transaction.Name, transaction.DetailKey, transaction.DetailName);
        }

        public static void TrackLoginInformation()
        {
            var _deviceInfo = App._container.Resolve<IDeviceInfo>();

            Insights.Track("Login concluído", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", App.UserAuthenticated.Email ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }

        public static void TrackCheckInInformation()
        {
            var _deviceInfo = App._container.Resolve<IDeviceInfo>();

            Insights.Track("CheckIn concluído", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", App.UserAuthenticated.Email ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }

        public static void TrackCheckOutInformation()
        {
            var _deviceInfo = App._container.Resolve<IDeviceInfo>();

            Insights.Track("CheckOut concluído", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", App.UserAuthenticated.Email ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }

        public static void TrackWhoSawMapsInformation()
        {
            var _deviceInfo = App._container.Resolve<IDeviceInfo>();

            Insights.Track("Mapa acessado por", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", App.UserAuthenticated.Email ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }
    }
}

