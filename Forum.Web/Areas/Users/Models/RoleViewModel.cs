using Forum.Models;
using System;
using System.Linq.Expressions;

namespace Forum.Web.Areas.Users.Models
{
    public class RoleViewModel
    {
        public static Expression<Func<ApplicationRole, RoleViewModel>> FromRole
        {
            get
            {
                return role => new RoleViewModel
                {
                    Name = role.Name
                };
            }
        }

        public string Name { get; set; }
    }
}