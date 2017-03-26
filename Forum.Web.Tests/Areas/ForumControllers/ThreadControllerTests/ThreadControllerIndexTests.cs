using AutoMapper;
using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Areas.Forum.Models;
using Forum.Web.Areas.Forum.Models.Contracts;
using Forum.Web.Common;
using Forum.Web.Factories;
using Forum.Web.Factories.Contracts;
using Forum.Web.Models.Common;
using Forum.Web.Models.Common.Contracts;
using Forum.Web.Tests.Areas.ForumControllers.Helpers;
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
        public void Forum_ThreadController_Index_ShouldReturnBadRequestWhenIdIsNull()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            ThreadController controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            HttpStatusCodeResult result = controller.Index(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(400, result.StatusCode);
        }

        [Test]
        public void Forum_ThreadController_Index_ShouldReturnHttpNotFoundIfThreadNull()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(new List<Thread>().AsQueryable());
            Mapper.Initialize(cfg => cfg.CreateMap<Thread, ThreadViewModel>());

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            HttpNotFoundResult result = controller.Index(1) as HttpNotFoundResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestCase(1)]
        [TestCase(234345)]
        public void Forum_ThreadController_Index_ShouldCallPagerViewModelFactoryWithCorrectData(int page)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1, page) as ViewResult;

            // Assert
            pagerFactory.Verify(p => p.CreatePagerViewModel("Thread", page, 6, WebConstants.PageSize));
        }

        [TestCase(0, 3)]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        public void Forum_ThreadController_Index_ShouldCallViewModelFactoryWithCorrectData(int atPosition, int expectedId)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1) as ViewResult;

            // Assert
            viewModelFactory.Verify(p => p.CreateForumThreadViewModel(It.Is<ThreadViewModel>(t => t.Id == 1 && t.Published == new DateTime(2017, 01, 01)),
                It.Is<IEnumerable<AnswerViewModel>>(l => l.ElementAt(atPosition).Id == expectedId), null));
        }

        [Test]
        public void Forum_ThreadController_Index_ShouldReturnCorrectPageViewModelPropertyControllerName()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();
            var viewModelFactory = new Mock<IViewModelFactory>();
            var forumThreadViewModel = new Mock<IForumThreadViewModel>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            pagerViewModel.Setup(p => p.ControllerName).Returns("Thread");
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(f => f.PagerViewModel).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(t => t.Thread).Returns(new ThreadViewModel() { Id = 1 });
            viewModelFactory.Setup(v => v.CreateForumThreadViewModel(It.IsAny<ThreadViewModel>(),
                It.IsAny<IEnumerable<AnswerViewModel>>(), It.IsAny<IPagerViewModel>())).Returns(forumThreadViewModel.Object);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            var resultModel = result.Model as IForumThreadViewModel;

            // Assert
            Assert.AreEqual("Thread", resultModel.PagerViewModel.ControllerName);
        }

        [TestCase(1)]
        [TestCase(1231)]
        public void Forum_ThreadController_Index_ShouldReturnCorrectPageViewModelPropertyCurrentPage(int currentPage)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();
            var viewModelFactory = new Mock<IViewModelFactory>();
            var forumThreadViewModel = new Mock<IForumThreadViewModel>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            pagerViewModel.Setup(p => p.CurrentPage).Returns(currentPage);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(f => f.PagerViewModel).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(t => t.Thread).Returns(new ThreadViewModel() { Id = 1 });
            viewModelFactory.Setup(v => v.CreateForumThreadViewModel(It.IsAny<ThreadViewModel>(),
                It.IsAny<IEnumerable<AnswerViewModel>>(), It.IsAny<IPagerViewModel>())).Returns(forumThreadViewModel.Object);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            var resultModel = result.Model as IForumThreadViewModel;

            // Assert
            Assert.AreEqual(currentPage, resultModel.PagerViewModel.CurrentPage);
        }

        [TestCase(121)]
        [TestCase(4331)]
        public void Forum_ThreadController_Index_ShouldReturnCorrectPageViewModelPropertyItemsCount(int itemsCount)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();
            var viewModelFactory = new Mock<IViewModelFactory>();
            var forumThreadViewModel = new Mock<IForumThreadViewModel>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            pagerViewModel.Setup(p => p.ItemsCount).Returns(itemsCount);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(f => f.PagerViewModel).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(t => t.Thread).Returns(new ThreadViewModel() { Id = 1 });
            viewModelFactory.Setup(v => v.CreateForumThreadViewModel(It.IsAny<ThreadViewModel>(),
                It.IsAny<IEnumerable<AnswerViewModel>>(), It.IsAny<IPagerViewModel>())).Returns(forumThreadViewModel.Object);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            var resultModel = result.Model as IForumThreadViewModel;

            // Assert
            Assert.AreEqual(itemsCount, resultModel.PagerViewModel.ItemsCount);
        }

        [TestCase(12)]
        [TestCase(43)]
        public void Forum_ThreadController_Index_ShouldReturnCorrectPageViewModelPropertyPageSize(int pageSize)
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();
            var viewModelFactory = new Mock<IViewModelFactory>();
            var forumThreadViewModel = new Mock<IForumThreadViewModel>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            pagerViewModel.Setup(p => p.PageSize).Returns(pageSize);
            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(f => f.PagerViewModel).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(t => t.Thread).Returns(new ThreadViewModel() { Id = 1 });
            viewModelFactory.Setup(v => v.CreateForumThreadViewModel(It.IsAny<ThreadViewModel>(),
                It.IsAny<IEnumerable<AnswerViewModel>>(), It.IsAny<IPagerViewModel>())).Returns(forumThreadViewModel.Object);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            var resultModel = result.Model as IForumThreadViewModel;

            // Assert
            Assert.AreEqual(pageSize, resultModel.PagerViewModel.PageSize);
        }

        [Test]
        public void Forum_ThreadController_Index_ShouldReturnForumThreadViewModelWithCorrectThread()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var pagerViewModel = new Mock<IPagerViewModel>();
            var viewModelFactory = new Mock<IViewModelFactory>();
            var forumThreadViewModel = new Mock<IForumThreadViewModel>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            pagerFactory.Setup(p => p.CreatePagerViewModel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(f => f.PagerViewModel).Returns(pagerViewModel.Object);
            forumThreadViewModel.Setup(t => t.Thread).Returns(new ThreadViewModel() { Id = 1 });
            viewModelFactory.Setup(v => v.CreateForumThreadViewModel(It.IsAny<ThreadViewModel>(),
                It.IsAny<IEnumerable<AnswerViewModel>>(), It.IsAny<IPagerViewModel>())).Returns(forumThreadViewModel.Object);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Thread, ThreadViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Comment, CommentViewModel>();
            });

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            // Act
            var result = controller.Index(1) as ViewResult;
            var resultModel = result.Model as IForumThreadViewModel;

            // Assert
            Assert.AreEqual(1, resultModel.Thread.Id);
        }

        //[Test]
        public void ThreadController_Index_ShouldReturnAnswersEqulToPageSizeAndOrederedByPublishedProperyAtPage2()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            data.Setup(d => d.Threads.All()).Returns(TestThread().AsQueryable());
            data.Setup(d => d.Answers.All()).Returns(AnswersCollection().AsQueryable);

            var controller = new ThreadController(data.Object, pagerFactory.Object, viewModelFactory.Object);

            var expected = new List<Answer>()
            {
                new Answer() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 04), ThreadId = 1, Content=string.Empty, Comments = new List<Comment>() { new Comment() { Id = 2, IsVisible = false } } },
                new Answer() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 05), ThreadId = 1, Content=string.Empty },
                new Answer() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 06), ThreadId = 1, Content=string.Empty }
            };

            // Act
            var result = controller.Index(1, 2) as ViewResult;
            ForumThreadViewModel resultModel = result.Model as ForumThreadViewModel;

            // Assert
            CollectionAssert.AreEqual(expected, resultModel.Answers, new AnswerComparer());
        }


        private ICollection<Answer> AnswersCollection()
        {
            return new List<Answer>()
            {
                new Answer() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 04), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() { new Comment() { Id = 2, IsVisible = false } } },
                new Answer() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() { new Comment() { Id = 1, IsVisible = true } } },
                new Answer() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 01), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() },
                new Answer() { Id = 4, IsVisible = true, Published = new DateTime(2017, 01, 03), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() },
                new Answer() { Id = 5, IsVisible = true, Published = new DateTime(2017, 01, 05), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() },
                new Answer() { Id = 6, IsVisible = true, Published = new DateTime(2017, 01, 06), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() },
                new Answer() { Id = 7, IsVisible = false, Published = new DateTime(2017, 01, 08), ThreadId = 1, Content=string.Empty, UserId = "id", User = new ApplicationUser() { UserName = "testUserName" }, Thread = new Thread() { Id = 1 }, Comments = new List<Comment>() }
            };
        }

        private IEnumerable<Thread> TestThread()
        {
            return new List<Thread>()
            {
                new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 01), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty },
                new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 02), Title = "SomeTitle", Content = "SomeContent", UserId = "id", User = new ApplicationUser() { UserName = "testUserName"}, Section = new Section() { Name = "testSectionName" }, Answers = new List<Answer>(), EditedById = string.Empty }
            };
        }
    }
}
