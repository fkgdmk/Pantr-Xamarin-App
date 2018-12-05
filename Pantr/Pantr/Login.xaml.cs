using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.Models;
using Pantr.DB;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            registerButtonFunc();
        }

        private void registerButtonFunc()
        {
            registerButton.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    App.Current.MainPage = new NavigationPage(new Register());
                })
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            JObject login = new JObject();
            login.Add("Username", username.Text);
            login.Add("Password", HashString(userPassword.Text));


            LoginService ls = new LoginService();
            var loginAuthenticated = await ls.AuthenticateUser(login);
            setLoggedInUser(loginAuthenticated);
            if(loginAuthenticated != null)
            {
                await Navigation.PushAsync(new Posts());
            } else
            {
                //fejlhåndtering
            }
        }

        private string HashString(string inputString)
        {
            byte[] data = Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = Encoding.ASCII.GetString(data);
            return hash;
        }

        private void setLoggedInUser(UserViewModelTest authenticatedUser)
        {
            if (authenticatedUser == null) return;
            Application.Current.Properties["Username"] = authenticatedUser.Username;
            Application.Current.Properties["ID"] = authenticatedUser.ID;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.Properties["Username"] = null;
            Application.Current.Properties["ID"] = null;
        }
    }
}