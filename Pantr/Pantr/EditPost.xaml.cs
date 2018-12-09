using Newtonsoft.Json.Linq;
using Pantr.DB;
using Pantr.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPost : ContentPage
	{
        PostViewModelCopy post;
           
        public EditPost(PostViewModelCopy post)
        {
            InitializeComponent();
            this.post = post;
            submit.IsVisible = true;
            IsBusy = false;
            BindingContext = this;
            DateTime postDate = DateTime.ParseExact(post.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            //Hente input felterne fra viewet og sætter deres værdier til pantopslagets 
            date.Date = postDate;
            startTime.Time = ConvertToTimeSpan(post.StartTime);
            endTime.Time = ConvertToTimeSpan(post.EndTime);
            numberOfBags.Text = post.Bags.ToString();
            numberOfSacks.Text = post.Sacks.ToString();
            numberOfCases.Text = post.Cases.ToString();
            address.Text = post.Address;
            if (post.Material != null)
            {
                picker.SelectedItem = post.Material;
            }
        }

        public TimeSpan ConvertToTimeSpan (string time)
        {
            string[] timeArr = time.Split(':');
            TimeSpan toTimeSpan = new TimeSpan(Convert.ToInt32(timeArr[0]), Convert.ToInt32(timeArr[1]), 0);
            return toTimeSpan;
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
                DisplayAlert("Ups", "Datoen skal være i fremtiden", "OK");
                return;
            }

            //Hvis der ikke er valgt en mægnde
            if (bags == null && cases == null && sacks == null)
            {
                DisplayAlert("Ups", "Udfyld mængden af pant", "OK");
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
                DisplayAlert("Ups", "Perioden skal vare mindst en time", "OK");
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
            //Skal ændres til brugern id
            tbl_Post.Add("FK_Giver", 5);
            tbl_Post.Add("Date", dateObj);
            tbl_Post.Add("StartTime", (int)start);
            tbl_Post.Add("EndTime", (int)end);
            tbl_Post.Add("tbl_Material", tbl_Material);
            tbl_Post.Add("tbl_Quantity", tbl_Quantity);


            PostService postService = new PostService();
            bool response = await postService.UpdatePost(5, tbl_Post);

            if (response)
            {
                IsBusy = false;
                submit.IsVisible = true;
                DisplayAlert("Sådan!", "Pantopslag blev redigeret", "OK");
                await Navigation.PushModalAsync(new ViewPost());
            } else
            {
                DisplayAlert("Ups", "Der opstod en fejl. Prøv igen", "OK");
            }
        }

    }
}