using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XMessageControl.Shared
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            
            InitializeComponent();
           
            btnOpenToastPage.Clicked += btnOpenToastPageclickevent;
            btnopenMessagePage.Clicked += btnopenMessagePageclickevent;
          
        }
        private async void btnOpenToastPageclickevent(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ToastMessagePage(), true);
        }
        private async void btnopenMessagePageclickevent(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MessageControlPage(), true);
        }
    }
}
