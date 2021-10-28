using CommonHelpers.Common.Args;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonHelpers.Tests.ExtensionsTests
{
    [TestClass]
    public class HttpClientExtensionsTests
    {
        [TestMethod]
        public async Task DownloadWithProgressTests()
        {
            // Arrange
            const string url = "https://dvlup.blob.core.windows.net/general-app-files/StaticResources/LoremIpsum.txt";

            string stringResult = string.Empty;
            Stream streamResult = null;
            int stringDownloadProgress = 0;
            int streamDownloadProgress = 0;

            var stringDownloadReporter = new Progress<DownloadProgressArgs>();
            var streamDownloadReporter = new Progress<DownloadProgressArgs>();
            using var client = new HttpClient();

            // Act
            stringDownloadReporter.ProgressChanged += (s, e) =>
            {
                stringDownloadProgress = Convert.ToInt32(e.PercentComplete);
            };
            
            stringResult = await client.DownloadStringWithProgressAsync(url, stringDownloadReporter);

            Trace.WriteLine($"String Progress After Completion: {stringDownloadProgress}");

            streamDownloadReporter.ProgressChanged += (s, e) =>
            {
                streamDownloadProgress = Convert.ToInt32(e.PercentComplete);
            };

            streamResult = await client.DownloadStreamWithProgressAsync(url, streamDownloadReporter);

            Trace.WriteLine($"Stream Progress After Completion: {streamDownloadProgress}");

            // Assert
            Assert.IsTrue(stringDownloadProgress == 100, "Download String - progress did not reach 100% completion");
            Assert.IsTrue(streamDownloadProgress == 100, "Download Stream - progress did not reach 100% completion");

            Assert.IsFalse(string.IsNullOrEmpty(stringResult), "String download was null or empty.");
            Assert.IsTrue(streamResult.Length > 0, "Stream download does not contain any data.");
        }
    }
}
