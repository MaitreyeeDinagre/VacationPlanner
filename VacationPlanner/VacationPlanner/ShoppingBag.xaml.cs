using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VacationPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingBag : ContentPage
    {
        private int tripId;
        public ShoppingBag(Trip trip)
        {
            InitializeComponent();
            tripId = trip.TripID;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListView_ShoppingBag.ItemsSource = null;
            trip_items();
        }
        private void trip_items()
        {
            var lst = from itm in App._Items where itm.TripID.Equals(tripId) select itm;
            ListView_ShoppingBag.ItemsSource = lst;
        }
        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var lst = from i in App._Items where i.ItemName.ToLower().Contains(Filter.Text.ToLower()) select i;
            ListView_ShoppingBag.ItemsSource = lst;
        }
        private async void GlobeImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GlobePage());
            if (Device.RuntimePlatform == Device.iOS)
            {
                await Launcher.OpenAsync("http://maps.apple.com/");
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the 'task chooser' so the user can pick Maps, Chrome or other mapping app
                await Launcher.OpenAsync("http://maps.google.com/");
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                await Launcher.OpenAsync("bingmaps:?rtp=adr.394 Pacific Ave San Francisco CA~adr.One Microsoft Way Redmond WA 98052");
            }
        }
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var index = new Trip();
            index.TripID = tripId;
            var itm = new ShoppingBagModel();
            itm.ItemName = itemName.Text;
            itm.Quantity = quantity.Text;
            ListView_ShoppingBag.BindingContext = itm;
            if (itm.ItemName != "" & itm.ItemName != null)
            {
                itm.TripID = tripId;
                await App._Database.SaveBagItemsAsync(itm);
                if (!App._Items.Contains(itm))
                {
                    App._Items.Add(itm);
                }
                await Navigation.PushAsync(new ShoppingBag(index));
            }
            else
            {
                await Navigation.PushAsync(new ShoppingBag(index));
            }
        }
        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            var index = new Trip();
            index.TripID = tripId;
            var itm = ListView_ShoppingBag.SelectedItem as ShoppingBagModel;
            await App._Database.DeleteBagItemsAsync(itm);
            App._Items.Remove(itm);
            await Navigation.PushAsync(new ShoppingBag(index));
        }
    }
}