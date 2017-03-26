using Forum.Web.Areas.Forum.Models.Contracts;
using Forum.Web.Models.Common.Contracts;
using System.Collections.Generic;

namespace Forum.Web.Areas.Forum.Models
{
    public class ForumThreadViewModel : IForumThreadViewModel
    {
        public ForumThreadViewModel(ThreadViewModel thread, IEnumerable<AnswerViewModel> answers, IPagerViewModel pagerViewModel)
        {
            this.Thread = thread;
            this.Answers = answers;
            this.PagerViewModel = pagerViewModel;
        }

        public IEnumerable<AnswerViewModel> Answers { get; set; }

        public ThreadViewModel Thread { get; set; }

        public IPagerViewModel PagerViewModel { get; set; }
    }
}