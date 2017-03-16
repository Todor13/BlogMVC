﻿using Forum.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Web.Tests.Areas.ForumControllers.HomeControllerTests.Helpers
{
    public class ThreadComparer : IComparer, IComparer<Thread>
    {
        public int Compare(object x, object y)
        {
            var lhs = x as Thread;
            var rhs = y as Thread;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Thread x, Thread y)
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
            }
        }
    }
}