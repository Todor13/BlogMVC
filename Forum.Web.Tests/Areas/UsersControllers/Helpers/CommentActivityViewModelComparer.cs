using Forum.Web.Areas.Users.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.UsersControllers.Helpers
{
    public class CommentActivityViewModelComparer : IComparer, IComparer<CommentActivityViewModel>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as CommentActivityViewModel;
            var rhs = y as CommentActivityViewModel;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(CommentActivityViewModel x, CommentActivityViewModel y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }
            else if (x.Published.CompareTo(y.Published) != 0)
            {
                return x.Published.CompareTo(y.Published);
            }
            else if (x.ThreadId.CompareTo(y.ThreadId) != 0)
            {
                return x.ThreadId.CompareTo(y.ThreadId);
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
