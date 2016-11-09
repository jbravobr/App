using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IcatuzinhoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        protected async override void OnAppearing()
        {
            if (Device.OS == TargetPlatform.iOS)
                await stackLogo.TranslateTo(0, stackLogo.Height * 0.20 * -1, 1000, Easing.Linear);
            else if (Device.OS == TargetPlatform.Android)
                await stackLogo.TranslateTo(0, 50 * -1, 1000, Easing.Linear);

            gridFormLogin.Opacity = 0;
            gridFormLogin.IsVisible = true;
            await gridFormLogin.FadeTo(1, 500);

            base.OnAppearing();
        }

        public LoginPage()
        {
            InitializeComponent();
        }
    }
}

