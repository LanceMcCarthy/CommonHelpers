using System;
using System.Linq;
using System.Threading.Tasks;
using CommonHelpers.Services;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services;

[TestClass]
public class ComicVineServiceTests : IDisposable
{
    private readonly ComicVineApiService service = new(StaticValues.ComicVineApiKey, StaticValues.UniqueUserAgentString);

    [TestMethod]
    public async Task GetCharacters()
    {
        const int expectedCount = 10;
        var result = await service.GetCharactersAsync(0, 10);
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedCount, result.Results.Count);
    }

    [TestMethod]
    public async Task GetVideos()
    {
        const int expectedCount = 10;
        var result = await service.GetVideosAsync(0, 10);
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedCount, result.Results.Count);
    }

    [TestMethod]
    public async Task GetImage()
    {
        var characterResult = await service.GetCharactersAsync(0, 3);
        var character = characterResult.Results.FirstOrDefault();
        Assert.IsNotNull(character);
        Assert.IsNotNull(character.Image);
        Assert.IsNotNull(character.Image.OriginalUrl);
        var imageUrl = character.Image.OriginalUrl;

        using var imageStream = await service.GetImageAsync(imageUrl);
        var imageBytes = imageStream.ToArray();

        Assert.IsNotNull(imageBytes);
        Assert.IsTrue(imageBytes.Length > 0);
    }

    [TestCleanup]
    public void Dispose()
    {
        service.Dispose();
    }
}