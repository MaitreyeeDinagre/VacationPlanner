using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


//Help from this page https://docs.microsoft.com/en-us/samples/xamarin/xamarin-forms-samples/workingwithfiles/

namespace VacationPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewEvent : TabbedPage
    {
        public int tripId;
        public Event events;
        public AddNewEvent(int tripid, Event editEvent)
        {
            InitializeComponent();
            tripId = tripid;
            events = new Event();
            var edit = editEvent;
        }
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            var eventTrip  = BindingContext as Event;
            if (eventTrip.EventName != "" & eventTrip.EventName != null)
            {
                eventTrip.TripID = tripId;
                eventTrip.TravelMode = events.TravelMode;
                await App._Database.SaveEventItemAsync(eventTrip);
                if (!App._Events.Contains(eventTrip))
                {
                    App._Events.Add(eventTrip);
                }
                var trip = new Trip();
                trip.TripID = eventTrip.TripID;
                await Navigation.PushAsync(new TripDetails(trip));
            }
            else
            {
                await DisplayAlert("Alert", "Please name your day!", "Ok");
            }
        }
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                events.TravelMode  = picker.Items[selectedIndex];
            }

        }
        private void AddStayButton_Clicked(object sender, EventArgs e)
        {
            CurrentPage = Children[1];
        }
    }
}