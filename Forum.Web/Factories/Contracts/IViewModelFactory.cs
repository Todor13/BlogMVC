using Forum.Web.Areas.Forum.Models;
using Forum.Web.Areas.Forum.Models.Contracts;
using Forum.Web.Models;
using Forum.Web.Models.Common.Contracts;
using Forum.Web.Models.Contracts;
using System.Collections.Generic;

namespace Forum.Web.Factories.Contracts
{
    public interface IViewModelFactory
    {
        IForumThreadViewModel CreateForumThreadViewModel(ThreadViewModel thread, IEnumerable<AnswerViewModel> answers, IPagerViewModel pagerViewModel);

        IHomePageViewModel CreateHomePageViewModel(ICollection<IndexPageThreadViewModel> newest, ICollection<IndexPageThreadViewModel> mostDiscussed,
            ICollection<IndexPageThreadViewModel> important);
    }
}
