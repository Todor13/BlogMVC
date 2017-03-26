using Forum.Web.Areas.Forum.Models;
using Forum.Web.Areas.Forum.Models.Contracts;
using Forum.Web.Models.Common.Contracts;
using System.Collections.Generic;

namespace Forum.Web.Factories.Contracts
{
    public interface IViewModelFactory
    {
        IForumThreadViewModel CreateForumThreadViewModel(ThreadViewModel thread, IEnumerable<AnswerViewModel> answers, IPagerViewModel pagerViewModel);
    }
}
