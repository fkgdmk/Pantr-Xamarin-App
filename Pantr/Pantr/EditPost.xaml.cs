using Newtonsoft.Json.Linq;
using Pantr.DB;
using System;
using System.Globalization;
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
            //Property IsBusy bruges til at starte spinneren når der bliver sendt data
            IsBusy = false;
            //Ved at sætte BindingContext til this er det muligt at sætte værdierne på xaml elementerne
            BindingContext = this;
            //Da datoen kommer som en string skal den parses til et Date objekt
            //CultureInfo giver information hvilken kalender/tidszone der bruges
            DateTime postDate = DateTime.ParseExact(post.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            //Henter input felterne fra viewet og sætter deres værdier til pantopslagets data
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

        //For at tilføje default data til et TimePicker elementer skal dataen være formateret som en TimeSpan
        public TimeSpan ConvertToTimeSpan (string time)
        {
            //Splitter klokkeslættet på : og får så et array med kun time og minutter
            string[] timeArr = time.Split(':');
            TimeSpan toTimeSpan = new TimeSpan(Convert.ToInt32(timeArr[0]), Convert.ToInt32(timeArr[1]), 0);
            return toTimeSpan;
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
            //Skal ændres til brugern id
            tbl_Post.Add("FK_Giver", 5);
            tbl_Post.Add("Date", dateObj);
            tbl_Post.Add("StartTime", (int)start);
            tbl_Post.Add("EndTime", (int)end);
            tbl_Post.Add("tbl_Material", tbl_Material);
            tbl_Post.Add("tbl_Quantity", tbl_Quantity);

            PostService postService = new PostService();
            //Kalder PostService klassens UpdatePost metode og sender id på brugeren og pantopslaget med
            bool response = await postService.UpdatePost(5, tbl_Post);

            //UpdatePost returnere true hvis pantopslaget blev ændret succesfuld og false hvis der skete en fejl
            if (response)
            {
                IsBusy = false;
                submit.IsVisible = true;
                await DisplayAlert("Sådan!", "Pantopslag blev redigeret", "OK");
                await Navigation.PushModalAsync(new ViewPost());
            } else
            {
                await DisplayAlert("Ups", "Der opstod en fejl. Prøv igen", "OK");
            }
        }

    }

}