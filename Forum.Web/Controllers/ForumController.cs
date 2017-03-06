using Forum.Data;
using System;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IUowData data;

        public ForumController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        public ForumController()
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}