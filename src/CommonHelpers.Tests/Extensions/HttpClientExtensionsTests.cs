using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CommonHelpers.Tests.Extensions
{
    [TestClass]
    public class HttpClientExtensionsTests
    {
        private const string TestUrl = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";

        [TestMethod]
        public async Task DownloadStringWithProgress_Works()
        {
            string result = "";
            var reporter = new Progress<DownloadProgressArgs>();
            float progress = 0;

            reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

            using var client = new HttpClient();
            result = await client.DownloadStringWithProgressAsync(TestUrl, reporter);

            IsFalse(string.IsNullOrEmpty(result), "String result was null");
            IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
        }

        [TestMethod]
        public async Task DownloadStringWithProgressAndCancellation_Works()
        {
            string result = null;
            var cts = new CancellationTokenSource();
            float progress = 0;
            var reporter = new Progress<DownloadProgressArgs>();
            reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

            using var client = new HttpClient();
            result = await client.DownloadStringWithProgressAsync(TestUrl, reporter, cts.Token);

            IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            IsFalse(string.IsNullOrEmpty(result), "String result was null.");
            IsTrue(progress is > 0 and <= 100, "Progress was not reported correctly");
        }

        [TestMethod]
        public async Task DownloadStreamWithProgress_Works()
        {
            Stream result = null;
            float progress = 0;
            var reporter = new Progress<DownloadProgressArgs>();
            reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

            using var client = new HttpClient();
            result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter);

            IsNotNull(result);
            IsTrue(result.Length > 0, "Stream is empty");
            IsTrue(progress > 0 && progress <= 100, "Progress was not reported correctly");
            await result.DisposeAsync();
        }

        [TestMethod]
        public async Task DownloadStreamWithProgressAndCancellation_Works()
        {
            Stream result = null;
            var cts = new CancellationTokenSource();
            float progress = 0;
            var reporter = new Progress<DownloadProgressArgs>();
            reporter.ProgressChanged += (s, e) => progress = e.PercentComplete;

            using var client = new HttpClient();
            result = await client.DownloadStreamWithProgressAsync(TestUrl, reporter, cts.Token);

            IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            IsNotNull(result);
            IsTrue(result.Length > 0, "Stream is empty");
            IsTrue(progress > 0 && progress <= 100, "Progress was not reported correctly");
            await result.DisposeAsync();
        }

        [TestMethod]
        public async Task DownloadStringWithProgress_Cancellation_Throws()
        {
            var cts = new CancellationTokenSource();
            await cts.CancelAsync();
            var reporter = new Progress<DownloadProgressArgs>();
            using var client = new HttpClient();
            await ThrowsExceptionAsync<TaskCanceledException>(async () =>
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
            await ThrowsExceptionAsync<TaskCanceledException>(async () =>
            {
                await client.DownloadStreamWithProgressAsync(TestUrl, reporter, cts.Token);
            });
        }

        [TestMethod]
        public async Task DownloadStringWithProgress_NullClient_Throws()
        {
            HttpClient client = null;
            var reporter = new Progress<DownloadProgressArgs>();
            await ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await client.DownloadStringWithProgressAsync(TestUrl, reporter);
            });
        }

        [TestMethod]
        public async Task DownloadStringWithProgress_NullUrl_Throws()
        {
            using var client = new HttpClient();
            var reporter = new Progress<DownloadProgressArgs>();
            await ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await client.DownloadStringWithProgressAsync(null, reporter);
            });
        }

        [TestMethod]
        public async Task DownloadStringWithProgress_NullReporter_Throws()
        {
            using var client = new HttpClient();
            await ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await client.DownloadStringWithProgressAsync(TestUrl, null);
            });
        }
    }
}
