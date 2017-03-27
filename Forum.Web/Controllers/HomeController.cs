using AutoMapper.QueryableExtensions;
using Forum.Data;
using Forum.Web.Common;
using Forum.Web.Factories.Contracts;
using Forum.Web.Models;
using System;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUowData data;
        private readonly IViewModelFactory viewModelFactory;

        public HomeController(IUowData data, IViewModelFactory viewModelFactory)
        {
            if (data == null)
            {
                throw new ArgumentNullException(WebConstants.IUowDataNullMessage, "data");
            }

            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(WebConstants.IViewModelFactoryNullMessage, "viewModelFactory");
            }

            this.data = data;
            this.viewModelFactory = viewModelFactory;
        }

        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult Index()
        {
            var newest = this.data.Threads.All()
                .OrderByDescending(t => t.Published)
                .Take(WebConstants.ThreadListCount)
                .ProjectTo<IndexPageThreadViewModel>()
                .ToArray();

            var mostDiscussed = this.data.Threads.All()
                .OrderByDescending(t => t.Answers.Count)
                .Take(WebConstants.ThreadListCount)
                .ProjectTo<IndexPageThreadViewModel>()
                .ToArray();

            var important = this.data.Threads.All()
                .Where(t => t.Section.Name == "Important")
                .OrderByDescending(x => x.Published)
                .OrderByDescending(x => x.Answers.Max(a => a.Published))
                .Take(WebConstants.ThreadListCount)
                .ProjectTo<IndexPageThreadViewModel>()
                .ToArray();

            var model = this.viewModelFactory.CreateHomePageViewModel(newest, mostDiscussed, important);

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