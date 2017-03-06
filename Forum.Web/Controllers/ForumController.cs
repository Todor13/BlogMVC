using Forum.Data;
using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net;
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

        public ActionResult Index()
        {
            var threads = this.data.Threads.All().ToArray();

            return this.View(threads);
        }

        public ActionResult Create()
        {
            var sections = this.data.Sections.All().ToArray();
            ViewBag.SectionId = new SelectList(sections, "Id", "Name");

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Thread thread)
        {
            if (ModelState.IsValid)
            {
                thread.Published = DateTime.Now;
                thread.IsVisible = true;
                this.data.Threads.Add(thread);
                this.data.SaveChanges();
                return RedirectToAction("Index");
            }

            return this.ViewBag(thread);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thread = this.data.Threads.GetById(id);

            if (thread == null)
            {
                return HttpNotFound();
            }

            return this.View(thread);
        }
    }
}