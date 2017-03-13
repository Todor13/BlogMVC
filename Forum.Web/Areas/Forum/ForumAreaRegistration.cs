using System.Web.Mvc;

namespace Forum.Web.Areas.Forum
{
    public class ForumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Forum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Forum_Home",
                "Forum/Home",
                defaults: new { controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "Forum_Search",
                "Forum/Search",
                defaults: new { controller = "Search", action = "Index" }
            );

            context.MapRoute(
                "Forum_Create",
                "Forum/Create",
                defaults: new { controller = "Create", action = "Index" }
            );

            context.MapRoute(
                "Forum_Thread",
                "Forum/Thread/{id}/{title}",
                defaults: new { controller = "Thread", action = "Index", id = UrlParameter.Optional, title = UrlParameter.Optional }
            );

            context.MapRoute(
                "Forum_default",
                "Forum/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}