using System;
using Xamarin.Forms;
using CumejaBeach.utility.controller;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaBeach.XAML.ring
{
    public class StackItemRing : Frame
    {
        public ItemPostoRicreativo postoRicreativo { get; set; }

        public StackItemRing()
        {

        }

        public void Disegna()
        {
            this.HasShadow = false;


            this.BorderColor = Color.Silver;


            StackLayout st = new StackLayout();
            st.Orientation = StackOrientation.Horizontal;
            st.HorizontalOptions = LayoutOptions.Center;
            var omb = new Label { Text = postoRicreativo.NameLocation+"  " };
            //var ret = new BoxView();
            ////var cellaimm = getImmagineOmbrellone(itemOmbrelloni);
            //ret.HeightRequest = 35;
            //ret.WidthRequest = 100;
            //ret.BackgroundColor = Color.FromRgb(240, 240, 255);
            //ret.HorizontalOptions = LayoutOptions.CenterAndExpand;
            //cellaimm.HorizontalOptions = LayoutOptions.Center;
            //cellaimm.VerticalOptions = LayoutOptions.Center;

            //var pos = new Label { Text = ombrellone.PosX.ToString() + "-" + ombrellone.Posy.ToString() };
            omb.FontSize = 20;
            omb.FontAttributes = FontAttributes.Bold;
            omb.VerticalTextAlignment = TextAlignment.Center;
            omb.HorizontalTextAlignment = TextAlignment.Start;
            omb.WidthRequest = 200;
          
            // pos.FontSize = 10;
            //omb.VerticalOptions = LayoutOptions.End;
            //omb.HorizontalOptions = LayoutOptions.CenterAndExpand;

            //this.Children.Add(cellaimm);
            //this.Children.Add(ret);
            Button bt = new Button();
            bt.Clicked += onButtonClicked;

            bt.BorderWidth = 2;
            bt.HeightRequest = 40;
            bt.WidthRequest = 100;
            bt.VerticalOptions = LayoutOptions.End;


            bt.Text = "Ricevuto";
            st.Children.Add(omb);
            st.Children.Add(bt);
            this.Content = st;
            //stack.Children.Add(pos);
            //stack.BackgroundColor = Color.Aqua;

        }

        async void onButtonClicked(object sender, EventArgs args)
        {
            INHTTPClient client = new INHTTPClient(App.inCliConfig, App.getInstance().inClient.CurrentWebSession);
            CumejaRingList.me.indicator.IsRunning = true;
            var rest =  await client.SendCommand("callappcommand?command=executemethod::class=com.cumejaring.datamodel.beach.crPostoRicreativo::method=ring_update::code_location=" + postoRicreativo.code_location + "::status=RING_READ::type="+postoRicreativo.type+"::",true,false);
            if (rest != null && rest.Equals("RING_UPDATE"))
            {
                await CumejaRingList.me.DisplayAlert("Cumeja Beach", "Risposta Inviata !!!", "OK");
                this.IsVisible = false;
            }
            else
            {
                await CumejaRingList.me.DisplayAlert("Cumeja Beach", "Problemi di connessione !!!", "OK");
            }
            CumejaRingList.me.indicator.IsRunning = false;

        }

       
    }
}
