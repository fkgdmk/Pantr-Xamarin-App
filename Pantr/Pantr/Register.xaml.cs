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
            UserViewModelTest registerUser = new UserViewModelTest()
            {
                Firstname = firstName.Text,
                Surname = surname.Text,
                Email = email.Text,
                Phone = phone.Text,
                IsPanter = isPanter.IsToggled,
                Login = new LoginViewModel()
                {
                    Username = userNameRegister.Text,
                    Password = HashString(password.Text)
                },
                Address = new AddressViewModel()
                {
                    Address = address.Text,
                    City = new CityViewModel()
                    {
                        City = null,
                        Zip = zip.Text
                    }
                }
            };


            UserService userService = new UserService();
            var userRegistered = await userService.RegisterUser(registerUser);
            if(userRegistered != null)
            {
                await Navigation.PushAsync(new Login());
            } else
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