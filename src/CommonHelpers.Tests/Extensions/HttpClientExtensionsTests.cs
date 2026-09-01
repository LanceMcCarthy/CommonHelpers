using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class HttpClientExtensionsTests
{
    private const string TestUrl = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";

    private static ServiceProvider CreateServiceProvider()
    {
        return new ServiceCollection().AddHttpClient().BuildServiceProvider();
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_Works()
    {
        var reporter = new Progress<DownloadProgressArgs>();
        float progress = 0;

        reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

        using var client = new HttpClient();
        var result = await client.DownloadStringWithProgressAsync(TestUrl, reporter);

        IsFalse(string.IsNullOrEmpty(result), "String result was null");
        IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStringWithProgressAndCancellation_Works()
    {
        var cts = new CancellationTokenSource();
        float progress = 0;
        var reporter = new Progress<DownloadProgressArgs>();
        reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

        using var client = new HttpClient();
        var result = await client.DownloadStringWithProgressAsync(TestUrl, reporter, cts.Token);

        IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
        IsFalse(string.IsNullOrEmpty(result), "String result was null.");
        IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_Works_WithFactoryCreatedClient()
    {
        var reporter = new Progress<DownloadProgressArgs>();
        float progress = 0;

        reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

        await using var serviceProvider = CreateServiceProvider();
        var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        using var client = clientFactory.CreateClient();
        var result = await client.DownloadStringWithProgressAsync(TestUrl, reporter);

        IsFalse(string.IsNullOrEmpty(result), "String result was null");
        IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStreamWithProgress_Works()
    {
        float progress = 0;
        var reporter = new Progress<DownloadProgressArgs>();
        reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

        using var client = new HttpClient();
        var result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter);

        IsNotNull(result);
        IsTrue(result.Length > 0, "Stream is empty");
        IsTrue(progress > 0 && progress <= 100, "Progress was not reported correctly");
        await result.DisposeAsync();
    }

    [TestMethod]
    public async Task DownloadStreamWithProgress_Works_WithFactoryCreatedClient()
    {
        float progress = 0;
        var reporter = new Progress<DownloadProgressArgs>();
        reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

        await using var serviceProvider = CreateServiceProvider();
        var clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        using var client = clientFactory.CreateClient();
        var result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter);

        IsNotNull(result);
        IsTrue(result.Length > 0, "Stream is empty");
        IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
        await result.DisposeAsync();
    }

    [TestMethod]
    public async Task DownloadStreamWithProgressAndCancellation_Works()
    {
        var cts = new CancellationTokenSource();
        float progress = 0;
        var reporter = new Progress<DownloadProgressArgs>();
        reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

        using var client = new HttpClient();
        var result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter, cts.Token);

        IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
        IsNotNull(result);
        IsTrue(result.Length > 0, "Stream is empty");
        IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
        await result.DisposeAsync();
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_Cancellation_Throws()
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var reporter = new Progress<DownloadProgressArgs>();
        using var client = new HttpClient();
        await ThrowsExactlyAsync<TaskCanceledException>(async () =>
        {
            await client.DownloadStringWithProgressAsync(TestUrl, reporter, cts.Token);
        });
    }

    [TestMethod]
    public async Task DownloadStreamWithProgress_Cancellation_Throws()
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var reporter = new Progress<DownloadProgressArgs>();
        using var client = new HttpClient();
        await ThrowsExactlyAsync<TaskCanceledException>(async () =>
        {
            await client.DownloadStreamWithProgressAsync(TestUrl, reporter, cts.Token);
        });
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_NullClient_Throws()
    {
        HttpClient client = null;
        var reporter = new Progress<DownloadProgressArgs>();
        await ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            await client.DownloadStringWithProgressAsync(TestUrl, reporter);
        });
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_NullUrl_Throws()
    {
        using var client = new HttpClient();
        var reporter = new Progress<DownloadProgressArgs>();
        await ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            await client.DownloadStringWithProgressAsync(null, reporter);
        });
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_NullReporter_Throws()
    {
        using var client = new HttpClient();
        await ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            await client.DownloadStringWithProgressAsync(TestUrl, null);
        });
    }

    [TestMethod]
    public async Task ApplyRetryDelayAsync_UsesDelta()
    {
        // Arrange: Delta = 500ms
        var delta = TimeSpan.FromMilliseconds(500);
        var header = new System.Net.Http.Headers.RetryConditionHeaderValue(delta);
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Act
        await header.ApplyRetryDelayAsync();
        sw.Stop();

        // Assert: Should be at least 400ms (allowing for timing inaccuracy)
        IsTrue(sw.ElapsedMilliseconds >= 400, $"Delay was too short: {sw.ElapsedMilliseconds}ms");
    }

    [TestMethod]
    public async Task ApplyRetryDelayAsync_UsesDate()
    {
        // Arrange: Date = 600ms in the future
        var future = DateTimeOffset.UtcNow.AddMilliseconds(600);
        var header = new System.Net.Http.Headers.RetryConditionHeaderValue(future);
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Act
        await header.ApplyRetryDelayAsync();
        sw.Stop();

        // Assert: Should be at least 500ms (allowing for timing inaccuracy)
        IsTrue(sw.ElapsedMilliseconds >= 500, $"Delay was too short: {sw.ElapsedMilliseconds}ms");
    }

    [TestMethod]
    public async Task ApplyRetryDelayAsync_DefaultsTo2Seconds()
    {
        // Arrange: Neither Delta nor Date set
        var header = new System.Net.Http.Headers.RetryConditionHeaderValue(DateTimeOffset.MinValue); // Use a valid value, but not in the future
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Act
        await header.ApplyRetryDelayAsync();
        sw.Stop();

        // Assert: Should be at least 1500ms (allowing for timing inaccuracy)
        IsTrue(sw.ElapsedMilliseconds >= 1500, $"Delay was too short: {sw.ElapsedMilliseconds}ms");
    }
}