using System.Collections.Generic;

namespace Forum.Web.Models.Contracts
{
    public interface IHomePageViewModel
    {
        ICollection<IndexPageThreadViewModel> Important { get; set; }
        ICollection<IndexPageThreadViewModel> MostDiscussed { get; set; }
        ICollection<IndexPageThreadViewModel> Newest { get; set; }
    }
}