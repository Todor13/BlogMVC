using AutoMapper;
using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Areas.Forum.Models;
using Forum.Web.Common;
using Forum.Web.Factories;
using Forum.Web.Models.Common.Contracts;
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
    public class SearchControllerIndexTests
    {
        [Test]
        public void Forum_SearchController_Index_ShouldReturnViewResult()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            //Act
            var result = controller.Index("SomeContent") as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Forum_SearchController_Index_ShouldReturnCorrectModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            //Act
            var result = controller.Index("SomeTitle") as ViewResult;

            //Assert
            Assert.IsInstanceOf<Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>>(result.Model);
        }

        [TestCase(13)]
        [TestCase(256343)]
        public void Forum_SearchController_Index_ShouldReturnPagerViewModelFactoryWithCorrectData(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("SomeTitle", page) as ViewResult;

            // Assert
            pagerFactory.Verify(p => p.CreatePagerViewModel("Search", page, 8, WebConstants.UsersPageSize));
        }

        [TestCase(2)]
        [TestCase(2234234)]
        public void Forum_SearchController_Index_ShouldReturnCorrectPageCount(int pagesCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.PagesCount).Returns(pagesCount);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("SomeContent") as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(pagesCount, resultModel.Item2.PagesCount);
        }

        [TestCase(1)]
        [TestCase(13)]
        public void Forum_SearchController_Index_ShouldReturnCorrectCurrentPage(int currentPage)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.CurrentPage).Returns(currentPage);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("SomeContent", currentPage) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(currentPage, resultModel.Item2.CurrentPage);
        }

        [TestCase(12)]
        [TestCase(26485)]
        public void Forum_SearchController_Index_ShouldReturnCorrectItemsCount(int itemsCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.ItemsCount).Returns(itemsCount);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("SomeContent") as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(itemsCount, resultModel.Item2.ItemsCount);
        }

        [TestCase(3)]
        [TestCase(10)]
        public void Forum_SearchController_Index_ShouldReturnCorrectPageSize(int pageSize)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.PageSize).Returns(pageSize);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("SomeContent") as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(pageSize, resultModel.Item2.PageSize);
        }

        [Test]
        public void Forum_SearchController_Index_ShouldReturnOnlyVisibleThreadsAtPage3()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            var expected = new ThreadViewModel[]
             {
                new ThreadViewModel() { Id = 7, Published = new DateTime(2017, 01, 07), Title = "SomeTitle", Content = "SomeContent", AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 8, Published = new DateTime(2017, 01, 08), Title = "SomeTitle", Content = "SomeContent", AnswersCount = 0, SectionName = "testSectionName", UserId = "id" }
             };

            // Act
            var result = controller.Index("SomeContent", 3) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadViewModelComparer());
        }

        [Test]
        public void Forum_SearchController_Index_ShouldReturnIndexPageViewModelWithCorrectControllerName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.ControllerName).Returns("Search");

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("SomeContent", 1) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual("Search", resultModel.Item2.ControllerName);
        }

        [TestCase("Test")]
        [TestCase("test")]
        [TestCase("TEsT")]
        public void Forum_SearchController_Index_ShouldReturnCorrectResultsCaseInsensitive(string searchTerm)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(SearchTestCollection().AsQueryable());

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            var expected = new ThreadViewModel[]
            {
                new ThreadViewModel() { Id = 2, Published = new DateTime(2017, 01, 02), Title = "test", Content = "SomeContent", AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 1, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = "Test", AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 4, Published = new DateTime(2017, 01, 04), Title = "Important topic", Content = "Unit testing has helped me alot!", AnswersCount = 0, SectionName = "testSectionName", UserId = "id" }
            };

            // Act
            var result = controller.Index(searchTerm, 1) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadViewModelComparer());
        }

        [TestCase("TEsT")]
        public void SearchController_Index_ShouldReturnCorrectResultsCaseInsensitiveSecondPage(string searchTerm)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(SearchTestCollection().AsQueryable());

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            SearchController controller = new SearchController(data.Object, pagerFactory.Object);

            var expected = new ThreadViewModel[]
            {
                new ThreadViewModel() { Id = 7, Published = new DateTime(2017, 01, 07), Title = "How to test it!", Content = "Need some help here!", AnswersCount = 0, SectionName = "testSectionName", UserId = "id" }
            };

            // Act
            var result = controller.Index(searchTerm, 2) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadViewModelComparer());
        }

        private ICollection<Thread> ThreadsCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 05), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 9, IsVisible = false, Published = new DateTime(2017, 01, 09), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty }
            };
        }

        private ICollection<Thread> SearchTestCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = "Test", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = "test", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = "SomeTitle", Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = "Important topic", Content = "Unit testing has helped me alot!", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 05), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = "How to test it!", Content = "Need some help here!", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = "Good night", Content = "What about going out tonight?", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 9, IsVisible = false, Published = new DateTime(2017, 01, 09), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty }
            };
        }
    }
}
