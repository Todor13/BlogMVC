using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Common
{
    public class PagingViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public string ControllerName { get; set; }
    }
}