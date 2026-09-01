using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class HttpClientExtensionsTests
{
    private const string TestUrl = "https://example.test/LoremIpsum.txt";
    private static readonly string TestContent = new('A', 10000);
    private static readonly byte[] TestContentBytes = Encoding.UTF8.GetBytes(TestContent);
    private readonly HttpClient client = CreateClient();

    private sealed class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(CreateResponse());
        }
    }

    private sealed class SynchronousProgressReporter : IProgress<DownloadProgressArgs>
    {
        public float PercentComplete { get; private set; }

        public void Report(DownloadProgressArgs value)
        {
            PercentComplete = value.PercentComplete;
        }
    }

    private static HttpClient CreateClient() => new(new FakeHttpMessageHandler());

    private static HttpResponseMessage CreateResponse()
    {
        var content = new ByteArrayContent(TestContentBytes);
        content.Headers.ContentLength = TestContentBytes.Length;

        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = content
        };
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_Works()
    {
        var reporter = new SynchronousProgressReporter();

        var result = await client.DownloadStringWithProgressAsync(TestUrl, reporter);

        Assert.AreEqual(TestContent, result);
        Assert.IsTrue(reporter.PercentComplete is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStringWithProgressAndCancellation_Works()
    {
        var cts = new CancellationTokenSource();
        var reporter = new SynchronousProgressReporter();

        var result = await client.DownloadStringWithProgressAsync(TestUrl, reporter, cts.Token);

        Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
        Assert.AreEqual(TestContent, result);
        Assert.IsTrue(reporter.PercentComplete is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStreamWithProgress_Works()
    {
        var reporter = new SynchronousProgressReporter();

        await using var result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter);
        using var reader = new StreamReader(result);
        var content = await reader.ReadToEndAsync();

        Assert.IsNotNull(result);
        Assert.AreEqual(TestContentBytes.Length, result.Length, "Stream length was unexpected");
        Assert.AreEqual(TestContent, content);
        Assert.IsTrue(reporter.PercentComplete is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStreamWithProgressAndCancellation_Works()
    {
        var cts = new CancellationTokenSource();
        var reporter = new SynchronousProgressReporter();

        await using var result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter, cts.Token);
        using var reader = new StreamReader(result);
        var content = await reader.ReadToEndAsync();

        Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
        Assert.IsNotNull(result);
        Assert.AreEqual(TestContentBytes.Length, result.Length, "Stream length was unexpected");
        Assert.AreEqual(TestContent, content);
        Assert.IsTrue(reporter.PercentComplete is > 0 and <= 100, "Progress was not reported correctly");
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_Cancellation_Throws()
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var reporter = new SynchronousProgressReporter();
        await Assert.ThrowsExactlyAsync<TaskCanceledException>(async () =>
        {
            await client.DownloadStringWithProgressAsync(TestUrl, reporter, cts.Token);
        });
    }

    [TestMethod]
    public async Task DownloadStreamWithProgress_Cancellation_Throws()
    {
        var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var reporter = new SynchronousProgressReporter();
        await Assert.ThrowsExactlyAsync<TaskCanceledException>(async () =>
        {
            await client.DownloadStreamWithProgressAsync(TestUrl, reporter, cts.Token);
        });
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_NullClient_Throws()
    {
        HttpClient nullClient = null;
        var reporter = new Progress<DownloadProgressArgs>();
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            await nullClient.DownloadStringWithProgressAsync(TestUrl, reporter);
        });
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_NullUrl_Throws()
    {
        var reporter = new Progress<DownloadProgressArgs>();
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            await client.DownloadStringWithProgressAsync(null, reporter);
        });
    }

    [TestMethod]
    public async Task DownloadStringWithProgress_NullReporter_Throws()
    {
        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
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
        Assert.IsTrue(sw.ElapsedMilliseconds >= 400, $"Delay was too short: {sw.ElapsedMilliseconds}ms");
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
        Assert.IsTrue(sw.ElapsedMilliseconds >= 500, $"Delay was too short: {sw.ElapsedMilliseconds}ms");
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
        Assert.IsTrue(sw.ElapsedMilliseconds >= 1500, $"Delay was too short: {sw.ElapsedMilliseconds}ms");
    }


    [TestCleanup]
    public void Dispose()
    {
        client.Dispose();
    }
}