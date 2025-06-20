using System.Text;
using System.IO;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace CommonHelpers.Tests.Extensions
{
    [TestClass]
    public class FileExtensionsTests
    {
        private string GetTempFilePath(string fileName) => Path.Combine(Path.GetTempPath(), fileName);

        private void CleanupFile(string fileName)
        {
            var filePath = GetTempFilePath(fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        [TestMethod]
        public void SaveToLocalFolder()
        {
            var fileName = "TestMethodTemp.txt";
            var fileContent = "Hi, this is some sample text for a file.";
            byte[] contentBytes = Encoding.ASCII.GetBytes(fileContent);
            var filePath = GetTempFilePath(fileName);
            try
            {
                var resultPath = contentBytes.SaveToLocalFolderAsync(filePath).Result;
                Assert.IsFalse(string.IsNullOrEmpty(resultPath));
                Assert.IsTrue(File.Exists(resultPath));
                var loaded = File.ReadAllText(resultPath);
                Assert.AreEqual(fileContent, loaded);
            }
            finally
            {
                CleanupFile(fileName);
            }
        }

        [TestMethod]
        public void SaveToLocalFolder_Stream()
        {
            var fileName = "TestMethodTempStream.txt";
            var fileContent = "Stream test content.";
            byte[] contentBytes = Encoding.ASCII.GetBytes(fileContent);
            using var ms = new MemoryStream(contentBytes);
            var filePath = GetTempFilePath(fileName);
            try
            {
                var resultPath = ms.SaveToLocalFolderAsync(filePath).Result;
                Assert.IsFalse(string.IsNullOrEmpty(resultPath));
                Assert.IsTrue(File.Exists(resultPath));
                var loaded = File.ReadAllText(resultPath);
                Assert.AreEqual(fileContent, loaded);
            }
            finally
            {
                CleanupFile(fileName);
            }
        }

        [TestMethod]
        public void LoadFileBytes()
        {
            var fileName = "LoadFileByteTest.txt";
            var fileContent = "Hi, this is some sample text for a file.";
            byte[] expectedContent = Encoding.ASCII.GetBytes(fileContent);
            var filePath = GetTempFilePath(fileName);
            try
            {
                var resultPath = expectedContent.SaveToLocalFolderAsync(filePath).Result;
                var contentResult = FileExtensions.LoadFileBytesAsync(resultPath).Result;
                CollectionAssert.AreEqual(expectedContent, contentResult);
            }
            finally
            {
                CleanupFile(fileName);
            }
        }

        [TestMethod]
        public void LoadFileStream()
        {
            var fileName = "LoadFileStreamTest.txt";
            var fileContent = "Hi, this is some sample text for a file.";
            byte[] content = Encoding.ASCII.GetBytes(fileContent);
            var filePath = GetTempFilePath(fileName);
            try
            {
                var resultPath = content.SaveToLocalFolderAsync(filePath).Result;
                using var stream = File.OpenRead(resultPath);
                var loadedBytes = new byte[content.Length];
                stream.Read(loadedBytes, 0, loadedBytes.Length);
                CollectionAssert.AreEqual(content, loadedBytes);
            }
            finally
            {
                CleanupFile(fileName);
            }
        }

        [TestMethod]
        public void OverwriteFile()
        {
            var fileName = "OverwriteTest.txt";
            var content1 = Encoding.ASCII.GetBytes("First");
            var content2 = Encoding.ASCII.GetBytes("Second");
            var filePath = GetTempFilePath(fileName);
            try
            {
                var filePath1 = content1.SaveToLocalFolderAsync(filePath).Result;
                var filePath2 = content2.SaveToLocalFolderAsync(filePath).Result;
                Assert.AreEqual(filePath1, filePath2);
                var loaded = File.ReadAllText(filePath2);
                Assert.AreEqual("Second", loaded);
            }
            finally
            {
                CleanupFile(fileName);
            }
        }

        [TestMethod]
        public async Task SaveToLocalFolder_NullBytes_Throws()
        {
            byte[] data = null;

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await data.SaveToLocalFolderAsync(GetTempFilePath("null.txt"));
            });
        }

        [TestMethod]
        public async Task SaveToLocalFolder_NullStream_Throws()
        {
            Stream stream = null;

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await stream.SaveToLocalFolderAsync(GetTempFilePath("nullstream.txt"));
            });
        }

        [TestMethod]
        public async Task SaveToLocalFolder_EmptyFileName_Throws()
        {
            var data = Encoding.ASCII.GetBytes("test");
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await data.SaveToLocalFolderAsync("");
            });
        }

        [TestMethod]
        public async Task LoadFileBytesAsync_FileNotFound_Throws()
        {
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(async () =>
            {
                await FileExtensions.LoadFileBytesAsync(GetTempFilePath("notfound.txt"));
            });
        }
    }
}
