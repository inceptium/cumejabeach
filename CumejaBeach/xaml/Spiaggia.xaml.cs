using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Connectivity;
using System.Net.Http;
using System.Threading;

namespace CumejaBeach.xaml
{
    public partial class Spiaggia : ContentPage
    {
        CancellationTokenSource tokenSource;

        public Spiaggia()
        {
            tokenSource = new CancellationTokenSource();
            InitializeComponent();
            Indicator1.IsRunning = true;
            disegnaOmbrelloni();
            CaricaListaOmbrelloni();
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

        public async void CaricaListaOmbrelloni()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Indicator1.IsRunning = true;
                lb_monitor.Text = "accesso al server......";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://192.168.101.112/wspa/getpuntiaddebito.aspx?data=26/07/2018");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(3);
                try
                {
                    HttpResponseMessage reposponse = await client.SendAsync(request, tokenSource.Token);
                    Indicator1.IsRunning = false;
                    if (reposponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = reposponse.Content;
                        var risposta = await content.ReadAsStringAsync();
                        Console.WriteLine(risposta);
                        lb_monitor.Text = "";
                        //await DisplayAlert("Messaggio 2",risposta.ToString(), "OK");

                    }
                }
                catch (Exception exception)
                {
                    //Console.WriteLine(exception.ToString());
                    Indicator1.IsRunning = false;
                    lb_monitor.Text = "Controllo http";
                    await DisplayAlert("messaggio", "problemi di connessione al servert", "OK");
                }

            }
            else
            {
                await DisplayAlert("messaggio", "Connettività assente", "OK");
            }

        }
    }
}
