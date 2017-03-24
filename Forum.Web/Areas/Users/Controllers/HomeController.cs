﻿using System.Linq;
using System.Web.Mvc;
using Forum.Data;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Common;
using Forum.Web.Models.Common;
using System.Collections.Generic;
using System;

namespace Forum.Web.Areas.Users.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUowData data) : base(data)
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

            var pagingViewModel = new PagingViewModel()
            {
                ControllerName = WebConstants.HomeController,
                CurrentPage = page,
                ItemsCount = usersCount,
                PageSize = WebConstants.UsersPageSize
            };

            var model = new Tuple<IEnumerable<UserViewModel>, PagingViewModel>(users, pagingViewModel);

            return this.View(model);
        }
    }
}