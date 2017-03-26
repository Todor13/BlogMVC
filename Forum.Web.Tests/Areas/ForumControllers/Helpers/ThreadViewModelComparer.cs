using Forum.Models;
using Forum.Web.Areas.Forum.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.ForumControllers.HomeControllerTests.Helpers
{
    public class ThreadViewModelComparer : IComparer, IComparer<ThreadViewModel>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as ThreadViewModel;
            var rhs = y as ThreadViewModel;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(ThreadViewModel x, ThreadViewModel y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }
            else if (x.Published.CompareTo(y.Published) != 0)
            {
                return x.Published.CompareTo(y.Published);
            }
            else if (x.AnswersCount.CompareTo(y.AnswersCount) != 0)
            {
                return x.AnswersCount.CompareTo(y.AnswersCount);
            }
            else if (x.Title.CompareTo(y.Title) != 0)
            {
                return x.Title.CompareTo(y.Title);
            }
            else if (x.Content.CompareTo(y.Content) != 0)
            {
                return x.Content.CompareTo(y.Content);
            }
            else if (x.SectionName.CompareTo(y.SectionName) != 0)
            {
                return x.SectionName.CompareTo(y.SectionName);
            }
            else if (x.UserId.CompareTo(y.UserId) != 0)
            {
                return x.UserId.CompareTo(y.UserId);
            }
            else
            {
                return 0;
            }
        }
    }
}
