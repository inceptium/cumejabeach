using System;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;
using CumejaRing.NavData.Pos;

namespace CumejaRing.NavData.Pos
{
    public class EventOnPosCategorySelect: INItemGridEvent
    {
        public INGridView poscatnav { get; set; }
        public ActivityIndicator indicator { get; set; }
        public EventOnPosCategorySelect()
        {
        }

        public void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            ArticlesCategory cat = (ArticlesCategory)itemGrid.VarObject;
            if (cat != null)
            {
                MyNavigatorPos pos = new MyNavigatorPos( itemGrid.IncClient, poscatnav, indicator);

                pos.CategorySelected = cat;
              
                _ = pos.LoadAsync();


            }
        }
    }
}
