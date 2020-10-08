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
    public class MyNavigatorDataCatalog : INNavigationGrid
    {
        public ArticlesCategory CategorySelected { get; set; }
        int row = 0;
        private ActivityIndicator activityIndicator = new ActivityIndicator();

        public MyNavigatorDataCatalog(INHTTPClient client) : base(client)
        {
            First_Stack.VerticalOptions = LayoutOptions.FillAndExpand;

            activityIndicator.IsVisible = true;
            activityIndicator.IsRunning = true;


        }

        public async Task LoadAsync()
        {
           
            this.TitleStack.Padding = new Thickness(0, 20, 0, 10);

            if (CategorySelected != null)
            {
                this.setTitleStack(CategorySelected.categoryFullPathDescription);
            }
            this.TitleStack.Children.Add(activityIndicator);

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


                List<ArticlesCategory> lista_categorie = JsonConvert.DeserializeObject<List<ArticlesCategory>>(categorie);
                foreach (ArticlesCategory cat in lista_categorie)
                {
                    INItemGrid item = new INItemGrid("", this.Navigation, this.inClient);
                    item.Orientation = StackOrientation.Horizontal;
                    item.VerticalOptions = LayoutOptions.FillAndExpand;
                    item.HorizontalOptions = LayoutOptions.Start;
                    item.HeightRequest = 90;
                    item.Padding = new Thickness(20, 0, 0, 0);
                    item.VarObject = cat;
                    //item.BackgroundColor = new Color(100, 100, 100);
                    Image imm = new Image();

                    var imageSource = new UriImageSource { Uri = new Uri(cat.iconURL) };
                    imageSource.CachingEnabled = true;
                    imageSource.CacheValidity = new TimeSpan(0, 1, 0, 0);
                    imm.Source = imageSource;
                    imm.VerticalOptions = LayoutOptions.FillAndExpand;
                    imm.HorizontalOptions = LayoutOptions.FillAndExpand;
                    imm.Aspect = Aspect.AspectFit;
                    imm.HeightRequest = 80;
                    imm.WidthRequest = 80;


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
                    inlabel.FontSize = 20;
                    inlabel.HorizontalTextAlignment = TextAlignment.Start;
                    inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                    inlabel.VerticalTextAlignment = TextAlignment.Center;
                    inlabel.Padding = new Thickness(10, 0, 0, 0);
                    inlabel.TextColor = Color.Gray;
                    item.Children.Add(inlabel);


                    item.addEventGrind(new EventONCategorySelect());

                    addLinea();

                    inGrid.addInPosition(row, 0, item);




                    //this.inGrid.RowSpacing = 0;
                    //"get_app_dock?devicedock=";
                    row++;

                }

                if (lista_categorie.Count > 0)
                {
                    addLinea();
                }
                else
                {
                    _ = LoadArticlesAsync();
                }


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
            inGrid.addViewInPosition(row, 0, box);
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


                        ItemArticles itemart = new ItemArticles(art, this.Navigation, inClient);

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
