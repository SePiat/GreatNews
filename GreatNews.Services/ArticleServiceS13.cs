using GreatNews.Models;
using GreatNews.UoW;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace GreatNews.Services
{
    public class ArticleServiceS13 : IHtmlArticleServiceS13
    {
        private readonly string SrsUrl = @"http://s13.ru/rss";
        private readonly IUnitOfWork _unitOfWork;

        public ArticleServiceS13(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<News> GetArticleFromUrl()
        {
            //string baseUrl = url; 
            //var web = new HtmlWeb();
            //var doc = web.Load(baseUrl);
            //var node = doc.DocumentNode.SelectSingleNode("/html/body/div");

            XmlReader feedReader = XmlReader.Create(SrsUrl);
            SyndicationFeed feed = SyndicationFeed.Load(feedReader);

            List<News> news = new List<News>();

            if (feed != null)
            {
                foreach (var article in feed.Items)
                {
                    news.Add(new News()
                    {
                        Heading = article.Title.Text,
                        //Description = Regex.Replace(article.Summary.Text, "<.*?>", string.Empty),
                        Source = article.Links.FirstOrDefault().Uri.ToString(),
                        Date = article.PublishDate.UtcDateTime,
                        PositiveIndex =0/* GetPositivityIndex(GetTextOfNews(article.Links.FirstOrDefault().Uri.ToString()))*/,
                        Content = GetTextOfNews(article.Links.FirstOrDefault().Uri.ToString())
                    });
                }
            }
            return news;
        }
        /*public int GetPositivityIndex(string content)
        {
            public string test = "тилли мили трямдяи стол собака табурет жили были";
        public string cont = $"[{{\"text\":\"{test}\"}}]";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=40246ed5a95bc23c0fc92e98977b615dd94e2d1d");
    request.Content = new StringContent(cont, Encoding.UTF8, "application/json");//CONTENT-TYPE header
    var x = client.SendAsync(request).Result;

    var responce = x.Content.ReadAsStringAsync().ToString();


}

            return 5;
        }*/



        private int i;
        public string GetTextOfNews(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var node = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/ul/li/div/div");
            var text = node.Skip(1).Take(1).FirstOrDefault().InnerText;
            var mas = new string[] { "&ndash; ", "&ndash;", "&mdash; ", "&mdash;", "&nbsp; ", "&nbsp; ", "&nbsp;", "&laquo; ", "&laquo;", "&raquo; ", "&raquo;" };

            foreach (var item in mas)

            {
                text = text.Replace(item, "");
            }

            Regex.Replace(text, "<.*?>", string.Empty);

            return text;
        }

        public bool AddRange(IEnumerable<News> news)
        {

            foreach (var item in news)
            {
                if (_unitOfWork.News_.AsQueryable().Where(u => u.Heading.Contains(item.Heading)).Count() == 0)
                {
                    if (i < 10)
                        _unitOfWork.News_.Insert(item);
                    i++;
                }
            }


            return true;
        }
        public bool Add(News news)
        {
            if (_unitOfWork.News_.AsQueryable().Where(u => u.Heading.Contains(news.Heading)).Count() == 0)
            {
                _unitOfWork.News_.Insert(news);
            }
            else
            {
                return false;
            }

            return true;
        }

        public IEnumerable<News> GetFromUrl()
        {
            throw new NotImplementedException();
        }

        public News GetById(Guid id)
        {
            throw new NotImplementedException();
        }

      


    }
}
