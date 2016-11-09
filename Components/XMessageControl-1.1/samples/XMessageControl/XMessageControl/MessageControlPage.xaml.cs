using Softweb.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XMessageControl.Shared
{
    public partial class MessageControlPage : ContentPage
    {
        public MessageControlPage()
        {
         
            InitializeComponent();
        	btnInfomessage.Clicked += btnInfoclickevent;		
			btnSuccessmessage.Clicked += btnSuccessclickevent;	
			btnErrormessage.Clicked += btnErrorclickevent;	
			btnWarningmessage.Clicked += btnWarningclickevent;
			btnCustomizationmessage.Clicked += btnCustomizationclickevent;        
           
		}

		private  async void btnInfoclickevent (object sender, EventArgs e)
		{
			UIMessage.ShowMessage ("Info", "Hey! You Clicked on Button", MessageControl.MessageTypes.Info);
		}

		private  async void btnSuccessclickevent (object sender, EventArgs e)
		{
			UIMessage.ShowMessage ("Success", "Hey! You Clicked on Button", MessageControl.MessageTypes.Success);
		}

		private  async void btnErrorclickevent (object sender, EventArgs e)
		{
			UIMessage.ShowMessage ("Error", "Hey! You Clicked on Button", MessageControl.MessageTypes.Error);
		}

		private  async void btnWarningclickevent (object sender, EventArgs e)
		{
			UIMessage.ShowMessage ("Warning", "Hey! You Clicked on Button", MessageControl.MessageTypes.Warning);
		}

		private  async void btnCustomizationclickevent (object sender, EventArgs e)
		{
            UIMessage.ShowMessage("Customization", "Hey! You Clicked on Button", MessageControl.MessageTypes.Warning, "validation.png", Color.Pink, Color.Black, true, OnDissmissControl);
		}

		public void OnDissmissControl ()
		{
			DisplayAlert ("Message", "HI", "OK");
		}
	}
    }

