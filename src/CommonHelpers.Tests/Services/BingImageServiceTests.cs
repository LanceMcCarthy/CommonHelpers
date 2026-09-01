using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommonHelpers.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services;

[TestClass]
public class BingImageServiceTests : IDisposable
{
    private readonly BingImageService service = new();

    [TestMethod]
    public async Task GetTodaysBingImage()
    {
        await using var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        using var client = clientFactory.CreateClient();

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