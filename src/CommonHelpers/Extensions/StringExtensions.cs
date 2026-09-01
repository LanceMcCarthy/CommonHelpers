using System;
using System.Security.Cryptography;
using System.Text;

namespace CommonHelpers.Extensions;

public static class StringExtensions
{
    public static string TimeOfDaySalutation(DateTime dateTime)
    {
        return
            dateTime.Hour < 12 ? "Good morning" :
            dateTime.Hour < 18 ? "Good afternoon" :
            dateTime.Hour < 21 ? "Good evening" :
            /* otherwise */ "Good night";
    }

    public static string TimeOfDaySalutation()
    {
        return TimeOfDaySalutation(DateTime.Now);
    }

    /// <summary>
    /// Hashes the given password using the provided SHA algorithm.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="sha">Default is SHA1Managed, but can accept any override (e.g. SHA1.Create(), SHA256.Create(), etc.)</param>
    /// <returns></returns>
    public static string Hash(this string password, HashAlgorithm sha = null)
    {
        if (password == null)
            throw new ArgumentNullException(nameof(password));

        sha ??= new SHA1Managed();

        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

        var sb = new StringBuilder(hash.Length * 2);

        foreach (var b in hash)
        {
            sb.Append(b.ToString());
        }

        return sb.ToString();
    }
}