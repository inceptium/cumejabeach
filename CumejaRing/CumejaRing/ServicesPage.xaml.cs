using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CumejaRing
{
    public partial class ServicesPage : ContentPage
    {
        public ServicesPage()
        {
            InitializeComponent();
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += TapRecognizer_TappedAsync;
            stack_chiama_cameriere.GestureRecognizers.Add(tapRecognizer);
        }

        private async void TapRecognizer_TappedAsync(object sender, EventArgs e)
        {
            //var page = new MainPage();
            var page = new SelectLocation();

            await Navigation.PushAsync(page);
        }
    }
}
