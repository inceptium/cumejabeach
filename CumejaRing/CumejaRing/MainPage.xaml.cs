using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CumejaRing
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public string CurrentLocationSelection { get; set; }
        public MainPage()
        {
            InitializeComponent();
            
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            
            
        }

        void radio_button_clicked(object sender, System.EventArgs e)
        {
            CurrentLocationSelection=(sender as RadioButton).ToString();
            //stack_NumeroPosto.IsVisible = true;
            bt_avanti.IsEnabled = true;
            Console.WriteLine("indice: "+ (sender as RadioButton));

        }

        void onBt_ChiamaCameriere(object sender, System.EventArgs e)
        {
           
        }

        void onBT_Avanti(object sender, System.EventArgs e)
        {
            //stack_contenitore.Children.Clear();
            //ViewNumPosto cont = new ViewNumPosto();
            //cont.location = CurrentLocationSelection;
            //stack_contenitore.Children.Add(cont);
            //cont.focus();
            
        }
    }
}
