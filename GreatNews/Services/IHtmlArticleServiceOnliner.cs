using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilityPackSample.Services;
using GreatNews.Models;

namespace GreatNews.Services
{
    public interface IHtmlArticleServiceOnliner : IArticleService
    {
        IEnumerable<News> GetArticleFromUrl();
        bool AddRange(IEnumerable<News> news);
    }
}
