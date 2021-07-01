using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CumejaRing.jsonmodel;
using InceptiumAPI.com.inceptium.httpclient;
using InceptiumAPI.com.inceptium.nav;
using Xamarin.Forms;

namespace CumejaRing.NavData
{
    public class ItemArticles
    {
        private ArticlesRegistry ArticleSelectd;
        INavigation internalNav; INHTTPClient internalClient;
        public ItemArticles(ArticlesRegistry article, INavigation nav, INHTTPClient client)
        {
            ArticleSelectd = article;
            internalNav = nav;
            internalClient = client;

        }

        public INItemGrid getNewItemGridArticles()
        {

            INItemGrid item = new INItemGrid("", internalNav, internalClient);
            StackLayout stackArticle = item.contentLayout;
            stackArticle.HorizontalOptions = LayoutOptions.FillAndExpand;
            stackArticle.VerticalOptions = LayoutOptions.Start;
            stackArticle.Orientation = StackOrientation.Horizontal;
            stackArticle.Padding = new Thickness(10,10,10,10);

            

            item.internalFrame.BorderColor = Color.LightGray;
            item.contentLayout.Orientation = StackOrientation.Horizontal;
            item.contentLayout.VerticalOptions = LayoutOptions.FillAndExpand;
            //item.HorizontalOptions = LayoutOptions.Start;
            item.mainLayout.HeightRequest = 130;

            item.mainLayout.Padding = new Thickness(10 ,10, 10,10);

            item.VarObject = ArticleSelectd;
            //item.BackgroundColor = new Color(100, 100, 100);
            Image imm = new Image();

            try
            {
                if (ArticleSelectd.imageURL.Length > 0)
                {
                    var imageSource = new UriImageSource { Uri = new Uri(ArticleSelectd.imageURL) };
                    imageSource.CachingEnabled = true;
                    imageSource.CacheValidity = new TimeSpan(0, 1, 0, 0);
                    imm.Source = imageSource;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            


            imm.VerticalOptions = LayoutOptions.FillAndExpand;
            imm.HorizontalOptions = LayoutOptions.Start;
            imm.Aspect = Aspect.AspectFit;
            imm.HeightRequest = 120;
            imm.WidthRequest = 80;


            stackArticle.Children.Add(imm);
            //item.Children.Add(imm);

            Label inlabel = new Label();
            inlabel.Text = ArticleSelectd.description;
            inlabel.HorizontalTextAlignment = TextAlignment.Start;
            inlabel.HorizontalOptions = LayoutOptions.Start;
            inlabel.VerticalTextAlignment = TextAlignment.Start;
            inlabel.Padding = new Thickness(10, 0, 0, 0);
            inlabel.TextColor = Color.FromHex("585f6b");
            inlabel.FontAttributes = FontAttributes.Bold;
            inlabel.FontSize = 16;

            //Label inlabelNote = new Label();
            //inlabelNote.Text = GetTextFromBase64(ArticleSelectd.note_b64);
            //inlabelNote.HorizontalTextAlignment = TextAlignment.Start;
            //inlabelNote.HorizontalOptions = LayoutOptions.Start;
            //inlabelNote.VerticalTextAlignment = TextAlignment.Start;
            //inlabelNote.Padding = new Thickness(10, 0, 0, 0);
            //inlabelNote.TextColor = Color.Gray;
            //inlabelNote.FontSize = 12;
            //inlabelNote.HeightRequest = 60 - inlabel.Height;



            Label inlabelprice = new Label();
            var culture = CultureInfo.CreateSpecificCulture("it-EU");
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";

            inlabelprice.Text = ArticleSelectd.sellingPriceTotVat.ToString("C", culture);
            inlabelprice.HorizontalTextAlignment = TextAlignment.Center;
            inlabelprice.HorizontalOptions = LayoutOptions.End;
            inlabelprice.VerticalTextAlignment = TextAlignment.Start;
            inlabelprice.TextColor = Color.DarkRed;
            inlabelprice.FontAttributes = FontAttributes.Bold;
            inlabelprice.FontSize = 16;


            StackLayout stakDes = new StackLayout();
            stakDes.HorizontalOptions = LayoutOptions.FillAndExpand;
            stakDes.VerticalOptions = LayoutOptions.StartAndExpand;


            stakDes.Children.Add(inlabelprice);
            stakDes.Children.Add(inlabel);
            //stakDes.Children.Add(inlabelNote);



            stackArticle.Children.Add(stakDes);
            //item.Children.Add(stakDes);

            //Frame fra = new Frame();
            //fra.Content = stackArticle;
            //fra.HasShadow = false;
            //fra.BorderColor = Color.LightGray;
            //fra.BackgroundColor = Color.White;
            //fra.HorizontalOptions = LayoutOptions.FillAndExpand;
            //fra.VerticalOptions = LayoutOptions.CenterAndExpand;
            //fra.Margin = new Thickness(0);


           

            item.addEventGrind(new EventONArticleSelect());
            return item;
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
