using System;
using System.Collections.Generic;
using CumejaRing.jsonmodel;
using CumejaRing.jsonmodel.ring;
using InceptiumAPI.com.inceptium.core.util;
using InceptiumAPI.com.inceptium.httpclient;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CumejaRing
{
    public partial class SelectLocation : ContentPage
    {
        private INHTTPClient incClient;

        public SelectLocation()
        {


        }

        public SelectLocation(INHTTPClient incClient)
        {
            this.incClient = incClient;
            InitializeComponent();
            caricaLista();
        }

        async public void caricaLista()
        {
            var serviceArea = await incClient.SendCommand("callappcommand?command=getrecords::class=com.cumejaring.datamodel.beach.ServiceArea_Ring::order.b64="+INConver.getB64String("order by progressiveorder")+"::",true, true);
            
            if (serviceArea.StartsWith("["))
            {
                List<ServiceArea_Ring> listafeature = JsonConvert.DeserializeObject<List<ServiceArea_Ring>>(serviceArea);

                //List<Item_Location> lista = new List<Item_Location>();
                //lista.Add(new Item_Location { title = "in spiaggia", image = null, location_type = "beach" });
                //lista.Add(new Item_Location { title = "ai divani", image = null, location_type = "sofas" });
                //lista.Add(new Item_Location { title = "ai tavoli", image = null, location_type = "desk" });
                //lista.Add(new Item_Location { title = "in piscina", image = null, location_type = "pool" });
                listlocation.ItemsSource = listafeature;

            }


        }

        async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            ServiceArea_Ring item = (ServiceArea_Ring)e.Item;
            var page = new ContentPage();
            var view = new ViewNumPosto(incClient);
            page.Content = view;
            view.location = item.titleInApp;
            view.location_type = item.id_ServiceArea_Ring;

            await Navigation.PushAsync(page);

            view.focus();
        }
    }

    //public class Item_Location
    //{
    //    public string title { get; set; }
    //    public Image image { get; set; }
    //    public string location_type { get; set; }
    //}
}
