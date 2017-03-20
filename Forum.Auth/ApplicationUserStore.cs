using Forum.Data;
using Forum.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Forum.Auth
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>,
      IUserStore<ApplicationUser>,
      IDisposable
    {
        public ApplicationUserStore(ForumDbContext context) : base(context) { }
    }
}
