using System;
using System.Net.Http;
using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services
{
    [TestClass]
    public class BingImageServiceTests : IDisposable
    {
        private readonly BingImageService service;

        public BingImageServiceTests()
        {
            // Arrange
            service = new BingImageService();
        }

        [TestMethod]
        public void GetTodaysBingImage()
        {
            // Arrange
            string imageUrl;
            byte[] imageBytes;

            // Act
            imageUrl = service.GetBingImageOfTheDayAsync().Result;

            // Assert
            using (var client = new HttpClient())
            using (var response = client.GetAsync(imageUrl).Result)
            {
                imageBytes = response.Content.ReadAsByteArrayAsync().Result;
            }

            // Assert
            var byteCount = imageBytes.Length;
            Assert.IsTrue(byteCount > 0);
        }

        [TestCleanup]
        public void Dispose()
        {
            service.Dispose();
        }
    }
}
