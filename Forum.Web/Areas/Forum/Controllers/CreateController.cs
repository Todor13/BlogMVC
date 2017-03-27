using System;
using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNet.Identity;
using Forum.Web.Common;
using Forum.Web.Areas.Forum.Models;
using Forum.Services.Contracts;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class CreateController : Controller
    {
        private readonly IUowData data;
        private readonly IMappingService mappingService;

        public CreateController(IUowData data, IMappingService mappingService)
        {
            if (data == null)
            {
                throw new ArgumentException(WebConstants.IUowDataNullMessage, "data");
            }

            if (mappingService == null)
            {
                throw new ArgumentException(WebConstants.IMappingServiceNullMessage, "mappingService");
            }

            this.data = data;
            this.mappingService = mappingService;
        }

        // GET: Forum/Create
        [Authorize]
        public ActionResult Index()
        {
            var sections = this.data.Sections.All().ToArray();
            ViewBag.SectionId = new SelectList(sections, "Id", "Name");

            return this.View();
        }

        // POST: Forum/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Title, Content, SectionId")]CreateThreadViewModel threadViewModel)
        {
            if (ModelState.IsValid)
            {
                var thread = this.mappingService.Map<Thread>(threadViewModel);
                thread.Published = DateTime.Now;
                thread.IsVisible = true;
                thread.UserId = User.Identity.GetUserId();
                this.data.Threads.Add(thread);
                this.data.SaveChanges();
                return RedirectToAction(WebConstants.IndexAction, WebConstants.ThreadController, new { id = thread.Id, title = thread.Title });
            }

            return this.View(threadViewModel);
        }
    }
}