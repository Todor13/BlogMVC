﻿using Forum.Web.Areas.Users.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.UsersControllers.Helpers
{
    public class AnswerActivityViewModelComparer : IComparer, IComparer<AnswerActivityViewModel>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as AnswerActivityViewModel;
            var rhs = y as AnswerActivityViewModel;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(AnswerActivityViewModel x, AnswerActivityViewModel y)
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
            else if (x.ThreadTitle.CompareTo(y.ThreadTitle) != 0)
            {
                return x.ThreadTitle.CompareTo(y.ThreadTitle);
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
