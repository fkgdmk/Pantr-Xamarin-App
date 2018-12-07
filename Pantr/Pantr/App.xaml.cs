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
            //if (Current.Properties.ContainsKey("Username"))
            //{
            //    MainPage = new NavigationPage(new Posts());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login());
            //}

            //Kode forslag: PostsPage og CreatePostPage ?

            MainPage = new GetPost();

<<<<<<< HEAD
=======
            //MainPage = new NavigationPage(new ViewReservations());

            //if (Current.Properties.ContainsKey("Username"))
            //{
            //        var username = Current.Properties["Username"] as string;
            //MainPage = new NavigationPage(new ViewPost());
>>>>>>> 47f5b63d9f59055f3401021692360a0b21b549bf
            //if (Current.Properties.ContainsKey("Username"))
            //{
            //    //    var username = Current.Properties["Username"] as string;
            //    MainPage = new NavigationPage(new Posts());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login());
            //}
            MainPage = new NavigationPage(new ViewReservations());
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
