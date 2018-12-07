using Newtonsoft.Json;
using Pantr.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.Models;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPost : ContentPage
	{
        //Post post = new Post { Id = 1, Address = "Lygten 18, 2400 Kbh", Quantity = "1 Kasse", Date="11/12/2018", Time = "10.30-12.00" };
        public ViewPost ()
		{            
        }

        protected override async void OnAppearing()
        {
            PostViewModelCopy post = await PostService.GetUsersPost(5);
            BindingContext = post;
            InitializeComponent();
        }

        void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                DisplayAlert("test", "test", "test", "test");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void edit(object sender, EventArgs e)
        {
            //var editpost = new editpost();

            //await navigation.pushmodalasync(editpost);

            //displayalert("test", "test", "test", "test");
        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            PostService postService = new PostService();
            bool response = await postService.DeletePost(5);
            if (response)
            {
                DisplayAlert("Annulleret", "Dit pantopslag blev annulleret", "OK");
            }
        }
    }
}