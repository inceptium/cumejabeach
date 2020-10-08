using System;
using System.Threading;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaRing.httpconfig
{
    public class EventoIncClient : INHttpClientConfigEvent
    {
        public EventoIncClient()
        {
        }



        public async void AfterCommandExecuteAsync(INHttpClientConnectionStatus status, INHTTPClient client)
        {
            if (status != INHttpClientConnectionStatus.CONNECTED)
            {
                Console.WriteLine("AfterCommandExecuteAsync -> Riprovo Connessione");
                Thread.Sleep(2000);
                _ = await client.getNewWebSessionAsync();
                //_ = await client.SendCommand("load_app?classapp=com.cumejaring.IncAppCumejaRing::", true, false);

            }
        }


        public async void RequestAutentication(string respondeBody, INHTTPClient client)
        {
            Console.WriteLine("RequestAutentication -> Riprovo Connessione");
            Thread.Sleep(2000);

            if (respondeBody.Equals("INCEPTIUM APP NOT LOAD"))
            {
                _ = await client.SendCommand("load_app?classapp=com.cumejaring.IncAppCumejaRing::", true, false);
            }


           
        }
    }
}
