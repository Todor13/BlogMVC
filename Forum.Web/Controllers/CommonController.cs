using System.Web.Mvc;

namespace Forum.Web.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Cancel()
        {
            return Content(string.Empty);
        }
    }
}