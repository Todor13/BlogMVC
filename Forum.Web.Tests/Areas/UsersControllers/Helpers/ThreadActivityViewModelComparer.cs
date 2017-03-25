using Forum.Web.Areas.Users.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.UsersControllers.Helpers
{
    class ThreadActivityViewModelComparer : IComparer, IComparer<ThreadActivityViewModel>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as ThreadActivityViewModel;
            var rhs = y as ThreadActivityViewModel;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(ThreadActivityViewModel x, ThreadActivityViewModel y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }
            else if (x.Published.CompareTo(y.Published) != 0)
            {
                return x.Published.CompareTo(y.Published);
            }
            else if (x.Title.CompareTo(y.Title) != 0)
            {
                return x.Title.CompareTo(y.Title);
            }
            else if (x.Content.CompareTo(y.Content) != 0)
            {
                return x.Content.CompareTo(y.Content);
            }
            else
            {
                return 0;
            }; 
        }
    }
}
