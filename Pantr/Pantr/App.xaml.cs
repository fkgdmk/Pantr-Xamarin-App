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
            ////    var username = Current.Properties["Username"] as string;
            //    MainPage = new NavigationPage(new Posts());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login());
            //}

            //Kode forslag: PostsPage og CreatePostPage ?
            MainPage = new ViewPost();

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
