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
                    Id = role.Id,
                    Name = role.Name
                };
            }
        }

        public RoleViewModel()
        {
        }

        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}