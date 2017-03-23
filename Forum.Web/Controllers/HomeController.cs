using Forum.Data;
using Forum.Web.Areas.Forum;
using Forum.Web.Common;
using Forum.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUowData data;

        public HomeController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        public ActionResult Index()
        {
            var newest = this.data.Threads.All()
                .OrderByDescending(t => t.Published)
                .Take(WebConstants.ThreadListCount)
                .ToArray();

            var mostDiscussed = this.data.Threads.All()
                .OrderByDescending(t => t.Answers.Count)
                .Take(WebConstants.ThreadListCount)
                .ToArray();

            var important = this.data.Threads.All()
                .Where(t => t.Section.Name == "Important")
                .OrderByDescending(x => x.Published)
                .OrderByDescending(x => x.Answers.Max(a => a.Published))
                .Take(WebConstants.ThreadListCount)
                .ToArray();

            var model = new HomePageViewModel()
            {
                Newest = newest,
                MostDiscussed = mostDiscussed,
                Important = important
            };

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}