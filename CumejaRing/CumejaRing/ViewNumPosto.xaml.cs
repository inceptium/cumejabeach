using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CumejaRing
{
    public partial class ViewNumPosto : ContentView
    {
        public string location { get; set; }
        public ViewNumPosto()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            label_status.Text = "la sua richiesta è stata correttamente inviata!!! " + location;
            stack_status_monitor.IsVisible = true;
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (entry_posto.Text.Length > 0)
            {
                bt_chiama_cameriere.IsEnabled = true;
            }
            else
            {
                bt_chiama_cameriere.IsEnabled = false;

            }
        }
    }
}
