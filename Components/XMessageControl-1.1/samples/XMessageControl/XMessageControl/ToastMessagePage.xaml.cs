using Softweb.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XMessageControl.Shared
{
    public partial class ToastMessagePage : ContentPage
    {
        public ToastMessagePage()
        {
            InitializeComponent();
            btntoptoast.Clicked += btntoptoastclickevent;
            btncentertoast.Clicked += btncentertoastclickevent;
            btnbottomtoast.Clicked += btnbottomtoastclickevent;
            btnlongdurationtoast.Clicked += btnlongdurationclickevent;
            btntoastcustomize.Clicked += btnCustomizationclickevent;
            btnToastinallPostion.Clicked += btnToastinallPostionclickevent;
        }

        private async void btntoptoastclickevent(object sender, EventArgs e)
        {
            UIMessage.ShowToast("Hi!Test Message.\n This is First toast Message with top postion", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Short);
        }

        private async void btncentertoastclickevent(object sender, EventArgs e)
        {
            UIMessage.ShowToast("Hi!Test Message", ToastMessage.ToastPosition.CENTER);
        }

        private async void btnbottomtoastclickevent(object sender, EventArgs e)
        {
            UIMessage.ShowToast("Hi!Test Message", ToastMessage.ToastPosition.BOTTOM);
        }

        private async void btnlongdurationclickevent(object sender, EventArgs e)
        {
            UIMessage.ShowToast("Hi!Test Message", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Long);
        }

        private async void btnCustomizationclickevent(object sender, EventArgs e)
        {
            UIMessage.ShowToast("Hi!Customized Toast Message.\nThis is First toast Message with top postion", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Short, "validation.png", Color.Red);
        }

        private async void btnToastinallPostionclickevent(object sender, EventArgs e)
        {
            UIMessage.ShowToast("Hi!Customized Toast Message.\nThis is First toast Message with top postion", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Short, "validation.png", Color.Red);
            UIMessage.ShowToast("Hi!Test Message", ToastMessage.ToastPosition.CENTER, ToastMessage.Duration.Short);
            UIMessage.ShowToast("Hi!Test Message", ToastMessage.ToastPosition.BOTTOM, ToastMessage.Duration.Short);
        }
    }
}
