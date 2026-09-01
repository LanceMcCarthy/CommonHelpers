using System;
using System.Security.Cryptography;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    public void TimeOfDaySalutation_ReturnsExpectedString()
    {
        // This test is time-dependent, so we just check that it returns one of the expected values
        var result = StringExtensions.TimeOfDaySalutation();
        CollectionAssert.Contains(new[] { "Good morning", "Good afternoon", "Good evening", "Good night" }, result);
    }

    [TestMethod]
    public void TimeOfDaySalutation_Morning()
    {
        var result = StringExtensions.TimeOfDaySalutation(new DateTime(2024, 1, 1, 8, 0, 0));
        Assert.AreEqual("Good morning", result);
    }

    [TestMethod]
    public void TimeOfDaySalutation_Afternoon()
    {
        var result = StringExtensions.TimeOfDaySalutation(new DateTime(2024, 1, 1, 15, 0, 0));
        Assert.AreEqual("Good afternoon", result);
    }

    [TestMethod]
    public void TimeOfDaySalutation_Evening()
    {
        var result = StringExtensions.TimeOfDaySalutation(new DateTime(2024, 1, 1, 19, 0, 0));
        Assert.AreEqual("Good evening", result);
    }

    [TestMethod]
    public void TimeOfDaySalutation_Night()
    {
        var result = StringExtensions.TimeOfDaySalutation(new DateTime(2024, 1, 1, 22, 0, 0));
        Assert.AreEqual("Good night", result);
    }

    [TestMethod]
    public void Hash_DefaultSHA1_ReturnsExpectedHash()
    {
        const string password = "password123";
        using var sha = SHA1.Create();
        var expected = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        var expectedString = string.Concat(Array.ConvertAll(expected, b => b.ToString()));
        var actual = password.Hash();
        Assert.AreEqual(expectedString, actual);
    }

    [TestMethod]
    public void Hash_WithSHA256_ReturnsExpectedHash()
    {
        const string password = "password123";
        using var sha = SHA256.Create();
        var expected = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        var expectedString = string.Concat(Array.ConvertAll(expected, b => b.ToString()));
        var actual = password.Hash(sha);
        Assert.AreEqual(expectedString, actual);
    }

    [TestMethod]
    public void Hash_NullPassword_Throws()
    {
        string password = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        Assert.ThrowsExactly<ArgumentNullException>(() => password.Hash());
    }
}