using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Data.Tests.Helpers
{
    public class FakeDbSet<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        ObservableCollection<TEntity> collection;
        IQueryable query;

        public FakeDbSet()
        {
            collection = new ObservableCollection<TEntity>();
            query = collection.AsQueryable();
        }

        public TEntity Add(TEntity entity)
        {
            collection.Add(entity);
            return entity;
        }


        public TEntity Attach(TEntity entity)
        {
            collection.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TEntity> Local
        {
            get { return collection; }
        }

        public TEntity Remove(TEntity entity)
        {
            collection.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public Type ElementType
        {
            get { return query.ElementType; }
        }

        public Expression Expression
        {
            get { return query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return query.Provider; }
        }
    }
}
