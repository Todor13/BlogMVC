using Forum.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Forum.Data
{
    public interface IForumDbContext
    {
        IDbSet<Answer> Answers { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Section> Sections { get; set; }

        IDbSet<Thread> Threads { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}