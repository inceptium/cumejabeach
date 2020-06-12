using System;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;

namespace CumejaRing.NavData
{
    public class EventONArticleSelect : INItemGridEvent
    {


        public void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            MyNavigatorDataSingleArticle scheda = new MyNavigatorDataSingleArticle(itemGrid.IncClient);
            scheda.ArticleSelected =(ArticlesRegistry) itemGrid.VarObject;
            nav.PushAsync(scheda);
            _ = scheda.LoadCard();
        }
    }
}
