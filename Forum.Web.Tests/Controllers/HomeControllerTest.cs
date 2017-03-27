using System.Web.Mvc;
using Forum.Web.Controllers;
using Forum.Data;
using Moq;
using NUnit.Framework;
using Forum.Web.Factories.Contracts;
using Forum.Web.Models.Contracts;
using System.Collections.Generic;
using Forum.Web.Models;
using Forum.Models;
using System;
using System.Linq;
using AutoMapper;

namespace Forum.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void HomeController_Index_ShouldReturnCorrectModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, IndexPageThreadViewModel>());

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());
            
            HomeController controller = new HomeController(data.Object, viewModelFactory.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            viewModelFactory.Verify(v => v.CreateHomePageViewModel(It.IsAny<ICollection<IndexPageThreadViewModel>>(), It.IsAny<ICollection<IndexPageThreadViewModel>>(), It.IsAny<ICollection<IndexPageThreadViewModel>>()));
        }

        [Test]
        public void HomeController_Index_ShouldReturnCorrectCount()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, IndexPageThreadViewModel>());

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            HomeController controller = new HomeController(data.Object, viewModelFactory.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            viewModelFactory.Verify(v => v.CreateHomePageViewModel(It.Is<ICollection<IndexPageThreadViewModel>>(t => t.Count == 3), It.Is<ICollection<IndexPageThreadViewModel>>(t => t.Count == 3), It.Is<ICollection<IndexPageThreadViewModel>>(t => t.Count == 1)));
        }

        [Test]
        public void HomeController_Index_ShouldReturnCorrectThreads()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            Mapper.Initialize(cfg => cfg.CreateMap<Thread, IndexPageThreadViewModel>());

            data.Setup(d => d.Threads.All()).Returns(ThreadsCollection().AsQueryable());

            HomeController controller = new HomeController(data.Object, viewModelFactory.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            viewModelFactory.Verify(v => v.CreateHomePageViewModel(It.Is<ICollection<IndexPageThreadViewModel>>(t => t.ElementAt(0).Id == 2), It.Is<ICollection<IndexPageThreadViewModel>>(t => t.ElementAt(0).Id == 1), It.Is<ICollection<IndexPageThreadViewModel>>(t => t.ElementAt(0).Id == 1)));
        }

        [Test]
        public void About()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            HomeController controller = new HomeController(data.Object, viewModelFactory.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var viewModelFactory = new Mock<IViewModelFactory>();

            HomeController controller = new HomeController(data.Object, viewModelFactory.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        private ICollection<Thread> ThreadsCollection()
        {
            return new List<Thread>()
            {
                                 new Thread() { Id = 1, IsVisible = true, Published = new DateTime(2017, 01, 01), Section = new Section() { Name = "Important" }, Answers = new List<Answer>() { new Answer() { Published = new DateTime(2017, 01, 02) } } },
                  new Thread() { Id = 2, IsVisible = true, Published = new DateTime(2017, 01, 03), Section = new Section() { Name = string.Empty }, Answers = new List<Answer>() },
                  new Thread() { Id = 3, IsVisible = true, Published = new DateTime(2017, 01, 02), Section = new Section() { Name = string.Empty }, Answers = new List<Answer>() }
            };
        }
    }
}
