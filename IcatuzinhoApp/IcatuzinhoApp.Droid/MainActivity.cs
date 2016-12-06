
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Xamarin;

namespace IcatuzinhoApp.Droid
{
    [Activity(Label = "Icatuzinho",
               Theme = "@style/AppTheme",
                Icon = "@drawable/ic_launcher", 
                MainLauncher = true,
                ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
                ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            

            Xamarin.FormsMaps.Init(this, bundle);
            Xamarin.Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                    Xamarin.Insights.PurgePendingCrashReports().Wait();
            };
#if DEBUG
            Xamarin.Insights.Initialize(Insights.DebugModeKey, this);
#else
            Xamarin.Insights.Initialize("af73d7945c2d65a46435cb2f6441453f416e9b43", this);
#endif
            UserDialogs.Init(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

