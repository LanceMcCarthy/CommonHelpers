using System;
using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services
{
    [TestClass]
    public class XkcdServiceTests : IDisposable
    {
        private readonly XkcdApiService service = new();

        // Arrange

        [TestMethod]
        public void GetTodaysComic()
        {
            //Act
            var xkcdComic = service.GetNewestComicAsync().Result;

            // Assert
            Assert.IsNotNull(xkcdComic);
        }

        [TestMethod]
        public void GetComicById()
        {
            //Arrange
            var comicNumber = 1214;

            // Act
            var xkcdComic = service.GetComicAsync(comicNumber).Result;

            // Assert
            Assert.IsNotNull(xkcdComic);
        }

        [TestCleanup]
        public void Dispose()
        {
            service.Dispose();
        }
    }
}
