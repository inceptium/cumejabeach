using System;
using Xamarin.Forms;
using System.Collections.Generic;

using Plugin.Connectivity;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
namespace CumejaBeach
{
    public class StackOmbrellone : StackLayout
    {
        public ItemOmbrelloni itemOmbrelloni { get; set; }

        public EventoTapOmbrellone evt_tap { get; set; }

        public string CodiceOmbrellone { get; set; }



        public StackOmbrellone()
        {
            
            var tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped+=TapRecognizer_Tapped;
           
            this.GestureRecognizers.Add(tapRecognizer);



            //this.GestureRecognizers.Add(new TapGestureRecognizer()
            //{
            //    Command = new Command(() => { eventoTap(); })
            //});




        }

        //private void eventoTap()
        //{
        //    //DisplayAlert("messaggio", itemOmbrelloni.Info, "OK");
        //    if (evt_tap != null)
        //    {
        //        evt_tap.OnTapOmbrellone(itemOmbrelloni);
        //    }
        //}

        void TapRecognizer_Tapped(object sender, EventArgs e)
        {
            if (evt_tap != null)
            {
                evt_tap.OnTapOmbrellone(itemOmbrelloni);
            }
        }


        public void Disegna()
        {

            this.HeightRequest = 250;
            this.WidthRequest = 120;
            var omb = new Label { Text = itemOmbrelloni.Codice };
            var ret = new BoxView();
            var cellaimm = getImmagineOmbrellone(itemOmbrelloni);
            ret.HeightRequest = 35;
            ret.WidthRequest = 100;
            ret.BackgroundColor = Color.FromRgb(240, 240, 255);
            ret.HorizontalOptions = LayoutOptions.CenterAndExpand;
            cellaimm.HorizontalOptions = LayoutOptions.Center;
            //cellaimm.VerticalOptions = LayoutOptions.Center;

            //var pos = new Label { Text = ombrellone.PosX.ToString() + "-" + ombrellone.Posy.ToString() };
            omb.FontSize = 35;
            omb.FontAttributes = FontAttributes.Bold;
            // pos.FontSize = 10;
            omb.VerticalOptions = LayoutOptions.End;
            omb.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.Children.Add(cellaimm);
            this.Children.Add(ret);
            this.Children.Add(omb);
            //stack.Children.Add(pos);
            //stack.BackgroundColor = Color.Aqua;
            this.VerticalOptions = LayoutOptions.CenterAndExpand;
            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
        }

        private Image getImmagineOmbrellone(ItemOmbrelloni omb)
        {
            Image rest = null;
            switch (omb.Stato)
            {
                case 0:
                    rest = new Image { Source = "OmbRossoApertoOccupato.png" };
                    break;
                case 1:
                    rest = new Image { Source = "OmbVerdeChiusoLibero.png" };
                    break;
                case 2:
                    rest = new Image { Source = "OmbGialloApertoPartenza.png" };
                    break;
                case 3:
                    rest = new Image { Source = "OmbViolaChiusoPrenotato.png" };
                    break;
                case 4:
                    rest = new Image { Source = "OmbVerdeChiusoLibero.png" };
                    break;

            }
            return rest;
        }
    }
}
