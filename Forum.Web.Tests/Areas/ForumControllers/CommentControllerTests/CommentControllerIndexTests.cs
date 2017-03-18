using Forum.Data;
using Forum.Web.Areas.Forum.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.CommentControllerTests
{
    [TestFixture]
    public class CommentControllerIndexTests
    {
        [Test]
        public void CommentController_Index_ShouldRenderCorrectPartialView()
        {
            // Arrange
            var data = new Mock<IUowData>();

            CommentController controller = new CommentController(data.Object);

            // Act
            PartialViewResult result = controller.Index() as PartialViewResult;

            // Assert
            Assert.AreEqual("_Comment", result.ViewName);
        }
    }
}
