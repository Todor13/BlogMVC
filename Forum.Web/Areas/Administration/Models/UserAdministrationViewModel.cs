using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Forum.Web.Areas.Administration.Models
{
    public class UserAdministrationViewModel
    {
        public Expression<Func<ApplicationUser, UserAdministrationViewModel>> FromUser
        {
            get
            {
                return user => new UserAdministrationViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = user.Roles
                };
            }
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<ApplicationUserRole> Roles { get; set; }
    }
}