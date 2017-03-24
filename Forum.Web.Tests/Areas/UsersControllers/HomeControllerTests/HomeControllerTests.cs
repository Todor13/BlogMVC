using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Users.Controllers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Web.Tests.Areas.UsersControllers.HomeControllerTests
{
    [TestFixture]
    public class HomeControllerTests
    {
        
        public void UsersHomeController_Index_Should()
        {
            // Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Users.All()).Returns(UsersCollection().AsQueryable());
            //HomeController controller = new HomeController(data.Object);

            // Act


            // Assert
        }

        private ICollection<ApplicationUser> UsersCollection()
        {
            return new List<ApplicationUser>()
            {
                new ApplicationUser() { Id = "9d47be05-069c-4b59-8491-d78c451fe7d5" },
                new ApplicationUser() { Id = "579c957d-b103-4b3a-acb0-1acb80f17692" },
                new ApplicationUser() { Id = "ea70a65b-12b4-4df3-8ee6-33b0554c47e7" },
                new ApplicationUser() { Id = "4264074f-1b34-4599-87f4-bc2616181c91" }
            };
        }
    }
}
