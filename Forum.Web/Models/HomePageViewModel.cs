using Forum.Web.Models.Contracts;
using System.Collections.Generic;

namespace Forum.Web.Models
{
    public class HomePageViewModel : IHomePageViewModel
    {
        public HomePageViewModel(ICollection<IndexPageThreadViewModel> newest, ICollection<IndexPageThreadViewModel> mostDiscussed,
            ICollection<IndexPageThreadViewModel> important)
        {
            this.Newest = newest;
            this.MostDiscussed = mostDiscussed;
            this.Important = important;
        }

        public ICollection<IndexPageThreadViewModel> Newest{ get; set; }

        public ICollection<IndexPageThreadViewModel> MostDiscussed { get; set; }

        public ICollection<IndexPageThreadViewModel> Important { get; set; }
    }
}