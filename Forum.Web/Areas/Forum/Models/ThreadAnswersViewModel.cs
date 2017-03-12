using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Web.Models.Forum
{
    public class ThreadAnswersViewModel
    {
        public IEnumerable<Answer> Answers { get; set; }

        public Thread Thread { get; set; }
    }
}