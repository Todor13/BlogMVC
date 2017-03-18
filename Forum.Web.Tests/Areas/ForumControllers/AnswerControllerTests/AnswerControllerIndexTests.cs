using NUnit.Framework;
using Moq;
using Forum.Data;
using Forum.Web.Areas.Forum.Controllers;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.AnswerControllerTests
{
    [TestFixture]
    public class AnswerControllerIndexTests
    {
        [Test]
        public void AnswerController_Index_ShouldReturnCorrectPartialView()
        {
            // Arrange
            var data = new Mock<IUowData>();

            AnswerController controller = new AnswerController(data.Object);

            // Act
            var result = controller.Index() as PartialViewResult;

            // Assert
            Assert.AreEqual("_Answer", result.ViewName);
        }
    }
}
