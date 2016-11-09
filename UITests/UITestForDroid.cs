using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class UITestForDroid
    {
        public AndroidApp app;

        [SetUp]
        public void Setup()
        {
            app = ConfigureApp.Android.ApkFile("/Users/ramaral/Desktop/com.icatuseguros.icatuzinhoapp.apk")
                              .StartApp();
        }

        [Test]
        public void NewTest()
        {
            app.Tap(x => x.Class("EntryEditText"));
            app.Screenshot("Tapped on view with class: EntryEditText");
            app.EnterText(x => x.Class("EntryEditText"), "teste@icatuseguros.com.br");
            app.PressEnter();
            app.Tap(x => x.Class("EntryEditText").Index(1));
            app.Screenshot("Tapped on view with class: EntryEditText");
            app.EnterText(x => x.Class("EntryEditText").Index(1), "Icatu123!");
            app.PressEnter();
            app.Tap(x => x.Text("Entrar"));
            app.Screenshot("Tapped on view with class: Button");
            app.Tap(x => x.Text("Itinerário"));
            app.Screenshot("Tapped on view with class: TextView");
            app.ScrollTo("Google Map");
            app.Screenshot("ScrollToEvent[AppView: Class=Xamarin.TestRecorder.Portable.Models.Class, Id=, Text=, Marked=Google Map, Css=, CssIndex=0, IndexInTree=0, Rect=[Rectangle: Left=0, Top=0, CenterX=384, CenterY=721, Width=768, Height=926, Bottom=1184, Right=768]]");
            app.Tap(x => x.Marked("Google Map"));
            app.Screenshot("Tapped on view with class: p marked: Google Map");
            app.Tap(x => x.ClassFull("com.android.internal.widget.ScrollingTabContainerView$TabView"));
            app.Screenshot("Tapped on view with class: ScrollingTabContainerView$TabView");
            app.Tap(x => x.Text("CHECK IN"));
            app.Screenshot("Tapped on view with class: Button");
            app.Tap(x => x.Text("CHECK OUT"));
            app.Screenshot("Tapped on view with class: Button");
            app.Tap(x => x.Id("button2"));
            app.Screenshot("Tapped on view with class: Button");
            app.Tap(x => x.Text("CHECK OUT"));
            app.Screenshot("Tapped on view with class: Button");
            app.Tap(x => x.Id("button1"));
            app.Screenshot("Tapped on view with class: Button");
        }
    }
}
