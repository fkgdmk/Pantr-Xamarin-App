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


        //Når der trykkes på registrer sættes JObjects med de korrekte data
        //JObject ligner datamodellen i apiet
        private async void Button_Clicked(object sender, EventArgs e)
        {
            JObject registerUser = new JObject();
            JObject registerLogin = new JObject();
            JObject registerCity = new JObject();
            JObject registerAddress = new JObject();

            registerCity.Add("City", null);
            registerCity.Add("Zip", zip.Text);

            registerAddress.Add("Address", address.Text);
            registerAddress.Add("tbl_City", registerCity);

            registerLogin.Add("Username", userNameRegister.Text);
            registerLogin.Add("Password", HashString(password.Text));

            registerUser.Add("Firstname", firstName.Text);
            registerUser.Add("Surname", surname.Text);
            registerUser.Add("Email", email.Text);
            registerUser.Add("Phone", phone.Text);
            registerUser.Add("IsPanter", isPanter.IsToggled);
            registerUser.Add("tbl_Address", registerAddress);
            registerUser.Add("tbl_Login", registerLogin);

            UserService userService = new UserService();

            //RegisterUser sørger for database kaldet og returnerer en task<bool> baseret på om det var succesfuldt eller ej
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


        //HAsher password vha sha256
        private string HashString(string inputString)
        {
            //encoder til byte[]
            byte[] data = Encoding.ASCII.GetBytes(inputString);

            //hasher
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

            //returnerer en hashet string
            string hash = Encoding.ASCII.GetString(data);

            return hash;
        }

        //Textchanged event som sammenligner passwords. Disabler submit-knappen så længe passwords er uens
        private void confirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Caster senderen til en entry
            Entry entry = (Entry)sender;

            //tjekker om den nye værdi i entryen matcher den i den forrige password entry
            //Hvs ikke sættes farven til rød og submit-knappen bliver disabled
            if (!e.NewTextValue.Equals(password.Text))
            {
                entry.TextColor = Color.Red;
                submitButton.IsEnabled = false;
                return;
            }

            //sætter farven på teksten til default hvis de matcher
            entry.TextColor = Color.Default;
            submitButton.IsEnabled = true;
        }
    }
}