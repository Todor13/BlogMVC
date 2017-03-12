using Forum.Data;
using Forum.Models;
using Forum.Web.Models.Common;
using Forum.Web.Models.Forum;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUowData data;
        protected const int PageSize = 3;

        public BaseController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        protected IUowData Data
        {
            get { return this.data; }
        }

        protected IndexPageViewModel CreateIndexPage(IEnumerable<Thread> threads, int page, int count)
        {
            var pagesCount = (count / PageSize) + (count % PageSize == 0 ? 0 : 1);

            var model = new IndexPageViewModel
            {
                Threads = threads,
                PageCounter = new PagingViewModel()
                {
                    CurrentPage = page,
                    PagesCount = pagesCount
                }
            };

            return model;
        }
    }
}