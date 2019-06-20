using System;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaBeach.utility.http
{
    public class INHTTPCumejaRingEvent : INHttpClientConfigEvent
    {
        public INHTTPCumejaRingEvent()
        {
        }

        void INHttpClientConfigEvent.AfterCommandExecute(INHttpClientConnectionStatus status)
        {


            if (status!= INHttpClientConnectionStatus.CONNECTED)
            {
                //App.getInstance().LeggiRingAsync(true);
            }
            //switch (status)
            //{
            //    case INHttpClientConnectionStatus.CONNECTED:
            //        App.getInstance().LeggiRingAsync();
            //        break;
            //    case INHttpClientConnectionStatus.CONNECTION_ERROR:
            //        break;
            //    case INHttpClientConnectionStatus.CONNECTION_UNAUTORIZED:
            //        break;
            //    case INHttpClientConnectionStatus.NOT_CONNECTED:
            //        break;
            //}
        }

        string INHttpClientConfigEvent.RequestAutentication(string respondeBody)
        {
            
            return respondeBody;
        }
    }
}
