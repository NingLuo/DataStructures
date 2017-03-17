using System;
using System.Collections;
using System.Collections.Generic;

namespace Queue.List
{
    public class Queue<T> : System.Collections.Generic.IEnumerable<T>
    {
        LinkedList<T> _items = new LinkedList<T>();

        public void Enqueue(T item)
        {
            _items.AddLast(item);
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T value = _items.First.Value;

            _items.RemoveFirst();

            return value; 
        }

        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return _items.First.Value;
        }

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
