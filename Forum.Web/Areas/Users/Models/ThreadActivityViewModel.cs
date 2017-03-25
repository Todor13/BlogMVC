using Forum.Models;
using Forum.Web.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class ThreadActivityViewModel
    {
        private string content;

        public static Expression<Func<Thread, ThreadActivityViewModel>> FromThread
        {
            get
            {
                return thread => new ThreadActivityViewModel
                {
                    Id = thread.Id,
                    Title = thread.Title,
                    Content = thread.Content,
                    Published = thread.Published
                };
            }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Published { get; set; }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                if (value.Count() >= WebConstants.ActivitySubString)
                {
                    this.content = value.Substring(0, WebConstants.ActivitySubString);
                }
                else
                {
                    this.content = value;
                }
            }
        }
    }
}