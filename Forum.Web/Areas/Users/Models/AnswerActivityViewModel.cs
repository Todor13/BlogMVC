using Forum.Models;
using Forum.Web.Common;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class AnswerActivityViewModel
    {
        public static Expression<Func<Answer, AnswerActivityViewModel>> FromAnswer
        {
            get
            {
                return answer => new AnswerActivityViewModel
                {
                    Id = answer.Id,
                    Content = answer.Content.Substring(0, WebConstants.ActivitySubString),
                    Published = answer.Published,
                    ThreadId = answer.ThreadId,
                    ThreadTitle = answer.Thread.Title
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