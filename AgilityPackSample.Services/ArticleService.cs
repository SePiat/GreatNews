using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatNews.News;


namespace AgilityPackSample.Services
{
    public class ArticleService : IHtmlArticleService
    {
        
        public bool Add(News article)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetArticleFromUrl(string url)
        {
            const string baseUrl = @"https://people.onliner.by/2019/10/02/sheremetevo-2";
            var web = new HtmlWeb();
            var doc = web.Load(baseUrl);

            var node = doc.DocumentNode.SelectSingleNode("/html/body/div");
            return null;
        }

        public News GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetFromUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}
