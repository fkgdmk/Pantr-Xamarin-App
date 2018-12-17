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
            DateTime dateObj = date.Date;
            double start = startTime.Time.TotalMinutes;
            double end = endTime.Time.TotalMinutes;
            string bags = numberOfBags.Text;
            string cases = numberOfCases.Text;
            string sacks = numberOfSacks.Text;
            string materialType = null;

            if (dateObj < DateTime.Today)
            {
                await DisplayAlert("Ups", "Datoen skal være i fremtiden", "OK");
                return;
            }

            //Hvis der ikke er valgt en mægnde
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

            submit.IsVisible = false;
            IsBusy = true;

            JObject tbl_Material = new JObject();
            tbl_Material.Add("Type", materialType);

            //JObject tbl_User = new JObject();
            ////Skal ændres til brugers id
            //tbl_User.Add("PK_User", 5);

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


            bool response = await PostService.CreatePostInDb(tbl_Post);

            if (response)
            {
                IsBusy = false;
                submit.IsVisible = true;
                await DisplayAlert("Sådan!", "Pantopslag blev oprettet", "OK");
            }

        }

        private string FormatQuantityAsString(string bags, string boxes, string sacks)
        {
            //Tjekker om typen er udfyldt og parser væriden til en integer
            int numberOfBags = bags != null ? int.Parse(bags) : 0;
            int numberOfSacks = sacks != null ? int.Parse(sacks) : 0;
            int numberOfBoxes = boxes != null ? int.Parse(boxes) : 0;

            //Tjekker om typen skal stå i ental eller flertal
            string bagsForm = numberOfBags == 1 ? "Pose" : "Poser";
            string sacksForm = numberOfSacks == 1 ? "Sæk" : "Sække";
            string boxesForm = numberOfBoxes == 1 ? "Kasse" : "Kasser";

            //Tilføj kun type hvis antallet ikke er nul
            string bagsString = numberOfBags != 0 ? numberOfBags + " " + bagsForm + " " : "";
            string sacksString = numberOfSacks != 0 ? numberOfSacks + " " + sacksForm + " " : "";
            string boxesString = numberOfBoxes != 0 ? numberOfBoxes + " " + boxesForm : " ";

            string quantity = bagsString + sacksString + boxesString;

            return quantity;
        }

    }
}