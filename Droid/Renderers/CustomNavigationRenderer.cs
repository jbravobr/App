using System;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Xamarin.Forms;
using IcatuzinhoApp;
using IcatuzinhoApp.Droid;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationRenderer))]
namespace IcatuzinhoApp.Droid
{
	public class CustomNavigationRenderer : PageRenderer
	{
		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);
			var actionBar = ((Activity)Context).ActionBar;

			if (actionBar != null)
			{
				ColorDrawable colorDrawable = new ColorDrawable(Android.Graphics.Color.Rgb(132, 136, 139));
				actionBar.SetBackgroundDrawable(colorDrawable);
			}
		}
	}
}