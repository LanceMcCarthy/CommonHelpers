using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class CollectionExtensionsTests
{
    [TestMethod]
    public void AddRange()
    {
        var source = new List<string> { "Four", "Five", "Six" };
        var target = new ObservableCollection<string> { "One", "Two", "Three" };
        var expectedCount = source.Count + target.Count;

        target.AddRange(source);

        Assert.IsTrue(target.Count == expectedCount);
        Assert.AreEqual("Four", target[3]);
    }

    [TestMethod]
    public void InsertRange()
    {
        var source = new List<string> { "Four", "Five", "Six" };
        var target = new ObservableCollection<string> { "One", "Two", "Three" };
        var expectedCount = source.Count + target.Count;

        target.InsertRange(source, 1);

        Assert.AreEqual(expectedCount, target.Count);
        Assert.AreEqual("Four", target[1]);
    }

    [TestMethod]
    public void RemoveRange()
    {
        const int startIndex = 1;
        const int lengthToRemove = 2;
        var target = new ObservableCollection<string> { "One", "Two", "Three", "Four", "Five", "Six" };
        var expectedCount = target.Count - lengthToRemove;

        target.RemoveRange(startIndex, lengthToRemove);

        Assert.AreEqual(expectedCount, target.Count);
        Assert.AreEqual("Four", target[1]);
    }
}