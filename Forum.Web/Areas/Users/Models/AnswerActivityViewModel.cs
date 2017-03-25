using Forum.Models;
using Forum.Web.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class AnswerActivityViewModel
    {
        private string content;

        public static Expression<Func<Answer, AnswerActivityViewModel>> FromAnswer
        {
            get
            {
                return answer => new AnswerActivityViewModel
                {
                    Id = answer.Id,
                    Content = answer.Content,
                    Published = answer.Published,
                    ThreadId = answer.ThreadId,
                    ThreadTitle = answer.Thread.Title
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