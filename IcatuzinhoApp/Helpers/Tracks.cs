using System;
using Xamarin;
using Plugin.DeviceInfo.Abstractions;
using Microsoft.Practices.Unity;
using Acr.Settings;

namespace IcatuzinhoApp
{
    public static class Tracks
    {
        const string UnknownUser = "Usuário não capturado";

        public static void SendTrackToInsights(Transaction transaction)
        {
            Insights.Track(transaction.Name, transaction.Details);
        }

        public static void TrackLoginInformation()
        {
            var _deviceInfo = Plugin.DeviceInfo.CrossDeviceInfo.Current;

            Insights.Track("Login concluído", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", Settings.Local.Get<string>("UserEmail") ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }

        public static void TrackCheckInInformation()
        {
            var _deviceInfo = Plugin.DeviceInfo.CrossDeviceInfo.Current;

            Insights.Track("CheckIn concluído", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", Settings.Local.Get<string>("UserEmail") ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }

        public static void TrackCheckOutInformation()
        {
            var _deviceInfo = Plugin.DeviceInfo.CrossDeviceInfo.Current;

            Insights.Track("CheckOut concluído", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", Settings.Local.Get<string>("UserEmail") ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }

        public static void TrackWhoSawMapsInformation()
        {
            var _deviceInfo = Plugin.DeviceInfo.CrossDeviceInfo.Current;

            Insights.Track("Mapa acessado por", new System.Collections.Generic.Dictionary<string, string>
                        {
                            {"Usuário", Settings.Local.Get<string>("UserEmail") ?? UnknownUser},
                            {"Data", DateTime.Now.ToString()},
                            {"OS", _deviceInfo.Platform.ToString()},
                            {"Version", _deviceInfo.Version}
                        });
        }
    }
}