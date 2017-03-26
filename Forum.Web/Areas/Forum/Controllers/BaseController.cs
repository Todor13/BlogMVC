using Forum.Data;
using Forum.Web.Common;
using Forum.Web.Factories;
using System;
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
    }
}