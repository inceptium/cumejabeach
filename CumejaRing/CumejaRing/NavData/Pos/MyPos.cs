using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.httpclient;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;
using Newtonsoft.Json;
namespace CumejaRing.NavData.Pos
{
    public class MyPos : INNavigationGrid
    {

        private ActivityIndicator activityIndicator = new ActivityIndicator();
        public MyPos(INHTTPClient client) : base(client)
        {
            First_Stack.VerticalOptions = LayoutOptions.FillAndExpand;

            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            this.TitleStack.Children.Add(activityIndicator);
            MyNavigatorPos pos= new MyNavigatorPos(client, inGrid,activityIndicator);
            _ = pos.LoadAsync();

        }
    }
}
