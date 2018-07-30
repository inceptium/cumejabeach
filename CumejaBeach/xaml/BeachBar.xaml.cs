using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CumejaBeach.xaml
{
    public partial class BeachBar : ContentPage
    {
        public BeachBar()
        {
            InitializeComponent();
            bt_vai_spiaggia.Clicked+=Bt_Vai_Spiaggia_Clicked;
        }

        async void Bt_Vai_Spiaggia_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

    }
}
