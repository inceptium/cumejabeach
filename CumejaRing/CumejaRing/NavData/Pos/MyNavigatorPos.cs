using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.httpclient;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;
using Newtonsoft.Json;
using CumejaRing.NavData.Pos;

namespace CumejaRing.NavData
{
    public class MyNavigatorPos 
    {
        public ArticlesCategory CategorySelected { get; set; }
        int row = 0;
        INHTTPClient inClient;
        INGridView inGrid;
        private ActivityIndicator activityIndicator;
        public static int ingridpos=0;

        public MyNavigatorPos(INHTTPClient client, INGridView ingrid, ActivityIndicator indicator)
        {

            this.inClient = client;
            this.inGrid = ingrid;
            this.activityIndicator = indicator;

        }

        public async Task LoadAsync()
        {
           
            
           

            String categorie = "";

            if (CategorySelected == null)
            {
                String selectrecor = "where id_Father='' and exposeOnWeb=true ";
                var stringa_inbyte = System.Text.Encoding.UTF8.GetBytes(selectrecor);

                String order = "Order by description";
                var stringa_order_in_byte = System.Text.Encoding.UTF8.GetBytes(order);

                categorie = await inClient.SendCommand("callappcommand?command=getrecords::class=com.eterea.data.registry.articles.ArticlesCategory::filter.b64=" +
                    Convert.ToBase64String(stringa_inbyte) + "::order.b64=" + Convert.ToBase64String(stringa_order_in_byte) + "::", false, true);
            }
            else
            {
                String selectrecor = "where id_Father='" + CategorySelected.id_ArticlesCategory + "' and exposeOnWeb=true ";
                var stringa_inbyte = System.Text.Encoding.UTF8.GetBytes(selectrecor);
                String order = "Order by description";
                var stringa_order_in_byte = System.Text.Encoding.UTF8.GetBytes(order);

                categorie = await inClient.SendCommand("callappcommand?command=getrecords::class=com.eterea.data.registry.articles.ArticlesCategory::filter.b64=" +
                    Convert.ToBase64String(stringa_inbyte) + "::order.b64=" + Convert.ToBase64String(stringa_order_in_byte) + "::", false, true);

            }
            if (categorie.StartsWith("["))
            {

                
                INGridView catPos = new INGridView();

                ScrollView scroll = new ScrollView();
                scroll.VerticalOptions = LayoutOptions.FillAndExpand;
                scroll.HorizontalOptions = LayoutOptions.FillAndExpand;
                scroll.Content = catPos;
                scroll.Orientation = ScrollOrientation.Horizontal;

                List<ArticlesCategory> lista_categorie = JsonConvert.DeserializeObject<List<ArticlesCategory>>(categorie);
                foreach (ArticlesCategory cat in lista_categorie)
                {
                    INItemGrid item = new INItemGrid("", null, this.inClient);
                    item.Orientation = StackOrientation.Vertical;
                    item.VerticalOptions = LayoutOptions.FillAndExpand;
                    item.HorizontalOptions = LayoutOptions.Start;
                    item.HeightRequest = 45;
                    item.Padding = new Thickness(0, 0, 0, 0);
                    item.VarObject = cat;
                    //item.BackgroundColor = new Color(100, 100, 100);
                    Image imm = new Image();

                    if (cat.iconURL.Length > 0)
                    {
                        var imageSource = new UriImageSource { Uri = new Uri(cat.iconURL) };
                        imageSource.CachingEnabled = true;
                        imageSource.CacheValidity = new TimeSpan(0, 1, 0, 0);
                        imm.Source = imageSource;
                    }
                    imm.VerticalOptions = LayoutOptions.FillAndExpand;
                    imm.HorizontalOptions = LayoutOptions.FillAndExpand;
                    imm.Aspect = Aspect.AspectFit;
                    imm.HeightRequest = 35;
                    imm.WidthRequest = 35;


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
                    inlabel.Text = cat.description;
                    inlabel.FontSize = 15;
                    inlabel.HorizontalTextAlignment = TextAlignment.Start;
                    inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    inlabel.VerticalTextAlignment = TextAlignment.Center;
                    inlabel.Padding = new Thickness(0, 0, 0, 0);
                    inlabel.TextColor = Color.Gray;
                    item.Children.Add(inlabel);


                    EventOnPosCategorySelect evet = new EventOnPosCategorySelect();
                    evet.indicator = activityIndicator;
                    evet.poscatnav = inGrid;
                    item.addEventGrind(evet);

                    // addLinea();

                    catPos.addInPosition(0, row, item);




                    //this.inGrid.RowSpacing = 0;
                    //"get_app_dock?devicedock=";
                    row++;

                }

                INItemGrid contenitoreCat = new INItemGrid("", null, this.inClient);
                contenitoreCat.Orientation = StackOrientation.Horizontal;
                contenitoreCat.VerticalOptions = LayoutOptions.FillAndExpand;
                contenitoreCat.HorizontalOptions = LayoutOptions.Start;
                contenitoreCat.HeightRequest = 50;
                contenitoreCat.Padding = new Thickness(20, 0, 0, 0);
               
                contenitoreCat.Children.Add(scroll);
                inGrid.addInPosition(ingridpos, 0, contenitoreCat);
                ingridpos++;
                //if (lista_categorie.Count > 0)
                //{
                //    addLinea();
                //}
                //else
                //{
                //    _ = LoadArticlesAsync();
                //}


            }

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;


        }

        private void addLinea()
        {
            BoxView box = new BoxView();
            box.HeightRequest = 1;
            box.BackgroundColor = Color.LightGray;
            box.HorizontalOptions = LayoutOptions.FillAndExpand;
            inGrid.addViewInPosition(0, 0, box);
            row++;
        }

        private async Task LoadArticlesAsync()
        {

            String articoli = "";


            String selectrecor = "where articlesCategoryCode='" + CategorySelected.categoryFullPathCode + "'";
            String order = "Order by description";
            var stringa_inbyte = System.Text.Encoding.UTF8.GetBytes(selectrecor);
            var stringa_order_in_byte = System.Text.Encoding.UTF8.GetBytes(order);

            articoli = await inClient.SendCommand("callappcommand?command=getrecords::class=com.eterea.data.registry.articles.QArticlesFeaturesForMobile::filter.b64=" +
                Convert.ToBase64String(stringa_inbyte) + "::order.b64=" + Convert.ToBase64String(stringa_order_in_byte) + "::", false, true);

            if (articoli.StartsWith("["))
            {

                ArticlesRegistry old = new ArticlesRegistry();
                List<ArticlesRegistry> lista_articoli = JsonConvert.DeserializeObject<List<ArticlesRegistry>>(articoli);
                foreach (ArticlesRegistry art in lista_articoli)
                {



                    if (!art.id_ArticlesRegistry.Equals(old.id_ArticlesRegistry))
                    {


                        ItemArticles itemart = new ItemArticles(art, null, inClient);

                        addLinea();
                        inGrid.addInPosition(row, 0, itemart.getNewItemGridArticles());
                        old = art;



                        //this.inGrid.RowSpacing = 0;
                        //"get_app_dock?devicedock=";
                        row++;
                    }

                }

                if (lista_articoli.Count == 0)
                {

                    Label inlabel = new Label();
                    inlabel.Text = "Non ci sono articoli in questa categoria";
                    inlabel.HorizontalTextAlignment = TextAlignment.Start;
                    inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    inlabel.VerticalTextAlignment = TextAlignment.Center;
                    inlabel.HeightRequest = 15;
                    inlabel.TextColor = Color.Gray;
                    inGrid.addViewInPosition(row, 0, inlabel);
                    row++;
                }

            }
            else
            {

                Label inlabel = new Label();
                inlabel.Text = "ci sono stati problemi durante il caricamento!!!";
                inlabel.HorizontalTextAlignment = TextAlignment.Start;
                inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                inlabel.VerticalTextAlignment = TextAlignment.Center;
                inlabel.HeightRequest = 15;
                inlabel.TextColor = Color.Gray;
                inGrid.addViewInPosition(row, 0, inlabel);
                Console.WriteLine(row);
                row++;
            }



            addLinea();



        }

    }
}
