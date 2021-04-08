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
           
            INGridView catgrid = new INGridView();
            catgrid.VerticalOptions = LayoutOptions.FillAndExpand;

            inGrid.VerticalOptions = LayoutOptions.FillAndExpand;
            //catgrid.LayoutChanged += InGrid_LayoutChanged1;

           


            MyNavigatorPos pos= new MyNavigatorPos(client, catgrid, activityIndicator, inGrid);
            First_Stack.VerticalOptions = LayoutOptions.FillAndExpand;
            TitleStack.Children.Add(catgrid);
            this.TitleStack.Children.Add(activityIndicator);
            TitleStack.Padding = new Thickness(0, 10, 0, 0);
            _ = pos.LoadAsync();

        }

        private void InGrid_LayoutChanged1(object sender, EventArgs e)
        {
            Console.WriteLine("cambio lay");
            INGridView catgrid = (INGridView)sender;
            catgrid.ForceLayout();
            double newheight = 0;
            foreach (View vi in catgrid.Children)
            {
                newheight = newheight + vi.Height;
            }
            catgrid.HeightRequest = newheight + 20d;
        }
    }
}
