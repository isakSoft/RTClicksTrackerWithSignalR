using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using com.ai.ext.upwork.test1.Hubs;

namespace com.ai.ext.upwork.test1.Models
{
    public class GenericUnitOfWork : IDisposable
    {
        private SampleDbContext context = null;

        public GenericUnitOfWork()
        {
            context = new SampleDbContext();
        }

        public Dictionary<Type, object> repos = new Dictionary<Type, object>();

        public IRepository<T> Repository<T>() where T : class
        {
            if(repos.Keys.Contains(typeof(T)) == true)
            {
                return repos[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new GenericRepository<T>(context);
            repos.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
            ClicksTrackerHub.SendClicks();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}