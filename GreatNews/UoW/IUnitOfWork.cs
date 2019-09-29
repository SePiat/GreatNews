using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using GreatNews.Repository;

namespace GreatNews.UoW
{
   public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<News> News_ { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Comment> Comments { get; }

        void Save();
    }
}
