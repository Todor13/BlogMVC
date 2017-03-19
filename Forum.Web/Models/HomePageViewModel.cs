using Forum.Models;
using System.Collections.Generic;

namespace Forum.Web.Models
{
    public class HomePageViewModel
    {
        public ICollection<Thread> Newest{ get; set; }

        public ICollection<Thread> MostDiscussed { get; set; }

        public ICollection<Thread> Important { get; set; }
    }
}