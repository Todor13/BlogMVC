using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Users.Controllers;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Common;
using Forum.Web.Factories;
using Forum.Web.Models.Common.Contracts;
using Forum.Web.Tests.Areas.UsersControllers.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.UsersControllers.ProfileControllerTests
{
    [TestFixture]
    public class ProfileControllerGetUserThreadsTests
    {
        [Test]
        public void Users_ProfileController_GetUserThreads_ShouldCorrectModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;

            // Assert
            Assert.IsInstanceOf<Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>>(result.Model);
        }

        [Test]
        public void Users_ProfileController_GetUserThreads_ShouldCorrectUserThreadsAtPage1()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            var expected = new ThreadActivityViewModel[]
            {
                new ThreadActivityViewModel() { Id = 3, Published = new DateTime(2017, 01, 01), Title = string.Empty, Content = "testContent" },
                new ThreadActivityViewModel() { Id = 2, Published = new DateTime(2017, 01, 02), Title = string.Empty, Content = "testContent" },
                new ThreadActivityViewModel() { Id = 1, Published = new DateTime(2017, 01, 03), Title = string.Empty, Content = "testContent"}
            };

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadActivityViewModelComparer());
        }

        [Test]
        public void Users_ProfileController_GetUserThreads_ShouldCorrectUserThreadsAtPage2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            var expected = new ThreadActivityViewModel[]
            {
                new ThreadActivityViewModel() { Id = 4, Published = new DateTime(2017, 01, 04), Title = string.Empty, Content = "testContent"}
            };

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", 2) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new ThreadActivityViewModelComparer());
        }

        [TestCase(134)]
        [TestCase(1284553)]
        public void Users_ProfileController_GetUserThreads_ShouldReturnCallPagerViewModelFactoryWithCorrectData(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", page) as ViewResult;

            // Assert
            pagerFactory.Verify(p => p.CreateAjaxPagerViewModel("Profile", "GetUserThreads", "forum-activity", page, 4, WebConstants.ActivityPageSize));
        }

        [Test]
        public void Users_ProfileController_GetUserThreads_ShouldReturnAjaxPagerViewModelWithCorrectControllerName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.ControllerName).Returns("Profile");
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", 1) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual("Profile", resultModel.Item2.ControllerName);
        }

        [Test]
        public void Users_ProfileController_GetUserThreads_ShouldReturnAjaxPagerViewModelWithCorrectActionName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.ActionName).Returns("GetUserThreads");
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", 1) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual("GetUserThreads", resultModel.Item2.ActionName);
        }

        [TestCase(243)]
        [TestCase(243123423)]
        public void Users_ProfileController_GetUserThreads_ShouldReturnAjaxPagerViewModelWithCorrectCurrentPage(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.CurrentPage).Returns(page);
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", page) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual(page, resultModel.Item2.CurrentPage);
        }

        [TestCase(243)]
        [TestCase(243123423)]
        public void Users_ProfileController_GetUserThreads_ShouldReturnAjaxPagerViewModelWithCorrectItemsCount(int itemsCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.ItemsCount).Returns(itemsCount);
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual(itemsCount, resultModel.Item2.ItemsCount);
        }

        [TestCase(123)]
        [TestCase(878)]
        public void Users_ProfileController_GetUserThreads_ShouldReturnAjaxPagerViewModelWithCorrectPageSize(int pageSize)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.PageSize).Returns(pageSize);
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual(pageSize, resultModel.Item2.PageSize);
        }

        [Test]
        public void Users_ProfileController_GetUserThreads_ShouldReturnAjaxPagerViewModelWithCorrectUpdateTarget()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.UpdateTarget).Returns("forum-activity");
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserThreads("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<ThreadActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual("forum-activity", resultModel.Item2.UpdateTarget);
        }

        private ICollection<Thread> ThreadsCollection()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Title = string.Empty, Content = "testContent"},
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Title = string.Empty, Content = "testContent"},
                new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Title = string.Empty, Content = "testContent"},
                new Thread() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Title = string.Empty, Content = "testContent"},
                new Thread() { Id = 6, IsVisible = false, Published = new DateTime(2017, 01, 07), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Title = string.Empty, Content = "testContent" },
                new Thread() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), UserId = "NotValid", Title = string.Empty, Content = "testContent" }
            };
        }
    }
}
