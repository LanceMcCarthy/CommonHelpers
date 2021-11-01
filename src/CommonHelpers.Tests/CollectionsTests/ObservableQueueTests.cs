using System.Linq;
using CommonHelpers.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.CollectionsTests
{
    [TestClass]
    public class ObservableQueueTests
    {
        [TestMethod]
        public void EnqueueItem()
        {
            // Arrange
            var queue = new ObservableQueue<string>();
            var itemOne = "One";
            var itemTwo = "Two";
            var itemThree = "Three";

            // Act
            queue.Enqueue(itemOne);
            queue.Enqueue(itemTwo);
            queue.Enqueue(itemThree);

            // Assert
            var firstItem = queue.FirstOrDefault();
            Assert.AreEqual(itemThree, firstItem, "The queue was not in the correct order. The first item int he list should have been the last item added.");
        }

        [TestMethod]
        public void DequeueItem()
        {
            // Arrange
            var queue = new ObservableQueue<string>();
            var itemOne = "One";
            var itemTwo = "Two";
            var itemThree = "Three";

            // Act
            queue.Enqueue(itemOne);
            queue.Enqueue(itemTwo);
            queue.Enqueue(itemThree);

            // Assert
            var dequeudItem = queue.Dequeue();
            Assert.AreEqual(itemThree, dequeudItem, "The de-queued item was incorrect. The last item queued should have been the deqeued.");
        }
    }
}
