using System;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;
using CumejaRing.NavData.Pos;

namespace CumejaRing.NavData.Pos
{
    public class EventOnPosCategorySelect : INItemGridEvent
    {
        public INGridView poscatingrid { get; set; }
        public ActivityIndicator indicator { get; set; }
        public INGridView articleGri { get; set; }
        public int posCat { get; set; }
        public EventOnPosCategorySelect()
        {
        }

        public void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            ArticlesCategory cat = (ArticlesCategory)itemGrid.VarObject;
            if (cat != null)
            {


                while (poscatingrid.Children.Count > posCat + 1)
                {
                    poscatingrid.Children.RemoveAt(poscatingrid.Children.Count - 1);
                    poscatingrid.RowDefinitions.RemoveAt(poscatingrid.Children.Count - 1);

                }
                
                



                MyNavigatorPos pos = new MyNavigatorPos(itemGrid.IncClient, poscatingrid, indicator, articleGri);

                pos.CategorySelected = cat;

                _ = pos.LoadAsync();


            }
        }
    }
}
