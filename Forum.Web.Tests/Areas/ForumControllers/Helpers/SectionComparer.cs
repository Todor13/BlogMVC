using Forum.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.ForumControllers.Helpers
{
    public class SectionComparer : IComparer, IComparer<Section>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as Section;
            var rhs = y as Section;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Section x, Section y)
        {
            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }
            else if (x.Name.CompareTo(y.Name) != 0)
            {
                return x.Name.CompareTo(y.Name);
            }
            else
            {
                return 0;
            }
        }
    }
}
