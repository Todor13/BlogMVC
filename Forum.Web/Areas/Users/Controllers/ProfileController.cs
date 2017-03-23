using Forum.Data;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Common;
using Forum.Web.Models.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Forum.Web.Areas.Users.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IUowData data) : base(data)
        {
        }

        // GET: Users/Profile
        public ActionResult Index(string id)
        {
            var user = this.Data.Users.GetById(id);
            
            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "There is no such user");
            }

            var userViewModel = new UserViewModel(user);

            return View(userViewModel);
        }

        public ActionResult GetUserThreads(string id, int page = 1)
        {
            var threads = this.Data.Threads.All()
                .Where(t => t.UserId == id && t.IsVisible == true)
                .OrderBy(t => t.Published)
                .Skip((page - 1) * WebConstants.ActivityPageSize)
                .Take(WebConstants.ActivityPageSize)
                .Select(ThreadActivityViewModel.FromThread)
                .ToArray();

            var threadsCount = this.Data.Threads.All().Count(t => t.UserId == id && t.IsVisible == true);

            var pageViwModel = new AjaxPagerViewModel()
            {
                CurrentPage = page,
                ItemsCount = threadsCount,
                ControllerName = WebConstants.Profile,
                ActionName = WebConstants.GetUserThreads,
                PageSize = WebConstants.ActivityPageSize,
                UpdateTarget = WebConstants.UpdateTarget
            };

            var model = new Tuple<IEnumerable<ThreadActivityViewModel>, AjaxPagerViewModel>(threads, pageViwModel);
           

            return PartialView("_Threads", model);
        }

        public ActionResult GetUserAnswers(string id)
        {
            var answers = this.Data.Answers.All()
                .Where(a => a.UserId == id && a.IsVisible == true)
                .Select(AnswerActivityViewModel.FromAnswer)
                .ToArray();

            return PartialView("_Answers", answers);
        }

        public ActionResult GetUserComments(string id)
        {
            var comments = this.Data.Comments.All()
                .Where(c => c.UserId == id && c.IsVisible == true)
                .Select(CommentActivityViewModel.FromComment)
                .ToArray();

            return PartialView("_Comments", comments);
        }

    }
}