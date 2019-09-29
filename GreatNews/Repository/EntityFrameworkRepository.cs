using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatNews.Models;
using Microsoft.EntityFrameworkCore;

namespace GreatNews.Repository
{
    public class EntityFrameworkRepository<T>: IGenericRepository<T> where T : DataBaseEntity
    {
        public NewsContext _context;
        private readonly DbSet<T> _table;

        public EntityFrameworkRepository(NewsContext _context)
        {
            this._context = _context;
            this._table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object Id)
        {
            return _table.Find(Id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object Id)
        {
            T existing = _table.Find(Id);
            _table.Remove(existing);
        }

        public IQueryable<T> AsQueryable()
        {
            return _table.AsQueryable();
        }


    }
}
