using Forum.Models;
using System.Data.Entity;

namespace Forum.Data
{
    public class ForumDbContext : DbContext, IForumDbContext
    {
        public ForumDbContext()
            :base("ForumConnection")
        {
        }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Section> Sections { get; set; }

        public IDbSet<Thread> Threads { get; set; }

        public IDbSet<User> Users { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
