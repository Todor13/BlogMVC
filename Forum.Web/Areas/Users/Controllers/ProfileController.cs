using Forum.Data;
using Forum.Web.Areas.Users.Models;
using Microsoft.AspNet.Identity;
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

        public ActionResult GetUserThreads(string id)
        {
            var threads = this.Data.Threads.All()
                .Where(t => t.UserId == id)
                .Select(ThreadActivityViewModel.FromThread)
                .ToArray();

            return PartialView("_Threads", threads);
        }

        public ActionResult GetUserAnswers(string id)
        {
            var answers = this.Data.Answers.All()
                .Where(a => a.UserId == id)
                .Select(AnswerActivityViewModel.FromAnswer)
                .ToArray();

            return PartialView("_Answers", answers);
        }

    }
}