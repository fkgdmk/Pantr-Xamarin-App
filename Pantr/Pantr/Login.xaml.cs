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

            //Sætter en gesturerecognizer på label "Registrer her" så det fungerer som et link
            registerButtonFunc();
        }

        //Her laves selve "eventet/gesturerecognizer"
        private void registerButtonFunc()
        {
            registerButton.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                //tilføjer en command til en tapgesturerecognizer som skifter side til register når man trykker på labelet
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new Register());
                })
            });
        }

        //login button event
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //sætter login variabler
            JObject login = new JObject();
            login.Add("Username", username.Text);
            login.Add("Password", HashString(userPassword.Text));


            LoginService ls = new LoginService();

            //authenticateuser klargøre database kald 
            var loginAuthenticated = await ls.AuthenticateUser(login);

            //sætter variable i application properties så ID kommer med rundt
            setLoggedInUser(loginAuthenticated);

            //skifter til posts hvis login er succesfuldt
            if(loginAuthenticated != null)
            {
                await Navigation.PushAsync(new Posts());
            } else
            {
                //fejlhåndtering
            }
        }

        //hasher password
        private string HashString(string inputString)
        {
            //encoder til bytearray
            byte[] data = Encoding.ASCII.GetBytes(inputString);

            //hasher vha sha256
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

            //encoder til string
            string hash = Encoding.ASCII.GetString(data);

            return hash;
        }

        //sætter application properties så userID kan bruges i andre views
        private void setLoggedInUser(UserViewModelTest authenticatedUser)
        {
            if (authenticatedUser == null) return;
            Application.Current.Properties["Username"] = authenticatedUser.Username;
            Application.Current.Properties["ID"] = authenticatedUser.ID;
        }
    }
}