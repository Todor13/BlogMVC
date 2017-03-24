using Forum.Data;
using System.Linq;
using System.Web.Mvc;
using Forum.Services.Contracts;
using Forum.Web.Factories;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUowData data, IPagerViewModelFactory pagerModelFactory) 
            : base(data, pagerModelFactory)
        {
        }

        public ActionResult Index(int page = 1)
        {
            var count = this.Data.Threads.All().Count(t => t.IsVisible == true);

            var threads = this.Data.Threads.All()
                .Where(t => t.IsVisible == true)
                .OrderBy(t => t.Published)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToArray();

            var model = this.CreateIndexPage(threads, page, count);

            return this.View(model);
        }
    }
}