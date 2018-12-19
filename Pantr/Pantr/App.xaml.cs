using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.Models;
using Pantr.DB;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Pantr
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Current.Properties["Username"] = "admin";
            //Current.Properties["Id"] = 1;

            if (Current.Properties.ContainsKey("Username"))
            {
                MainPage = new NavigationPage(new Posts());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        protected async override void OnStart()
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
