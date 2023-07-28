using System;
using CommonHelpers.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.CollectionsTests
{
    [TestClass]
    public class ObservableRangeCollectionTests
    {
        [TestMethod]
        public void AddRangeExpectedCount()
        {
            // Arrange
            var rangeCollection = new ObservableRangeCollection<string> { "One", "Two", "Three" };
            var rangeToAdd = new[] { "Four", "Five", "Six"};


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
            var rangeToAdd = new[] { "Four", "Five", "Six"};


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
    }
}
