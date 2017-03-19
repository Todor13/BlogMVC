using NUnit.Framework;
using Moq;
using Forum.Data;
using Forum.Web.Areas.Forum.Controllers;
using System.Web.Mvc;
using Forum.Models;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Forum.Web.Tests.Areas.ForumControllers.AnswerControllerTests
{
    [TestFixture]
    public class AnswerControllerIndexPostTests
    {
        [Test]
        public void AnswerController_Index_Post_ShouldReturnBadRequestWhenIdIsNull()
        {
            // Arrange
            var data = new Mock<IUowData>();

            AnswerController controller = new AnswerController(data.Object);
            Answer answer = new Answer();

            // Act
            var result = controller.Index(answer, null, "ThreadTitle") as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestCase("3f0c9a41-19e2-4a5c-a901-b3b056e50dgs")]
        [TestCase("4398df12-1604-429c-9214-1715a72fd56e")]
        public void AnswerController_Index_Post_ShouldReturnAnswerWithCorrectIdSet(string id)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", id);

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            var result = controller.Index(answer, 1, "ThreadTitle") as ViewResult;

            // Assert
            data.Verify(a => a.Answers.Add(It.Is<Answer>(x => x.UserId == id)));
        }

        [Test]
        public void AnswerController_Index_Post_ShouldReturnAnswerWithInstanceOfDateTimeSet()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            var result = controller.Index(answer, 1, "ThreadTitle") as ViewResult;

            // Assert
            data.Verify(a => a.Answers.Add(It.Is<Answer>(x => x.Published.GetType() == typeof(DateTime))));
        }

        [Test]
        public void AnswerController_Index_Post_ShouldReturnAnswerWithPropertyIsVisibleSetToTrue()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            var result = controller.Index(answer, 1, "ThreadTitle") as ViewResult;

            // Assert
            data.Verify(a => a.Answers.Add(It.Is<Answer>(x => x.IsVisible == true)));
        }

        [TestCase(1)]
        [TestCase(256)]
        [TestCase(2147483646)]
        public void AnswerController_Index_Post_ShouldReturnAnswerWithPropertyThreadIdSet(int threadId)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            var result = controller.Index(answer, threadId, "ThreadTitle") as ViewResult;

            // Assert
            data.Verify(a => a.Answers.Add(It.Is<Answer>(x => x.ThreadId == threadId)));
        }

        [Test]
        public void AnswerController_Index_Post_ShouldCallDataSaveChanges()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            var result = controller.Index(answer, 1, "ThreadTitle") as ViewResult;

            // Assert
            data.Verify(d => d.SaveChanges(), Times.Once);
        }

        [Test]
        public void AnswerController_Index_Post_ShouldRedirectToCorrectLastPage()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            RedirectToRouteResult result = controller.Index(answer, 1, "ThreadTitle") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(2, result.RouteValues["page"]);
        }

        [Test]
        public void AnswerController_Index_Post_ShouldRedirectToTheRightController()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            RedirectToRouteResult result = controller.Index(answer, 1, "ThreadTitle") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Thread", result.RouteValues["Controller"]);
        }

        [Test]
        public void AnswerController_Index_Post_ShouldRedirectToTheRightAction()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            RedirectToRouteResult result = controller.Index(answer, 1, "ThreadTitle") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }

        [TestCase(13)]
        [TestCase(2147483646)]
        public void AnswerController_Index_Post_ShouldRedirectWithCorrectParamThreadId(int threadId)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            RedirectToRouteResult result = controller.Index(answer, threadId, "ThreadTitle") as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(threadId, result.RouteValues["id"]);
        }

        [TestCase("First%20Thread%20ever!")]
        [TestCase("Second%20Thread")]
        public void AnswerController_Index_Post_ShouldRedirectWithCorrectParamThreadTitle(string threadTitle)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            var claim = new Claim("test", "id-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            AnswerController controller = new AnswerController(data.Object)
            {
                ControllerContext = context.Object
            };

            Answer answer = new Answer();

            // Act
            RedirectToRouteResult result = controller.Index(answer, 13, threadTitle) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual(threadTitle, result.RouteValues["title"]);
        }

        private ICollection<Answer> AnswersCollection()
        {
            return new List<Answer>()
            {
                new Answer() { Id = 1, IsVisible = true, ThreadId = 1 },
                new Answer() { Id = 2, IsVisible = true, ThreadId = 1 },
                new Answer() { Id = 3, IsVisible = true, ThreadId = 1 },
                new Answer() { Id = 4, IsVisible = true, ThreadId = 1 },
                new Answer() { Id = 5, IsVisible = true, ThreadId = 1 },
                new Answer() { Id = 6, IsVisible = true, ThreadId = 1 },
                new Answer() { Id = 7, IsVisible = false, ThreadId = 1 }
            };
        }
    }
}
