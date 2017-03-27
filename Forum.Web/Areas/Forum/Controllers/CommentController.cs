using Forum.Data;
using Forum.Models;
using Forum.Web.Common;
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
                throw new ArgumentException(WebConstants.IUowDataNullMessage, "data");
            }

            this.data = data;
        }

        // GET: Forum/Comment
        [Authorize]
        public ActionResult Index()
        {
            return PartialView(WebConstants.CommentPartialView);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Content")]Comment comment, int? id, int threadId, string title, int page = 1)
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
                return RedirectToAction(WebConstants.IndexAction, WebConstants.ThreadController, new { id = threadId, title = title, page = page });
            }
            
            return this.View();
        }

        public ActionResult Cancel()
        {
            return this.Content(string.Empty);
        }
    }
}