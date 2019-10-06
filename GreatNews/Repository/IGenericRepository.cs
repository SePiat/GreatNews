using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatNews.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        void Insert(T obj);

        void InsertRange(List<T> obj);
        void Update(T obj);
        void Delete(object Id);
        IQueryable<T> AsQueryable();
    }
}
