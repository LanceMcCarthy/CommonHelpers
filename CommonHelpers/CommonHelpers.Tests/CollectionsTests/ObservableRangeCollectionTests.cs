using System.Linq;
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
            var rangeCollection = new ObservableRangeCollection<string>();
            var rangeToAdd = new[] {"One", "Two", "Three", "Four"};

            // Act
            rangeCollection.AddRange(rangeToAdd);

            // Assert
            Assert.AreEqual(rangeToAdd.Length, rangeCollection.Count);
        }
    }
}
