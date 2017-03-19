using System;
using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNet.Identity;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class CreateController : BaseController
    {
        public CreateController(IUowData data) : base(data)
        {
        }

        // GET: Forum/Create
        public ActionResult Index()
        {
            var sections = this.Data.Sections.All().ToArray();
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
                this.Data.Threads.Add(thread);
                this.Data.SaveChanges();
                return RedirectToAction("Index", "Thread", new { id = thread.Id, title = thread.Title });
            }

            return this.View(thread);
        }
    }
}