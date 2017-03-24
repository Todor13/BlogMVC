using Forum.Data;
using Forum.Models;
using Forum.Web.Common;
using Forum.Web.Factories;
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
        private readonly IPagerViewModelFactory pagerModelFactory;
        protected const int PageSize = WebConstants.PageSize;

        public BaseController(IUowData data, IPagerViewModelFactory pagerModelFactory)
        {
            if (data == null)
            {
                throw new ArgumentException(WebConstants.IUowDataNullMessage, "data");
            }

            if (pagerModelFactory == null)
            {
                throw new ArgumentNullException(WebConstants.IPagerViewModelFactoryNullMessage, "pagerModelFactory");
            }

            this.data = data;
            this.pagerModelFactory = pagerModelFactory;
        }

        protected IUowData Data
        {
            get
            {
                return this.data;
            }
        }

        protected IPagerViewModelFactory PagerViewModelFactory
        {
            get
            {
                return this.pagerModelFactory;
            }
        }

        protected IndexPageViewModel CreateIndexPage(IEnumerable<Thread> threads, int page, int count, string controllerName = null)
        {
            var model = new IndexPageViewModel();
            //{
            //    Threads = threads,
            //    PageCounter = new PagerViewModel()
            //    {
            //        CurrentPage = page,
            //        PageSize = PageSize,
            //        ItemsCount = count,
            //        ControllerName = controllerName
            //    }
            //};

            return model;
        }
    }
}