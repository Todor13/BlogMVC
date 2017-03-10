using Forum.Data;
using Forum.Models;
using Forum.Web.Models.Common;
using Forum.Web.Models.Forum;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class ForumController : Controller
    {
        private const int PageSize = 3;
        private readonly IUowData data;

        public ForumController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        public ActionResult Index(int page = 1)
        {
            var count = this.data.Threads.All().Count();

            var threads = this.data.Threads.All()
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToArray();

            var model = this.CreateIndexPage(threads, page, count);

            return this.View(model);
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
                thread.UserId = User.Identity.GetUserId();
                this.data.Threads.Add(thread);
                this.data.SaveChanges();
                return RedirectToAction("Index");
            }

            return this.View(thread);
        }

        public ActionResult Threads(int? id)
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

            var answers = this.data.Answers.All()
                .Where(a => a.ThreadId == id)
                .ToArray();

            var model = new ThreadAnswersViewModel()
            {
                Answers = answers,
                Thread = thread
            };

            return this.View(model);
        }

        public ActionResult Search(string query, int page = 1)
        {
            var count = this.data.Threads.All()
                .Count(x => x.Title.ToLower().Contains(query.ToLower()) && x.Content.ToLower().Contains(query.ToLower()));

            var threads = this.data.Threads.All()
                .Where(x => x.Title.ToLower().Contains(query.ToLower()) && x.Content.ToLower().Contains(query.ToLower()))
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToArray();

            var model = this.CreateIndexPage(threads, page, count);

            return this.View(model);
        }

        public ActionResult Answer()
        {
            return PartialView("_Answer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer(Answer answer, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                answer.UserId = User.Identity.GetUserId();
                answer.Published = DateTime.Now;
                answer.IsVisible = true;
                answer.ThreadId = (int)id;
                this.data.Answers.Add(answer);
                this.data.SaveChanges();
                return RedirectToAction("Threads", new { id = id });
            }

            return this.View(answer);
        }

        private IndexPageViewModel CreateIndexPage(IEnumerable<Thread> threads, int page, int count)
        {
            var pagesCount = (count / PageSize) + (count % PageSize == 0 ? 0 : 1);

            var model = new IndexPageViewModel
            {
                Threads = threads,
                PageCounter = new PagingViewModel()
                {
                    CurrentPage = page,
                    PagesCount = pagesCount
                }
            };

            return model;
        }
    }
}