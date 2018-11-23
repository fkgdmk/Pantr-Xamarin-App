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

namespace Pantr
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

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
                    App.Current.MainPage = new Register();
                })
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            LoginViewModel login = new LoginViewModel()
            {
                Username = username.Text,
                Password = userPassword.Text
            };
            var test = MethodAsync(login);
        }

        public async Task MethodAsync(LoginViewModel login)
        {
            Task<HttpResponseMessage> loginTest = LoginAsync(login);
            var result = await loginTest;
        }


        public async Task<HttpResponseMessage> LoginAsync(LoginViewModel login, bool isNewItem = false)
        {
            HttpResponseMessage response = null;

            using (HttpClient client = new HttpClient())
            {
                string url = @"http://localhost:50001/api/users";
                var uri = new Uri(url);


                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //if (isNewItem)
                //{
                response = await client.PostAsync(uri, content);
                //}


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"                TodoItem successfully saved.");

                }
            }
            return response;

        }
    }
}