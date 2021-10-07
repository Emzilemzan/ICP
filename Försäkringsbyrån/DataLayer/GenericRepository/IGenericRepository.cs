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
   public interface IGenericRepository<TEntity> where TEntity : class 
    {
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// lamdaexpression(linq) can be used. 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);

    }
}
