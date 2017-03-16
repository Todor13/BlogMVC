using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Models.Forum;
using Forum.Web.Tests.Areas.ForumControllers.Helpers;
using Forum.Web.Tests.Areas.ForumControllers.HomeControllerTests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.ThreadControllerTests
{
    [TestFixture]
    public class ThreadControllerIndexTests
    {
        [Test]
        public void ThreadController_Index_ShouldReturnBadRequestWhenIdIsNull()
        {
            // Arrange
            var data = new Mock<IUowData>();

            ThreadController controller = new ThreadController(data.Object);

            // Act
            HttpStatusCodeResult result = controller.Index(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void ThreadController_Index_ShouldReturnHttpNotFoundIfThreadNull()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.GetById(It.IsAny<int>())).Returns(null as Thread);

            var controller = new ThreadController(data.Object);

            // Act
            HttpNotFoundResult result = controller.Index(1) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void ThreadController_Index_ShouldReturnHttpNotFoundIfIsVisiblePropertyIsFalse()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.GetById(It.IsAny<int>())).Returns(new Thread() { Id = 1, IsVisible = false });

            var controller = new ThreadController(data.Object);

            // Act
            HttpNotFoundResult result = controller.Index(1) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public void ThreadController_Index_ShouldReturnCorrectThread()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.GetById(It.IsAny<int>())).Returns(TestThread());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            var controller = new ThreadController(data.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            ThreadAnswersViewModel resultModel = result.Model as ThreadAnswersViewModel;

            // Assert
            Assert.AreEqual(1, resultModel.Thread.Id);
        }

        [Test]
        public void ThreadController_Index_ShouldReturnCorrectThread2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.GetById(It.IsAny<int>())).Returns(TestThread());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            var controller = new ThreadController(data.Object);
            var expected = new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 01) };

            // Act
            var result = controller.Index(1) as ViewResult;
            ThreadAnswersViewModel resultModel = result.Model as ThreadAnswersViewModel;

            // Assert
            Assert.That(expected, Has.Property("Id").EqualTo(resultModel.Thread.Id)
                & Has.Property("IsVisible").EqualTo(resultModel.Thread.IsVisible)
                & Has.Property("Published").EqualTo(resultModel.Thread.Published));
        }

        [Test]
        public void ThreadController_Index_ShouldReturnAnswersEqulToPageSizeAndOrederedByPublishedPropery()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.GetById(It.IsAny<int>())).Returns(TestThread());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            var controller = new ThreadController(data.Object);

            var expected = new List<Answer>()
            {
                new Answer() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), ThreadId = 1, Content=string.Empty, Comments = new List<Comment>() { new Comment() { Id = 1, IsVisible = true } } },
                new Answer() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 03), ThreadId = 1, Content=string.Empty },
            };

            // Act
            var result = controller.Index(1, 1) as ViewResult;
            ThreadAnswersViewModel resultModel = result.Model as ThreadAnswersViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Answers, new AnswerComparer());
        }

        [Test]
        public void ThreadController_Index_ShouldReturnAnswersEqulToPageSizeAndOrederedByPublishedProperyAtPage2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.GetById(It.IsAny<int>())).Returns(TestThread());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            var controller = new ThreadController(data.Object);

            var expected = new List<Answer>()
            {
                new Answer() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 04), ThreadId = 1, Content=string.Empty, Comments = new List<Comment>() { new Comment() { Id = 2, IsVisible = false } } },
                new Answer() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 05), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 06), ThreadId = 1, Content=string.Empty }
            };

            // Act
            var result = controller.Index(1, 2) as ViewResult;
            ThreadAnswersViewModel resultModel = result.Model as ThreadAnswersViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Answers, new AnswerComparer());
        }

        private ICollection<Answer> AnswersCollection()
        {
            return new List<Answer>()
            {
                new Answer() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 04), ThreadId = 1, Content=string.Empty, Comments = new List<Comment>() { new Comment() { Id = 2, IsVisible = false } } },
                new Answer() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), ThreadId = 1, Content=string.Empty, Comments = new List<Comment>() { new Comment() { Id = 1, IsVisible = true } } },
                new Answer() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 03), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 05), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 06), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 7, IsVisible = false, Published = new DateTime(2017, 01, 08), ThreadId = 1, Content=string.Empty }
            };
        }

        private Thread TestThread()
        {
            return new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01 ,01) };
        }
    }
}
