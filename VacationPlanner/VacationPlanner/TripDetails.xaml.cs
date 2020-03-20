using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VacationPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripDetails : TabbedPage
    {
        public int tripId;
        public TripDetails(Trip trip)
        {
            InitializeComponent();
            tripId = trip.TripID;         
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();     
            ListView_Details.ItemsSource = null;
            Trip_events();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ListView_Details.ItemsSource = null;
            Trip_events();
        }
        private void Trip_events()
        {
            var lst = from eve in App._Events where eve.TripID.Equals(tripId) select eve;
            ListView_Details.ItemsSource = lst;            
        }
        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var lst = from t in App._Events where t.EventName.ToLower().Contains(Filter.Text.ToLower()) select t;
            ListView_Details.ItemsSource = lst;
        }
        private async void AddTripEventImageButton_Clicked(object sender, EventArgs e)
        {
            var edit = new Event();
            await Navigation.PushAsync(new AddNewEvent(tripId, edit)
            {
                BindingContext = new Event()
            });
        }
        private async void BagImageButton_Clicked(object sender, EventArgs e)
        {
            var index = BindingContext as Trip;
            await Navigation.PushAsync(new ShoppingBag(index));
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
                await Launcher.OpenAsync("http://maps.google.com/");
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                await Launcher.OpenAsync("bingmaps:?rtp=adr.394 Pacific Ave San Francisco CA~adr.One Microsoft Way Redmond WA 98052");
            }
        }
        private async void EditEventButton_Clicked(object sender, EventArgs e)
        {
            var index = BindingContext as Trip;
            var itm = ListView_Details.SelectedItem as Event;
            if (index.IsCancel == false)
            {
                await Navigation.PushAsync(new AddNewEvent(tripId, itm)
                {
                    BindingContext = itm
                });
            }
            else
            {
                await DisplayAlert("Alert", "Sorry! The trip is already cancelled!", "Ok");
            }
        }
        private async void EditTripButton_Clicked(object sender, EventArgs e)
        {
            var itm = BindingContext as Trip;
            if(itm.IsCancel == false)
            {
                await Navigation.PushAsync(new AddTrip(itm)
                {
                    BindingContext = itm
                });
            }
            else
            {
                await DisplayAlert("Alert", "Sorry! The trip is already cancelled!", "Ok");
            }
        }
        private async void DeleteEventButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete this day", "Are you sure?", "Yes", "No");
            if (answer == true)
            {
                var tripId = new Trip();
                var itm = ListView_Details.SelectedItem as Event;
                await App._Database.DeleteEventItemAsync(itm);
                App._Events.Remove(itm);
                await Navigation.PushAsync(new TripDetails(tripId));
            }
            else
            {
                var tripd = new Trip();
                tripd.TripID = tripId;
                await Navigation.PushAsync(new TripDetails(tripd));
                ListView_Details.ItemsSource = null;
                Trip_events();
            }
        }
        private async void DeleteTripButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Cancel whole Trip", "Are you sure?", "Yes", "No");
            if (answer == true) 
            {   
                var itm = BindingContext as Trip;
                itm.IsCancel = true;
                await App._Database.SaveTripItemAsync(itm);
                if (!App._Trips.Contains(itm))
                {
                    App._Trips.Add(itm);
                }
                await Navigation.PushAsync(new AllTrips());
            }
            else
            {
                var tripd = new Trip();
                tripd.TripID = tripId;
                await Navigation.PushAsync(new TripDetails(tripd));
            }
        }
        private void ListView_Details_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Children[1].BindingContext = (sender as ListView).SelectedItem as Event;
            CurrentPage = Children[1];
        }
    }
}