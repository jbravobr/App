using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IcatuzinhoApp
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();

            listViewSchedules.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}

