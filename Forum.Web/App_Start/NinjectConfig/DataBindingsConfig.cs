using Ninject.Modules;
using Ninject.Web.Common;
using Forum.Data;

namespace Forum.Web.App_Start
{
    public class DataBindingsConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IForumDbContext>().To<ForumDbContext>().InRequestScope();
            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            this.Bind<IUowData>().To<UowData>();
        }
    }
}