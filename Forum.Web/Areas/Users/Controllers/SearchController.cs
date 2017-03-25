using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Common;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Factories;
using Forum.Web.Models.Common.Contracts;

namespace Forum.Web.Areas.Users.Controllers
{
    public class SearchController : BaseController
    {
        public SearchController(IUowData data, IPagerViewModelFactory pagerModelFactory) 
            : base(data, pagerModelFactory)
        {
        }

        // GET: Users/Search
        public ActionResult Index(string query, int page = 1)
        {
            var users = this.Data.Users.All()
                .Where(u => u.Email.ToLower().Contains(query.ToLower()) || u.UserName.ToLower().Contains(query.ToLower()))
                .OrderBy(u => u.Email)
                .Skip((page - 1) * WebConstants.UsersPageSize)
                .Take(WebConstants.UsersPageSize)
                .Select(UserViewModel.FromUser)
                .ToArray();

            var usersCount = this.Data.Users.All().Count(u => u.Email.ToLower().Contains(query.ToLower()) || u.UserName.ToLower().Contains(query.ToLower()));

            var pagerViewModel = this.PagerModelFactory.CreatePagerViewModel(WebConstants.SearchController, page, usersCount, WebConstants.UsersPageSize);

            var model = new Tuple<IEnumerable<UserViewModel>, IPagerViewModel>(users, pagerViewModel);

            return View(model);
        }
    }
}