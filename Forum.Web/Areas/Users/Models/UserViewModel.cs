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
                    Threads = user.Threads.AsQueryable().Select(ThreadViewModel.FromThread),
                    Answers = user.Answers.AsQueryable().Select(AnswerViewModel.FromAnswer),
                    Comments = user.Comments.AsQueryable().Select(CommentViewModel.FromComment)
                };
            }
        }

        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;
            Threads = user.Threads.Select(x => new ThreadViewModel(x));
            Answers = user.Answers.Select(x => new AnswerViewModel(x));
            Comments = user.Comments.Select(x => new CommentViewModel(x));
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<ThreadViewModel> Threads { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}