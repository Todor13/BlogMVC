using Forum.Web.Models.Common;
using NUnit.Framework;

namespace Forum.Web.Tests.ModelsTests
{
    [TestFixture]
    public class PagerViewModelTests
    {
        [TestCase(12, 6, 2)]
        [TestCase(22, 7, 4)]
        [TestCase(30, 5, 6)]
        public void PagerViewModel_ShouldCalculatePagesCorrectly(int itemsCount, int pageSize, int expected)
        {
            // Arrange & Act
            var model = new PagerViewModel("SomeName", 1, itemsCount, pageSize);

            // Assert
            Assert.AreEqual(expected, model.PagesCount);
        }
    }
}
