using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.CreateControllerTests
{
    [TestFixture]
    public class CreateControllerIndexPostTests
    {
        [Test]
        public void CreateController_Index_Post_ShouldAddThread()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            data.Setup(d => d.Threads).Returns(threadRepository.Object);
            var claim = new Claim("test", "asd-123");
            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i=>i.FindFirst(It.IsAny<string>())).Returns(claim);
            var context = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            CreateController controller = new CreateController(data.Object)
            {
                ControllerContext = context.Object
            };
            
            var thread = new Thread();

            //Act
            var result = controller.Index(thread) as ViewResult;

            //Assert
            threadRepository.Verify(d => d.Add(It.IsAny<Thread>()));
        }
    }
}
