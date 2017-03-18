using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.CommentControllerTests
{
    [TestFixture]
    public class CommentControllerIndexPostTests
    {
        [Test]
        public void CommentController_Index_Post_ShouldReturnBadRequestWhenIdIsNull()
        {
            // Arrange
            var data = new Mock<IUowData>();

            CommentController controller = new CommentController(data.Object);
            Comment comment = new Comment();

            // Act
            HttpStatusCodeResult result = controller.Index(comment, null, 1, 1, "threadTitle") as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestCase("3f0c9a41-19e2-4a5c-a901-b3b056e50dgs")]
        [TestCase("4398df12-1604-429c-9214-1715a72fd56e")]
        public void CommentController_Index_Post_ShouldSetUserIdCorrect(string userId)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var commentsRepository = new Mock<IRepository<Comment>>();
            data.Setup(d => d.Comments).Returns(commentsRepository.Object);

            var claim = new Claim("test", userId);

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            CommentController controller = new CommentController(data.Object)
            {
                ControllerContext = context.Object
            };

            Comment comment = new Comment();

            // Act
            ViewResult result = controller.Index(comment, 1, 1, 1, "threadTitle") as ViewResult;

            // Assert
            commentsRepository.Verify(d => d.Add(It.Is<Comment>(c => c.UserId == userId)));
        }

        [Test]
        public void CommentController_Index_Post_ShouldReturnCommentWithInstanceOfDateTimeSet()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var commentsRepository = new Mock<IRepository<Comment>>();
            data.Setup(d => d.Comments).Returns(commentsRepository.Object);

            var claim = new Claim("test", "qwe-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            CommentController controller = new CommentController(data.Object)
            {
                ControllerContext = context.Object
            };

            Comment comment = new Comment();

            // Act
            ViewResult result = controller.Index(comment, 1, 1, 1, "threadTitle") as ViewResult;

            // Assert
            commentsRepository.Verify(d => d.Add(It.Is<Comment>(c => c.Published.GetType() == typeof(DateTime))));
        }

        [Test]
        public void CommentController_Index_Post_ShouldReturnCommentWithPropertyIsVisibleSetToTrue()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var commentsRepository = new Mock<IRepository<Comment>>();
            data.Setup(d => d.Comments).Returns(commentsRepository.Object);

            var claim = new Claim("test", "qwe-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            CommentController controller = new CommentController(data.Object)
            {
                ControllerContext = context.Object
            };

            Comment comment = new Comment();

            // Act
            ViewResult result = controller.Index(comment, 1, 1, 1, "threadTitle") as ViewResult;

            // Assert
            commentsRepository.Verify(d => d.Add(It.Is<Comment>(c => c.IsVisible == true)));
        }

        [TestCase(1)]
        [TestCase(256)]
        [TestCase(2147483646)]
        public void CommentController_Index_Post_ShouldReturnCommentWithPropertyAnswerIdSetCorrectly(int answerId)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var commentsRepository = new Mock<IRepository<Comment>>();
            data.Setup(d => d.Comments).Returns(commentsRepository.Object);

            var claim = new Claim("test", "qwe-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            CommentController controller = new CommentController(data.Object)
            {
                ControllerContext = context.Object
            };

            Comment comment = new Comment();

            // Act
            ViewResult result = controller.Index(comment, answerId, 1, 1, "threadTitle") as ViewResult;

            // Assert
            commentsRepository.Verify(d => d.Add(It.Is<Comment>(c => c.AnswerId == answerId)));
        }

        [Test]
        public void CommentController_Index_Post_ShouldCallDataSaveChanges()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var commentsRepository = new Mock<IRepository<Comment>>();
            data.Setup(d => d.Comments).Returns(commentsRepository.Object);

            var claim = new Claim("test", "qwe-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            CommentController controller = new CommentController(data.Object)
            {
                ControllerContext = context.Object
            };

            Comment comment = new Comment();

            // Act
            ViewResult result = controller.Index(comment, 1, 1, 1, "threadTitle") as ViewResult;

            // Assert
            data.Verify(d => d.SaveChanges(), Times.Once);
        }
    }
}
