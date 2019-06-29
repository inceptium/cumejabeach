using System;
using System.Collections.Generic;
using CumejaBeach.utility.controller;

using Xamarin.Forms;

namespace CumejaBeach.XAML.ring
{
    public partial class CumejaRingList : ContentPage
    {
        public static CumejaRingList me;
        public ActivityIndicator indicator;
        public CumejaRingList()
        {
            InitializeComponent();
            caricaLista();
            me = this;
            indicator = indicatorRing;
        }

        private void caricaLista()
        {
           var lista= App.controllerRing.getRings();

            if (lista != null)
            {


                foreach (ItemPostoRicreativo posto in lista)
                {
                    StackItemRing st = new StackItemRing();
                    st.postoRicreativo = posto;
                    st.Disegna();
                    ContenitoreRing.Children.Add(st);


                }
            }
        }
    }
}
