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

        private async void ReservationsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView reservedPosts = sender as ListView;
            PostViewModelCopy selectedPost = reservedPosts == null ? null : reservedPosts.SelectedItem as PostViewModelCopy;

            var cancelled = await DisplayAlert("Afmeld reservation", selectedPost.Quantity + "\n" +
                                                                 selectedPost.Address + "\n" +
                                                               selectedPost.Date + "\n" +
                                                               selectedPost.PeriodForPickup, "Afmeld", "Tilbage");
            if (cancelled)
            {
                TransactionService transactionService = new TransactionService();
                transactionService.CancelReservation(selectedPost);
            }
        }
    }
}