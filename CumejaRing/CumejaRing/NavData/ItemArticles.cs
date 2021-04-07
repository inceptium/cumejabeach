using System;
using System.Globalization;
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
            item.Orientation = StackOrientation.Horizontal;
            //item.VerticalOptions = LayoutOptions.FillAndExpand;
            //item.HorizontalOptions = LayoutOptions.Start;
            item.HeightRequest = 70;
            
            item.Padding = new Thickness(10, 0, 15, 0);

            item.VarObject = ArticleSelectd;
            //item.BackgroundColor = new Color(100, 100, 100);
            Image imm = new Image();

            if (ArticleSelectd.urlLink.Length > 0)
            {
                var imageSource = new UriImageSource { Uri = new Uri(ArticleSelectd.urlLink) };
                imageSource.CachingEnabled = true;
                imageSource.CacheValidity = new TimeSpan(0, 1, 0, 0);
                imm.Source = imageSource;
            }


            imm.VerticalOptions = LayoutOptions.FillAndExpand;
            imm.HorizontalOptions = LayoutOptions.Start;
            imm.Aspect = Aspect.AspectFit;
            imm.HeightRequest = 70;
            imm.WidthRequest = 70;


            item.Children.Add(imm);



            Label inlabel = new Label();
            inlabel.Text = ArticleSelectd.description;
            inlabel.FontSize = 12;
            inlabel.HorizontalTextAlignment = TextAlignment.Start;
            inlabel.HorizontalOptions = LayoutOptions.Start;
            inlabel.VerticalTextAlignment = TextAlignment.Start;
            inlabel.Padding = new Thickness(10, 0, 0, 0);
            inlabel.TextColor = Color.Gray;


            Label inlabelprice = new Label();
            var culture = CultureInfo.CreateSpecificCulture("it-EU");
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";

            inlabelprice.Text = ArticleSelectd.sellingPrice.ToString("C",culture);
           
            inlabelprice.HorizontalTextAlignment = TextAlignment.Center;
            inlabelprice.HorizontalOptions = LayoutOptions.End;
            inlabelprice.VerticalTextAlignment = TextAlignment.End;
            inlabelprice.TextColor = Color.Gray;

            StackLayout stakDes = new StackLayout();
            stakDes.HorizontalOptions = LayoutOptions.FillAndExpand;
            stakDes.VerticalOptions = LayoutOptions.CenterAndExpand;
            

            stakDes.Children.Add(inlabel);
            stakDes.Children.Add(inlabelprice);


            item.Children.Add(stakDes);


            item.addEventGrind(new EventONArticleSelect());
            return item;
        }
    }
}
