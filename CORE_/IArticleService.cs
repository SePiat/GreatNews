using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GreatNews.Services
{
    public interface IArticleService<T> where T : class
    {
        IEnumerable<T> GetFromUrl();
        T GetById(Guid id);
        bool Add(T article);
    }
}
