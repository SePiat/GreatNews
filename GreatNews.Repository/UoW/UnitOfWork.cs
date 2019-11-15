using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using GreatNews.Repository;

namespace GreatNews.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsContext _context;
        private readonly IGenericRepository<News> _newsRepository;
        private readonly IGenericRepository<UserDB> _userRepository;
        private readonly IGenericRepository<Comment> _commentRepository;

        public UnitOfWork(NewsContext context,
            IGenericRepository<News> newsRepository,
            IGenericRepository<UserDB> userRepository,
            IGenericRepository<Comment> commentRepository)
        {
            _context = context;
            _newsRepository = newsRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }


        public IGenericRepository<News> News_ => _newsRepository;

        public IGenericRepository<UserDB> Users => _userRepository;

        public IGenericRepository<Comment> Comments => _commentRepository;

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();

        }
    }
}
