using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using GreatNews.Repository;

namespace GreatNews
{
    public class CommentRepository : EntityFrameworkRepository<Comment>
    {
        public CommentRepository(ApplicationContext _context) : base(_context)
        {
        }
    }
}
