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
        public ObservableCollection<PostViewModelCopy> AllPosts { get; set; }

        public Posts()
        {
            InitializeComponent();   
                try
                {   // Sætter AllPosts-propertien til en observable collection med alle ikke-tagede opslag
                    getAllPosts();
                    // sætter vores listview i viewet til at vise alle posts
                    listView.ItemsSource = AllPosts;
                }
                catch (Exception e)
                {
                    //Laver en popup hvis der er en fejl med dette
                    //  hvilket normalt skyldes en database fejl
                    DisplayAlert("Fejl", "Ingen forbindelse til internettet", "Forstået");
                    Console.WriteLine(e.StackTrace);
                }
        }

        //Metode til at hente alle posts og sætter AllPosts-propertien tildette 
        protected async void getAllPosts()
        {
            AllPosts = new ObservableCollection<PostViewModelCopy>();
            var allPosts = await PostService.GetAllPosts();
            foreach (var item in allPosts)
            {
                AllPosts.Add(item);
            }
        }
       
        //Metode til at hente alle posts med et bestemt postnummer og sætter AllPosts-propertien til dette 
        protected async void getAllPostsFromZip(string zipcode)
        {
            AllPosts = new ObservableCollection<PostViewModelCopy>();
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
                var selection = e.SelectedItem as PostViewModelCopy;
                bool claimed = await DisplayAlert("Du vil gerne hente", "post med id " + selection.Id + " som har adressen: " + selection.Address, "OK", "Nej!");
                if (claimed)
                {
                    await Navigation.PushModalAsync(new ViewPost(selection, false));
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
                //Henter alle posts med det specifikke postnummer
                getAllPostsFromZip(e.NewTextValue);
                
                //TJekker om listen er tom  
                if (AllPosts.Count != 0)
                {
                    listView.ItemsSource = AllPosts;
                }
                else
                {
                    listView.ItemsSource = null;
                }
            }
        }
    }
}