using System;
using System.Security.Cryptography;
using System.Text;

namespace CommonHelpers.Extensions;

public static class StringExtensions
{
    public static string TimeOfDaySalutation()
    {
        var now = DateTime.Now;

        return
            now.Hour < 12 ? "Good morning" :
            now.Hour < 18 ? "Good afternoon" :
            now.Hour < 21 ? "Good evening" :
            /* otherwise */ "Good night";
    }

    /// <summary>
    /// Hashes the given password using the provided SHA algorithm.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="sha">Default is SHA1Managed, but can accept any override (e.g. SHA256Managed or SHA512Managed)</param>
    /// <returns></returns>
    public static string Hash(this string password, HashAlgorithm sha = null)
    {
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