using System;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaRing.httpconfig
{
    public class EventoIncClient : INHttpClientConfigEvent
    {
        public EventoIncClient()
        {
        }

       

        public void AfterCommandExecute(INHttpClientConnectionStatus status, INHTTPClient client)
        {
           if (status != INHttpClientConnectionStatus.CONNECTED)
            {
                _ = client.getNewWebSessionAsync();
                _= client.SendCommand("load_app?classapp=com.cumejaring.IncAppCumejaRing::", true, false);
            }
        }

       

        public string RequestAutentication(string respondeBody, INHTTPClient client)
        {
            Console.WriteLine("Riprovo Connessione");

            _ = client.getNewWebSessionAsync();

            return "";
        }
    }
}
