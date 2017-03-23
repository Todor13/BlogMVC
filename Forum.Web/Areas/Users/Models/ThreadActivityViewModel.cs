using Forum.Models;
using Forum.Web.Common;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class ThreadActivityViewModel
    {
        public static Expression<Func<Thread, ThreadActivityViewModel>> FromThread
        {
            get
            {
                return thread => new ThreadActivityViewModel
                {
                    Id = thread.Id,
                    Title = thread.Title,
                    Content = thread.Content.Substring(0, WebConstants.ActivitySubString),
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