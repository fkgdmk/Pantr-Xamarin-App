using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Posts : ContentPage
    {
        public Posts()
        {
            InitializeComponent();
          //   listView.ItemsSource = Test;
            Console.WriteLine("11111111111111111111111111");
            try
            {
                Console.WriteLine("222222222222222222222222");
                GetAllPosts(listView);
                Console.WriteLine("2,55555555555555555555555");
            } catch (Exception e)
            {
                Console.WriteLine("3333333333333333333333333333");
                Console.WriteLine("Whaaaaaaaaaaaaaat?");
                Console.WriteLine(e.StackTrace);
            }
        }

        public async void GetAllPosts2()
        {
            var client = new HttpClient(new AndroidClientHandler());

            var response = await client.GetAsync("http://192.168.0.22:45457/api/posts");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            //var result = JsonConvert.DeserializeObject(content);
        }


        public async void GetAllPosts(ListView listView)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (var r = await client.GetAsync(new Uri("http://10.111.180.139:45455/api/posts")))
                    {
                        string result = await r.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }

                }
                catch (Exception e) {
                    Console.WriteLine("=!=!=!=!=!=!=!=!=!=!=!=!");
                    Console.WriteLine(e.StackTrace);
                }
            }

//            listView.ItemsSource = null;
        }

        ObservableCollection<Post> Test = new ObservableCollection<Post> {
            new Post{Id=1, Address="Enmeget Lang testAddresse 44", Quantity="En masse", Time="Søndag klokken 16", Zipcode = "2200" },
            new Post{Id=2, Address="Tokevejeveve14", Quantity="En smule", Time="Onsdag klokken 16", Zipcode ="2100" },
            new Post{Id=3, Address="Charlottenlund Stationsplads 22", Quantity="En mellem", Time="Lørdag klokken 16" , Zipcode = "2720"},
            new Post{Id=4, Address="Tokevejevevevv16", Quantity="En mellem", Time="Lørdag klokken 16",Zipcode = "2350" },
            new Post{Id=5, Address="Tokevejevevevve17", Quantity="En mellem", Time="Lørdag klokken 16",Zipcode = "2560" },
            new Post{Id=6, Address="Borgmester Christiansens Gade 123", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" },
            new Post{Id=7, Address="Tokevejevevevveve19", Quantity="En mellem", Time="Lørdag klokken 16" , Zipcode = "2560"},
            new Post{Id=8, Address="Lygten 22", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" },
            new Post{Id=9, Address="Stærevej 10", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" },
            new Post{Id=9, Address="Borgmester Jakob Jensens Gade 33", Quantity="En mellem", Time="Lørdag klokken 16", Zipcode = "2560" }

        };

        public class Post
        {
            public int Id { get; set; }
            public string Address { get; set; }
            public string[] AddresChunks
            {
                get { return AddresChunks; }
                set { this.Address.Split(' '); }
            }
            public string Quantity { get; set; }
            public string Time { get; set; }
            public string Zipcode { get; set; }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ((ListView)sender).SelectedItem = null;
                var selection = e.SelectedItem as Post;
                var claimed = await DisplayAlert("Du vil gerne hente", "post med id " + selection.Id + " som har adressen: " + selection.Address, "OK", "Nej!");
                if (claimed)
                {
                    Console.WriteLine("åh?");
                
                    // Kode for hvis et opslag bliver claimed

                    HttpClient client = new HttpClient();

                    Console.WriteLine("åh? igen");


                    client.MaxResponseContentBufferSize = 256000;

                    Console.WriteLine("åh 3?");


                    //var json = JsonConvert.SerializeObject(selection);
                   // Console.WriteLine(json);

                    var content = new StringContent("{'Claimed' : True }", Encoding.UTF8, "application/json");

                    Console.WriteLine(content);


                    var response = await client.PutAsync("http://localhost:50001/api/put/"+selection.Id, content);

                }
                else
                {
                    // Opslag ikke taget

                }
            }
        }

        private void ZipcodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 0)
            {
             //   listView.ItemsSource = GetAllPosts;
            }

            if (e.NewTextValue.Length > 3)
            {
                IEnumerable<Post> allRelevantPosts = null;

                try
                {
//                    Console.WriteLine(GetAllPosts.GetType());
  //                  allRelevantPosts = GetAllPosts.Where(x => x.Address.Contains(e.NewTextValue));
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