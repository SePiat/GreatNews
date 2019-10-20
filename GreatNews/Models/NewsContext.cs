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
        public DbSet<Comment> Comments_ { get; set; }
        public DbSet<UserDB> Users_ { get; set; }

        public NewsContext(DbContextOptions<NewsContext> options):base(options)
        {
            Database.EnsureCreated();
        }
    }
}
