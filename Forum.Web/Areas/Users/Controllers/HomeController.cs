using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Common;
using System.Collections.Generic;
using System;
using Forum.Web.Factories;
using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Areas.Users.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUowData data, IPagerViewModelFactory pagerModelFactory)
            : base(data, pagerModelFactory)
        {
        }

        public ActionResult Index(int page = 1)
        {
            var users = this.Data.Users.All()
                .OrderBy(u => u.Email)
                .Skip((page - 1) * WebConstants.UsersPageSize)
                .Take(WebConstants.UsersPageSize)
                .Select(UserViewModel.FromUser)
                .ToArray();

            var usersCount = this.Data.Users.All().Count();

            var pagingViewModel = this.PagerModelFactory.CreatePagerViewModel(WebConstants.HomeController, page, usersCount, WebConstants.UsersPageSize);

            var model = new Tuple<IEnumerable<UserViewModel>, IPagerViewModel>(users, pagingViewModel);

            return this.View(model);
        }
    }
}