using Forum.Data;
using Forum.Web.Common;
using Forum.Web.Factories;
using System;
using System.Web.Mvc;

namespace Forum.Web.Areas.Users.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUowData data;
        private readonly IPagerViewModelFactory pagerModelFactory;

        public BaseController(IUowData data, IPagerViewModelFactory pagerModelFactory)
        {
            if (data == null)
            {
                throw new ArgumentNullException(WebConstants.IUowDataNullMessage, "data");
            }

            if (pagerModelFactory == null)
            {
                throw new ArgumentNullException(WebConstants.IPagerViewModelFactoryNullMessage, "pagerModelFactory");
            }

            this.data = data;
            this.pagerModelFactory = pagerModelFactory;
        }

        public IUowData Data
        {
            get
            {
                return this.data;
            }
        }

        public IPagerViewModelFactory PagerModelFactory
        {
            get
            {
                return this.pagerModelFactory;
            }
        }
    }
}