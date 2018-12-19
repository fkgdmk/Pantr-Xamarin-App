using Pantr.DB;
using Pantr.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewReservations : ContentPage
	{
        //Instantierer et object af typen observablecollection som bruges af listviewet
        //ObservableCollection har den fordel over lists at den lytter for ændringer således at de
        //vises eller fjernes fra viewet  efterhånden som de bliver tilføjet eller fjernet fra listen

        //ObservableCollection<PostViewModelCopy> reservations = new ObservableCollection<PostViewModelCopy>();
        public ObservableCollection<PostViewModelCopy> reservations { get; set; }

        public ViewReservations ()
		{
			InitializeComponent ();
            reservations = new ObservableCollection<PostViewModelCopy>();

            //binder listviewet på reservations
            ReservationsView.ItemsSource = reservations;
        }

        //OnAppearing er en metode som kaldes så viewet bliver kaldt. Så snart man skifter til denne side
        //bliver denne metode kaldt som henter alle ens reservationer
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // Userid hardcodes til 1 for at undgå at skulle logge ind for at se om det virker
            //if (!Application.Current.Properties.ContainsKey("ID"))
            //{
            //    await DisplayAlert("Hov du!", "Der er vidst sket en fejl\nLuk appen ned og log ind igen", "OK");
            //}
            //var userId = (int)Application.Current.Properties["ID"];
            var userId = 1;

            TransactionService transactionService = new TransactionService();

            //metode som håndterer db kald og returnerer liste af ens egne reservationer
            var userReservations = await transactionService.GetOwnReservations(userId);

            //Tilføjes til listen som viewet listview er bundet til
            foreach(var item in userReservations)
            {
                reservations.Add(item);
            }

        }

        //Et event som bliver kaldt når man trykker på et listitem
        private async void ReservationsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //caster senderen til et listview
            ListView reservedPosts = sender as ListView;

            //ternary operator som sætter en postviewmodelcopy til null hvis listen er null, ellers sætter den selectedpost til
            //det samme som det listitem fra viewet
            PostViewModelCopy selectedPost = reservedPosts == null ? null : reservedPosts.SelectedItem as PostViewModelCopy;

            //til sidst skiftes der side til viewPost som viser dne valgte post
            await Navigation.PushAsync(new ViewPost(selectedPost, 1));
        }
    }
}