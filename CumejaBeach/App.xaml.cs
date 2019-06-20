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
        static ConnectionPref connectionPref;
        static App me;
        static INHttpClientConfigEvent inhttpEvent;
        static INHTTPClientConfig inCliConfig;
        static INHTTPClientCredential inCredential;
        public INHTTPClient inClient;
        static ControllerRing controllerRing;
       


        public App()
        {
            me = this;
            InitializeComponent();

            Task.Run(async () => { await GetPreferencecs(); }).Wait();
            ConfigureINHTTP();

            MainPage = new CumejaBeach.xaml.MainPage();

           


        }

        public async static Task<ConnectionPref> GetPreferencecs()
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
                    await DataBasePref.SaveItemAsync(item);

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

        private void ConfigureINHTTP()
        {
            inCliConfig = new INHTTPClientConfig();
            inCliConfig.serverIP = App.GetPreferencecs().Result.ServerInceptiumIP;
            inCliConfig.serverPort = App.GetPreferencecs().Result.InceptiumPort;

            inCredential = new INHTTPClientCredential();
            inCredential.inceptiumID = "FC001";
            inCredential.login = "admin";
            inCredential.password = "gibuti";

            inCliConfig.inceptiumCredential = inCredential;

            //if (inhttpEvent == null)
            //{
            //    inhttpEvent = new INHTTPCumejaRingEvent();
            //    inCliConfig.inevent = inhttpEvent;

            //}
            inClient = new INHTTPClient(inCliConfig, "");


            var message = new StartLongRunningTaskMessage();
            MessagingCenter.Send(message, "StartLongRunningTaskMessage");


        }



        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }
    }
    public class StartLongRunningTaskMessage { }

    public class StopLongRunningTaskMessage { }
}
