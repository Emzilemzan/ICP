using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public GenericRepository(DbContext context)
        {
            Context = context; 
        }
        public void Delete(T id)
        {
            Context.Set<T>().Remove(id);
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public virtual T Get(object id)
        {
            return Context.Set<T>().Find(id);
        }
        public void Insert(T obj)
        {
            Context.Set<T>().Add(obj);
        }
        public void Save()
        {
            throw new NotImplementedException(); //app.SaveChanges();
        }

        public virtual void Update(T objToUpdate)
        {
            //Context.Attach(objToUpdate);
            //app.Entry(objToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public void InsertRange(IEnumerable<T> entitet)
        {
            Context.Set<T>().AddRange(entitet);
        }

        public void DeleteRange(IEnumerable<T> entitet)
        {
            Context.Set<T>().RemoveRange(entitet);
        }


    }
}