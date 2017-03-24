using System;
using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNet.Identity;
using Forum.Web.Common;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class CreateController : Controller
    {
        private readonly IUowData data;

        public CreateController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException(WebConstants.IUowDataNullMessage, "data");
            }

            this.data = data;
        }

        // GET: Forum/Create
        public ActionResult Index()
        {
            var sections = this.data.Sections.All().ToArray();
            ViewBag.SectionId = new SelectList(sections, "Id", "Name");

            return this.View();
        }

        // POST: Forum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Thread thread)
        {
            if (ModelState.IsValid)
            {
                thread.Published = DateTime.Now;
                thread.IsVisible = true;
                thread.UserId = User.Identity.GetUserId();
                this.data.Threads.Add(thread);
                this.data.SaveChanges();
                return RedirectToAction("Index", "Thread", new { id = thread.Id, title = thread.Title });
            }

            return this.View(thread);
        }
    }
}