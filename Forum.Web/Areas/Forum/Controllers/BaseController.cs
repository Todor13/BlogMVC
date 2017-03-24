using Forum.Data;
using Forum.Models;
using Forum.Web.Common;
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
        protected const int PageSize = WebConstants.PageSize;

        public BaseController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException(WebConstants.IUowDataNullMessage, "data");
            }

            this.data = data;
        }

        protected IUowData Data
        {
            get { return this.data; }
        }

        protected IndexPageViewModel CreateIndexPage(IEnumerable<Thread> threads, int page, int count, string controllerName = null)
        {
            var model = new IndexPageViewModel
            {
                Threads = threads,
                PageCounter = new PagingViewModel()
                {
                    CurrentPage = page,
                    PageSize = PageSize,
                    ItemsCount = count,
                    ControllerName = controllerName
                }
            };

            return model;
        }
    }
}