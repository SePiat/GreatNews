﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using AgilityPackSample.Services;
using GreatNews.Models;
using GreatNews.UoW;
using HtmlAgilityPack;

namespace GreatNews.Services
{
    public class ArticleServiceOnliner : IHtmlArticleServiceOnliner
    {
        private readonly string SrsUrl = @"https://tech.onliner.by/feed";
        private readonly IUnitOfWork _unitOfWork;

        public ArticleServiceOnliner(IUnitOfWork unitOfWork)
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
                        PositiveIndex = 0,
                        Content = GetTextOfNews(article.Links.FirstOrDefault().Uri.ToString())
                    });
                }
            }


            return news;
        }

        private int i;
        public string GetTextOfNews(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var node = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div");
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
        public bool Add(News article)
        {
            throw new NotImplementedException();
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
