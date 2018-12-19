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
        PostViewModel post;

        //denne construktor bruges fra ViewReservations så afmeld knappen kun vises hvis isOwnPost = true
        public ViewPost(PostViewModel post, int viewType)
        {
            //afmeld knappen vises kun hvis isOwnPost er true
            InitializeComponent();

            //Bestemmer hvilken knap der skal vises baseret på hivlken type opslag det er
            //HVis det er en panthenter der trykker på et pantopslag i sine reserveret pantopslag
            if (viewType == 1) {
                afmeldButton.IsVisible = true;
            //Hvis det er brugerens eget pantopslag
            } else if (viewType == 2)
            {
                cancelBtn.IsVisible = true;
                editBtn.IsVisible = true;
            //HVis det er en panthenter der trykker på et pantopslag i listen over pantopslag
            }
            else
            {
                reserverBtn.IsVisible = true;
            }

            this.post = post;
            BindingContext = post;
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
        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            PostService postService = new PostService();
            bool response = await postService.DeletePost(this.post.Id);

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
            var cancelledPost = (PostViewModel)BindingContext;

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

        private void ReserverBtn_Clicked(object sender, EventArgs e)
        {
            //PostService service = new PostService();


            //JObject user = new JObject
            //{
            //    { "PK_User", post. },
            //    { "Firstname", "Roland" },
            //    { "Surname", "Kock" },
            //    { "Phone", 88888888 },
            //    { "Email", "roland@kock.nu" },
            //    { "IsPanter", "true" },
            //    { "FK_Address", 1 },
            //    { "FK_Login", 4 }
            //};

            ////bool response = await service.ClaimPost(user);

            //if (response)
            //{
            //    DisplayAlert("Reserveret", "Du har reserveret dette pantopslag!", "OK");


            //}
        }

    }
}