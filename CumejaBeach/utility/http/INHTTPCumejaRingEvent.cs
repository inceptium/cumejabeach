using System;
using InceptiumAPI.com.inceptium.httpclient;

namespace CumejaBeach.utility.http
{
    public class INHTTPCumejaRingEvent : INHttpClientConfigEvent
    {
        public INHTTPCumejaRingEvent()
        {
        }

        public void AfterCommandExecuteAsync(INHttpClientConnectionStatus status, INHTTPClient client)
        {
            throw new NotImplementedException();
        }

        public void RequestAutentication(string respondeBody, INHTTPClient client)
        {
            throw new NotImplementedException();
        }

       
    }
}
