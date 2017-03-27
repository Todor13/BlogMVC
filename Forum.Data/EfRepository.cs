using Forum.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data
{
    public class EfRepository<T> : IRepository<T>
        where T : class
    {
        protected IForumDbContext context;
        protected IDbSet<T> dbSet;

        public EfRepository(IForumDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("An instance of IForumDbContext is required to use this repository.", "context");
            }

            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public void Add(T entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public IQueryable<T> All()
        {
            return this.dbSet;
        }

        public void Delete(T entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public T GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(T entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            entry.State = state;
        }
    }
}
