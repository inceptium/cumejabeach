using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CumejaBeach.xaml
{
    public partial class Impostazioni : ContentPage
    {
        public Impostazioni()
        {
            InitializeComponent();
            Bottone.Clicked+=Bottone_Clicked;
            bt_beachBar.Clicked+=Bt_BeachBar_Clicked;
        }

        async void Bt_BeachBar_Clicked(object sender, EventArgs e)
        {
          
            await Navigation.PushAsync(new BeachBar());
        }


        async void Bottone_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
