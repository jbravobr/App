
You can add an `XMessageControl` as shown in the following example.  
  
#Example  

Follow the steps below to create a Xamarin.Forms PCL solution which will use the `XMessageControl` control.  
  
1. Create a new Xamarin.Forms Portable solution that includes Android, iOS and windows phone and PCL projects.    
  
2. Add the `XMessageControl` component/dll to your PCL project.  
  
3. Create XAML page with below XAML and Code behind. (Change sample Page name with your new page)
    
DESIGN-TIME XAML MARKUP (*XMessageControldemoPage.xaml* file)  
  
```
	<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
	             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
	             x:Class="XMessageControl.XMessageControldemoPage" >
		<StackLayout VerticalOptions="Center" Padding="20" >
	       <Button Text="Show Message" x:Name="btnmessage"></Button>
		    <Button Text="Show Toast" x:Name="btnmessagetaost"></Button>
		</StackLayout>
	</ContentPage>
 ``` 

C# CODE BEHIND (*XMessageControldemoPage.xaml.cs* file)

 ``` 
	using Softweb.Xamarin.Forms.Controls;
	
	public partial class XMessageControldemoPage : ContentPage
		{
			public XMessageControldemoPage ()
			{
		        btnmessage.Clicked += (object sender, EventArgs e) => {
					UIMessage.ShowMessage ("Customization", "Hey! You Clicked on Button", MessageControl.MessageTypes.Warning, "validation.png", Color.Pink, Color.Black, true, OnDissmissControl);
				};
				btnmessagetaost.Clicked += (object sender, EventArgs e) => {
				   UIMessage.ShowToast ("Hi!Customized Toast Message.\nThis is First toast Message with top postion", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Short, "validation.png", Color.Red);
			       UIMessage.ShowToast ("Hi!Test Message", ToastMessage.ToastPosition.CENTER);
				   UIMessage.ShowToast ("Hi!Test Message", ToastMessage.ToastPosition.BOTTOM, ToastMessage.Duration.Short);
				};
	    	}
			public void OnDissmissControl ()
			{
				DisplayAlert ("Message", "HI", "OK");
			}
		}	
```