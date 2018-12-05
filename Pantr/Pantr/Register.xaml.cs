using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pantr.DB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.Models;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            JObject registerUser = new JObject();
            JObject registerLogin = new JObject();
            JObject registerCity = new JObject();
            JObject registerAddress = new JObject();

            registerCity.Add("City", null);
            registerCity.Add("Zip", zip.Text);

            registerAddress.Add("Address", address.Text);
            registerAddress.Add("City", registerCity);

            registerLogin.Add("Username", userNameRegister.Text);
            registerLogin.Add("Password", HashString(password.Text));

            registerUser.Add("Firstname", firstName.Text);
            registerUser.Add("Surname", surname.Text);
            registerUser.Add("Email", email.Text);
            registerUser.Add("Phone", phone.Text);
            registerUser.Add("IsPanter", isPanter.IsToggled);
            registerUser.Add("Address", registerAddress);
            registerUser.Add("Login", registerLogin);

            UserService userService = new UserService();
            var userRegistered = await userService.RegisterUser(registerUser);
            if (userRegistered)
            {
                await Navigation.PushAsync(new Login());
            }
            else
            {
                //Fejlhåndtering
            }
        }



        private string HashString(string inputString)
        {
            byte[] data = Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = Encoding.ASCII.GetString(data);
            return hash;
        }

        //Textchanged event som sammenligner passwords. Disabler submit-knappen så længe passwords er uens
        private void confirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            if (!e.NewTextValue.Equals(password.Text))
            {
                entry.TextColor = Color.Red;
                submitButton.IsEnabled = false;
                return;
            }

            entry.TextColor = Color.Default;
            submitButton.IsEnabled = true;
        }
    }
}