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
        ObservableCollection<PostViewModelCopy> reservations = new ObservableCollection<PostViewModelCopy>();
		public ViewReservations ()
		{
			InitializeComponent ();
            ReservationsView.ItemsSource = reservations;
            //reservations.Add(new PostViewModelCopy() { Quantity="1 sække", Date = "11-12-13", Address = "Ligeher", PeriodForPickup = "kl 13-16" });
            //reservations.Add(new PostViewModelCopy() { Quantity="2 poser, 1 kasser", Date = "41-34-13", Address = "Ligeder", PeriodForPickup = "kl 12-17" });
            //reservations.Add(new PostViewModelCopy() { Quantity="2 poser, 2 kasser, 2 sække", Date = "54-23-41", Address = "Ligehvor", PeriodForPickup = "kl 14-18" });
            //reservations.Add(new PostViewModelCopy() { Quantity="1 pose", Date = "31-12-34", Address = "Ligenu", PeriodForPickup = "kl 15-17" });

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //if (!Application.Current.Properties.ContainsKey("ID"))
            //{
            //    await DisplayAlert("Hov du!", "Der er vidst sket en fejl\nLuk appen ned og log ind igen", "OK");
            //}
            //var userId = (int)Application.Current.Properties["ID"];
            var userId = 1;

            TransactionService transactionService = new TransactionService();
            var userReservations = await transactionService.GetOwnReservations(userId);
            foreach(var item in userReservations)
            {
                reservations.Add(item);
            }

        }
    }
}