using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Areas.Users.Models;

namespace Forum.Web.Areas.Users.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUowData data) : base(data)
        {
        }

        public ActionResult Index()
        {
            var users = this.Data.Users.All()
                .OrderBy(u => u.Email)
                .Select(UserViewModel.FromUser)
                .ToArray();         

            return this.View(users);
        }
    }
}