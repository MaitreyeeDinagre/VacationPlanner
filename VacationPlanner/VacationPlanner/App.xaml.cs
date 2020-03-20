using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace VacationPlanner
{
    public partial class App : Application
    {
        public static List<Trip> _Trips;
        public static List<Event> _Events;
        public static List<ShoppingBagModel> _Items;
        static MyDatabase database;
        public static MyDatabase _Database
        {
            get
            {
                if (database == null)
                {
                    database = new MyDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("VacationPlaDB.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override async void OnStart()
        {
            _Trips = await _Database.GetTripItemsAsync();
            _Events = await _Database.GetEventItemsAsync();
            _Items = await _Database.GetBagItemsAsync();
           
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        //internal static object BindingContextProperty()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
