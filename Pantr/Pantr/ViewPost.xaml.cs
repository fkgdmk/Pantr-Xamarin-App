using Newtonsoft.Json;
using Pantr.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPost : ContentPage
	{
        public class Post
        {
            public int Id { get; set; }
            public string Address { get; set; }
            public string Quantity { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
        }

        Post post = new Post { Id = 1, Address = "Lygten 18, 2400 Kbh", Quantity = "1 Kasse", Date="11/12/2018", Time = "10.30-12.00" };
        public ViewPost ()
		{

            //PostService service = new PostService();

            //var test = service.GetUsersPost();

            GetUsersPost();
			InitializeComponent ();

		}

        public async void GetUsersPost()
        {
            //PostViewModel post = null;

            HttpClient client = new HttpClient();

            var uri = new Uri(string.Format("http://192.168.1.173:45455/api/post/getuserspost/1"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var post = JsonConvert.DeserializeObject<PostViewModel.PostView>(content);

                post.StartTime = FormatTime(post.StartTime);
                post.EndTime = FormatTime(post.EndTime);
                //string date = post.Date.ToString("dd/MM/yyyy");
                //DateTime formatedDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //post.Date = formatedDate;

                BindingContext = post;
            }
        }

        public string FormatTime(string time)
        {
            string[] strArr = time.Split(':');
            return strArr[0] + ":" + strArr[1]; 
        }

        void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                DisplayAlert("test", "test", "test", "test");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void edit (object sender, EventArgs e)
        {
            var editPost = new EditPost();

            await Navigation.PushModalAsync(editPost);

            //DisplayAlert("test", "test", "test", "test");
        }

        private void submit_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("test", "", "test");

        }
    }
}