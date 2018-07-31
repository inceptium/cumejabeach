using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CumejaBeach.XAML.pos
{
    public partial class PosForm : ContentPage
    {
        public PosForm()
        {
            InitializeComponent();

        }
        public void setTitolo(String titolo){
            lb_titolo.Text = titolo;
        }
    }
}
