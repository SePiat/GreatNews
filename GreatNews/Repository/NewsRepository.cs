using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;

namespace GreatNews.Repository
{
    public class NewsRepository : EntityFrameworkRepository<News>
    {
        public NewsRepository(NewsContext _context) : base(_context)
        {
        }
    }
}
