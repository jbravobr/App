using System;
using PropertyChanged;
using Xamarin.Forms;
namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class SettingPageViewModel : BasePageViewModel
    {
        public int ShowNotification { get; set; }

        public Command EnableDisableNotification
        {
            get
            {
                return new Command(() => { ShowNotification = 1; });
            }
        }
    }
}

