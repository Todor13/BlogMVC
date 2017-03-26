using Ninject.Modules;
using Ninject.Web.Common;
using Forum.Data;
using Forum.Auth;
using Microsoft.AspNet.Identity;
using Forum.Models;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Forum.Web.Factories;
using Ninject.Extensions.Factory;
using Forum.Web.Models.Common.Contracts;
using Forum.Web.Models.Common;
using Forum.Web.Factories.Contracts;
using Forum.Web.Areas.Forum.Models.Contracts;
using Forum.Web.Areas.Forum.Models;

namespace Forum.Web.App_Start
{
    public class DataBindingsConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IForumDbContext>().To<ForumDbContext>().InRequestScope();
            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            this.Bind<IUowData>().To<UowData>();

            this.Bind<IUserStore<ApplicationUser>>().To<ApplicationUserStore>();
            this.Bind<UserManager<ApplicationUser>>().ToSelf();

            this.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            this.Bind<ApplicationSignInManager>().ToMethod((context) =>
            {
                var cbase = new HttpContextWrapper(HttpContext.Current);
                return cbase.GetOwinContext().Get<ApplicationSignInManager>();
            });

            this.Bind<ApplicationUserManager>().ToSelf();

            this.Bind<IPagerViewModel>().To<PagerViewModel>();
            this.Bind<IAjaxPagerViewModel>().To<AjaxPagerViewModel>();
            this.Bind<IForumThreadViewModel>().To<ForumThreadViewModel>();

            this.Bind<IPagerViewModelFactory>().ToFactory().InRequestScope();
            this.Bind<IViewModelFactory>().ToFactory().InRequestScope();
        }
    }
}