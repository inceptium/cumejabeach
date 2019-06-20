using System;

using System.Threading.Tasks;
using CumejaBeach.utility.pref;
using CumejaBeach.utility.pref.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CumejaBeach.utility.appstorage;
using Newtonsoft.Json;
using InceptiumAPI.com.inceptium.httpclient;
using CumejaBeach.utility.http;
using System.Threading;

namespace CumejaBeach.utility.controller
{
    public class ControllerRing
    {
        static volatile bool leggiRingRun = false;
        private INHTTPClient inClient;
        private bool newsession = true;

        public ControllerRing(INHTTPClient client)
        {
            this.inClient = client;

        }


        public async Task LeggiRingAsync(CancellationToken token)
        {



            await Task.Run(async () =>
            {
                for (long i = 0; i < long.MaxValue; i++)
                {
                    token.ThrowIfCancellationRequested();
                    try
                    {

                        if (!inClient.connected)
                        {
                            var sessione = await inClient.getNewWebSessionAsync();
                            Console.WriteLine("Leggo -> Ring: new session");
                            newsession = false;
                        }

                        var app = await inClient.SendCommand("load_app?classapp=com.cumejaring.AppCumejaRing::", false, false);
                        if (app != null)
                        {
                            var rest = await inClient.SendCommand("callappcommand?command=executemethod::class=com.cumejaring.datamodel.beach.crPostoRicreativo::method=ring_list::", false, false);
                            if (rest != null)
                            {
                                Console.WriteLine("Leggo -> Ring");
                            }


                        }
                        else
                        {
                            Console.WriteLine("inceptium app Cumeja Ring not load !!!!");
                        }

                    }
                    catch
                    {
                        Console.WriteLine("connessione server inceptium assente");
                    }

                    await Task.Delay(5000);






                    //var message = new TickedMessage
                    //{
                    //    Message = i.ToString()
                    //};

                    //Device.BeginInvokeOnMainThread(() => {
                    //    MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                    //});
                }
            }, token);



        }
    }
}
