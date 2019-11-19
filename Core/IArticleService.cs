using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreatNews.Models;

namespace AgilityPackSample.Services
{
    public interface IArticleService
    {
        IEnumerable<News> GetFromUrl();
        News GetById(Guid id);
        bool Add(News article);
    }
}
