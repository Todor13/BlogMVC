using Forum.Data;
using Forum.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUowData data;

        public CommentController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        // GET: Forum/Comment
        public ActionResult Index()
        {
            return PartialView("_Comment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Comment comment, int? id, int page, int threadId, string title )
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                comment.UserId = User.Identity.GetUserId();
                comment.Published = DateTime.Now;
                comment.IsVisible = true;
                comment.AnswerId = (int)id;
                this.data.Comments.Add(comment);
                this.data.SaveChanges();
                return RedirectToAction("Index", "Thread", new { id = threadId, title = title, page = page });
            }
            
            return this.View();
        }

        public ActionResult Cancel()
        {
            return this.Content(string.Empty);
        }
    }
}