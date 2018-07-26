using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CumejaBeach.xaml
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterPage.listView.ItemSelected += OnItemSelected;
            MasterBehavior = MasterBehavior.Popover;

            //if (Device.RuntimePlatform == Device.UWP)
            //{
                
            //}
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
      
    }
}
