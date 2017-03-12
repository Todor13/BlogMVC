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
                "Forum_default",
                "Forum/{controller}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
               "Forum",
               "Forum/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}