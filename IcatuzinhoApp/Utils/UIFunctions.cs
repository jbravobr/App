using Microsoft.Practices.Unity;
using Acr.UserDialogs;
using System;
using System.Globalization;

namespace IcatuzinhoApp
{
    public static class UIFunctions
    {
        static IUserDialogs _userDialogs;

        public static void ShowErrorMessageToUI()
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Alert(new AlertConfig
            {
                Message = "Desculpe, houve um erro na aplicação. Por favor tente novamente",
                OkText = "OK",
                Title = "Erro"
            });
        }

        public static void ShowErrorMessageToUI(string message)
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Alert(new AlertConfig
            {
                Message = message,
                OkText = "OK"
            });
        }

        public static void ShowErrorForConnectivityMessageToUI()
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Alert(new AlertConfig
            {
                Message = "Desculpe, você precisa estar conectado a internet para usar este aplicativo",
                OkText = "OK",
                Title = "Erro"
            });
        }

        public static void ShowToastErrorMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Error, message, timeout));
        }

        public static void ShowToastWarningMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Warning, message, timeout));
        }

        public static void ShowToastInfoMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Info, message, timeout));
        }

        public static void ShowToastSuccessMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Success, message, timeout));
        }

        public static void ShowToastToUI(string message, int timeout = 3000)
        {
            _userDialogs = App._container.Resolve<IUserDialogs>();
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Warning, message, timeout));
        }

        public static void ShowNotificationForNextTravel(string time)
        {
            Plugin.LocalNotifications
                  .CrossLocalNotifications
                  .Current
                  .Show("Icatuzinho", $"Atenção à próxima saída que acontecerá às {time}", 1);
        }

        public static void RemoveNotificationForNextTravel()
        {
            Plugin.LocalNotifications
                  .CrossLocalNotifications
                  .Current
                  .Cancel(1);
        }

        static ToastConfig SetToastUIConfiguration(EnumToastEventType typeEvent, string message, int duration)
        {
            ToastConfig t = null;

            switch (typeEvent)
            {
                case EnumToastEventType.Error:
                    t = new ToastConfig(ToastEvent.Error, string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.Crimson,
                        Title = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        TextColor = System.Drawing.Color.White
                    };
                    break;

                case EnumToastEventType.Info:
                    t = new ToastConfig(ToastEvent.Info, string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.Gold,
                        Title = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        TextColor = System.Drawing.Color.White
                    };
                    break;

                case EnumToastEventType.Success:
                    t = new ToastConfig(ToastEvent.Success, string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.LimeGreen,
                        Title = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        TextColor = System.Drawing.Color.White
                    };
                    break;

                case EnumToastEventType.Warning:
                    t = new ToastConfig(ToastEvent.Warn, string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.DodgerBlue,
                        Title = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        TextColor = System.Drawing.Color.White
                    };
                    break;

                default:
                    t = new ToastConfig(ToastEvent.Info, string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.Gold,
                        Title = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        TextColor = System.Drawing.Color.White
                    };
                    break;
            }

            return t;
        }

        static System.Drawing.Color ColorTransform(string colorHex)
        {
            int argb = int.Parse(colorHex.Replace("#", ""), NumberStyles.HexNumber);
            return System.Drawing.Color.FromArgb(argb);
        }
    }
}

