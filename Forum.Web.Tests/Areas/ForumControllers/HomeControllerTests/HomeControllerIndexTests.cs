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

namespace Forum.Web.Tests.Areas.ForumControllers.HomeControllerTests
{
    [TestFixture]
    public class HomeControllerIndexTests
    {
        [Test]
        public void Forum_HomeController_Index_ShouldReturnViewResult()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Forum_HomeController_Index_ShouldReturnCorrectModel()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOf<Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>>(result.Model);
        }

        [TestCase(1)]
        [TestCase(25664623)]
        public void Forum_HomeController_Index_ShouldReturnPagerViewModelFactoryWithCorrectData(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index(page) as ViewResult;

            // Assert
            pagerFactory.Verify(p => p.CreatePagerViewModel("Home", page, 8, WebConstants.UsersPageSize));
        }

        [TestCase(2)]
        [TestCase(2234234)]
        public void Forum_HomeController_Index_ShouldReturnCorrectPageCount(int pagesCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.PagesCount).Returns(pagesCount);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(pagesCount, resultModel.Item2.PagesCount);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(38388903)]
        public void Forum_HomeController_Index_ShouldReturnCorrectCurrentPage(int currentPage)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.CurrentPage).Returns(currentPage);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index(currentPage) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(currentPage, resultModel.Item2.CurrentPage);
        }

        [TestCase(23)]
        [TestCase(266885)]
        public void Forum_HomeController_Index_ShouldReturnCorrectItemsCount(int itemsCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.ItemsCount).Returns(itemsCount);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(itemsCount, resultModel.Item2.ItemsCount);
        }

        [TestCase(3)]
        [TestCase(10)]
        public void Forum_HomeController_Index_ShouldReturnCorrectPageSize(int pageSize)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            pagerViewModel.Setup(p => p.PageSize).Returns(pageSize);

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(pageSize, resultModel.Item2.PageSize);
        }

        [Test]
        public void Forum_HomeController_Index_ShouldReturnFirstThreadsEqualsToPageSizeAndOrderedByPublishedPropertyAtPage1()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            var expected = new ThreadViewModel[]
             {
                new ThreadViewModel() { Id = 3, Published = new DateTime(2017, 01, 01), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 2, Published = new DateTime(2017, 01, 02), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 1, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" }
             };

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadViewModelComparer());
        }

        [Test]
        public void Forum_HomeController_Index_ShouldReturnFirstThreadsEqualsToPageSizeAndOrderedByPublishedPropertyAtPage2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            var expected = new ThreadViewModel[]
             {
                new ThreadViewModel() { Id = 4, Published = new DateTime(2017, 01, 04), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 6, Published = new DateTime(2017, 01, 05), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 5, Published = new DateTime(2017, 01, 06), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" }
             };

            // Act
            var result = controller.Index(2) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadViewModelComparer());
        }

        [Test]
        public void Forum_HomeController_Index_ShouldReturnOnlyVisibleThreadsAtPage3()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            var expected = new ThreadViewModel[]
             {
                new ThreadViewModel() { Id = 7, Published = new DateTime(2017, 01, 07), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" },
                new ThreadViewModel() { Id = 8, Published = new DateTime(2017, 01, 08), Title = string.Empty, Content = string.Empty, AnswersCount = 0, SectionName = "testSectionName", UserId = "id" }
             };

            // Act
            var result = controller.Index(3) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadViewModelComparer());
        }

        private ICollection<Thread> ThreadsCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 05), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 7, IsVisible = true, Published = new DateTime(2017, 01, 07), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 8, IsVisible = true, Published = new DateTime(2017, 01, 08), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 9, IsVisible = false, Published = new DateTime(2017, 01, 09), Title = string.Empty, Content = string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty }
            };
        }
    }
}
