using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace com.ai.ext.upwork.test1.Models
{
    public interface IRepository<T> where T : class
    {
        //Expression<> used for LINQ: Now the 'IQueryable' version of the 'Where' method will be used.
        //If not: Now the 'IEnumerable' version of the 'Where' method will be used. Here all table data is gathered
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Attach(T entity);
        void Delete(T entity);
    }
}