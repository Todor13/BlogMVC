using Forum.Models;
using Forum.Web.Areas.Forum.Models;
using Forum.Web.Models.Common;
using System.Collections.Generic;

namespace Forum.Web.Models.Forum
{
    public class IndexPageViewModel
    {
        public IEnumerable<Thread> Threads { get; set; }

        public PagingViewModel PageCounter { get; set; }
    }
}