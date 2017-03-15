using Forum.Data;
using Forum.Web.Tests.Areas.ForumControllers.BaseControllerTests.Mocked;
using Moq;
using NUnit.Framework;

namespace Forum.Web.Tests.Areas.ForumControllers
{
    [TestFixture]
    public class BaseControllerShould
    {
        [Test]
        public void BaseController_ShouldExposeDataPropertyToAllClassesWhichInheritsFromIt()
        {
            // Arrange
            var data = new Mock<IUowData>();

            // Act
            MockedController controller = new MockedController(data.Object);

            // Assert
            Assert.AreEqual(data.Object, controller.ActionInvoker)
        }
    }
}
