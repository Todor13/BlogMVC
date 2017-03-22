using Forum.Data;
using Forum.Web.Areas.Users.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Web.Areas.Administration.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUowData data;

        public UsersController(IUowData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("An instance of IUowData is required to use this repository.", "data");
            }

            this.data = data;
        }

        public ActionResult Index(string id)
        {
            var user = this.data.Users.GetById(id);

            return this.View("ById", user);
        }

        // GET: Administration/Users
        public ActionResult All()
        {
            var users = this.data.Users.All()
                .OrderBy(u => u.Email)
                .Select(UserViewModel.FromUser)
                .ToArray();

            return View(users);
        }
    }
}