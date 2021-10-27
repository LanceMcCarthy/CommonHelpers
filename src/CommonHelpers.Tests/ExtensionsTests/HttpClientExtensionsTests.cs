using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

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
            string stringResult = string.Empty;
            int progress = 0;
            const string url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = Convert.ToInt32(e.PercentComplete);
            };

            using var client = new HttpClient();

            stringResult = await client.DownloadStringWithProgressAsync(url, reporter);

            Trace.WriteLine($"Progress After Completion: {progress}");

            // Assert
            Assert.IsTrue(progress == 100, "DownloadStringWithProgress - progress did not reach 100% completion");
            Assert.IsFalse(string.IsNullOrEmpty(stringResult), "String result was null");
        }
        
        [TestMethod]
        public async Task DownloadStreamWithProgress()
        {
            // Arrange
            int progress = 0;
            const string url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";
            var reporter = new Progress<DownloadProgressArgs>();

            // Act
            reporter.ProgressChanged += (s, e) =>
            {
                progress = Convert.ToInt32(e.PercentComplete);
            };

            using var client = new HttpClient();

            await using var streamResult = await client.DownloadStreamWithProgressAsync(url, reporter);

            Trace.WriteLine($"Progress After Completion: {progress}");

            // Assert
            Assert.IsTrue(progress == 100, "DownloadStreamWithProgress - progress did not reach 100% completion");
            Assert.IsTrue(streamResult != null, "Stream is null");
        }
    }
}
