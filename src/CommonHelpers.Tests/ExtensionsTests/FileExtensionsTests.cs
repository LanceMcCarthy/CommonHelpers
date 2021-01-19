using System.Text;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.ExtensionsTests
{
    [TestClass]
    public class FileExtensionsTests
    {
        [TestMethod]
        public void SaveToLocalFolder()
        {
            // Arrange
            var fileName = "TestMethodTemp.txt";
            var fileContent = "Hi, this is some sample text for a file.";
            byte[] contentBytes = Encoding.ASCII.GetBytes(fileContent);

            // Act
            var filePath = contentBytes.SaveToLocalFolderAsync(fileName).Result;

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(filePath));
        }

        [TestMethod]
        public void LoadFileBytes()
        {
            // Arrange
            var fileName = "LoadFileByteTest.txt";
            var fileContent = "Hi, this is some sample text for a file.";
            byte[] expectedContent = Encoding.ASCII.GetBytes(fileContent);
            var filePath = expectedContent.SaveToLocalFolderAsync(fileName).Result;

            // Act
            var contentResult = FileExtensions.LoadFileBytesAsync(filePath).Result;

            // Assert
            Assert.AreEqual(expectedContent.Length, contentResult.Length);
        }

        [TestMethod]
        public void LoadFileStream()
        {
            // Arrange
            var fileName = "LoadFileStreamTest.txt";
            var fileContent = "Hi, this is some sample text for a file.";
            byte[] content = Encoding.ASCII.GetBytes(fileContent);
            var filePath = content.SaveToLocalFolderAsync(fileName).Result;

            // Act
            var stream = FileExtensions.LoadFileStreamAsync(filePath).Result;

            // Assert
            Assert.IsNotNull(stream);

            // Local cleanup
            stream.Dispose();
        }
    }
}
