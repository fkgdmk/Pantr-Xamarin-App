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

<<<<<<< HEAD
            //Kode forslag: PostsPage og CreatePostPage ?
            MainPage = new Posts();

=======
            MainPage = new NavigationPage(new ViewReservations());
>>>>>>> 1858af0c1f0302cce773c7814d2adc04c7c40a80
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
