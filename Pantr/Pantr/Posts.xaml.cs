using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Pantr.Models;
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