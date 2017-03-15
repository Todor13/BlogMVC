using Forum.Data;
using Forum.Web.Areas.Forum.Controllers;

namespace Forum.Web.Tests.Areas.ForumControllers.BaseControllerTests.Mocked
{
    public class MockedController : BaseController
    {
        public MockedController(IUowData data) : base(data)
        {
        }
    }
}
