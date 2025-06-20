using System;
using System.Linq;
using CommonHelpers.Services;
using CommonHelpers.Services.DataModels;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services
{
    [TestClass]
    public class ComicVineServiceTests : IDisposable
    {
        private readonly ComicVineApiService service = new(StaticValues.ComicVineApiKey, StaticValues.UniqueUserAgentString);

        [TestMethod]
        public void GetCharacters()
        {
            //Arrange
            int expectedCount = 10;

            //Act
            CharactersResult result = service.GetCharactersAsync(0, 10).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, result.Results.Count);
        }

        [TestMethod]
        public void GetVideos()
        {
            //Arrange
            int expectedCount = 10;

            //Act
            VideosResult result = service.GetVideosAsync(0, 10).Result;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, result.Results.Count);
        }

        [TestMethod]
        public void GetImage()
        {
            //Arrange
            byte[] imageBytes;

            var characterResult = service.GetCharactersAsync(0, 3).Result;
            var character = characterResult.Results.FirstOrDefault();
            var imageUrl = character?.Image.OriginalUrl;

            //Act
            using (var imageStream = service.GetImageAsync(imageUrl).Result)
            {
                imageBytes = imageStream.ToArray();
            }

            //Assert
            Assert.IsNotNull(imageBytes);
            Assert.IsTrue(imageBytes.Length > 0);
        }

        [TestCleanup]
        public void Dispose()
        {
            service.Dispose();
        }
    }
}
