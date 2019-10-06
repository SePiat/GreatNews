using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityPackSample.Services
{
    interface IArticleService
    {
        IEnumerable<News> GetFromUrl(string url);
        News GetById(Guid id);
        bool Add(News article);
    }
}
