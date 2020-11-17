using System;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;

namespace CumejaRing.NavData
{
    public class EventONCategorySelect : INItemGridEvent
    {
        public async void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            ArticlesCategory cat =(ArticlesCategory) itemGrid.VarObject;
            if (cat != null)
            {
                MyNavigatorDataCatalog catalogo = new MyNavigatorDataCatalog(itemGrid.IncClient);
                
                catalogo.CategorySelected= cat;
         
                await nav.PushAsync(catalogo);
                _ = catalogo.LoadAsync();
                

            }
            
        }
    }
}
