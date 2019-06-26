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
using System.Collections.Generic;

namespace CumejaBeach.utility.controller
{
    public class ControllerRing
    {
        private volatile bool leggiRingRun = false;
        private INHTTPClient inClient;
        private bool newsession = true;
        public volatile bool close;
        public string lastread = "";
        private readonly object balanceLock = new object();



        public List<ItemPostoRicreativo> getRings()
        {
            lock (balanceLock)
            {
                if (!lastread.Equals(""))
                {
                    List<ItemPostoRicreativo> containerRichieste;
                    containerRichieste = JsonConvert.DeserializeObject<List<ItemPostoRicreativo>>(lastread);

                    return containerRichieste;
                }
                else
                {
                    return null;
                }
            }

        }



        public ControllerRing(INHTTPClient client)
        {
            this.inClient = client;

        }

        public async Task LeggiRingAsync(CancellationToken token)
        {
            if (!leggiRingRun)
            {


                Console.WriteLine("Inizio tasssssssssskkkkkkkkkk");
                string incTaskRest = "";
                close = false;
                await Task.Run(async () =>
                {
                    leggiRingRun = true;

                    while (!close)
                    {
                        token.ThrowIfCancellationRequested();

                        if (close)
                            break;

                        try
                        {


                            if (!inClient.connected)
                            {
                                App.getInstance().ConfigureINHTTP();
                                var sessione = await inClient.getNewWebSessionAsync();
                                Console.WriteLine("Leggo -> Ring: new session");
                                newsession = false;
                                incTaskRest = await inClient.SendCommand("load_app?classapp=com.cumejaring.AppCumejaRing::", true, false);
                            }



                            if (incTaskRest.StartsWith("TASK"))
                            {
                                var rest = await inClient.SendCommand("callappcommand?command=executemethod::class=com.cumejaring.datamodel.beach.crPostoRicreativo::method=ring_list::", false, false);
                                lock (balanceLock)
                                {
                                    if (rest != null)
                                    {
                                        lastread = rest;

                                        Console.WriteLine("Leggo -> Ring");
                                    }
                                    else
                                    {
                                        lastread = "";
                                    }
                                }

                            }
                            else
                            {
                                lock (balanceLock)
                                {
                                    lastread = "";
                                    Console.WriteLine("inceptium app Cumeja Ring not load !!!!");
                                }
                            }


                        }
                        catch
                        {
                            Console.WriteLine("connessione server inceptium assente");
                        }

                        await Task.Delay(5000, token);

                        //var message = new TickedMessage
                        //{
                        //    Message = i.ToString()
                        //};

                        //Device.BeginInvokeOnMainThread(() => {
                        //    MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                        //});
                    }
                    Console.WriteLine("Controller Ring Fermato");
                    leggiRingRun = false;
                    inClient.connected = false;
                }, token);

            }

        }
    }
}
