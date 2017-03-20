using Forum.Data;
using Forum.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Forum.Web.Areas.Administration.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUowData data;

        public UsersController(IUowData data)
        {
            this.data = data;
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

        public ActionResult ById(string id)
        {
            var user = this.data.Users.GetById(id);
                

            return this.View(user);
        }
    }
}