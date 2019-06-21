using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Connectivity;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using CumejaBeach.XAML.pos;
using CumejaBeach.XAML.ring;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaBeach.xaml
{
    public partial class Spiaggia : ContentPage
    {
        CancellationTokenSource tokenSource;
        string currentDate = "";
        static Spiaggia me;
        EventoTap eventotap;
        volatile bool caricoOmbrelloniInCorso = false;

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
            eventotap = new EventoTap();

            startDatePicker.Unfocused += StartDatePicker_Unfocused;

            //btInfo_ok.Clicked+=BtInfo_Ok_Clicked;
            //App.getInstance().LeggiRingAsync(true);

        }

        

        void BtInfo_Ok_Clicked(object sender, EventArgs e)
        {
            //overlay.IsVisible = false;
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {



        }

        void StartDatePicker_Unfocused(object sender, FocusEventArgs e)

        {
            currentDate = startDatePicker.Date.Day.ToString() + "/" + startDatePicker.Date.Month.ToString() + "/" + startDatePicker.Date.Year.ToString();
            CaricaListaOmbrelloni();
        }


        


        public void Handle_Clicked(object sender, System.EventArgs e)
        {

            currentDate = startDatePicker.Date.Day.ToString() + "/" + startDatePicker.Date.Month.ToString() + "/" + startDatePicker.Date.Year.ToString();
            CaricaListaOmbrelloni();

        }

        public void Handle_Ring_Clicked(object sender, System.EventArgs e)
        {

            chiamaCumejaRing();

        }

        
        

        public async void CaricaListaOmbrelloni()
        {
            if (caricoOmbrelloniInCorso)
            {
                return;

            }

            caricoOmbrelloniInCorso = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                Indicator1.IsRunning = true;
                lb_monitor.Text = "accesso al server......";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://" + App.connectionPref.ServerDNRIP + "/wspa/getpuntiaddebito.aspx?data=" + currentDate);
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
                        Console.WriteLine(risposta);
                        var ombrellone_List = JsonConvert.DeserializeObject<List<ItemOmbrelloni>>(risposta);
                        GrigliaOmbrelloni.Children.Clear();
                        int xx = 0;
                        int yy = 0;

                        foreach (ItemOmbrelloni ombrellone in ombrellone_List)
                        {
                            if (!ombrellone.Codice.StartsWith("L"))
                            {
                                //Console.WriteLine(ombrellone.Codice);
                                //var stack = new StackLayout();

                                var stack = new StackOmbrellone();
                                stack.itemOmbrelloni = ombrellone;
                                stack.Disegna();

                                stack.evt_tap = eventotap;



                                GrigliaOmbrelloni.Children.Add(stack, xx, yy);
                                xx++;
                                if (xx == 8)
                                {
                                    xx++; ;
                                }
                                if (xx == 20)
                                {
                                    yy++;
                                    xx = 0;
                                }

                            }
                            //scroolOmbrelloni.WidthRequest = Content.Width;
                        }

                        lb_monitor.Text = "";
                        Console.WriteLine("tot elementi in griglia -> " + GrigliaOmbrelloni.Children.Count.ToString());
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
            caricoOmbrelloniInCorso = false;

        }

        public async void chiamaPos(String titolo)
        {
            var pos = new PosForm();
            pos.setTitolo(titolo);
            await Navigation.PushAsync(pos);
        }

        public async void chiamaCumejaRing()
        {

            var ring = new CumejaRingList();

            await Navigation.PushAsync(ring);
        }

        class EventoTap : EventoTapOmbrellone
        {
            public void OnTapOmbrellone(ItemOmbrelloni item)
            {
                if (Spiaggia.me.cb_pos.IsChecked)
                {
                    if (item.Stato == 0)
                    {
                        Spiaggia.me.chiamaPos("Consegna Ordine all'Ombrellone n: " + item.Codice);
                    }
                    else
                    {
                        Spiaggia.me.DisplayAlert("Attenzione !!!", "Questo Posto non è occupato", "OK");
                    }
                }
                else
                {
                    Spiaggia.me.DisplayAlert("Info Ombrellone " + item.Codice, item.Info, "OK");
                }



                //Spiaggia.me.DisplayAlert("Info Ombrellone " + item.Codice, item.Info, "OK");

                //Spiaggia.me.InfoLabel_titolo.Text = "Info Ombrellone " + item.Codice;
                //Spiaggia.me.InfoLabel_Content.Text = item.Info;
                //Spiaggia.me.overlay.IsVisible = true;
            }


        }


    }


}
