using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Forum.Web.Models
{
    public class ThreadViewModel
    {
        public static Expression<Func<Thread, ThreadViewModel>> FromThread
        {
            get
            {
                return thread => new ThreadViewModel
                {

                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }

        public string UserId { get; set; }

        public DateTime EditedOn { get; set; }

        public string EditedById { get; set; }
    }
}