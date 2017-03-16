using Forum.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Web.Tests.Areas.ForumControllers.Helpers
{
    public class AnswerComparer : IComparer, IComparer<Answer>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as Answer;
            var rhs = y as Answer;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Answer x, Answer y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }
            else if (x.Published.CompareTo(y.Published) != 0)
            {
                return x.Published.CompareTo(y.Published);
            }
            else if (x.IsVisible.CompareTo(y.IsVisible) != 0)
            {
                return x.IsVisible.CompareTo(y.IsVisible);
            }
            else if (x.Content.CompareTo(y.Content) != 0)
            {
                return x.Content.CompareTo(y.Content);
            }
            else
            {
                return 0;
            }
        }
    }
}
