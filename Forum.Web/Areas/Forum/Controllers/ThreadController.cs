using System.Linq;
using System.Net;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Models.Forum;
using Forum.Models;
using Microsoft.AspNet.Identity;
using System;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class ThreadController : BaseController
    {
        public ThreadController(IUowData data) : base(data)
        {
        }

        // GET: Forum/Thread
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thread = this.Data.Threads.GetById(id);

            if (thread == null)
            {
                return HttpNotFound();
            }

            var answers = this.Data.Answers.All()
                .Where(a => a.ThreadId == id)
                .ToArray();

            var model = new ThreadAnswersViewModel()
            {
                Answers = answers,
                Thread = thread
            };

            return this.View(model);
        }

        public ActionResult Cancel()
        {
            return this.Content(string.Empty);
        }

    }
}