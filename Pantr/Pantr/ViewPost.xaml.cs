using Newtonsoft.Json;
using Pantr.DB;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.Models;
using Newtonsoft.Json.Linq;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPost : ContentPage
	{

        //Da metoder der udfylder rest kald altid er asynkrone skal metoden være async
        //protected override async void OnAppearing()
        //{
        //    //Henter brugerens pantopslag og sætter BindingContext til objektet

        //    PostViewModelCopy post = await PostService.GetUsersPost(5);
        //    BindingContext = post;

        //    InitializeComponent();
        //    //Rediger knappen vises kun hvis pantopslaget ikke er blevet taget
        //    //Det skal ikke være muligt at redigere et pantopslag der allerede er taget
        //    editBtn.IsVisible = !post.Claimed;
        //}

        //denne construktor bruges fra ViewReservations så afmeld knappen kun vises hvis isOwnPost = true
        public ViewPost (PostViewModelCopy post, bool isOwnPost)
        { 
            //afmeld knappen vises kun hvis isOwnPost er true
            if (!isOwnPost) afmeldButton.IsEnabled = false;
            BindingContext = post;
            InitializeComponent();
        }



        public ViewPost()
        {

        }
        protected override async void OnAppearing()
        {
            //PostViewModelCopy post = await PostService.GetUsersPost(5);
            //BindingContext = post;
            //InitializeComponent();
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

        //Event som afmelder ens egen transaction
        private async void AfmeldButton_Clicked(object sender, EventArgs e)
        {
            TransactionService transactionService = new TransactionService();
           
            //caster bindingcontexten (som sættes i constructoren) til en postviewmodelcopy
            var cancelledPost = (PostViewModelCopy)BindingContext;

            //Instantierer et jobject som bruges i transactionservice med post og panter id
            //id'erne bruges til at afmelde den rette post
            JObject postJObject = new JObject();
            postJObject.Add("postId", cancelledPost.Id);

            //postJObject.Add("panterId", (int)Application.Current.Properties["Id"]);
            postJObject.Add("panterId", 1);

            bool result = await transactionService.CancelReservation(postJObject);

            //Hvis den succesfuldt blev annuleret/afmeld sendes vi tilbage til vores reservationer
            if (result) await Navigation.PushAsync(new ViewReservations());
        }
    }
}