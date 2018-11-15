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
	public partial class CreatePost : ContentPage
	{
		public CreatePost ()
		{
			InitializeComponent ();
		}

        private void submit_Clicked(object sender, EventArgs e)
        {
            DateTime daten = date.Date;
            long start = startTime.Time.Ticks;
            long end = endTime.Time.Ticks;
            string bags = numberOfBags.Text;
            string boxes = numberOfBoxes.Text;
            string sacks = numberOfSacks.Text;
            string materialType = null;

            //Hvis tidsperioden er sat til mindre end time 
            if (end - start < 3600)
            {
                DisplayAlert("Fejl", "Perioden skal være en time eller mere", "Cool");
                return;
            }

            //Hvis der ikke er valgt materiale type
            if (picker.SelectedIndex > -1)
            {
                materialType = picker.Items[picker.SelectedIndex];
            }

            //Hvis der ikke er valgt en mægnde
            if (bags == null && boxes == null && sacks == null)
            {
                DisplayAlert("Fejl", "Udfyld mængde af pant", "Ok");
                return;
            } else
            {
            DisplayAlert("Test", " kasser " + boxes + " " + " poser " + bags + " sække " + sacks + " date " + 
                daten.ToString("dd/MM/yyyy") + " starttid " + start + " slut " + end + " type " + materialType, "Farvel");
            }

        }
    }
}