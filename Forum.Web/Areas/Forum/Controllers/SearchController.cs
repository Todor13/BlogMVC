using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Factories;
using Forum.Web.Common;
using System.Collections.Generic;
using System;
using Forum.Web.Areas.Forum.Models;
using Forum.Web.Models.Common.Contracts;
using AutoMapper.QueryableExtensions;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class SearchController : BaseController
    {
        public SearchController(IUowData data, IPagerViewModelFactory pagerModelFactory) 
            : base(data, pagerModelFactory)
        {
        }

        // GET: Forum/Search
        public ActionResult Index(string query, int page = 1)
        {
            var threadsCount = this.Data.Threads.All()
                .Count(x => x.IsVisible == true && x.Title.ToLower().Contains(query.ToLower()) || x.Content.ToLower().Contains(query.ToLower()) && x.IsVisible == true);

            var threads = this.Data.Threads.All()
                .Where(x => x.IsVisible == true && x.Title.ToLower().Contains(query.ToLower()) || x.Content.ToLower().Contains(query.ToLower()) && x.IsVisible == true)
                .OrderBy(t => t.Published)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<ThreadViewModel>()
                .ToArray();

            var pagerViewModel = this.PagerViewModelFactory.CreatePagerViewModel(WebConstants.SearchController,
               page, threadsCount, WebConstants.PageSize);

            var model = new Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>(threads, pagerViewModel);

            return this.View(model);
        }
    }
}