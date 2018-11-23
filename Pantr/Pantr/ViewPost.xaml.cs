using System;
using System.Collections.Generic;
using System.Linq;
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

        bool noPost = true;
        Post post = new Post { Id = 1, Address = "Lygten 18, 2400 Kbh", Quantity = "1 Kasse", Date="11/12/2018", Time = "10.30-12.00" };
        public ViewPost ()
		{
            BindingContext = post;
            BindingContext = noPost;
			InitializeComponent ();
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