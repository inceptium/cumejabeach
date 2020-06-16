using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaRing
{
    public partial class ViewNumPosto : ContentView
    {
        INHTTPClient inClient;
        public string location { get; set; }
        public string location_type { get; set; }

        public ViewNumPosto(INHTTPClient inClient)
        {
            InitializeComponent();
            this.inClient = inClient;

        }

        public void focus()
        {
            entry_posto.Focus();
        }
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {

            indicator.IsRunning = true;
            await connettiAsync();
            indicator.IsRunning = false;
            stack_status_monitor.IsVisible = true;
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (entry_posto.Text.Length > 0)
            {
                bt_chiama_cameriere.IsEnabled = true;
            }
            else
            {
                bt_chiama_cameriere.IsEnabled = false;

            }
        }

        public async Task connettiAsync()
        {
            string incTaskRest = "";
            try
            {

                Console.WriteLine("Leggo -> : new session");


                var sessione = await inClient.getNewWebSessionAsync();


                incTaskRest = await inClient.SendCommand("load_app?classapp=com.cumejaring.IncAppCumejaRing::", true, false);



                if (incTaskRest.StartsWith("TASK"))
                {
                    var taskwait = await inClient.WaitTask(incTaskRest, 3000);
                    //var rest = await inClient.SendCommand("callappcommand?command=executemethod::class=com.cumejaring.datamodel.beach.PostoRicreativo_Ring::method=ring::code_location=" + entry_posto.Text + "::ringfrom=device::type=" + location_type + "::", false, false);
                    var rest = await inClient.executeMethod("com.cumejaring.datamodel.beach.PostoRicreativo_Ring", "ring",
                        "code_location=" + entry_posto.Text + "::ringfrom=device::type=" + location_type);

                    if (rest == "RING_OK")
                    {
                        label_status.Text = "la sua richiesta è stata correttamente inviata!!! ";
                        blocco_domanda.IsVisible = false;
                    }
                    else
                    {
                        label_status.Text = "Controlla il numero di posto !!! (" + location + ")";
                    }


                }
                else
                {
                    label_status.Text = "Applicazione non disponibile per il momento";
                }


            }
            catch
            {
                label_status.Text = "Ci sono probemi di connessione !!!";

            }



            //var message = new TickedMessage
            //{
            //    Message = i.ToString()
            //};

            //Device.BeginInvokeOnMainThread(() => {
            //    MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
            //});
        }


    }
}


