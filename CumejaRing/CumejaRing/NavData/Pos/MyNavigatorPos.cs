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
        INGridView inArticleGrid;
        private ActivityIndicator activityIndicator;
        public int ingridpos = 0;
        private int articleRow = 0;

        public MyNavigatorPos(INHTTPClient client, INGridView ingrid, ActivityIndicator indicator, INGridView articlegrid)
        {

            this.inClient = client;
            this.inGrid = ingrid;
            this.activityIndicator = indicator;
            this.inArticleGrid = articlegrid;
            ingridpos = ingrid.Children.Count;

        }

        public async Task LoadAsync()
        {

            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;


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
                catPos.HorizontalOptions = LayoutOptions.Start;


                ScrollView scroll = new ScrollView();
                scroll.VerticalOptions = LayoutOptions.FillAndExpand;
                scroll.HorizontalOptions = LayoutOptions.FillAndExpand;
                scroll.Content = catPos;
                scroll.Orientation = ScrollOrientation.Horizontal;

                List<ArticlesCategory> lista_categorie = JsonConvert.DeserializeObject<List<ArticlesCategory>>(categorie);
                int yy = 0;

                foreach (ArticlesCategory cat in lista_categorie)
                {
                    INItemGrid item = new INItemGrid("", null, this.inClient);
                    item.Orientation = StackOrientation.Vertical;
                    item.VerticalOptions = LayoutOptions.FillAndExpand;
                    item.HorizontalOptions = LayoutOptions.Start;
                    item.HeightRequest = 60;
                    item.WidthRequest = 80;
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
                    inlabel.FontSize = 12;
                    inlabel.HorizontalTextAlignment = TextAlignment.Center;
                    inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    inlabel.VerticalTextAlignment = TextAlignment.Center;
                    inlabel.Padding = new Thickness(0, 0, 0, 0);
                    inlabel.TextColor = Color.Gray;
                    item.Children.Add(inlabel);


                    EventOnPosCategorySelect evet = new EventOnPosCategorySelect();
                    evet.indicator = activityIndicator;
                    evet.poscatingrid = inGrid;
                    evet.posCat = inGrid.Children.Count + 1;
                    evet.articleGri = inArticleGrid;
                    item.addEventGrind(evet);



                    catPos.addInPosition(yy, row, item);
                    row++;
                    if (row > 5)
                    {
                        yy++;

                        row = 0;

                    }

                    //this.inGrid.RowSpacing = 0;
                    //"get_app_dock?devicedock=";


                }
                if (lista_categorie.Count == 0)
                {
                    _ = LoadArticlesAsync();
                }
                else
                {

                    INItemGrid contenitoreCat = new INItemGrid("", null, this.inClient);
                    contenitoreCat.Orientation = StackOrientation.Horizontal;
                    contenitoreCat.VerticalOptions = LayoutOptions.FillAndExpand;
                    contenitoreCat.HorizontalOptions = LayoutOptions.Start;
                    contenitoreCat.HeightRequest = 65 * (yy + 1);
                    if (CategorySelected != null)
                    {
                        contenitoreCat.VarObject = CategorySelected;
                    }
                    else
                    {
                        contenitoreCat.VarObject = null;
                    }

                    contenitoreCat.Padding = new Thickness(20, 0, 0, 0);

                    contenitoreCat.Children.Add(scroll);
                    inGrid.addInPosition(ingridpos, 0, contenitoreCat);
                    //ricalcola dimensione griglia


                    ingridpos++;
                    addLinea();
                }

            }

            activityIndicator.IsVisible = false;
            activityIndicator.IsRunning = false;


        }



        private void addLinea()
        {
            BoxView box = new BoxView();
            box.HeightRequest = 2;
            box.BackgroundColor = Color.LightSalmon;
            box.HorizontalOptions = LayoutOptions.FillAndExpand;
            inGrid.addViewInPosition(ingridpos, 0, box);
            ingridpos++;

        }

        private void addLineaArt()
        {
            BoxView box = new BoxView();
            box.HeightRequest = 1;
            box.BackgroundColor = Color.LightGray;
            box.HorizontalOptions = LayoutOptions.FillAndExpand;
            inArticleGrid.addViewInPosition(articleRow, 0, box);
            articleRow++;


        }

        private async Task LoadArticlesAsync()
        {
            inArticleGrid.Children.Clear();
            inArticleGrid.RowDefinitions.Clear();

            ScrollView scroll = (ScrollView)inArticleGrid.Parent;
            //scroll.Content.HeightRequest=
            Console.WriteLine("altezza container " + scroll.Content.Height);
            _ = scroll.ScrollToAsync(0, 0, true);

            articleRow = 0;

            String articoli = "";


            String selectrecor = "where articlesCategoryCode='" + CategorySelected.categoryFullPathCode + "'";
            Console.WriteLine(selectrecor);
            String order = "Order by description";
            var stringa_inbyte = System.Text.Encoding.UTF8.GetBytes(selectrecor);
            var stringa_order_in_byte = System.Text.Encoding.UTF8.GetBytes(order);

            articoli = await inClient.SendCommand("callappcommand?command=getrecords::class=com.eterea.data.registry.articles.QArticleRegistryIstant::filter.b64=" +
                Convert.ToBase64String(stringa_inbyte) + "::order.b64=" + Convert.ToBase64String(stringa_order_in_byte) + "::", false, true);

            if (articoli.StartsWith("["))
            {

          
                List<ArticlesRegistry> lista_articoli = JsonConvert.DeserializeObject<List<ArticlesRegistry>>(articoli);
                foreach (ArticlesRegistry art in lista_articoli)
                {

                        ItemArticles itemart = new ItemArticles(art, null, inClient);
                        inArticleGrid.addInPosition(articleRow, 0, itemart.getNewItemGridArticles());
                        articleRow++;
                        //addLineaArt();


                        //this.inGrid.RowSpacing = 0;
                        //"get_app_dock?devicedock=";

                   

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
                    inArticleGrid.addViewInPosition(articleRow, 0, inlabel);
               
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
                inArticleGrid.addViewInPosition(articleRow, 0, inlabel);
                Console.WriteLine(articleRow);
               
            }
        }

    }
}
