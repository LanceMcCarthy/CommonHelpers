using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void AddRange()
        {
            // Arrange
            var source = new List<string> { "Four", "Five", "Six" };
            var target = new ObservableCollection<string> { "One", "Two", "Three" };
            var expectedCount = source.Count + target.Count;


            // Act
            target.AddRange(source);


            // Assert
            // Checking total count
            Assert.IsTrue(target.Count == expectedCount);

            // Check items are at the expected location
            Assert.AreEqual(target[3], "Four");
        }

        [TestMethod]
        public void InsertRange()
        {
            // Arrange
            var source = new List<string> { "Four", "Five", "Six" };
            var target = new ObservableCollection<string> { "One", "Two", "Three" };
            var expectedCount = source.Count + target.Count;


            // Act
            target.InsertRange(source, 1);


            // Assert
            // Checking total count
            Assert.IsTrue(target.Count == expectedCount);

            // Check items are at the expected location
            Assert.AreEqual(target[1], "Four");
        }

        [TestMethod]
        public void RemoveRange()
        {
            // Arrange
            var target = new ObservableCollection<string> { "One", "Two", "Three", "Four", "Five", "Six" };
            var startIndex = 1;
            var lengthToRemove = 2;
            var expectedCount = target.Count - lengthToRemove;


            // Act
            target.RemoveRange(startIndex, lengthToRemove);


            // Assert
            // Checking total count
            Assert.IsTrue(target.Count == expectedCount);

            // Check items are at the expected location
            Assert.AreEqual(target[1], "Four");
        }
    }
}
