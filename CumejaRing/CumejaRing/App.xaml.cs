using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InceptiumAPI.com.inceptium.httpclient;
using Xamarin.Essentials;

namespace CumejaRing
{
    public partial class App : Application
    {


        static App me;
        static INHttpClientConfigEvent inhttpEvent;
        public static INHTTPClientConfig inCliConfig;
        static INHTTPClientCredential inCredential;
        public INHTTPClient inClient;


        public System.Threading.CancellationTokenSource lastCancellationTokenRing;
        public App()
        {
            InitializeComponent();
            
            MainPage = new MasterPage();
            me = this;
            inClient = new INHTTPClient();
            inCliConfig = new INHTTPClientConfig();
            inCredential = new INHTTPClientCredential();

            //string id = System.Guid.NewGuid().ToString();
            Console.WriteLine("ID Sistema -> "+System.Guid.NewGuid().ToString());
            Console.WriteLine("ID Sistema -> " + System.Guid.NewGuid().ToString());
            Console.WriteLine("ID Sistema Idiom -> " + DeviceInfo.Idiom);
            Console.WriteLine("ID Sistema Manufacture -> " + DeviceInfo.Manufacturer);
            Console.WriteLine("ID Sistema Model -> " + DeviceInfo.Model);
            Console.WriteLine("ID Sistema Name -> " + DeviceInfo.Name);
            Console.WriteLine("ID Sistema Platform -> " + DeviceInfo.Platform);
            Console.WriteLine("ID Sistema Versione -> " + DeviceInfo.Version);
            Console.WriteLine("ID Sistema Ver to String -> " + DeviceInfo.VersionString);

        }
        public static App getInstance()
        {
            return me;
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

        public void ConfigureINHTTP()
        {
  

            inCliConfig.serverIP = "192.168.101.115";
            inCliConfig.serverPort = "8888";


            inCredential.inceptiumID = "FC001";
            inCredential.login = "admin";
            inCredential.password = "gibuti";

            inCliConfig.inceptiumCredential = inCredential;
            inClient.httpInceptiumConfig = inCliConfig;

            //if (inhttpEvent == null)
            //{
            //    inhttpEvent = new INHTTPCumejaRingEvent();
            //    inCliConfig.inevent = inhttpEvent;

            //}






        }
    }
}
