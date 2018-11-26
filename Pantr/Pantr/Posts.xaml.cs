using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.DB;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Posts : ContentPage
    {
        public Posts()
        {
            
           InitializeComponent();
           
            listView.ItemsSource = GetAllPosts;
        }


        ObservableCollection<Post> GetAllPosts = new ObservableCollection<Post> {

            new Post{Id=1, Address="Stærevej 75", Quantity="En masse", Time="Søndag klokken 16", Zipcode = "2200" },
            new Post{ Id =2, Address="Hærevej 45 2100", Quantity="En smule", Time="Onsdag klokken 16", Zipcode ="2100" },
            new Post{Id = 3,Address="Mærevej 25", Quantity="En mellem", Time="Lørdag klokken 16" , Zipcode = "2720"},
            new Post{Id = 4, Address="Mærevej 25", Quantity="En mellem", Time="Lørdag klokken 16",Zipcode = "2350" },
            new Post{Id = 5, Address="Mærevej 25", Quantity="En mellem", Time="Lørdag klokken 16",Zipcode = "2560" },
            new Post{Id = 6, Address="Mærevej 25", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" },
            new Post{Id=7, Address="Mærevej 25 2700", Quantity="En mellem", Time="Lørdag klokken 16" , Zipcode = "2560"},
            new Post{Id=8, Address="Mærevej 25 2730", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" },
            new Post{Id=9, Address="Mærevej 25 ", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" }
        };

        public class Post
        {
            public int Id { get; set; }
            public string Address { get; set; }
            public string Quantity { get; set; }
            public string Time { get; set; }
            public string Zipcode { get; set; }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                ((ListView)sender).SelectedItem = null;
                var selection = e.SelectedItem as Post;
                var claim = await DisplayAlert("Du vil gerne hente","post med id " + selection.Id + " som har adressen: " + selection.Address, "OK", "Nej!");
                if (claim)
                {
                    // Kode for hvis et opslag bliver claimed

                }
                else
                {
                    // Opslag ikke taget

                }
            }
        }

        private void zipcodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 0)
            {
                listView.ItemsSource = GetAllPosts;
            }

            if (e.NewTextValue.Length > 3)
            {
                IEnumerable<Post> allRelevantPosts = null;

                try
                {
                    Console.WriteLine(GetAllPosts.GetType());
                    allRelevantPosts = GetAllPosts.Where(x => x.Address.Contains(e.NewTextValue));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fanget i try catch!");
                    Console.WriteLine(ex.Source + " " + ex.Message);
                }
                if (allRelevantPosts == null)
                {
                    Console.WriteLine("Alle relevant er null");
               
                    listView.ItemsSource = new List<Post> { new Post { Id=-1, Address="Der er desværre ikke noget pant", Quantity="" , Time="" } };

                }
                else
                {
                    listView.ItemsSource = allRelevantPosts;
                }
            }
        }
    }
}