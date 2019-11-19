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
        private readonly ApplicationContext _context;
        private readonly IGenericRepository<News> _newsRepository;
        
        private readonly IGenericRepository<Comment> _commentRepository;

        public UnitOfWork(ApplicationContext context,
            IGenericRepository<News> newsRepository,
            
            IGenericRepository<Comment> commentRepository)
        {
            _context = context;
            _newsRepository = newsRepository;
            
            _commentRepository = commentRepository;
        }


        public IGenericRepository<News> News_ => _newsRepository;

        

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
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
