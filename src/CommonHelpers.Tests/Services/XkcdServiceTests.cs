using System;
using System.Threading.Tasks;
using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services;

[TestClass]
public class XkcdServiceTests : IDisposable
{
    private readonly XkcdApiService service = new();

    [TestMethod]
    public async Task GetTodaysComic()
    {
        var xkcdComic = await service.GetNewestComicAsync();
        Assert.IsNotNull(xkcdComic);
    }

    [TestMethod]
    public async Task GetComicById()
    {
        const int comicNumber = 1214;
        var xkcdComic = await service.GetComicAsync(comicNumber);
        Assert.IsNotNull(xkcdComic);
    }

    [TestCleanup]
    public void Dispose()
    {
        service.Dispose();
    }
}