using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    /// <summary>
    /// A generic Type defention that basicly is a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext dbContext) => _context = dbContext;
        public TEntity GetById(object id) => _context.Set<TEntity>().Find(id);
        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _context.Set<TEntity>().Where(predicate);
        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().AddRange(entities);
        public void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().RemoveRange(entities);

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Attach(entity);
            
        }
    }
} 