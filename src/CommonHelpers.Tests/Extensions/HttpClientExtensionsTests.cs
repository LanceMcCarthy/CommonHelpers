using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Extensions
{
    [TestClass]
    public class HttpClientExtensionsTests
    {
        [TestMethod]
        public void DownloadStringWithProgress()
        {
            // Arrange
            string result;
            var reporter = new Progress<DownloadProgressArgs>();
            float progress = 0;
            var url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            using (var client = new HttpClient())
            {
                result = client.DownloadStringWithProgressAsync(url, reporter).Result;
            }

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(result), "String result was null");
        }

        [TestMethod]
        public void DownloadStringWithProgressAndCancellation()
        {
            // Arrange
            string result;
            var cts = new CancellationTokenSource();
            float progress = 0;
            var url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            using (var client = new HttpClient())
            {
                result = client.DownloadStringWithProgressAsync(url, reporter, cts.Token).Result;
            }

            // Assert
            Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            Assert.IsFalse(string.IsNullOrEmpty(result), "String result was null.");
        }

        [TestMethod]
        public void DownloadStreamWithProgress()
        {
            // Arrange
            Stream result = null;
            float progress = 0;
            var url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            using (var client = new HttpClient())
            {
                result = client.DownloadStreamWithProgressAsync(url, reporter).Result;
            }

            // Assert
            Assert.IsTrue(result.Length > 0, "Stream is empty");

            result.Dispose();
        }

        [TestMethod]
        public void DownloadSteamWithProgressAndCancellation()
        {
            // Arrange
            Stream result = null;
            var cts = new CancellationTokenSource();
            float progress = 0;
            var url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = e.PercentComplete;
            };

            using (var client = new HttpClient())
            {
                result = client.DownloadStreamWithProgressAsync(url, reporter, cts.Token).Result;
            }

            // Assert
            Assert.IsFalse(cts.Token.IsCancellationRequested, "Cancellation was incorrectly requested.");
            Assert.IsTrue(result.Length > 0, "Stream is empty");

            result.Dispose();
        }
    }
}
