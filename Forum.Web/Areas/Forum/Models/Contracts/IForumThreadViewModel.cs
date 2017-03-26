using System.Collections.Generic;
using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Areas.Forum.Models.Contracts
{
    public interface IForumThreadViewModel
    {
        IEnumerable<AnswerViewModel> Answers { get; set; }
        IPagerViewModel PagerViewModel { get; set; }
        ThreadViewModel Thread { get; set; }
    }
}