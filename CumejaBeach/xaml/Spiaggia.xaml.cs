using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Connectivity;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace CumejaBeach.xaml
{
    public partial class Spiaggia : ContentPage
    {
        CancellationTokenSource tokenSource;
        String currentDate = "";
        static Spiaggia me;

        public Spiaggia()
        {
            tokenSource = new CancellationTokenSource();
            currentDate = System.DateTime.Now.Day.ToString() + "/" + System.DateTime.Now.Month.ToString() + "/" + System.DateTime.Now.Year.ToString();
            InitializeComponent();
            Indicator1.IsRunning = true;
            //disegnaOmbrelloni();
            CaricaListaOmbrelloni();

            Console.WriteLine("Data -> " + currentDate);
            me = this;

        }



        private void disegnaOmbrelloni()
        {
            var Omb1 = new Label { Text = "Top Left" };
            var Omb2 = new Label { Text = "Top Right" };
            var Omb3 = new Label { Text = "Bottom Left" };
            var Omb4 = new Label { Text = "Bottom Right" };

            GrigliaOmbrelloni.Children.Add(Omb1, 0, 0);
            GrigliaOmbrelloni.Children.Add(Omb2, 0, 1);
            GrigliaOmbrelloni.Children.Add(Omb3, 1, 0);
            GrigliaOmbrelloni.Children.Add(Omb4, 1, 1);


            //var img1=new Image {}


        }

        public void Handle_Clicked(object sender, System.EventArgs e)
        {
            CaricaListaOmbrelloni();
        }

        public async void CaricaListaOmbrelloni()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Indicator1.IsRunning = true;
                lb_monitor.Text = "accesso al server......";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://192.168.101.112/wspa/getpuntiaddebito.aspx?data=" + currentDate);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(7);
                try
                {
                    HttpResponseMessage reposponse = await client.SendAsync(request, tokenSource.Token);

                    if (reposponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = reposponse.Content;
                        var risposta = await content.ReadAsStringAsync();
                        //Console.WriteLine(risposta);
                        var ombrellone_List = JsonConvert.DeserializeObject<List<ItemOmbrelloni>>(risposta);
                        GrigliaOmbrelloni.Children.Clear();
                        int xx = 0;
                        int yy = 0;

                        foreach (ItemOmbrelloni ombrellone in ombrellone_List)
                        {
                            if (!ombrellone.Codice.StartsWith("L"))
                            {
                                Console.WriteLine(ombrellone.Codice);
                                //var stack = new StackLayout();

                                var stack = new StackOmbrellone();
                                stack.itemOmbrelloni = ombrellone;
                                stack.Disegna();
                                stack.evt_tap = new eventotap();


                                //stack.HeightRequest = 250;
                                //stack.WidthRequest = 120;
                                //var omb = new Label { Text = ombrellone.Codice };
                                //var ret = new BoxView();
                                //var cellaimm = getImmagineOmbrellone(ombrellone);
                                //ret.HeightRequest = 35;
                                //ret.WidthRequest = 100;
                                //ret.BackgroundColor = Color.FromRgb(240, 240, 255);
                                //ret.HorizontalOptions = LayoutOptions.CenterAndExpand;
                                //cellaimm.HorizontalOptions = LayoutOptions.Center;
                                ////cellaimm.VerticalOptions = LayoutOptions.Center;

                                ////var pos = new Label { Text = ombrellone.PosX.ToString() + "-" + ombrellone.Posy.ToString() };
                                //omb.FontSize = 20;
                                //omb.FontAttributes = FontAttributes.Bold;
                                //// pos.FontSize = 10;
                                //omb.VerticalOptions = LayoutOptions.End;
                                //omb.HorizontalOptions = LayoutOptions.CenterAndExpand;
                                //stack.Children.Add(cellaimm);
                                //stack.Children.Add(ret);
                                //stack.Children.Add(omb);
                                ////stack.Children.Add(pos);
                                ////stack.BackgroundColor = Color.Aqua;
                                //stack.VerticalOptions = LayoutOptions.CenterAndExpand;
                                //stack.HorizontalOptions = LayoutOptions.CenterAndExpand;

                                GrigliaOmbrelloni.Children.Add(stack, xx, yy);
                                xx++;
                                if (xx == 8)
                                {
                                    xx++; ;
                                }
                                if (xx == 21)
                                {
                                    yy++;
                                    xx = 0;
                                }

                            }
                            //scroolOmbrelloni.WidthRequest = Content.Width;
                        }

                        lb_monitor.Text = "";
                        //await DisplayAlert("Messaggio 2",risposta.ToString(), "OK");

                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    Indicator1.IsRunning = false;
                    lb_monitor.Text = "Controllo http";
                    await DisplayAlert("messaggio", "problemi di connessione al servert", "OK");
                }

            }
            else
            {
                await DisplayAlert("messaggio", "Connettività assente", "OK");
            }
            Indicator1.IsRunning = false;

        }

        class eventotap : EventoTapOmbrellone
        {
            public void OnTapOmbrellone(ItemOmbrelloni item)
            {
                Spiaggia.me.DisplayAlert("Info Ombrellone", item.Info, "OK");
            }
        }


    }


}
