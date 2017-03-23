﻿using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;
using Forum.Data;
using System.Linq;
using Forum.Web.Common;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IUowData data;

        public AnswerController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        // GET: Forum/Answer
        public ActionResult Index()
        {
            return PartialView("_Answer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Answer answer, int? id, string title)
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
                var answersCount = this.data.Answers.All().Count(a => a.ThreadId == id  && a.IsVisible == true);
                var page = (answersCount / WebConstants.PageSize) + (answersCount % WebConstants.PageSize == 0 ? 0 : 1);
                return RedirectToAction("Index", "Thread", new { id = id, title = title, page = page });
            }

            return this.View(answer);
        }
    }
}