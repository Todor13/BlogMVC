using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Data;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class SearchController : BaseController
    {
        public SearchController(IUowData data) : base(data)
        {
        }

        // GET: Forum/Search
        public ActionResult Index(string query, int page = 1)
        {
            var count = this.Data.Threads.All()
                .Count(x => x.Title.ToLower().Contains(query.ToLower()) && x.Content.ToLower().Contains(query.ToLower()));

            var threads = this.Data.Threads.All()
                .Where(x => x.Title.ToLower().Contains(query.ToLower()) && x.Content.ToLower().Contains(query.ToLower()))
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToArray();

            var model = this.CreateIndexPage(threads, page, count);

            return this.View(model);
        }
    }
}