using System;
using NUnit.Framework;
using Moq;
using Forum.Data;
using Forum.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Forum.Data.Tests.Helpers;

namespace Forum.Data.Tests
{
    [TestFixture]
    public class EfRepositoryTests
    {
        [Test]
        public void EfRepository_ShouldThrowWhenDbContextIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new EfRepository<Thread>(null));
        }

        [Test]
        public void EfRepository_Threads_All_ShouldReturnCorrectThreadsCount()
        {
            // Arrange
            var threads = GetThreads(3);
            var threadsDbSet = GetThreadsDbSet(threads);
            var forumDbContext = new Mock<IForumDbContext>();

            forumDbContext.Setup(c => c.Set<Thread>()).Returns(threadsDbSet);

            var repository = new EfRepository<Thread>(forumDbContext.Object);

            // Act
            var result = repository.All().ToList();

            // Assert
            Assert.AreEqual(threadsDbSet.Count(), result.Count);
        }

        [Test]
        public void EfRepository_Threads_Should()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Thread>>();

            var mockContext = new Mock<IForumDbContext>();
            mockContext.Setup(m => m.Threads).Returns(mockSet.Object);

            var repository = new EfRepository<Thread>(mockContext.Object);
            repository.Add(new Thread());

            mockSet.Verify(m => m.Add(It.IsAny<Thread>()), Times.Once());
        }


        private IEnumerable<Thread> GetThreads(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new Thread
                {
                    Id = i
                };
            }
        }

        private FakeDbSet<Thread> GetThreadsDbSet(IEnumerable<Thread> threads)
        {
            var threadsDbSet = new FakeDbSet<Thread>();
            foreach (var thread in threads)
            {
                threadsDbSet.Add(thread);
            }

            return threadsDbSet;
        }
    }
}
