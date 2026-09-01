using CommonHelpers.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace CommonHelpers.Tests.Collections;

[TestClass]
public class ObservableRangeCollectionTests
{
    [TestMethod]
    public void AddRangeExpectedCount()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three" };
        var rangeToAdd = new[] { "Four", "Five", "Six" };
        var originalCount = rangeCollection.Count;
        rangeCollection.AddRange(rangeToAdd);
        var expectedCount = rangeToAdd.Length + originalCount;
        Assert.AreEqual(expectedCount, rangeCollection.Count);
    }

    [TestMethod]
    public void RemoveRangeExpectedCount()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three", "Four", "Five", "Six" };
        var rangeToRemove = new[] { "Two", "Three", "Four" };
        var originalCount = rangeCollection.Count;
        rangeCollection.RemoveRange(rangeToRemove);
        var difference = originalCount - rangeToRemove.Length;
        var expectedCount = difference < 0 ? 0 : difference;
        Assert.AreEqual(expectedCount, rangeCollection.Count);
    }

    [TestMethod]
    public void AddRangeExpectedPresence()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three" };
        var rangeToAdd = new[] { "Four", "Five", "Six" };
        rangeCollection.AddRange(rangeToAdd);
        foreach (var item in rangeToAdd)
            Assert.IsTrue(rangeCollection.Contains(item));
    }

    [TestMethod]
    public void RemoveRangeExpectedPresence()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three", "Four", "Five", "Six" };
        var rangeToRemove = new[] { "Two", "Three", "Four" };
        rangeCollection.RemoveRange(rangeToRemove);
        foreach (var item in rangeToRemove)
            Assert.IsFalse(rangeCollection.Contains(item));
    }

    [TestMethod]
    public void EnsureMaximumCount()
    {
        const int expectedCount = 10;
        var rangeCollection = new ObservableRangeCollection<string> { MaximumCount = expectedCount };
        rangeCollection.AddRange(["One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine"]);
        rangeCollection.AddRange(["Ten", "Eleven", "Twelve"]);
        Assert.AreEqual(expectedCount, rangeCollection.Count);
        Assert.AreEqual("Three", rangeCollection[0]);
        Assert.AreEqual("Twelve", rangeCollection[9]);
    }

    [TestMethod]
    public void AddRange_RaisesCollectionChanged()
    {
        var rangeCollection = new ObservableRangeCollection<string>();
        NotifyCollectionChangedEventArgs eventArgs = null;
        rangeCollection.CollectionChanged += (s, e) => eventArgs = e;
        var items = new[] { "A", "B" };
        rangeCollection.AddRange(items);
        Assert.IsNotNull(eventArgs);
        Assert.AreEqual(NotifyCollectionChangedAction.Add, eventArgs.Action);
        Assert.AreSequenceEqual(items, eventArgs.NewItems!.Cast<string>());
    }

    [TestMethod]
    public void RemoveRange_RaisesCollectionChanged()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "A", "B", "C" };
        NotifyCollectionChangedEventArgs eventArgs = null;
        rangeCollection.CollectionChanged += (s, e) => eventArgs = e;
        var toRemove = new[] { "A", "B" };
        rangeCollection.RemoveRange(toRemove);
        Assert.IsNotNull(eventArgs);
        Assert.AreEqual(NotifyCollectionChangedAction.Reset, eventArgs.Action);
    }

    [TestMethod]
    public void ReplaceRange_ReplacesAllItems()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "A", "B", "C" };
        var newItems = new[] { "X", "Y" };
        rangeCollection.ReplaceRange(newItems);
        CollectionAssert.AreEqual(newItems, rangeCollection.ToList());
    }

    [TestMethod]
    public void Replace_ReplacesWithSingleItem()
    {
        var rangeCollection = new ObservableRangeCollection<string> { "A", "B", "C" };
        rangeCollection.Replace("Z");
        Assert.AreEqual(1, rangeCollection.Count);
        Assert.AreEqual("Z", rangeCollection[0]);
    }

    [TestMethod]
    public void AddRange_Null_Throws()
    {
        var rangeCollection = new ObservableRangeCollection<string>();
        Assert.ThrowsExactly<ArgumentNullException>(() => rangeCollection.AddRange(null));
    }

    [TestMethod]
    public void RemoveRange_Null_Throws()
    {
        var rangeCollection = new ObservableRangeCollection<string>();
        Assert.ThrowsExactly<ArgumentNullException>(() => rangeCollection.RemoveRange(null));
    }

    [TestMethod]
    public void AddRange_InvalidNotificationMode_Throws()
    {
        var rangeCollection = new ObservableRangeCollection<string>();
        Assert.ThrowsExactly<ArgumentException>(() => rangeCollection.AddRange(["A"], (NotifyCollectionChangedAction)999));
    }

    [TestMethod]
    public void RemoveRange_InvalidNotificationMode_Throws()
    {
        var rangeCollection = new ObservableRangeCollection<string>();
        Assert.ThrowsExactly<ArgumentException>(() => rangeCollection.RemoveRange(["A"], (NotifyCollectionChangedAction)999));
    }

    [TestMethod]
    public void DefaultAddCollectionChangedAction_Property()
    {
        var rangeCollection = new ObservableRangeCollection<string>
        {
            DefaultAddCollectionChangedAction = NotifyCollectionChangedAction.Reset
        };
        Assert.AreEqual(NotifyCollectionChangedAction.Reset, rangeCollection.DefaultAddCollectionChangedAction);
    }

    [TestMethod]
    public void DefaultRemoveCollectionChangedAction_Property()
    {
        var rangeCollection = new ObservableRangeCollection<string>
        {
            DefaultRemoveCollectionChangedAction = NotifyCollectionChangedAction.Remove
        };
        Assert.AreEqual(NotifyCollectionChangedAction.Remove, rangeCollection.DefaultRemoveCollectionChangedAction);
    }
}