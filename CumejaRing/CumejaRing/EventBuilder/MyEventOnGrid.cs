﻿using System;
using CumejaRing.NavData;
using InceptiumAPI.com.inceptium.core.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using CumejaRing.NavData.Pos;
using Xamarin.Forms;

namespace CumejaRing.EventBuilder
{
    public class MyEventOnGrid : INItemGridEvent
    {
        public MyEventOnGrid()
        {
        }

        public void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            Console.WriteLine(itemGrid.MenuItemGrid.menuName+ " - " + itemGrid.MenuItemGrid.id_menu);
            INMenu menu = itemGrid.MenuItemGrid;
            if (menu.id_menu.Equals("men_1"))
            {
                CallCatalogo(itemGrid, nav);
            }
            else if (menu.id_menu.Equals("men_5"))
            {
                CallCameriere(itemGrid, nav);
            }
            else if (menu.id_menu.Equals("id_1.3"))
            {
                CallCaratteristiche(itemGrid, nav);
            }else if (menu.id_menu.Equals("men_rist"))
            {
                CallPos(itemGrid, nav);
            }
        }

        private void CallCatalogo(INItemGrid itemGrid, INavigation nav)
        {
            MyNavigatorDataCatalog catalogo = new MyNavigatorDataCatalog(itemGrid.IncClient);
            catalogo.setTitleStack(itemGrid.MenuItemGrid.menuName);
            _ = catalogo.LoadAsync();
            
            nav.PushAsync(catalogo);
        }

        private void CallCameriere(INItemGrid itemGrid, INavigation nav)
        {
            SelectLocation feat = new SelectLocation(itemGrid.IncClient);
            
            nav.PushAsync(feat);
        }

        private void CallCaratteristiche(INItemGrid itemGrid, INavigation nav)
        {
            MyNavigatorDataFeatures feat = new MyNavigatorDataFeatures(itemGrid.IncClient);
            feat.WhereClausole = "where description not like 'linea%'";
            _ = feat.LoadAsync();
            nav.PushAsync(feat);
        }
        private void CallPos(INItemGrid itemGrid, INavigation nav)
        {
            MyPos po = new MyPos(itemGrid.IncClient);
            //po.setTitleStack(itemGrid.MenuItemGrid.menuName);
            

            nav.PushAsync(po);
        }

    }
}
