using Microsoft.AspNet.Identity.EntityFramework;

namespace Forum.Models
{
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
    }
}