using Forum.Models;
using Forum.Web.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class CommentActivityViewModel
    {
        private string content;

        public static Expression<Func<Comment, CommentActivityViewModel>> FromComment
        {
            get
            {
                return comment => new CommentActivityViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    Published = comment.Published,
                    ThreadId = comment.Answer.ThreadId,
                    ThreadTitle = comment.Answer.Thread.Title
                };
            }
        }

        public int Id { get; set; }
        public DateTime Published { get; set; }
        public int ThreadId { get; set; }
        public string ThreadTitle { get; set; }

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