using Forum.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Forum.Data
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IForumDbContext
    {
        public ForumDbContext()
            :base("ForumConnection")
        {
        }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Section> Sections { get; set; }

        public IDbSet<Thread> Threads { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static ForumDbContext Create()
        {
            return new ForumDbContext();
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
