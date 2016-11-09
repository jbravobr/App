// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace IcatuzinhoApp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string ShowNotificationForNextTravelKey = "ShowNotificationForNextTravel";
        private static readonly bool ShowNotificationForNextTravelDefault = true;

        #endregion


        public static bool GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(ShowNotificationForNextTravelKey, ShowNotificationForNextTravelDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(ShowNotificationForNextTravelKey, value);
            }
        }

    }
}