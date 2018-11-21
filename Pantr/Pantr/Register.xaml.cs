using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private byte[] HashString(string inputString)
        {
            byte[] data = Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return data;
        }

        //Textchanged event som sammenligner passwords. Disabler submit-knappen så længe passwords er uens
        private void confirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (entry.Text.Equals(password.Text))
            {
                entry.TextColor = Color.Default;
                submitButton.IsEnabled = true;
            }
            else
            {
                entry.TextColor = Color.Red;
                submitButton.IsEnabled = false;
            }
        }
    }
}