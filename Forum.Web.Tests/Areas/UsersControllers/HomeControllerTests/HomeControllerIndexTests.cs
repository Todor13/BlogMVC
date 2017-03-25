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

namespace Forum.Web.Tests.Areas.UsersControllers.HomeControllerTests
{
    [TestFixture]
    public class HomeControllerIndexTests
    {
        [Test]
        public void Users_HomeController_Index_ShouldReturnCorrectModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOf<Tuple<IEnumerable<UserViewModel>, IPagerViewModel>>(result.Model);
        }

        [Test]
        public void Users_HomeController_Index_ShouldReturnCorrectUserCollectionConvertedToUserViewModelAtPage1()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            var expected = new UserViewModel[]
            {
                new UserViewModel() { Id = "9d47be05-069c-4b59-8491-d78c451fe7d5", Email = "abc@test.ts" },
                new UserViewModel() { Id = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Email = "bcd@test.ts" },
                new UserViewModel() { Id = "579c957d-b103-4b3a-acb0-1acb80f17692", Email = "cde@test.ts" }
            };

            // Act
            var result = controller.Index(1) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new UserViewModelComparer());
        }

        [Test]
        public void Users_HomeController_Index_ShouldReturnCorrectUserCollectionConvertedToUserViewModelAtPage2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            var expected = new UserViewModel[]
            {
                new UserViewModel() { Id = "4264074f-1b34-4599-87f4-bc2616181c91", Email = "def@test.ts" }
            };

            // Act
            var result = controller.Index(2) as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new UserViewModelComparer());
        }

        [TestCase(1)]
        [TestCase(25664623)]
        public void Users_HomeController_Index_ShouldReturnCallPagerViewModelFactoryWithCorrectData(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index(page) as ViewResult;

            // Assert
            pagerFactory.Verify(p => p.CreatePagerViewModel("Home", page, UsersCollection().Count, WebConstants.UsersPageSize));
        }

        [Test]
        public void Users_HomeController_Index_ShouldReturnModelWithCorrectControllerName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerViewModel.Setup(p => p.ControllerName).Returns("Home");
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual("Home", resultModel.Item2.ControllerName);
        }

        [TestCase(1)]
        [TestCase(2568903)]
        public void Users_HomeController_Index_ShouldReturnModelWithCorrectCurrentPage(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerViewModel.Setup(p => p.CurrentPage).Returns(page);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(page, resultModel.Item2.CurrentPage);
        }

        [TestCase(5)]
        [TestCase(234235)]
        public void Users_HomeController_Index_ShouldReturnModelWithCorrectItemsCount(int itemsCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerViewModel.Setup(p => p.ItemsCount).Returns(itemsCount);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(itemsCount, resultModel.Item2.ItemsCount);
        }

        [TestCase(35)]
        [TestCase(2342235)]
        public void Users_HomeController_Index_ShouldReturnModelWithCorrectPagesCount(int pagesCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerViewModel.Setup(p => p.PagesCount).Returns(pagesCount);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(pagesCount, resultModel.Item2.PagesCount);
        }

        [TestCase(345)]
        [TestCase(32512312)]
        public void Users_HomeController_Index_ShouldReturnModelWithCorrectPageSize(int pageSize)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            pagerViewModel.Setup(p => p.PageSize).Returns(pageSize);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);

            HomeController controller = new HomeController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index() as ViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<UserViewModel>, IPagerViewModel>;

            // Assert
            Assert.AreEqual(pageSize, resultModel.Item2.PageSize);
        }

        private ICollection<ApplicationUser> UsersCollection()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser() { Id = "9d47be05-069c-4b59-8491-d78c451fe7d5", Email = "abc@test.ts" },
                new ApplicationUser() { Id = "579c957d-b103-4b3a-acb0-1acb80f17692", Email = "cde@test.ts" },
                new ApplicationUser() { Id = "4264074f-1b34-4599-87f4-bc2616181c91", Email = "def@test.ts" },
                new ApplicationUser() { Id = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Email = "bcd@test.ts" }, 
            };
        }
    }
}
