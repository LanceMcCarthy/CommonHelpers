using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonHelpers.Tests.Services;

[TestClass]
public class BingImageServiceTests : IDisposable
{
    private readonly BingImageService service = new();

    [TestMethod]
    public async Task GetTodaysBingImage()
    {
        using var client = new HttpClient();

        var imageUrl = await service.GetBingImageOfTheDayAsync();

        using var response = await client.GetAsync(imageUrl);
        var imageBytes = await response.Content.ReadAsByteArrayAsync();
        var byteCount = imageBytes.Length;
        Assert.IsTrue(byteCount > 0);
    }

    [TestCleanup]
    public void Dispose()
    {
        service.Dispose();
    }
}