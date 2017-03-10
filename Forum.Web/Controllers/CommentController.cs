using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index()
        {
            return PartialView("_Comment");
        }
    }
}