using Forum.Data;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Common;
using Forum.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Forum.Web.Factories;
using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Areas.Users.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IUowData data, IPagerViewModelFactory pagerModelFactory) 
            : base(data, pagerModelFactory)
        {
        }

        public ActionResult Index(string id)
        {
            var user = this.Data.Users.GetById(id);

            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, WebConstants.UserNotFound);
            }

            var userRoles = new List<RoleViewModel>();

            foreach (var role in user.Roles)
            {
                userRoles.Add(new RoleViewModel(this.Data.Roles.GetById(role.RoleId)));
            }

            var userViewModel = new UserViewModel(user);
            userViewModel.Roles = userRoles;

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

            var pagerViwModel = this.PagerModelFactory.CreateAjaxPagerViewModel(WebConstants.Profile,
                WebConstants.GetUserThreads, WebConstants.UsersActivityUpdateTarget, page, threadsCount, WebConstants.ActivityPageSize);

            var model = new Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>(threads, pagerViwModel);

            return PartialView(WebConstants.ThreadsPartialView, model);
        }

        public ActionResult GetUserAnswers(string id, int page = 1)
        {
            var answers = this.Data.Answers.All()
                .Where(a => a.UserId == id && a.IsVisible == true)
                .OrderBy(t => t.Published)
                .Skip((page - 1) * WebConstants.ActivityPageSize)
                .Take(WebConstants.ActivityPageSize)
                .Select(AnswerActivityViewModel.FromAnswer)
                .ToArray();

            var answersCount = this.Data.Answers.All().Count(t => t.UserId == id && t.IsVisible == true);

            var pagerViwModel = this.PagerModelFactory.CreateAjaxPagerViewModel(WebConstants.Profile,
                WebConstants.GetUserAnswers, WebConstants.UsersActivityUpdateTarget, page, answersCount, WebConstants.ActivityPageSize);

            var model = new Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>(answers, pagerViwModel);

            return PartialView(WebConstants.AnswersPartialView, model);
        }

        public ActionResult GetUserComments(string id, int page = 1)
        {
            var comments = this.Data.Comments.All()
                .Where(c => c.UserId == id && c.IsVisible == true)
                .OrderBy(t => t.Published)
                .Skip((page - 1) * WebConstants.ActivityPageSize)
                .Take(WebConstants.ActivityPageSize)
                .Select(CommentActivityViewModel.FromComment)
                .ToArray();

            var commentsCount = this.Data.Comments.All().Count(c => c.UserId == id && c.IsVisible == true);

            var pagerViwModel = this.PagerModelFactory.CreateAjaxPagerViewModel(WebConstants.Profile,
                WebConstants.GetUserComments, WebConstants.UsersActivityUpdateTarget, page, commentsCount, WebConstants.ActivityPageSize);

            var model = new Tuple<IEnumerable<CommentActivityViewModel>, IAjaxPagerViewModel>(comments, pagerViwModel);

            return PartialView(WebConstants.CommentsPartialView, model);
        }
    }
}