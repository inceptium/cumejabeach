using System;
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
            item.VerticalOptions = LayoutOptions.FillAndExpand;
            item.HorizontalOptions = LayoutOptions.Start;
            item.HeightRequest = 80;

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
            imm.HorizontalOptions = LayoutOptions.CenterAndExpand;
            imm.Aspect = Aspect.AspectFit;
            imm.HeightRequest = 80;
            imm.WidthRequest = 80;


            item.Children.Add(imm);
            Label inlabel = new Label();
            inlabel.Text = ArticleSelectd.description;
            inlabel.HorizontalTextAlignment = TextAlignment.Start;
            inlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
            inlabel.VerticalTextAlignment = TextAlignment.Center;
            inlabel.TextColor = Color.Gray;
            item.Children.Add(inlabel);


            item.addEventGrind(new EventONArticleSelect());
            return item;
        }
    }
}
