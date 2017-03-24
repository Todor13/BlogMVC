using Forum.Web.Areas.Users.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.UsersControllers.Helpers
{
    public class UserViewModelComparer : IComparer, IComparer<UserViewModel>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as UserViewModel;
            var rhs = y as UserViewModel;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(UserViewModel x, UserViewModel y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }
            else if (x.Email.CompareTo(y.Email) != 0)
            {
                return x.Email.CompareTo(y.Email);
            }
            else
            {
                return 0;
            }
        }
    }
}
