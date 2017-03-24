using Forum.Models;
using Forum.Web.Common;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class CommentActivityViewModel
    {
        public static Expression<Func<Comment, CommentActivityViewModel>> FromComment
        {
            get
            {
                return comment => new CommentActivityViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content.Substring(0, WebConstants.ActivitySubString),
                    Published = comment.Published,
                    ThreadId = comment.Answer.ThreadId,
                    ThreadTitle = comment.Answer.Thread.Title
                };
            }
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public int ThreadId { get; set; }
        public string ThreadTitle { get; set; }
    }
}