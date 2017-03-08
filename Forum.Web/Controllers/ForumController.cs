﻿using Forum.Data;
using Forum.Models;
using Forum.Web.Models.Common;
using Forum.Web.Models.Forum;
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
            var sections = this.data.Sections.All().ToArray();
            ViewBag.Sections = new SelectList(sections, "Id", "Name");

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
                this.data.Threads.Add(thread);
                this.data.SaveChanges();
                return RedirectToAction("Index");
            }

            return this.ViewBag();
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

            return this.View(thread);
        }

        public ActionResult Search(string query, int page = 1)
        {
            var sections = this.data.Sections.All().ToArray();
            ViewBag.Sections = new SelectList(sections, "Id", "Name");

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