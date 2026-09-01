using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class FileExtensionsTests
{
    private static string GetTempFilePath(string fileName) => Path.Combine(Path.GetTempPath(), fileName);

    private static void CleanupFile(string fileName)
    {
        var filePath = GetTempFilePath(fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    [TestMethod]
    public async Task SaveToLocalFolder()
    {
        const string fileName = "TestMethodTemp.txt";
        const string fileContent = "Hi, this is some sample text for a file.";
        var contentBytes = Encoding.ASCII.GetBytes(fileContent);
        var filePath = GetTempFilePath(fileName);
        try
        {
            var resultPath = await contentBytes.SaveToLocalFolderAsync(filePath);
            Assert.IsFalse(string.IsNullOrEmpty(resultPath));
            Assert.IsTrue(File.Exists(resultPath));
            var loaded = await File.ReadAllTextAsync(resultPath);
            Assert.AreEqual(fileContent, loaded);
        }
        finally
        {
            CleanupFile(fileName);
        }
    }

    [TestMethod]
    public async Task SaveToLocalFolder_Stream()
    {
        const string fileName = "TestMethodTempStream.txt";
        const string fileContent = "Stream test content.";
        var contentBytes = Encoding.ASCII.GetBytes(fileContent);
        using var ms = new MemoryStream(contentBytes);
        var filePath = GetTempFilePath(fileName);
        try
        {
            var resultPath = await ms.SaveToLocalFolderAsync(filePath);
            Assert.IsFalse(string.IsNullOrEmpty(resultPath));
            Assert.IsTrue(File.Exists(resultPath));
            var loaded = await File.ReadAllTextAsync(resultPath);
            Assert.AreEqual(fileContent, loaded);
        }
        finally
        {
            CleanupFile(fileName);
        }
    }

    [TestMethod]
    public async Task LoadFileBytes()
    {
        const string fileName = "LoadFileByteTest.txt";
        const string fileContent = "Hi, this is some sample text for a file.";
        var expectedContent = Encoding.ASCII.GetBytes(fileContent);
        var filePath = GetTempFilePath(fileName);
        try
        {
            var resultPath = await expectedContent.SaveToLocalFolderAsync(filePath);
            var contentResult = await FileExtensions.LoadFileBytesAsync(resultPath);
            CollectionAssert.AreEqual(expectedContent, contentResult);
        }
        finally
        {
            CleanupFile(fileName);
        }
    }

    [TestMethod]
    public async Task LoadFileStream()
    {
        const string fileName = "LoadFileStreamTest.txt";
        const string fileContent = "Hi, this is some sample text for a file.";
        var content = Encoding.ASCII.GetBytes(fileContent);
        var filePath = GetTempFilePath(fileName);
        try
        {
            var resultPath = await content.SaveToLocalFolderAsync(filePath);
            await using var stream = File.OpenRead(resultPath);
            var loadedBytes = new byte[content.Length];
            await stream.ReadExactlyAsync(loadedBytes, 0, loadedBytes.Length);
            CollectionAssert.AreEqual(content, loadedBytes);
        }
        finally
        {
            CleanupFile(fileName);
        }
    }

    [TestMethod]
    public async Task OverwriteFile()
    {
        const string fileName = "OverwriteTest.txt";
        var content1 = Encoding.ASCII.GetBytes("First");
        var content2 = Encoding.ASCII.GetBytes("Second");
        var filePath = GetTempFilePath(fileName);
        try
        {
            var filePath1 = await content1.SaveToLocalFolderAsync(filePath);
            var filePath2 = await content2.SaveToLocalFolderAsync(filePath);
            Assert.AreEqual(filePath1, filePath2);
            var loaded = await File.ReadAllTextAsync(filePath2);
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

        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            await data.SaveToLocalFolderAsync(GetTempFilePath("null.txt"));
        });
    }

    [TestMethod]
    public async Task SaveToLocalFolder_NullStream_Throws()
    {
        Stream stream = null;

        await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            await stream.SaveToLocalFolderAsync(GetTempFilePath("nullstream.txt"));
        });
    }

    [TestMethod]
    public async Task SaveToLocalFolder_EmptyFileName_Throws()
    {
        var data = Encoding.ASCII.GetBytes("test");
        await Assert.ThrowsExactlyAsync<ArgumentException>(async () =>
        {
            await data.SaveToLocalFolderAsync("");
        });
    }

    [TestMethod]
    public async Task LoadFileBytesAsync_FileNotFound_Throws()
    {
        await Assert.ThrowsExactlyAsync<FileNotFoundException>(async () =>
        {
            await FileExtensions.LoadFileBytesAsync(GetTempFilePath("notfound.txt"));
        });
    }
}