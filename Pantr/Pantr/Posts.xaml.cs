using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Pantr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pantr.DB;
using System.Net.Http;
using Xamarin.Android.Net;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Posts : ContentPage
    {
        public Posts()
        {

            InitializeComponent();
            //   listView.ItemsSource = Test;
            //Console.WriteLine("11111111111111111111111111");
            //try
            //{
            //    Console.WriteLine("222222222222222222222222");
                PostService.GetAllPosts(listView);
            //    //listView.ItemsSource = Test;
            //    Console.WriteLine("2,55555555555555555555555");
            //} catch (Exception e)
            //{
            //    Console.WriteLine("3333333333333333333333333333");
            //    Console.WriteLine("Whaaaaaaaaaaaaaat?");
            //    Console.WriteLine(e.StackTrace);
            //}
        }

        public async void GetAllPosts2()
        {
            var client = new HttpClient(new AndroidClientHandler());

            var response = await client.GetAsync("http://192.168.0.22:45457/api/posts");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            //var result = JsonConvert.DeserializeObject(content);

            IsBusy = true;
             try
            {
                IEnumerable<PostViewModelCopy> allPosts = (IEnumerable < PostViewModelCopy >) PostService.GetAllPosts("");
                listView.ItemsSource = allPosts;
                IsBusy = false;

            
            } catch (Exception e)
            {
                DisplayAlert("Fejl","Ingen forbindelse til internettet", "Forstået");
                Console.WriteLine(e.StackTrace);
             }
        }

  
     


        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ((ListView)sender).SelectedItem = null;
                var selection = e.SelectedItem as PostViewModelCopy;
                var claimed = await DisplayAlert("Du vil gerne hente", "post med id " + selection.Id + " som har adressen: " + selection.Address, "OK", "Nej!");
                if (claimed)
                {

                }
                else
                {
                    // Opslag ikke taget

                }
            }
        }

        private void ZipcodeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 3)
            {
                IEnumerable<PostViewModelCopy> allRelevantPosts =(IEnumerable < PostViewModelCopy >) PostService.GetAllPosts(e.NewTextValue);
                
                if (allRelevantPosts == null)
                {
                   listView.ItemsSource = null;

                }
                else
                {
                    listView.ItemsSource = allRelevantPosts;
                }
            }
        }
    }
}