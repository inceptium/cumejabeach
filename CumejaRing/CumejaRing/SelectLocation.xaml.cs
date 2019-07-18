using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CumejaRing
{
    public partial class SelectLocation : ContentPage
    {
        public SelectLocation()
        {
            InitializeComponent();
            caricaLista();

        }

        public void caricaLista()
        {
            List<Item_Location> lista = new List<Item_Location>();
            lista.Add(new Item_Location { title = "in spiaggia", image = null });
            lista.Add(new Item_Location { title = "ai divani", image = null });
            lista.Add(new Item_Location { title = "ai tavoli", image = null });
            lista.Add(new Item_Location { title = "in piscina", image = null });
            listlocation.ItemsSource = lista;

            

        }



        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var page = new ContentPage();
            var view = new ViewNumPosto();
            page.Content = view;



            await Navigation.PushAsync(page);
            view.focus();
        }
    }

    public class Item_Location
    {

        public string title { get; set; }
        public Image image { get; set; }
    }
}
