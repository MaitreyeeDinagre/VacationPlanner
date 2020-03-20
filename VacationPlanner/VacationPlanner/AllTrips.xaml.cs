using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VacationPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllTrips : ContentPage
    {
        public AllTrips()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListView_Trips.ItemsSource = App._Trips;
        }
        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var lst = from t in App._Trips where t.TripName.ToLower().Contains(Filter.Text.ToLower()) select t;
            ListView_Trips.ItemsSource = lst;
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
        private async void AddTripImageButton_Clicked(object sender, EventArgs e)
        {
            var trip = new Trip();
            await Navigation.PushAsync(new AddTrip(trip)
            {
                BindingContext = new Trip()
            });
        }
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                DateTime dateNow = DateTime.Now; //Today's date in dateNow
                if (selectedIndex==0) //Select all upcoming trips
                {
                    var lst = from t in App._Trips where t.DatePicker > dateNow & t.IsCancel == false select t;
                    ListView_Trips.ItemsSource = lst;
                }
                else if (selectedIndex == 1) //Select all the completed trips
                {
                    var lst = from t in App._Trips where t.DatePicker < dateNow & t.IsCancel == false select t;
                    ListView_Trips.ItemsSource = lst;
                }
                else if (selectedIndex ==2) //All cancelled trips
                {
                    var lst = from t in App._Trips where t.IsCancel==true select t;
                    ListView_Trips.ItemsSource = lst;
                }
                else //All trips
                {
                    var lst = from t in App._Trips select t;
                    ListView_Trips.ItemsSource = lst;
                }
                

            }
        }
        private async void ListView_Trips_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new TripDetails(e.Item as Trip)
                {
                    BindingContext = e.Item as Trip
                });
            }
        }
    }
}