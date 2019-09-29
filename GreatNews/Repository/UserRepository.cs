using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;

namespace GreatNews.Repository
{
    public class UserRepository : EntityFrameworkRepository<User>
    {
        public UserRepository(NewsContext _context) : base(_context)
        {
        }
    }
}
