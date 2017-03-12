﻿using System.Collections.Generic;
using System.Linq;

namespace Forum.Data
{
    public interface IRepository<T>
     where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
