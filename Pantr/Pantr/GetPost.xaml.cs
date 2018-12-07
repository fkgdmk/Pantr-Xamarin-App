using Newtonsoft.Json.Linq;
using Pantr.DB;
using Pantr.Models;
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
	public partial class GetPost : ContentPage
	{
		public GetPost ()
        { 
        }

        PostViewModel post;

        protected override async void OnAppearing()
        {
            PostViewModel post = await PostService.GetPost();
            BindingContext = post;
            this.post = post;
            InitializeComponent();
        }
    
        private async void Button_Reserver(object sender, EventArgs e)
        {
            PostService service = new PostService();


            JObject user = new JObject
            {
                { "PK_User", 1 },
                { "Firstname", "Roland" },
                { "Surname", "Kock" },
                { "Phone", 88888888 },
                { "Email", "roland@kock.nu" },
                { "IsPanter", "true" },
                { "FK_Address", 1 },
                { "FK_Login", 4 }
            };

            bool response = await service.ClaimPost(user);

            if (response)
            {
                DisplayAlert("Reserveret", "Du har reserveret dette pantopslag!", "OK");
            }
        }
    }
}