using Forum.Models;

namespace Forum.Data
{
    public interface IUowData
    {
        IRepository<Answer> Answers { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Section> Sections { get; }

        IRepository<Thread> Threads { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}