using Android.App;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Acr.UserDialogs;
using Xamarin;

namespace IcatuzinhoApp.Droid
{
    [Activity(
        Label = "Icatuzinho",
        Theme = "@style/AppTheme",
        Icon = "@drawable/ic_launcher",
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        MainLauncher = true)
    ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

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

            Xamarin.Forms.Forms.Init(this, bundle);

            /*

            Uncomment to remove StatusBar in Android
            
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            */

            LoadApplication(new App());


#pragma warning disable 618
            // Hiding ActionBar Icon on Android versions using Material Design
            //if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
            //{
            //ActionBar.SetIcon(
            //new ColorDrawable(
            //Resources.GetColor(Android.Resource.Color.Transparent)
            //)
            //);
            //}
#pragma warning restore 618
        }
    }
}


