using System.Linq;
using System.Collections.Specialized;
using CommonHelpers.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Collections
{
    [TestClass]
    public class ObservableQueueTests
    {
        [TestMethod]
        public void EnqueueItem()
        {
            var queue = new ObservableQueue<string>();
            var itemOne = "One";
            var itemTwo = "Two";
            var itemThree = "Three";
            queue.Enqueue(itemOne);
            queue.Enqueue(itemTwo);
            queue.Enqueue(itemThree);
            var firstItem = queue.FirstOrDefault();
            Assert.AreEqual(itemThree, firstItem, "The queue was not in the correct order. The first item in the list should have been the last item added.");
            Assert.AreEqual(3, queue.Count);
        }

        [TestMethod]
        public void DequeueItem()
        {
            var queue = new ObservableQueue<string>();
            var itemOne = "One";
            var itemTwo = "Two";
            var itemThree = "Three";
            queue.Enqueue(itemOne);
            queue.Enqueue(itemTwo);
            queue.Enqueue(itemThree);
            var dequeuedItem = queue.Dequeue();
            Assert.AreEqual(itemThree, dequeuedItem, "The de-queued item was incorrect. The last item queued should have been dequeued.");
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void PeekItem()
        {
            var queue = new ObservableQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            Assert.AreEqual(20, queue.Peek());
            Assert.AreEqual(2, queue.Count, "Peek should not remove the item.");
        }

        [TestMethod]
        public void DequeueOnEmptyQueue_ReturnsDefault()
        {
            var queue = new ObservableQueue<string>();
            var result = queue.Dequeue();
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void PeekOnEmptyQueue_ReturnsDefault()
        {
            var queue = new ObservableQueue<int>();
            var result = queue.Peek();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ClearQueue()
        {
            var queue = new ObservableQueue<string>();
            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void CollectionChangedEvents()
        {
            var queue = new ObservableQueue<string>();
            int eventCount = 0;
            queue.CollectionChanged += (s, e) => eventCount++;
            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Dequeue();
            queue.Clear();
            Assert.IsTrue(eventCount >= 3, "CollectionChanged should be raised for enqueue, dequeue, and clear.");
        }

        [TestMethod]
        public void ConstructorWithInitializer()
        {
            var initial = new[] { "X", "Y", "Z" };
            var queue = new ObservableQueue<string>(initial);
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual("X", queue.First());
            Assert.AreEqual("Z", queue.Last());
        }
    }
}
