using Pantr.DB;
using Pantr.Models;
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
	public partial class GetPost : ContentPage
	{
		public GetPost ()
        { 
        }

        PostViewModel post;

        protected override async void OnAppearing()
        {
            PostViewModel post = await PostService.GetPost();
            BindingContext = post;
            this.post = post;
            InitializeComponent();
        }
    
        private void Button_Reserver(object sender, EventArgs e)
        {
            

        }
    }
}