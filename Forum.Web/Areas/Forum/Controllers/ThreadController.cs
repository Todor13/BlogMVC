using System.Linq;
using System.Net;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Common;
using Forum.Web.Factories;
using AutoMapper.QueryableExtensions;
using Forum.Web.Areas.Forum.Models;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class ThreadController : BaseController
    {
        public ThreadController(IUowData data, IPagerViewModelFactory pagerModelFactory) 
            : base(data, pagerModelFactory)
        {
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

            var count = this.Data.Answers.All()
                .Count(a => a.ThreadId == id && a.IsVisible == true);

            var pagesCount = (count / PageSize) + (count % PageSize == 0 ? 0 : 1);

            var model = new ThreadAnswersViewModel()
            {
                Answers = answers,
                Thread = thread,
                PageCounter = new PagerViewModel("Thread", page, count, 3)
            };

            return this.View(model);
        }
    }
}