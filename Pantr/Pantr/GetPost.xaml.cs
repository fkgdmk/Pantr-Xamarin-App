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
            InitializeComponent();
            BindingContext = tester;
        }

    Post tester = new Post { Id = 1, Address = "testvej 42", Quantity = 5, Time = "TT:TT:test" };

    public class Post
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        public string Time { get; set; }
    }
    
    private void Button_Reserver(object sender, EventArgs e)
    {
        

    }
}
}