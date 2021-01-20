using System;
using System.Diagnostics;
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
        [TestMethod]
        public async Task DownloadStringWithProgress()
        {
            // Arrange
            var reporter = new Progress<DownloadProgressArgs>();
            int progress = 0;
            const string url = "https://raw.githubusercontent.com/LanceMcCarthy/CommonHelpers/main/src/CommonHelpers.Tests/ExtensionsTests/HttpClientExtensionsTests.cs";

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = (int)e.PercentComplete;
            };

            var client = new HttpClient();
            var result = await client.DownloadStringWithProgressAsync(url, reporter);

            Trace.WriteLine($"Progress After Completion: {progress}");

            // Assert
            Assert.IsTrue(progress == 100, "DownloadStringWithProgress - progress did not reach 100% completion");
            Assert.IsFalse(string.IsNullOrEmpty(result), "String result was null");

            client.Dispose();
        }

        [TestMethod]
        public async Task DownloadStringWithProgressAndCancellation()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            int progress = 0;
            const string url = "https://raw.githubusercontent.com/LanceMcCarthy/CommonHelpers/main/src/CommonHelpers.Tests/ExtensionsTests/HttpClientExtensionsTests.cs";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = (int)e.PercentComplete;
            };

            var client = new HttpClient();
            var result = await client.DownloadStringWithProgressAsync(url, reporter, cts.Token);

            Trace.WriteLine($"Progress After Completion: {progress}");

            // Assert
            Assert.IsTrue(progress == 100, "DownloadStringWithProgressAndCancellation - progress did not reach 100% completion.");
            Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            Assert.IsFalse(string.IsNullOrEmpty(result), "String result was null.");

            client.Dispose();
        }

        [TestMethod]
        public async Task DownloadStreamWithProgress()
        {
            // Arrange
            Stream result = null;
            int progress = 0;
            const string url = "https://progressdevsupport.blob.core.windows.net/sampledocs/pdfviewer-overview.pdf";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = (int)e.PercentComplete;
            };

            var client = new HttpClient();
            result = await client.DownloadStreamWithProgressAsync(url, reporter);

            Trace.WriteLine($"Progress After Completion: {progress}");

            // Assert
            Assert.IsTrue(progress == 100, "DownloadStreamWithProgress - progress did not reach 100% completion");
            Assert.IsTrue(result != null, "Stream is null");

            await result.DisposeAsync();
            client.Dispose();
        }

        [TestMethod]
        public async Task DownloadSteamWithProgressAndCancellation()
        {
            // Arrange
            Stream result = null;
            var cts = new CancellationTokenSource();
            int progress = 0;
            const string url = "https://progressdevsupport.blob.core.windows.net/sampledocs/pdfviewer-overview.pdf";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = (int)e.PercentComplete;
            };

            var client = new HttpClient();
            result = await client.DownloadStreamWithProgressAsync(url, reporter, cts.Token);

            Trace.WriteLine($"Progress After Completion: {progress}");

            // Assert
            Assert.IsTrue(progress == 100, "DownloadSteamWithProgressAndCancellation - progress did not reach 100% completiond.");
            Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            Assert.IsTrue(result != null, "Stream is null");

            await result.DisposeAsync();
            client.Dispose();
        }
    }
}
