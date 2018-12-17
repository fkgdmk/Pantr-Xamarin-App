using Newtonsoft.Json;
using Pantr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pantr.DB;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePost : ContentPage
    {
        public CreatePost()
        {
            InitializeComponent();
            BindingContext = this;
            IsBusy = false;
        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            //Udhender data fra input felterne og formaterer dem
            DateTime dateObj = date.Date;
            double start = startTime.Time.TotalMinutes;
            double end = endTime.Time.TotalMinutes;
            string bags = numberOfBags.Text;
            string cases = numberOfCases.Text;
            string sacks = numberOfSacks.Text;
            string materialType = null;

            //Hvis den valgte dato ikke er i fremtiden
            if (dateObj < DateTime.Today)
            {
                await DisplayAlert("Ups", "Datoen skal være i fremtiden", "OK");
                return;
            }

            //Hvis der ikke er valgt en mængde
            if (bags == null && cases == null && sacks == null)
            {
                await DisplayAlert("Ups", "Udfyld mængden af pant", "OK");
                return;
            }

            //Hvis der ikke er valgt materiale type
            if (picker.SelectedIndex > -1)
            {
                materialType = picker.Items[picker.SelectedIndex];
            }

            //Hvis tidsperioden er sat til mindre end time 
            if (end - start < 60)
            {
                await DisplayAlert("Ups", "Perioden skal vare mindst en time", "OK");
                return;
            }

            //Gemmer knappen og starter spinneren
            submit.IsVisible = false;
            IsBusy = true;

            JObject tbl_Material = new JObject();
            tbl_Material.Add("Type", materialType);

            JObject tbl_Quantity = new JObject();
            tbl_Quantity.Add("Bags", Convert.ToInt32(bags));
            tbl_Quantity.Add("Sacks", Convert.ToInt32(sacks));
            tbl_Quantity.Add("Cases", Convert.ToInt32(cases));

            JObject tbl_Post = new JObject();
            tbl_Post.Add("FK_Giver", 5);
            tbl_Post.Add("Date", dateObj);
            tbl_Post.Add("StartTime", (int)start);
            tbl_Post.Add("EndTime", (int)end);
            tbl_Post.Add("tbl_Material", tbl_Material);
            tbl_Post.Add("tbl_Quantity", tbl_Quantity);

            //Kalder PostService klassens CreatePost metode og pantopslaget med
            PostService postService = new PostService();
            bool response = await postService.CreatePostInDb(tbl_Post);

            //CreatePost returnere true hvis pantopslaget blev ændret succesfuld og false hvis der skete en fejl
            if (response)
            {
                IsBusy = false;
                submit.IsVisible = true;
                await DisplayAlert("Sådan!", "Pantopslag blev oprettet", "OK");
            } else
            {
                await DisplayAlert("Ups", "Der skete en fejl. Prøv igen", "OK");
            }

        }
    }
}