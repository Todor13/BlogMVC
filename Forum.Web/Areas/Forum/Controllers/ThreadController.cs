using System.Linq;
using System.Net;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Models.Forum;
using Forum.Web.Models.Common;

namespace Forum.Web.Areas.Forum.Controllers
{
    public class ThreadController : BaseController
    {
        private const int PegeSize = 3;

        public ThreadController(IUowData data) : base(data)
        {
        }

        // GET: Forum/Thread
        public ActionResult Index(int? id, int page = 1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var thread = this.Data.Threads.GetById(id);

            if (thread == null || thread.IsVisible == false)
            {
                return HttpNotFound();
            }

            var answers = this.Data.Answers.All()
                .Where(a => a.ThreadId == id && a.IsVisible == true && a.Comments.All(c => c.IsVisible == true))
                .OrderBy(a => a.Published)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToArray();

            var count = this.Data.Answers.All()
                .Count(a => a.ThreadId == id && a.IsVisible == true);

            var pagesCount = (count / PageSize) + (count % PageSize == 0 ? 0 : 1);

            var model = new ThreadAnswersViewModel()
            {
                Answers = answers,
                Thread = thread,
                PageCounter = new PagingViewModel()
                {
                    CurrentPage = page,
                    PagesCount = pagesCount
                }
            };

            return this.View(model);
        }
    }
}