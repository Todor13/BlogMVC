using System.Web.Mvc;

namespace Forum.Web.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Users_ALl",
                "Users/All",
                new { controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "Users_Activity_Threads",
                "Users/Profile/GetUserThreads/{id}",
                new { controller = "Profile", action = "GetUserThreads", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Users_Profile",
                "Users/Profile/{id}",
                new { controller = "Profile", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Users_default",
                "Users/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}