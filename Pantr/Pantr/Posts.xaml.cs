using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Pantr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.DB;
using System.Net.Http;
using Xamarin.Android.Net;
using System.Collections.ObjectModel;

namespace Pantr
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Posts : ContentPage
    {
        //En observable collection der holder alle posts der skal vises i viewets "listView"-variabel 
        public ObservableCollection<PostViewModel> AllPosts { get; set; }
        public PostViewModel Post { get; set; }

        public Posts()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            //PostViewModelCopy post = (PostViewModelCopy)Application.Current.Properties["Post"];
            //getUsersPost();

            try
            {   // Sætter AllPosts-propertien til en observable collection med alle ikke-tagede opslag
                PostService postService = new PostService();
                if (!Application.Current.Properties.ContainsKey("ID"))
                {
                    await DisplayAlert("Hov du!", "Der er vidst sket en fejl\nLuk appen ned og log ind igen", "OK");
                }
                var userId = (int)Application.Current.Properties["ID"];
                //Henter brugers pantopslag
                Post = await postService.GetUsersPost(userId);

                if (Post != null)
                {
                    viewPost.IsVisible = true;
                    createPost.IsVisible = false;
                }
                else
                {
                    createPost.IsVisible = true;
                }
                getAllPosts();
                // sætter vores listview i viewet til at vise alle posts
                listView.ItemsSource = AllPosts;
            }
            catch (Exception e)
            {
                //Laver en popup hvis der er en fejl med dette
                //  hvilket normalt skyldes en database fejl
                await DisplayAlert("Fejl", "Ingen forbindelse til internettet", "Forstået");
                Console.WriteLine(e.StackTrace);
            }
        }


        private async void getUsersPost()
        {
            PostService postService = new PostService();
            Post = new PostViewModel();
            if (!Application.Current.Properties.ContainsKey("ID"))
            {
                await DisplayAlert("Hov du!", "Der er vidst sket en fejl\nLuk appen ned og log ind igen", "OK");
            }
            var userId = (int)Application.Current.Properties["ID"];
            Post = await postService.GetUsersPost(userId);
        }

        //Metode til at hente alle posts og sætter AllPosts-propertien tildette 
        private async void getAllPosts()
        {
            AllPosts = new ObservableCollection<PostViewModel>();
            var allPosts = await PostService.GetAllPosts();
            foreach (var item in allPosts)
            {
                AllPosts.Add(item);
            }
        }

        //Metode til at hente alle posts med et bestemt postnummer og sætter AllPosts-propertien til dette 
        protected async void getAllPostsFromZip(string zipcode)
        {
            AllPosts = new ObservableCollection<PostViewModel>();
            var allPosts = await PostService.GetAllPosts(zipcode);
            foreach (var item in allPosts)
            {
                AllPosts.Add(item);
            }
        }


        // Metode der bliver kaldt når et list item fra vores ListView i viewet bliver trykket på.
        // Åbner dette i et nyt view
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Sikrer os at det valgte item ikke er null
            if (e.SelectedItem != null)
            {
                // Fjerner det valgte item til null, så vi får et visuelt indtryk
                // af at det valgte item ikke bliver "markeret" men klikket på
                ((ListView)sender).SelectedItem = null;

                // vi caster vores objekt til et PostViewModel
                var selection = e.SelectedItem as PostViewModel;
                bool claimed = await DisplayAlert("Pantopslag", "Vil du se pantopslag?", "Ja", "Nej");
                if (claimed)
                {
                    await Navigation.PushAsync(new ViewPost(selection, 3));
                }
                else
                {
                    // Opslag ikke taget
                }
            }
        }

        // en listener der er sat på entry-feltet ZipcodeSearch
        // der lytter på om teksten bliver ændret
        private void ZipcodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Sørger for at vi kune laver en søgning hvor søgestrengen er præcist 4
            // hvilket som bekendt er længden på de danske postnumre
            if (e.NewTextValue.Length == 4)
            {
                IsBusy = true;
                //Henter alle posts med det specifikke postnummer
                getAllPostsFromZip(e.NewTextValue);
               
                listView.ItemsSource = AllPosts;
                IsBusy = false;
            }
            else if(e.NewTextValue.Length == 0)
            {
                getAllPosts();
                listView.ItemsSource = AllPosts;
            }
        }

        private void CreatePost_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePost());
        }

        private void ViewPost_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewPost(Post, 2));
        }

        private void ReservationsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewReservations());
        }
    }
}