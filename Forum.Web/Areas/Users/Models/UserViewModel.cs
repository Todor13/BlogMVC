using Forum.Models;
using System;
using System.Collections.Generic;
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
                    Roles = user.Roles,
                    Threads = user.Threads,
                    Answers = user.Answers,
                    Comments = user.Comments
                };
            }
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<ApplicationUserRole> Roles { get; set; }

        public ICollection<Thread> Threads { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}