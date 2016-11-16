using Microsoft.Practices.Unity;
using Acr.UserDialogs;
using System;
using System.Globalization;

namespace IcatuzinhoApp
{
    public static class UIFunctions
    {
        static IUserDialogs _userDialogs = _userDialogs = UserDialogs.Instance;

        public static void ShowErrorMessageToUI()
        {
            _userDialogs.Alert(new AlertConfig
            {
                Message = "Desculpe, houve um erro na aplicação. Por favor tente novamente",
                OkText = "OK",
                Title = "Erro"
            });
        }

        public static void ShowErrorMessageToUI(string message)
        {
            _userDialogs.Alert(new AlertConfig
            {
                Message = message,
                OkText = "OK"
            });
        }

        public static void ShowErrorForConnectivityMessageToUI()
        {
            _userDialogs.Alert(new AlertConfig
            {
                Message = "Desculpe, você precisa estar conectado a internet para usar este aplicativo",
                OkText = "OK",
                Title = "Erro"
            });
        }

        public static void ShowToastErrorMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Error, message, timeout));
        }

        public static void ShowToastWarningMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Warning, message, timeout));
        }

        public static void ShowToastInfoMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Info, message, timeout));
        }

        public static void ShowToastSuccessMessageToUI(string message, int timeout = 3000)
        {
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Success, message, timeout));
        }

        public static void ShowToastToUI(string message, int timeout = 3000)
        {
            _userDialogs.Toast(SetToastUIConfiguration(EnumToastEventType.Warning, message, timeout));
        }

        static ToastConfig SetToastUIConfiguration(EnumToastEventType typeEvent, string message, int duration)
        {
            ToastConfig t = null;

            switch (typeEvent)
            {
                case EnumToastEventType.Error:
                    t = new ToastConfig(string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.Crimson,
                        Message = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        MessageTextColor = System.Drawing.Color.White
                    };
                    break;

                case EnumToastEventType.Info:
                    t = new ToastConfig(string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.Gold,
                        Message = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        MessageTextColor = System.Drawing.Color.White
                    };
                    break;

                case EnumToastEventType.Success:
                    t = new ToastConfig(string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.LimeGreen,
                        Message = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        MessageTextColor = System.Drawing.Color.White
                    };
                    break;

                case EnumToastEventType.Warning:
                    t = new ToastConfig(string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.DodgerBlue,
                        Message = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        MessageTextColor = System.Drawing.Color.White
                    };
                    break;

                default:
                    t = new ToastConfig(string.Empty)
                    {
                        BackgroundColor = System.Drawing.Color.Gold,
                        Message = message,
                        Duration = TimeSpan.FromMilliseconds(duration),
                        MessageTextColor = System.Drawing.Color.White
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