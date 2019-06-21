using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CumejaBeach.xaml
{
    public partial class Impostazioni : ContentPage
    {
        public Impostazioni()
        {
            InitializeComponent();
            //Bottone.Clicked+=Bottone_Clicked;
            //bt_beachBar.Clicked+=Bt_BeachBar_Clicked;
            bt_salva.Clicked += Bottone_Clicked;
            en_IN_ipserver.Text = App.connectionPref.ServerInceptiumIP;
            en_IN_server_port.Text = App.connectionPref.InceptiumPort;
            en_IN_ID.Text = App.connectionPref.InceptiumID;
            en_IN_User.Text = App.connectionPref.InceptiumUser;
            en_DNR_ipserver.Text = App.connectionPref.ServerDNRIP;
        }





        async void Bottone_Clicked(object sender, EventArgs e)
        {

                       
            App.connectionPref.ServerInceptiumIP = en_IN_ipserver.Text;
            App.connectionPref.InceptiumPort = en_IN_server_port.Text;
            App.connectionPref.InceptiumID = en_IN_ID.Text;
            App.connectionPref.InceptiumUser = en_IN_User.Text;
            App.connectionPref.InceptiumPassword = en_IN_password.Text;
            App.connectionPref.ServerDNRIP = en_DNR_ipserver.Text;
            App.SavePreferences();
            await DisplayAlert("messaggio", "Impostazioni Salvate", "OK");

        }

    }
}
