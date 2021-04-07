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
           
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;
            this.TitleStack.Children.Add(activityIndicator);
            INGridView catgrid = new INGridView();

            MyNavigatorPos pos= new MyNavigatorPos(client, catgrid, activityIndicator, inGrid);
            First_Stack.VerticalOptions = LayoutOptions.FillAndExpand;
            TitleStack.Children.Add(catgrid);
            TitleStack.Padding = new Thickness(0, 10, 0, 0);
            _ = pos.LoadAsync();

        }
    }
}
