using Forum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                throw new ArgumentNullException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        public IUowData Data
        {
            get { return this.data; }
        }
    }
}