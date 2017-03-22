using Forum.Data;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
            var profile = this.Data.Users.All().Where(u => u.Id == id).Select(UserViewModel.FromUser).FirstOrDefault();

            if (user == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "There is no such user");
            }

            return View(user);
        }

    }
}