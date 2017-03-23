using Forum.Models;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class ThreadActivityViewModel
    {
        private const int Length = 100;

        public static Expression<Func<Thread, ThreadActivityViewModel>> FromThread
        {
            get
            {
                return thread => new ThreadActivityViewModel
                {
                    Id = thread.Id,
                    Title = thread.Title.Substring(0, Length),
                    Content = thread.Content.Substring(0, Length),
                    Published = thread.Published
                };
            }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
    }
}