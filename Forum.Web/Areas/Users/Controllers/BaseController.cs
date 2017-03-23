using Forum.Data;
using Forum.Web.Common;
using System;
using System.Web.Mvc;

namespace Forum.Web.Areas.Users.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUowData data;

        public BaseController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(WebConstants.IUowDataNullMessage, "data");
            }

            this.data = data;
        }

        public IUowData Data
        {
            get { return this.data; }
        }
    }
}