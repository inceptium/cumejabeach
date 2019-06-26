using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using CumejaBeach.utility.pref;
using CumejaBeach.utility.pref.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CumejaBeach.utility.appstorage;
using Newtonsoft.Json;
using InceptiumAPI.com.inceptium.httpclient;
using CumejaBeach.utility.http;
using CumejaBeach.utility.controller;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CumejaBeach
{
    public partial class App : Application
    {
        
        static PreferenzeDB databasePref;
        public static ConnectionPref connectionPref;
        static  App me;
        static INHttpClientConfigEvent inhttpEvent;
        static INHTTPClientConfig inCliConfig;
        static INHTTPClientCredential inCredential;
        public INHTTPClient inClient;
        public static ControllerRing controllerRing;
        public Task lastControllerRingTask;
        public System.Threading.CancellationTokenSource lastCancellationTokenRing;



        public App()
        {
            me = this;
            InitializeComponent();

            Task.Run(async () => { await LoadPreferences(); }).Wait();
            inCliConfig = new INHTTPClientConfig();
            inCredential = new INHTTPClientCredential();
            inClient = new INHTTPClient(inCliConfig, "");

            ConfigureINHTTP();
            

            MainPage = new CumejaBeach.xaml.MainPage();

           


        }

        public async static void SavePreferences()
        {
            string cnntojson = JsonConvert.SerializeObject(connectionPref);
            Console.WriteLine("write: -> " + cnntojson);
            ItemPref item= await DataBasePref.GetItemByKeyAsync("ConnectionPref");
            item.key = "ConnectionPref";
            item.value = cnntojson;
            //{
            //    key = "ConnectionPref",
            //    value = cnntojson
            //};
            await DataBasePref.SaveItemAsync(item);
        }

        public async static Task<ConnectionPref> LoadPreferences()
        {

            if (connectionPref == null)
            {
                var tasklist = await DataBasePref.GetItemByKeyAsync("ConnectionPref");
                if (tasklist != null)
                {
                    connectionPref = JsonConvert.DeserializeObject<ConnectionPref>(tasklist.value);

                }
                else
                {
                    connectionPref = new ConnectionPref();
                    string cnntojson = JsonConvert.SerializeObject(connectionPref);
                    Console.WriteLine("write: -> " + cnntojson);
                    ItemPref item = new ItemPref
                    {
                        key = "ConnectionPref",
                        value = cnntojson
                    };
                    await DataBasePref.AddItemAsync(item);

                }


            }
            return connectionPref;

        }


        public static App getInstance()
        {
            return me;
        }

        public static PreferenzeDB DataBasePref
        {
            get
            {
                if (databasePref == null)
                {
                    Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
                    databasePref = new PreferenzeDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PrefSQLite.db3"));
                }
                return databasePref;
            }
        }

        public void ConfigureINHTTP()
        {
            
            inCliConfig.serverIP = connectionPref.ServerInceptiumIP;
            inCliConfig.serverPort = connectionPref.InceptiumPort;

            
            inCredential.inceptiumID = connectionPref.InceptiumID;
            inCredential.login = connectionPref.InceptiumUser;
            inCredential.password = connectionPref.InceptiumPassword;

            inCliConfig.inceptiumCredential = inCredential;

            //if (inhttpEvent == null)
            //{
            //    inhttpEvent = new INHTTPCumejaRingEvent();
            //    inCliConfig.inevent = inhttpEvent;

            //}
            

            
            


        }



        

        protected override void OnStart()
        {
            // Handle when your app starts
            //var message = new StartLongRunningTaskMessage();
            //MessagingCenter.Send(message, "StartLongRunningTaskMessage");
            controllerRing = new ControllerRing(inClient);
            lastCancellationTokenRing = new System.Threading.CancellationTokenSource();
            lastControllerRingTask = controllerRing.LeggiRingAsync(lastCancellationTokenRing.Token);
            Console.WriteLine("Applicazione Avviata!!!!!");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            lastCancellationTokenRing.Dispose();
            controllerRing.close = true;

            Console.WriteLine("Applicazione On Sleep");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            lastCancellationTokenRing = new System.Threading.CancellationTokenSource();
            controllerRing = new ControllerRing(inClient);
            lastControllerRingTask = controllerRing.LeggiRingAsync(lastCancellationTokenRing.Token);
            Console.WriteLine("Applicazione On resume");

        }
    }
    public class StartLongRunningTaskMessage { }

    public class StopLongRunningTaskMessage { }
}
