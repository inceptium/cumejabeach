using System;
using System.Collections.Generic;
using CumejaBeach.utility.controller;

using Xamarin.Forms;

namespace CumejaBeach.XAML.ring
{
    public partial class CumejaRingList : ContentPage
    {
        public CumejaRingList()
        {
            InitializeComponent();
            caricaLista();
            
        }

        private void caricaLista()
        {
           var lista= App.controllerRing.getRings();

            foreach (ItemPostoRicreativo posto in lista)
            {
                Console.WriteLine(posto.code_location);
            }
        }
    }
}
