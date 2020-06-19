using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.httpclient;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;

namespace CumejaRing.NavData
{
    public class MyNavigatorDataSingleArticle : INNavigationGrid
    {
        public ArticlesRegistry ArticleSelected { get; set; }

        public MyNavigatorDataSingleArticle(INHTTPClient client) : base(client)
        {
            First_Stack.VerticalOptions = LayoutOptions.FillAndExpand;
        }

        public async Task LoadCard()
        {

            ///carico immegine
            INItemGrid item = new INItemGrid("", this.Navigation, this.inClient);
            item.Orientation = StackOrientation.Vertical;
            item.VerticalOptions = LayoutOptions.FillAndExpand;
            item.HorizontalOptions = LayoutOptions.CenterAndExpand;

            item.HeightRequest = 1500;


            //item.BackgroundColor = new Color(100, 100, 100);
            StackLayout contenitoreImmagine = new StackLayout();
            contenitoreImmagine.HorizontalOptions = LayoutOptions.CenterAndExpand;

            Image imm = new Image();

            if (ArticleSelected.urlLink.Length > 0)
            {
                var imageSource = new UriImageSource { Uri = new Uri(ArticleSelected.urlLink) };
                imageSource.CachingEnabled = true;
                imageSource.CacheValidity = new TimeSpan(0, 1, 0, 0);
                imm.Source = imageSource;
            }
            
            imm.VerticalOptions = LayoutOptions.FillAndExpand;
            imm.HorizontalOptions = LayoutOptions.CenterAndExpand;
            imm.Aspect = Aspect.AspectFit;
            imm.HeightRequest = 200;
            contenitoreImmagine.Children.Add(imm);
            //---------------------------------------------


            item.Children.Add(contenitoreImmagine);

            //titolo articolo
            Label inlabel_titolo = new Label();
            inlabel_titolo.Text = ArticleSelected.description;
            inlabel_titolo.HorizontalTextAlignment = TextAlignment.Start;
            inlabel_titolo.HorizontalOptions = LayoutOptions.CenterAndExpand;
            inlabel_titolo.VerticalTextAlignment = TextAlignment.Center;
            inlabel_titolo.TextColor = Color.Gray;
            inlabel_titolo.FontAttributes = FontAttributes.Bold;
            inlabel_titolo.HeightRequest = 60;

            item.Children.Add(inlabel_titolo);

           
            // description
            Label inlabel_des = new Label();
            inlabel_des.Text = GetTextFromBase64(ArticleSelected.note_b64);
            inlabel_des.HorizontalTextAlignment = TextAlignment.Start;
            inlabel_des.HorizontalOptions = LayoutOptions.CenterAndExpand;
            inlabel_des.VerticalTextAlignment = TextAlignment.Center;
            inlabel_des.TextColor = Color.Gray;
            inlabel_des.Padding = new Thickness(20, 0, 20, 0);

            item.Children.Add(inlabel_des);


            //prezzo
            Label inlabelprice = new Label();
            var culture = CultureInfo.CreateSpecificCulture("it-EU");
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";

            inlabelprice.Text = ArticleSelected.sellingPrice.ToString("C", culture);
            inlabelprice.HorizontalTextAlignment = TextAlignment.Center;
            inlabelprice.HorizontalOptions = LayoutOptions.Center;
            inlabelprice.VerticalTextAlignment = TextAlignment.Center;
            inlabelprice.TextColor = Color.Gray;

            item.Children.Add(inlabelprice);

            //// description
            //Button bt_documentazione = new Button();
            //bt_documentazione.Text = "  Documentazione  ";
            //bt_documentazione.BorderWidth = 1;
            //bt_documentazione.BorderColor = Color.Silver;
            //bt_documentazione.HorizontalOptions = LayoutOptions.CenterAndExpand;


            //item.Children.Add(bt_documentazione);
            ////--------------------------------------


            ////entri quantitità
            //StackLayout sta_quantita = new StackLayout();
            //sta_quantita.Orientation = StackOrientation.Horizontal;
            //sta_quantita.HorizontalOptions = LayoutOptions.CenterAndExpand;

            //Label lb_qua = new Label();
            //lb_qua.Text = "Quantità";
            //lb_qua.VerticalTextAlignment = TextAlignment.Center;
            //Entry entru_qua = new Entry();
            //entru_qua.Text = "0";
            //sta_quantita.Children.Add(lb_qua);
            //sta_quantita.Children.Add(entru_qua);

            //item.Children.Add(sta_quantita);

            inGrid.addInPosition(0, 0, item);
            //Label inlabel = new Label();
            //inlabel.Text = art.description;
            //inlabel.HorizontalTextAlignment = TextAlignment.Start;
            //inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            //inlabel.VerticalTextAlignment = TextAlignment.Center;
            //inlabel.TextColor = Color.Gray;
            //item.Children.Add(inlabel);
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }


        public static string GetTextFromBase64(String base64Text)
        {
            byte[] data = Convert.FromBase64String(base64Text);
            string decodedString = Encoding.UTF8.GetString(data);
            return StripHTML(decodedString);
        }


    }
}

