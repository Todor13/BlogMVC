using Forum.Data;
using Forum.Models;
using Forum.Web.Areas.Forum.Controllers;
using Forum.Web.Tests.Areas.ForumControllers.Helpers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Web.Tests.Areas.ForumControllers.CreateControllerTests
{
    [TestFixture]
    public class CreateControllerIndexTests
    {
        [Test]
        public void CreateController_Index_ShouldReturnAnInstanceOfSelectList()
        {
            //Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Sections.All()).Returns(GetSections().AsQueryable());

            CreateController controller = new CreateController(data.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOf<SelectList>(result.ViewBag.SectionId);
        }

        [Test]
        public void CreateController_Index_ShouldReturnCorrectSectionsInSelectList()
        {
            //Arrange
            var data = new Mock<IUowData>();
            data.Setup(d => d.Sections.All()).Returns(GetSections().AsQueryable());

            CreateController controller = new CreateController(data.Object);

            //Act
            var result = controller.Index() as ViewResult;
            var selectList = result.ViewBag.SectionId as SelectList;

            //Assert
            CollectionAssert.AreEqual(GetSections(), selectList.Items, new SectionComparer());
        }

        private ICollection<Section> GetSections()
        {
            return new List<Section>()
            {
                new Section() { Id = 1, Name = "First" },
                new Section() { Id = 2, Name = "Second" },
                new Section() { Id = 3, Name = "Third" }
            };
        }
    }
}
