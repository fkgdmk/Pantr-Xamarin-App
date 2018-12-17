using Newtonsoft.Json;
using Pantr.DB;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.Models;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPost : ContentPage
	{
        PostViewModelCopy post;
        public ViewPost ()
		{            
        }

        //Da metoder der udfylder rest kald altid er asynkrone skal metoden være async
        protected override async void OnAppearing()
        {
            //Henter brugerens pantopslag og sætter BindingContext til objektet

            PostViewModelCopy post = await PostService.GetUsersPost(5);
            BindingContext = post;

            InitializeComponent();
            //Rediger knappen vises kun hvis pantopslaget ikke er blevet taget
            //Det skal ikke være muligt at redigere et pantopslag der allerede er taget
            editBtn.IsVisible = !post.Claimed;
        }

        //Når der trykkes på Rediger knappen sendes brugeren videre til EditPost siden
        private async void edit(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EditPost(post));
        }

        //Sletter pantopslaget
        private async void submit_Clicked(object sender, EventArgs e)
        {
            PostService postService = new PostService();
            bool response = await postService.DeletePost(5);

            //DeletePost returnere en true hvis pantopslaget blev slettet og og false hvis der skete en fejl
            if (response)
            {
                await DisplayAlert("Annulleret", "Dit pantopslag blev annulleret", "OK");
            } else
            {
                await DisplayAlert("Ups", "Der skete en fejl. Prøv igen", "OK");
            }
        }
    }
}