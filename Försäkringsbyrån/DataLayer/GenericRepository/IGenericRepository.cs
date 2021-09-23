using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public interface IGenericRepository<T> where T : class 
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);   //kan använda en lambda-expression för att filtrera en list, på samma sätt som vi gjort i MSQL = från Mosh youtube
        T Get(object id);

        void Insert(T obj);

        void InsertRange(IEnumerable<T> entitet);

        void Delete(T id);
        void DeleteRange(IEnumerable<T> entitet);

        void Save();
        void Update(T obj);


    }
}
