using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CommonHelpers.Collections
{
    public class ObservableQueue<T> : ObservableCollection<T>
    {
        public ObservableQueue()
        {
            
        }

        public ObservableQueue(IEnumerable<T> initializer) 
            : base(initializer)
        {
        }

        public void Enqueue(T item)
        {
            base.Insert(0, item);
        }

        public T Dequeue()
        {
            if (Count == 0) return default(T);
            var ret = base[0];
            base.RemoveAt(0);
            return ret;
        }

        public T Peek()
        {
            if (Count == 0) return default(T);
            return base[0];
        }
    }
}