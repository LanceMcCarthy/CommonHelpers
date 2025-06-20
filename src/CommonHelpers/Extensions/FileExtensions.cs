﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace CommonHelpers.Extensions;

public static class FileExtensions
{
    private static readonly string LocalFolder;

    static FileExtensions()
    {
        // Gets the target platform's valid save location
        LocalFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }

    // Byte[] extension methods

    public static async Task<string> SaveToLocalFolderAsync(this byte[] dataBytes, string fileName)
    {
        if (dataBytes == null) throw new ArgumentNullException(nameof(dataBytes));
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("File name must not be empty.", nameof(fileName));

        return await Task.Run(() =>
        {
            // Use Combine so that the correct file path slashes are used
            var filePath = Path.Combine(LocalFolder, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            File.WriteAllBytes(filePath, dataBytes);

            return filePath;
        });
    }

    public static async Task<byte[]> LoadFileBytesAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path must not be empty.", nameof(filePath));

        return await Task.Run(() => File.ReadAllBytes(filePath));
    }
    
    // Stream extension methods

    public static async Task<string> SaveToLocalFolderAsync(this Stream dataStream, string fileName)
    {
        if (dataStream == null) throw new ArgumentNullException(nameof(dataStream));
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("File name must not be empty.", nameof(fileName));

        // Use Combine so that the correct file path slashes are used
        var filePath = Path.Combine(LocalFolder, fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);

        using var fileStream = File.OpenWrite(filePath);
        if (dataStream.CanSeek)
            dataStream.Position = 0;

        await dataStream.CopyToAsync(fileStream);

        return filePath;
    }

    public static async Task<Stream> LoadFileStreamAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path must not be empty.", nameof(filePath));

        // Do not dispose the stream before returning
        return await Task.Run(() => (Stream)File.OpenRead(filePath));
    }
}