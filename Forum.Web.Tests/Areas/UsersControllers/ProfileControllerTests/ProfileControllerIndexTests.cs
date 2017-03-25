using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Users.Controllers;
using Forum.Web.Areas.Users.Models;
using Forum.Web.Factories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.UsersControllers.ProfileControllerTests
{
    [TestFixture]
    public class ProfileControllerIndexTests
    {
        [Test]
        public void Users_ProfileController_Index_ShouldReturnCorrectViewModel()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act
            var result = controller.Index("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as ViewResult;

            // Assert
            Assert.IsInstanceOf<UserViewModel>(result.Model);
        }

        [Test]
        public void Users_ProfileController_Index_ShouldReturnCorrectUser()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            var expected = new UserViewModel() { Id = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7", Email = "bcd@test.ts" };

            // Act
            var result = controller.Index("ea70a65b-12b4-4df3-8ee6-33b0554c47e7") as ViewResult;
            var resultModel = result.Model as UserViewModel;

            // Assert
            Assert.That(expected, Has.Property("Id").EqualTo(resultModel.Id) &
                Has.Property("Email").EqualTo(resultModel.Email));
        }

        [Test]
        public void Users_ProfileController_Index_ShouldThrowWhenUserNotFound()
        {
            // Arrange
            var data = new Mock<IUowData>();
            var pagerFactory = new Mock<IPagerViewModelFactory>();

            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());

            ProfileController controller = new ProfileController(data.Object, pagerFactory.Object);

            // Act & Assert
            Assert.Throws<HttpException>(() => controller.Index("SuchUserDoesNotExist"));
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
