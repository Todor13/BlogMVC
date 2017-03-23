using Forum.Models;
using System;
using System.Collections.Generic;

namespace Forum.Data
{
    public class UowData : IUowData
    {
        private readonly IForumDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData(IForumDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of IForumDbContext is required to use this repository.", "context");
            }

            this.context = context;
        }

        public IRepository<Thread> Threads
        {
            get
            {
                return this.GetRepository<Thread>();
            }
        }

        public IRepository<Answer> Answers
        {
            get
            {
                return this.GetRepository<Answer>();
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                return this.GetRepository<Comment>();
            }
        }

        public IRepository<Section> Sections
        {
            get
            {
                return this.GetRepository<Section>();
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<ApplicationRole> Roles
        {
            get
            {
                return this.GetRepository<ApplicationRole>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
