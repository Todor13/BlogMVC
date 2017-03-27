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
                defaults: new { controller = "Thread", action = "Index", title = UrlParameter.Optional }
            );

            context.MapRoute(
                "Forum_Answer",
                "Forum/Answer/{id}/{title}",
                defaults: new { controller = "Answer", action = "Index", id = UrlParameter.Optional, title = UrlParameter.Optional }
            );

            context.MapRoute(
                "Forum_Comment_Cancel",
                "Forum/Comment/Cancel",
                defaults: new { controller = "Comment", action = "Cancel" }
            );

            context.MapRoute(
                "Forum_Comment",
                "Forum/Comment/{id}/{title}/{threadId}/{page}",
                defaults: new { controller = "Comment", action = "Index", id = UrlParameter.Optional, title = UrlParameter.Optional, threadId = UrlParameter.Optional, page = UrlParameter.Optional }
            );

            context.MapRoute(
                "Forum_default",
                "Forum/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}