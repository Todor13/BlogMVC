using Forum.Models;
using Forum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class UserViewModel
    {
        public static Expression<Func<ApplicationUser, UserViewModel>> FromUser
        {
            get
            {
                return user => new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Threads = user.Threads.AsQueryable().Select(ThreadsViewModel.FromThread),
                    Answers = user.Answers.AsQueryable().Select(AnswersViewModel.FromAnswer),
                    Comments = user.Comments.AsQueryable().Select(CommentsViewModel.FromComment)
                };
            }
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<ThreadsViewModel> Threads { get; set; }

        public IEnumerable<AnswersViewModel> Answers { get; set; }

        public IEnumerable<CommentsViewModel> Comments { get; set; }
    }
}