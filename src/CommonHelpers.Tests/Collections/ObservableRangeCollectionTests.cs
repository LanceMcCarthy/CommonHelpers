using CommonHelpers.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace CommonHelpers.Tests.Collections
{
    [TestClass]
    public class ObservableRangeCollectionTests
    {
        [TestMethod]
        public void AddRangeExpectedCount()
        {
            // Arrange
            var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three" };
            var rangeToAdd = new[] { "Four", "Five", "Six" };

            // Act
            var originalCount = rangeCollection.Count;
            rangeCollection.AddRange(rangeToAdd);
            var expectedCount = rangeToAdd.Length + originalCount;

            // Assert
            Assert.AreEqual(expectedCount, rangeCollection.Count);
        }

        [TestMethod]
        public void RemoveRangeExpectedCount()
        {
            // Arrange
            var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three", "Four", "Five", "Six" };
            var rangeToRemove = new[] { "Two", "Three", "Four" };

            // Act
            var originalCount = rangeCollection.Count;
            rangeCollection.RemoveRange(rangeToRemove);
            var difference = originalCount - rangeToRemove.Length;
            var expectedCount = difference < 0 ? 0 : difference;

            // Assert
            Assert.AreEqual(expectedCount, rangeCollection.Count);
        }

        [TestMethod]
        public void AddRangeExpectedPresence()
        {
            // Arrange
            var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three" };
            var rangeToAdd = new[] { "Four", "Five", "Six" };

            // Act
            rangeCollection.AddRange(rangeToAdd);

            // Assert
            foreach (var item in rangeToAdd)
                Assert.IsTrue(rangeCollection.Contains(item));
        }

        [TestMethod]
        public void RemoveRangeExpectedPresence()
        {
            // Arrange
            var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three", "Four", "Five", "Six" };
            var rangeToRemove = new[] { "Two", "Three", "Four" };

            // Act
            rangeCollection.RemoveRange(rangeToRemove);

            // Assert
            foreach (var item in rangeToRemove)
                Assert.IsFalse(rangeCollection.Contains(item));
        }

        [TestMethod]
        public void EnsureMaximumCount()
        {
            // Arrange
            var expectedCount = 10;
            var rangeCollection = new ObservableRangeCollection<string> { MaximumCount = expectedCount };
            rangeCollection.AddRange(new[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" });

            // Act
            rangeCollection.AddRange(new[] { "Ten", "Eleven", "Twelve" });

            // Assert
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
            CollectionAssert.AreEqual(items, eventArgs.NewItems.Cast<string>().ToList());
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
            Assert.ThrowsException<ArgumentNullException>(() => rangeCollection.AddRange(null));
        }

        [TestMethod]
        public void RemoveRange_Null_Throws()
        {
            var rangeCollection = new ObservableRangeCollection<string>();
            Assert.ThrowsException<ArgumentNullException>(() => rangeCollection.RemoveRange(null));
        }

        [TestMethod]
        public void AddRange_InvalidNotificationMode_Throws()
        {
            var rangeCollection = new ObservableRangeCollection<string>();
            Assert.ThrowsException<ArgumentException>(() => rangeCollection.AddRange(new[] { "A" }, (NotifyCollectionChangedAction)999));
        }

        [TestMethod]
        public void RemoveRange_InvalidNotificationMode_Throws()
        {
            var rangeCollection = new ObservableRangeCollection<string>();
            Assert.ThrowsException<ArgumentException>(() => rangeCollection.RemoveRange(new[] { "A" }, (NotifyCollectionChangedAction)999));
        }

        [TestMethod]
        public void DefaultAddCollectionChangedAction_Property()
        {
            var rangeCollection = new ObservableRangeCollection<string>();
            rangeCollection.DefaultAddCollectionChangedAction = NotifyCollectionChangedAction.Reset;
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, rangeCollection.DefaultAddCollectionChangedAction);
        }

        [TestMethod]
        public void DefaultRemoveCollectionChangedAction_Property()
        {
            var rangeCollection = new ObservableRangeCollection<string>();
            rangeCollection.DefaultRemoveCollectionChangedAction = NotifyCollectionChangedAction.Remove;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, rangeCollection.DefaultRemoveCollectionChangedAction);
        }
    }
}
