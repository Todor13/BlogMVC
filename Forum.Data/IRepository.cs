using System.Collections.Generic;

namespace Forum.Data
{
    public interface IRepository<T>
     where T : class
    {
        IEnumerable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
