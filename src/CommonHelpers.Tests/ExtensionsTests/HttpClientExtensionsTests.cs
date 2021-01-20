using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.ExtensionsTests
{
    [TestClass]
    public class HttpClientExtensionsTests
    {
        private readonly HttpClient client = new();

        [TestMethod]
        public async Task DownloadStringWithProgress()
        {
            // Arrange
            var reporter = new Progress<DownloadProgressArgs>();
            float progress = 0;
            const string url = "https://httpbin.org/encoding/utf8";

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            var result = await client.DownloadStringWithProgressAsync(url, reporter);

            // Assert
            Assert.IsTrue(progress > 0, "DownloadStringWithProgress - progress was not incremented");
            Assert.IsFalse(string.IsNullOrEmpty(result), "String result was null");
        }

        [TestMethod]
        public async Task DownloadStringWithProgressAndCancellation()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            float progress = 0;
            const string url = "https://httpbin.org/encoding/utf8";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            var result = await client.DownloadStringWithProgressAsync(url, reporter, cts.Token);

            // Assert
            Assert.IsTrue(progress > 0, "DownloadStringWithProgressAndCancellation - progress was not incremented.");
            Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            Assert.IsFalse(string.IsNullOrEmpty(result), "String result was null.");
        }

        [TestMethod]
        public async Task DownloadStreamWithProgress()
        {
            // Arrange
            Stream result = null;
            float progress = 0;
            const string url = "https://progressdevsupport.blob.core.windows.net/sampledocs/pdfviewer-overview.pdf";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            result = await client.DownloadStreamWithProgressAsync(url, reporter);

            // Assert
            Assert.IsTrue(progress > 0, "DownloadStreamWithProgress - progress was not incremented");
            Assert.IsTrue(result != null, "Stream is null");

            await result.DisposeAsync();
        }

        [TestMethod]
        public async Task DownloadSteamWithProgressAndCancellation()
        {
            // Arrange
            Stream result = null;
            var cts = new CancellationTokenSource();
            float progress = 0;
            const string url = "https://progressdevsupport.blob.core.windows.net/sampledocs/pdfviewer-overview.pdf";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            result = await client.DownloadStreamWithProgressAsync(url, reporter, cts.Token);

            // Assert
            Assert.IsTrue(progress > 0, "DownloadSteamWithProgressAndCancellation - progress was not incremented.");
            Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            Assert.IsTrue(result != null, "Stream is null");

            await result.DisposeAsync();
        }
    }
}
