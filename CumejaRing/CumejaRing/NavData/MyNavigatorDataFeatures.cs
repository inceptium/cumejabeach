using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.httpclient;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;
using Newtonsoft.Json;


namespace CumejaRing.NavData
{
    public class MyNavigatorDataFeatures : INNavigationGrid
    {
        public FeaturesRegistry FeatureSelected { get; set; }
        int row = 0;
        private ActivityIndicator activityIndicator = new ActivityIndicator();
        public string WhereClausole { get; set; }

        public MyNavigatorDataFeatures(INHTTPClient client) : base(client)
        {
            First_Stack.VerticalOptions = LayoutOptions.FillAndExpand;
            WhereClausole = "";



        }

        public async Task LoadAsync()
        {
            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;

            if (FeatureSelected != null)
            {
                Label inlabel = new Label();
                inlabel.Text = FeatureSelected.description;
                inlabel.HorizontalTextAlignment = TextAlignment.Start;
                inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                inlabel.VerticalTextAlignment = TextAlignment.Center;
                inlabel.TextColor = Color.Gray;
                inlabel.FontAttributes = FontAttributes.Bold;

                this.TitleStack.Children.Add(inlabel);
            }
            this.TitleStack.Children.Add(activityIndicator);

            String features = "";

            if (FeatureSelected == null)
            {
                string filter64 = "";
                if (WhereClausole.Length > 0)
                {
                    var stringa_inbyte = System.Text.Encoding.UTF8.GetBytes(WhereClausole);
                    filter64 = "filter.b64=" + Convert.ToBase64String(stringa_inbyte) + "::";
                }
                
                features = await inClient.SendCommand("callappcommand?command=getrecords::class=com.eterea.data.registry.articles.FeaturesRegistry::"+filter64, false, true);
            }

            if (features.ToString().Length > 0)
            {

                List<FeaturesRegistry> listafeature = JsonConvert.DeserializeObject<List<FeaturesRegistry>>(features);
                foreach (FeaturesRegistry feat in listafeature)
                {
                    INItemGrid item = new INItemGrid("", this.Navigation, this.inClient);
                    
                    item.Orientation = StackOrientation.Horizontal;
                    ////item.VerticalOptions = LayoutOptions.FillAndExpand;
                    item.HorizontalOptions = LayoutOptions.FillAndExpand;
                    item.HeightRequest = 130;

                    item.VarObject = feat;
                    //item.BackgroundColor = new Color(100, 100, 100);
                    Image imm = new Image();

                    var imageSource = new UriImageSource { Uri = new Uri("https://www.inceptium.it/portaledef/media/eterea/app/icona-base.png") };
                    imageSource.CachingEnabled = true;
                    imageSource.CacheValidity = new TimeSpan(0, 1, 0, 0);
                    imm.Source = imageSource;
                    imm.VerticalOptions = LayoutOptions.FillAndExpand;
                    imm.HorizontalOptions = LayoutOptions.Start;
                    imm.Aspect = Aspect.AspectFit;
                    imm.HeightRequest = 100;
                    imm.WidthRequest = 100;

                    //Frame frm_immagine = new Frame();
                    //frm_immagine.HasShadow = true;
                    //frm_immagine.Margin = 0;
                    //frm_immagine.WidthRequest =80;
                    //frm_immagine.MinimumWidthRequest = 80;
                    //frm_immagine.HeightRequest = 80;
                    //frm_immagine.HorizontalOptions = LayoutOptions.Start;
                    //frm_immagine.Content = imm;


                    item.Children.Add(imm);

                    Label inlabel = new Label();
                    inlabel.Text = feat.description;
                    inlabel.HorizontalTextAlignment = TextAlignment.Start;
                    inlabel.HorizontalOptions = LayoutOptions.EndAndExpand;
                    inlabel.VerticalTextAlignment = TextAlignment.Start;
                    inlabel.TextColor = Color.Gray;

                    item.Children.Add(inlabel);


                    item.addEventGrind(new EventONFeatureSelect());


                   
                    //inGrid.addInPosition(row,0,)


                    //this.inGrid.RowSpacing = 0;
                    //"get_app_dock?devicedock=";
                    row++;

                }

            }
            _ = LoadArticlesAsync();
            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;


        }

        private async Task LoadArticlesAsync()
        {
            String articoli = "";


            String selectrecor = "where id_FeaturesRegistry='" + FeatureSelected.id_FeaturesRegistry + "'";
            var stringa_inbyte = System.Text.Encoding.UTF8.GetBytes(selectrecor);
            articoli = await inClient.SendCommand("callappcommand?command=getrecords::class=com.eterea.data.registry.articles.QArticlesFeaturesForMobile::filter.b64=" + Convert.ToBase64String(stringa_inbyte) + "::", true, true);

            if (articoli.ToString().Length > 0)
            {
                ArticlesRegistry old = new ArticlesRegistry();
                List<ArticlesRegistry> lista_articoli = JsonConvert.DeserializeObject<List<ArticlesRegistry>>(articoli);
                foreach (ArticlesRegistry art in lista_articoli)
                {
                    if (!art.id_ArticlesRegistry.Equals(old.id_ArticlesRegistry))
                    {


                        ItemArticles itemart = new ItemArticles(art, this.Navigation, inClient);


                        inGrid.addInPosition(row, 0, itemart.getNewItemGridArticles());
                        old = art;

                        //this.inGrid.RowSpacing = 0;
                        //"get_app_dock?devicedock=";
                        row++;
                    }

                }

            }

            if (row == 0)
            {
                Label inlabel = new Label();
                inlabel.Text = "Non ci sono articoli in questa categoria";
                inlabel.HorizontalTextAlignment = TextAlignment.Start;
                inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                inlabel.VerticalTextAlignment = TextAlignment.Center;
                inlabel.TextColor = Color.Gray;
                inGrid.Children.Add(inlabel);
            }

        }
    }
}
