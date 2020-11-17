using System;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;

namespace CumejaRing.NavData
{
    public class EventONFeatureSelect : INItemGridEvent
    {
        public void OnTapItemGridAsync(INItemGrid itemGrid, INavigation nav)
        {
            FeaturesRegistry feat =(FeaturesRegistry) itemGrid.VarObject;
            if (feat != null)
            {
                MyNavigatorDataFeatures features = new MyNavigatorDataFeatures(itemGrid.IncClient);

                features.FeatureSelected = feat;
  
                nav.PushAsync(features);
                _ = features.LoadAsync();
                

            }
            
        }
    }
}
