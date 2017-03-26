using Forum.Data;
using System.Linq;
using System.Web.Mvc;
using Forum.Web.Factories;
using System;
using System.Collections.Generic;
using Forum.Web.Models.Common.Contracts;
using Forum.Web.Common;
using AutoMapper.QueryableExtensions;
using Forum.Web.Areas.Forum.Models;

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
            var threadsCount = this.Data.Threads.All().Count(t => t.IsVisible == true);

            var threads = this.Data.Threads.All()
                .Where(t => t.IsVisible == true)
                .OrderBy(t => t.Published)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<ThreadViewModel>()
                .ToArray();

            var pagerViewModel = this.PagerViewModelFactory.CreatePagerViewModel(WebConstants.HomeController,
                page, threadsCount, WebConstants.PageSize);

            var model = new Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>(threads, pagerViewModel);

            return this.View(model);
        }
    }
}