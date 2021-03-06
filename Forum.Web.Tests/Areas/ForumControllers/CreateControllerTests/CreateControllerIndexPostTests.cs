﻿using Forum.Data;
using Forum.Models;
using Forum.Services.Contracts;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Areas.Forum.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.CreateControllerTests
{
    [TestFixture]
    public class CreateControllerIndexPostTests
    {
        [Test]
        public void Forum_CreateController_Index_Post_ShouldAddInstanceOfAThread()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var mappingService = new Mock<IMappingService>();

            var threadRepository = new Mock<IRepository<Thread>>();
            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            var result = controller.Index(thread) as ViewResult;

            //Assert
            threadRepository.Verify(d => d.Add(It.IsAny<Thread>()));
        }

        [Test]
        public void Forum_CreateController_Index_Post_ShouldAddInstanceOfAThreadWithPublishedPropertySet()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            var result = controller.Index(thread) as ViewResult;

            //Assert
            threadRepository.Verify(d => d.Add(It.Is<Thread>(t => t.Published.GetType() == typeof(DateTime))));
        }

        [Test]
        public void Forum_CreateController_Index_Post_ShouldAddInstanceOfAThreadWithVisiblePropertySetToTrue()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            var result = controller.Index(thread) as ViewResult;

            //Assert
            threadRepository.Verify(d => d.Add(It.Is<Thread>(t => t.IsVisible == true)));
        }

        [TestCase("3f0c9a41-19e2-4a5c-a901-b3b056e50dbf")]
        [TestCase("4398df12-1604-429c-9214-1715a72fd56e")]
        [TestCase("sdf798sd-s878-sd3s-ass2-1715afds878s")]
        public void Forum_CreateController_Index_Post_ShouldAddInstanceOfAThreadWithCorrectUser(string id)
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", id);

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            var result = controller.Index(thread) as ViewResult;

            //Assert
            threadRepository.Verify(d => d.Add(It.Is<Thread>(t => t.UserId == id)));
        }

        [Test]
        public void Forum_CreateController_Index_Post_ShouldCallDataSaveChanges()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            var result = controller.Index(thread) as ViewResult;

            //Assert
            data.Verify(d => d.SaveChanges(), Times.Once);
        }

        [Test]
        public void Forum_CreateController_Index_Post_ShouldCallRedirectToActionWithCorrectControllerParam()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            RedirectToRouteResult redirectResult = controller.Index(thread) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Thread", redirectResult.RouteValues["Controller"]);
        }

        [Test]
        public void Forum_CreateController_Index_Post_ShouldCallRedirectToActionWithCorrectActionParam()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread());

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            RedirectToRouteResult redirectResult = controller.Index(thread) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual("Index", redirectResult.RouteValues["Action"]);
        }

        [Test]
        public void Forum_CreateController_Index_Post_ShouldCallRedirectToActionWithCorrectRouteParams()
        {
            //Arrange
            var data = new Mock<IUowData>();
            var threadRepository = new Mock<IRepository<Thread>>();
            var mappingService = new Mock<IMappingService>();

            data.Setup(d => d.Threads).Returns(threadRepository.Object);

            var claim = new Claim("test", "asd-123");

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.Identity).Returns(identity.Object);

            var context = new Mock<ControllerContext>();
            context.Setup(c => c.HttpContext.User).Returns(principal.Object);

            mappingService.Setup(m => m.Map<Thread>(It.IsAny<CreateThreadViewModel>())).Returns(new Thread() { Id = 3 });

            CreateController controller = new CreateController(data.Object, mappingService.Object)
            {
                ControllerContext = context.Object
            };

            var thread = new CreateThreadViewModel();

            //Act
            RedirectToRouteResult redirectResult = controller.Index(thread) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(3, redirectResult.RouteValues["id"]);
        }
    }
}
