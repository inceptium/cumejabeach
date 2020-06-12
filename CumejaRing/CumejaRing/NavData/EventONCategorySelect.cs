using System;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;

namespace CumejaRing.NavData
{
    public class EventONCategorySelect : INItemGridEvent
    {
        public void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            ArticlesCategory cat =(ArticlesCategory) itemGrid.VarObject;
            if (cat != null)
            {
                MyNavigatorDataCatalog catalogo = new MyNavigatorDataCatalog(itemGrid.IncClient);
                
                catalogo.CategorySelected= cat;
                nav.PushAsync(catalogo);
                _ = catalogo.LoadAsync();
                

            }
            
        }
    }
}
