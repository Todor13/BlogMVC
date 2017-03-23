using Forum.Models;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class AnswerActivityViewModel
    {
        private const int Length = 100;

        public static Expression<Func<Answer, AnswerActivityViewModel>> FromAnswer
        {
            get
            {
                return answer => new AnswerActivityViewModel
                {
                    Id = answer.Id,
                    Content = answer.Content.Substring(0, Length),
                    Published = answer.Published
                };
            }
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
    }
}