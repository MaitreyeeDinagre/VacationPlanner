using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Essentials;

//Help from this page https://docs.microsoft.com/en-us/samples/xamarin/xamarin-forms-samples/workingwithfiles/

namespace VacationPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewTrip : ContentPage
    {
        public AddNewTrip(Trip trip)
        {
            InitializeComponent();
            var editTrip = trip;
        }
        private async void SlideMenuImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SlideMenuMaster());
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
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var itm = BindingContext as Trip;
            DateTime dateNow = DateTime.Now;
            if (itm.TripName != "" & itm.TripName != null & itm.DatePicker != null & itm.DatePicker > dateNow)
            {
                await App._Database.SaveTripItemAsync(itm);
                if (!App._Trips.Contains(itm))
                {
                    App._Trips.Add(itm);
                }
                await Navigation.PushAsync(new AllTrips());
            }
            else
            {
                await DisplayAlert("Alert", "Please name your trip and enter valid start date!", "Ok");
            }
        }
    }
}