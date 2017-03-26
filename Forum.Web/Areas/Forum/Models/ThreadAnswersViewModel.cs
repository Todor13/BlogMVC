using Forum.Models;
using Forum.Web.Areas.Forum.Models;
using Forum.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Forum
{
    public class ThreadAnswersViewModel
    {
        public IEnumerable<AnswerViewModel> Answers { get; set; }

        public ThreadViewModel Thread { get; set; }

        public PagerViewModel PageCounter { get; set; }
    }
}