using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilityPackSample.Services
{
    public interface IHtmlArticleService : IArticleService
    {
        IEnumerable<News> GetArticleFromUrl(string url);
    }
}
