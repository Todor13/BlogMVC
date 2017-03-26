using System.Linq;
using System.Net;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Factories;
using AutoMapper.QueryableExtensions;
using Forum.Web.Areas.Forum.Models;
using Forum.Web.Common;
using Forum.Web.Factories.Contracts;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class ThreadController : BaseController
    {
        private readonly IViewModelFactory viewModelFactory;

        public ThreadController(IUowData data, IPagerViewModelFactory pagerModelFactory, IViewModelFactory viewModelFactory) 
            : base(data, pagerModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        // GET: Forum/Thread
        public ActionResult Index(int? id, int page = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thread = this.Data.Threads.All()
                .Where(t => t.Id == id && t.IsVisible == true)
                .ProjectTo<ThreadViewModel>()
                .SingleOrDefault();

            if (thread == null)
            {
                return HttpNotFound();
            }

            var answers = this.Data.Answers.All()
                .Where(a => a.ThreadId == id && a.IsVisible == true)
                .OrderBy(a => a.Published)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ProjectTo<AnswerViewModel>()
                .ToArray();

            var answersCount = this.Data.Answers.All()
                .Count(a => a.ThreadId == id && a.IsVisible == true);

            var pagerViewModel = this.PagerViewModelFactory.CreatePagerViewModel(WebConstants.ThreadController,
                page, answersCount, WebConstants.PageSize);

            var model = this.viewModelFactory.CreateForumThreadViewModel(thread, answers, pagerViewModel);

            return this.View(model);
        }
    }
}