﻿using Forum.Data;
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
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.UsersControllers.ProfileControllerTests
{
    [TestFixture]
    public class ProfileControllerGetUserAnswersTests
    {
        [Test]
        public void Users_ProfileController_GetUserAnswers_ShouldCorrectModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;

            // Assert
            Assert.IsInstanceOf<Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>>(result.Model);
        }

        [Test]
        public void Users_ProfileController_GetUserAnswers_ShouldCorrectUserAnswersAtPage1()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            var expected = new AnswerActivityViewModel[]
            {
                new AnswerActivityViewModel() { Id = 3, Published = new DateTime(2017, 01, 01), Content = "testContent", ThreadId = 1, ThreadTitle = "testTitle" },
                new AnswerActivityViewModel() { Id = 2, Published = new DateTime(2017, 01, 02), Content = "testContent", ThreadId = 1, ThreadTitle = "testTitle" },
                new AnswerActivityViewModel() { Id = 1, Published = new DateTime(2017, 01, 03), Content = "testContent", ThreadId = 1, ThreadTitle = "testTitle" }
            };

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new AnswerActivityViewModelComparer());
        }

        [Test]
        public void Users_ProfileController_GetUserAnswers_ShouldCorrectUserAnwersAtPage2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            var expected = new AnswerActivityViewModel[]
            {
                new AnswerActivityViewModel() { Id = 4, Published = new DateTime(2017, 01, 04), Content = "testContent", ThreadId = 1, ThreadTitle = "testTitle" }
            };

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", 2) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Item1, new AnswerActivityViewModelComparer());
        }

        [TestCase(13)]
        [TestCase(1283)]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnCallPagerViewModelFactoryWithCorrectData(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", page) as ViewResult;

            // Assert
            pagerFactory.Verify(p => p.CreateAjaxPagerViewModel("Profile", "GetUserAnswers", "forum-activity", page, 4, WebConstants.ActivityPageSize));
        }

        [Test]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnAjaxPagerViewModelWithCorrectControllerName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.ControllerName).Returns("Profile");
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", 1) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual("Profile", resultModel.Item2.ControllerName);
        }

        [Test]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnAjaxPagerViewModelWithCorrectActionName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.ActionName).Returns("GetUserAnswers");
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", 1) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual("GetUserAnswers", resultModel.Item2.ActionName);
        }

        [TestCase(243)]
        [TestCase(243123423)]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnAjaxPagerViewModelWithCorrectCurrentPage(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.CurrentPage).Returns(page);
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7", page) as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual(page, resultModel.Item2.CurrentPage);
        }

        [TestCase(243)]
        [TestCase(243123423)]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnAjaxPagerViewModelWithCorrectItemsCount(int itemsCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.ItemsCount).Returns(itemsCount);
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual(itemsCount, resultModel.Item2.ItemsCount);
        }

        [TestCase(123)]
        [TestCase(878)]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnAjaxPagerViewModelWithCorrectPageSize(int pageSize)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.PageSize).Returns(pageSize);
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual(pageSize, resultModel.Item2.PageSize);
        }

        [Test]
        public void Users_ProfileController_GetUserAnswers_ShouldReturnAjaxPagerViewModelWithCorrectUpdateTarget()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var ajaxPagerViewModel = new Mock<IAjaxPagerViewModel>();

            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable());
            ajaxPagerViewModel.Setup(a => a.UpdateTarget).Returns("forum-activity");
            pagerFactory.Setup(p => p.CreateAjaxPagerViewModel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(ajaxPagerViewModel.Object);

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.GetUserAnswers("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as PartialViewResult;
            var resultModel = result.Model as Tuple<IEnumerable<AnswerActivityViewModel>, IAjaxPagerViewModel>;

            // Assert
            Assert.AreEqual("forum-activity", resultModel.Item2.UpdateTarget);
        }

        private ICollection<Answer> AnswersCollection()
        {
            return new List<Answer>()
            {
                new Answer() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 03), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Content = "testContent", ThreadId = 1, Thread = new Thread() { Title = "testTitle" }},
                new Answer() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Content = "testContent", ThreadId = 1, Thread = new Thread() { Title = "testTitle" }},
                new Answer() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Content = "testContent", ThreadId = 1, Thread = new Thread() { Title = "testTitle" }},
                new Answer() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 04), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Content = "testContent", ThreadId = 1, Thread = new Thread() { Title = "testTitle" }},
                new Answer() { Id = 6, IsVisible = false, Published = new DateTime(2017, 01, 07), UserId = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Content = "testContent", ThreadId = 1, Thread = new Thread() { Title = "testTitle" }},
                new Answer() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 06), UserId = "NotValid", Content = "testContent", ThreadId = 1, Thread = new Thread() { Title = "testTitle" } }
            };
        }
    }
}
