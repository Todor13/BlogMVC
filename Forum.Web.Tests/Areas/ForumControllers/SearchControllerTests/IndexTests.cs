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

namespace Forum.Web.Tests.Areas.ForumControllers.SearchControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        [Test]
        public void SearchController_Index_ShouldReturnViewResult()
        {
            //Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            SearchController controller = new SearchController(data.Object);

            //Act
            var result = controller.Index("SomeContent") as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchController_Index_ShouldReturnViewWithIndexPageViewModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            //Act
            var result = controller.Index("SomeTitle") as ViewResult;

            //Assert
            Assert.IsInstanceOf<IndexPageViewModel>(result.Model);
        }

        [Test]
        public void SearchController_Index_ShouldReturnCorrectPageCount()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            // Act
            var result = controller.Index("SomeContent") as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            Assert.AreEqual(3, resultModel.PageCounter.PagesCount);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void SearchController_Index_ShouldReturnCorrectCurrentPage(int currentPage)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            // Act
            var result = controller.Index("SomeContent" ,currentPage) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            Assert.AreEqual(currentPage, resultModel.PageCounter.CurrentPage);
        }

        [Test]
        public void SearchController_Index_ShouldReturnOnlyVisibleThreads()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            var expected = new Thread[]
             {
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = "SomeTitle", Content = "SomeContent" }
             };

            // Act
            var result = controller.Index("SomeContent", 3) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Threads, new ThreadComparer());
        }

        [Test]
        public void SearchController_Index_ShouldReturnIndexPageViewModelWithCorrectControllerName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            // Act
            var result = controller.Index("SomeContent", 1) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            Assert.AreEqual("Search", resultModel.PageCounter.ControllerName);
        }

        [TestCase("Test")]
        [TestCase("test")]
        [TestCase("TEsT")]
        public void SearchController_Index_ShouldReturnCorrectResultsCaseInsensitive(string searchTerm)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(SearchTestCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            var expected = new Thread[]
            {
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = "test", Content = "SomeContent" },
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = "Test" },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = "Important topic", Content = "Unit testing has helped me alot!" },
            };

            // Act
            var result = controller.Index(searchTerm, 1) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Threads, new ThreadComparer());
        }

        [TestCase("TEsT")]
        public void SearchController_Index_ShouldReturnCorrectResultsCaseInsensitiveSecondPage(string searchTerm)
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Threads.All()).Returns(SearchTestCollection().AsQueryable());
            SearchController controller = new SearchController(data.Object);

            var expected = new Thread[]
            {
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = "How to test it!", Content = "Need some help here!" }
            };

            // Act
            var result = controller.Index(searchTerm, 2) as ViewResult;
            IndexPageViewModel resultModel = result.Model as IndexPageViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Threads, new ThreadComparer());
        }

        private ICollection<Thread> ThreadsCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 05), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 9, IsVisible = false, Published = new DateTime(2017, 01, 09), Title = "SomeTitle", Content = "SomeContent" }
            };
        }

        private ICollection<Thread> SearchTestCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = "Test" },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = "test", Content = "SomeContent" },
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = "SomeTitle", Content = string.Empty },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = "Important topic", Content = "Unit testing has helped me alot!" },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), Title = "SomeTitle", Content = "SomeContent" },
                new Thread() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 05), Title = string.Empty, Content = string.Empty },
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = "How to test it!", Content = "Need some help here!" },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = "Good night", Content = "What about going out tonight?" },
                new Thread() { Id = 9, IsVisible = false, Published = new DateTime(2017, 01, 09), Title = "SomeTitle", Content = "SomeContent" }
            };
        }
    }
}
