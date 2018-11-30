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
            string boxes = numberOfBoxes.Text;
            string sacks = numberOfSacks.Text;
            string materialType = null;

            //Hvis der ikke er valgt en mægnde
            if (bags == null && boxes == null && sacks == null)
            {
                DisplayAlert("Fejl", "Udfyld mængde af pant", "Ok");
                return;
            }
            string quantity = FormatQuantityAsString(bags, boxes, sacks);

            //Hvis der ikke er valgt materiale type
            if (picker.SelectedIndex > -1)
            {
                materialType = picker.Items[picker.SelectedIndex];
            }

            //Hvis tidsperioden er sat til mindre end time 
            if (end - start < 60)
            {
                DisplayAlert("Fejl", "Perioden skal være mindt en time", "Cool");
                return;
            }

            IsBusy = true;

            PostViewModel post = new PostViewModel
            {
                Date = dateObj.ToString("dd/MM/yyyy"),
                StartTime = (int)start,
                EndTime = (int)end,
                Quantity = quantity,
                Material = new MaterialViewModel
                {
                    Type = materialType
                }
            };

            HttpResponseMessage response = await PostService.CreatePostInDb(post);

            if (response.IsSuccessStatusCode)
            {
                IsBusy = false;
            }

        }

        private string FormatQuantityAsString (string bags, string boxes, string sacks)
        {
            //Tjekker om typen er udfyldt og parser væriden til en integer
            int numberOfBags = bags != null ? int.Parse(bags) : 0;
            int numberOfSacks = sacks != null ? int.Parse(sacks) : 0;
            int numberOfBoxes = boxes != null ? int.Parse(boxes) : 0;

            //Tjekker om typen skal stå i ental eller flertal
            string bagsForm = numberOfBags == 1 ? "Pose" : "Poser";
            string sacksForm = numberOfSacks == 1 ? "Sæk" : "Sække";
            string boxesForm = numberOfBoxes == 1 ? "Kasse" : "Kasser";

            string quantity = numberOfBags + " " + bagsForm + ", " + numberOfSacks + " " + sacksForm + ", " + numberOfBoxes + " " + boxesForm;

            return quantity;
        }

    }
}