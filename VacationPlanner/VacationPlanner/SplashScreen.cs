using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VacationPlanner
{
    class SplashScreen : ContentPage
    {
        Image splashImage;
        Label label;
        ActivityIndicator activityIndicator = new ActivityIndicator();

        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            label = new Label
            {
               Text = "        Welcome!",
               FontSize = 50
            };
            splashImage = new Image
            {
                Source = "vacationplanning.jpg",
                WidthRequest = 440,
                HeightRequest = 440
            };

            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(label);
            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#a6bcc9");
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashImage.ScaleTo(1, 1000); //Time Consuming Process... Such as Initialization
            await splashImage.ScaleTo(0.5, 500, Easing.SpringOut);
            await splashImage.ScaleTo(1, 1000, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new AllTrips());
        }
    }
}
