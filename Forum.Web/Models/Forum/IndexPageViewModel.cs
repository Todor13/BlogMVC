using Forum.Models;
using System.Collections.Generic;

namespace Forum.Web.Models.Forum
{
    public class IndexPageViewModel
    {
        public IEnumerable<Thread> Threads { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}