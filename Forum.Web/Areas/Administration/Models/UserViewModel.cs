using Forum.Models;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Administration.Models
{
    public class UserViewModel
    {
        public static Expression<Func<ApplicationUser, UserViewModel>> FromUser
        {
            get
            {
                return user => new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}