using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InceptiumAPI.com.inceptium.httpclient;
using Xamarin.Essentials;
using InceptiumAPI.com.inceptium.core.builder;
using CumejaRing.EventBuilder;
using CumejaRing.httpconfig;

namespace CumejaRing
{
    public partial class App : Application
    {


        static App me;
        


        public System.Threading.CancellationTokenSource lastCancellationTokenRing;
        public App()
        {
            InitializeComponent();

          

            //string id = System.Guid.NewGuid().ToString();
            Console.WriteLine("ID Sistema -> " + System.Guid.NewGuid().ToString());
            Console.WriteLine("ID Sistema -> " + System.Guid.NewGuid().ToString());
            Console.WriteLine("ID Sistema Idiom -> " + DeviceInfo.Idiom);
            Console.WriteLine("ID Sistema Manufacture -> " + DeviceInfo.Manufacturer);
            Console.WriteLine("ID Sistema Model -> " + DeviceInfo.Model);
            Console.WriteLine("ID Sistema Name -> " + DeviceInfo.Name);
            Console.WriteLine("ID Sistema Platform -> " + DeviceInfo.Platform);
            Console.WriteLine("ID Sistema Versione -> " + DeviceInfo.Version);
            Console.WriteLine("ID Sistema Ver to String -> " + DeviceInfo.VersionString);

            INHTTPClientConfig config = new INHTTPClientConfig();
            config.serverIP = "portal.inceptium.it";
            config.serverPort = "443";
            config.reversProxyPath = "inapi/";
            config.sslMOde = true;
            config.inevent = new EventoIncClient();

            INHTTPClientCredential credential = new INHTTPClientCredential();
            credential.inceptiumID = "fc";
            credential.login = "bar1";
            credential.password = "*bar1";

            config.inceptiumCredential = credential;

            INBuilder inbuilda = new INBuilder(config, "com.cumejaring.IncAppCumejaRing", "https://portal.inceptium.it/AppMedia/fc/app/logo_cumeja.png");
            

            inbuilda.UserEventItemGrid = new MyEventOnGrid();

            NavigationPage navigationPage = new NavigationPage(inbuilda.MainPage());
            
            
            navigationPage.BarBackgroundColor = Color.White;
            navigationPage.BackgroundColor = Color.White;
            //navigationPage.BarTextColor = Color.Brown;

            MainPage = navigationPage;

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

        
    }
}
