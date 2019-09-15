using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GreatNews.Models
{
    public class NewsContext:DbContext
    {
        public DbSet<News> News_ { get; set; }
        public DbSet<Comments> Comments_ { get; set; }
        public DbSet<Users> Users_ { get; set; }

        public NewsContext(DbContextOptions<NewsContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
