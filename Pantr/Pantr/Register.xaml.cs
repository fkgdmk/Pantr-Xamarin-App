using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{

    public class User
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public bool isPanter { get; set; }
        public bool isAdult { get; set; }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Username = userNameRegister.Text,
                Password = HashString(password.Text),
                FirstName = firstName.Text,
                Surname = surname.Text,
                Address = address.Text,
                Zip = zip.Text,
                isPanter = isPanter.IsToggled,
                isAdult = isAdult.IsToggled
            };


            JObject jObject = new JObject();
            string sUrl = "http://localhost:50001/api/users";
            string sContentType = "application/json";

            jObject.Add("username", user.Username);
            jObject.Add("Password", HashString(password.Text));
            jObject.Add("FirstName", firstName.Text);
            jObject.Add("Surname", surname.Text);
            jObject.Add("Address", address.Text);
            jObject.Add("Zip", zip.Text);
            jObject.Add("isPanter", isPanter.IsToggled);
            jObject.Add("isAdult", isAdult.IsToggled);


            
        }

        private byte[] HashString(string inputString)
        {
            byte[] data = Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hash = Encoding.ASCII.GetString(data);
            return data;
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