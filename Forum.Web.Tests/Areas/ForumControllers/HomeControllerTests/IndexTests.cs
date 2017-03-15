using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Models.Forum;
using Forum.Web.Tests.Areas.ForumControllers.HomeControllerTests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.HomeControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        [Test]
        public void HomeController_Index_ShouldReturnViewResult()
        {
            //Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            HomeController controller = new HomeController(data.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void HomeController_Index_ShouldReturnViewWithIndexPageViewModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            HomeController controller = new HomeController(data.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOf<IndexPageViewModel>(result.Model);
        }

        [Test]
        public void HomeController_Index_ShouldReturnCorrectPageCount()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            HomeController controller = new HomeController(data.Object);

            // Act
            var result = controller.Index() as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            Assert.AreEqual(3, resultModel.PageCounter.PagesCount);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void HomeController_Index_ShouldReturnCorrectCurrentPage(int currentPage)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            HomeController controller = new HomeController(data.Object);

            // Act
            var result = controller.Index(currentPage) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            Assert.AreEqual(currentPage, resultModel.PageCounter.CurrentPage);
        }

        [Test]
        public void HomeController_Index_ShouldReturnFirstThreadsEqualsToPageSizeAndOrderedByPublishedProperty()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            HomeController controller = new HomeController(data.Object);
            var expected = new Thread[]
             {
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = string.Empty }
             };

            // Act
            var result = controller.Index() as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Threads, new ThreadComparer());
        }

        [Test]
        public void HomeController_Index_ShouldReturnOnlyVisibleThreads()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            HomeController controller = new HomeController(data.Object);
            var expected = new Thread[]
             {
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = string.Empty, Content = string.Empty }
             };

            // Act
            var result = controller.Index(3) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Threads, new ThreadComparer());
        }

        private ICollection<Thread> ThreadsCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 05), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 9, IsVisible = false, Published = new DateTime(2017, 01, 09), Title = string.Empty, Content = string.Empty }
            };
        }
    }
}
