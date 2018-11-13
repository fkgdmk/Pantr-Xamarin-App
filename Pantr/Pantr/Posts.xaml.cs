using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Posts : ContentPage
    {
        public Posts()
        {
          //  foreach (Post post in GetData())
           // {
            //    System.Diagnostics.Debug.WriteLine(post.Address);
            //}

            Console.WriteLine("Test?");
            InitializeComponent();
            Title = "Opslag";
            Padding = new Thickness(0, 20, 0, 0);
            var listView = new ListView();
            listView.ItemsSource = DataList;
            Content = listView;

   
       
        }

        #region Datalist
        ObservableCollection<Post> DataList = new ObservableCollection<Post> {

            new Post{ Address="Stærevej 75", Quantity="En masse", Time="Søndag klokken 16" },
            new Post{ Address="Hærevej 45", Quantity="En smule", Time="Onsdag klokken 16" },
            new Post{ Address="Mærevej 25", Quantity="En mellem", Time="Lørdag klokken 16" },
            new Post{ Address="Mærevej 25", Quantity="En mellem", Time="Lørdag klokken 16" }
        };
        #endregion

        public class Post
        {
            public string Address { get; set; }
            public string Quantity { get; set; }
            public string Time { get; set; }
        }
	}
}