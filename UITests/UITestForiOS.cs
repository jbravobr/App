using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class UITestForiOS
    {
        public iOSApp app;
        [SetUp]
        public void Setup()
        {
            app = ConfigureApp.iOS
                              .AppBundle("/var/folders/c4/y8n24g7j03z6qyw1jf8p28kw0000gn/T/test-recorder/ios-injection/linked-05-04-16-10-34-10-app.app")
                              .StartApp();
        }

        [Test]
        public void NewTest()
        {
            app.Tap(x => x.Marked("E-mail"));
            app.Screenshot("Tapped on view with class: UITextFieldLabel marked: E-mail");
            app.EnterText(x => x.Class("UITextField"), "teste@icatuseguros.com.br");
            app.PressEnter();
            app.Tap(x => x.Marked("Senha"));
            app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Senha");
            app.EnterText(x => x.Class("UITextField").Index(1), "Icatu123!");
            app.Tap(x => x.Text("Entrar"));
            app.Screenshot("Tapped on view with class: UIButtonLabel marked: Entrar");
            app.Tap(x => x.Id("bus-full.png"));
            app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
            app.Tap(x => x.Marked("Almirante Barroso"));
            app.Screenshot("Tapped on view with class: MKPinAnnotationView marked: Almirante Barroso");
            app.Tap(x => x.Id("house-full.png"));
            app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
            app.Tap(x => x.Text("CHECK IN"));
            app.Screenshot("Tapped on view with class: UIButtonLabel marked: CHECK IN");
            app.Tap(x => x.Marked("CHECK OUT"));
            app.Screenshot("Tapped on view with class: UIButton marked: CHECK OUT");
            app.Tap(x => x.Marked("Sim"));
            app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Sim");
        }
    }
}
