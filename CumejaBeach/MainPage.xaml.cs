using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;

namespace CumejaBeach
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Indicator1.IsRunning = true;
            disegnaOmbrelloni();

        }
        private void disegnaOmbrelloni()
        {
            var Omb1 = new Label { Text = "Top Left" };
            var Omb2 = new Label { Text = "Top Right" };
            var Omb3 = new Label { Text = "Bottom Left" };
            var Omb4 = new Label { Text = "Bottom Right" };

            GrigliaOmbrelloni.Children.Add(Omb1,0,0);
            GrigliaOmbrelloni.Children.Add(Omb2,0,1);
            GrigliaOmbrelloni.Children.Add(Omb3,1,0);
            GrigliaOmbrelloni.Children.Add(Omb4,1,1);

            //var img1=new Image {}


        }
    }


}
