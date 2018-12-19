using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pantr
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new Posts());

                MainPage = new NavigationPage(new Posts());
            //if (Current.Properties.ContainsKey("Username"))
            //{
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login());
            //}

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
