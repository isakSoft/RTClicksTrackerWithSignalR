using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace com.ai.ext.upwork.test1.Models
{
    public class GenericRepository<T> :IRepository<T> where T : class
    {
        private SampleDbContext context = null;
        DbSet<T> _dbSet;

        public GenericRepository(SampleDbContext _context)
        {
            context = _context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if(predicate != null)
            {
                return _dbSet.Where(predicate);
            }

            return _dbSet.AsEnumerable();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.First(predicate);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Attach(T entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }            
        }
        
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}