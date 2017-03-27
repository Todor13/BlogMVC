using Forum.Web.Models.Common;
using NUnit.Framework;

namespace Forum.Web.Tests.ModelsTests
{
    [TestFixture]
    public class AjaxPagerViewModelTests
    {
        [TestCase(6, 3, 2)]
        [TestCase(7, 3, 3)]
        [TestCase(21, 5, 5)]
        public void PagerViewModel_ShouldCalculatePagesCorrectly(int itemsCount, int pageSize, int expected)
        {
            // Arrange & Act
            var model = new AjaxPagerViewModel("SomeName", "action", "target", 1, itemsCount, pageSize);

            // Assert
            Assert.AreEqual(expected, model.PagesCount);
        }
    }
}
