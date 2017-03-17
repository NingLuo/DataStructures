using System;
using System.Collections;
using System.Collections.Generic;

namespace Queue.Array
{
    public class Queue<T> : System.Collections.Generic.IEnumerable<T>
    {
        T[] _items = new T[0];

        int _size = 0;

        int _head = 0;

        int _tail = -1;

        public void Enqueue(T item)
        {
            // if the array needs to grow
            if(_size == _items.Length)
            {
                int newLength = (_size == 0) ? 4 : _size * 2;

                T[] newArray = new T[newLength];

                // if _size > 0 we need to copy the items from the current array to the new array
                if (_size > 0)
                {
                    //copy contents...
                    //
                    int targetIndex = 0;

                    // if the current queue wraps in the current array
                    if (_tail < _head)
                    {
                        //copy _items[head]..._items[end of array] --> newArray[0]...newArray[N]
                        for (int index = _head; index < _items.Length; index++)
                        {
                            newArray[targetIndex] = _items[index];
                            targetIndex++;
                        }

                        //copy _items[0]...items[tail] --> newArray[N+1]...
                        for (int index = 0; index <= _tail; index++)
                        {
                            newArray[targetIndex] = _items[index];
                            targetIndex++;
                        }
                    }
                    else
                    {
                        // if the current queue doesn't wrap in the current array
                        // copy _items[head]..._items[tail] --> newArray[0]...
                        for (int index = _head; index < _tail; index++)
                        {
                            newArray[targetIndex] = _items[index];
                            targetIndex++;
                        }
                    }

                    _head = 0;
                    _tail = targetIndex - 1; //compensate for the extra bump
                }
                else
                {
                    _head = 0;
                    _tail = -1;
                }

                _items = newArray;          
            }

            //now we have a properly sized array and can focus on adding the new item

            //before we add the new item after the _tail of the array we need to check the position of the _tail
            if (_tail == _items.Length - 1)
            {
                //wrap the _tail to the beginning of the array
                _tail = 0;
            }
            else
            {
                _tail++;
            }

            _items[_tail] = item;
            _size++;
        }

        public T Dequeue()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T value = _items[_head];

            if (_head == _items.Length - 1)
            {
                // if the head is at the last index in the array - wrap around
                _head = 0;
            }
            else
            {
                //move to the next value
                _head++;
            }

            _size--;

            return value;
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return _items[_head];
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        public void Clear()
        {
            _size = 0;
            _head = 0;
            _tail = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_size > 0)
            {
                // if the queue wraps then handle that case
                if (_tail < _head)
                {
                    // head -> end
                    for (int index = _head; index < _items.Length; index++)
                    {
                        yield return _items[index];
                    }

                    // 0 -> tail
                    for (int index = 0; index <= _tail; index++)
                    {
                        yield return _items[index];
                    }
                }
                else
                {
                    // head -> tail
                    for (int index = _head; index <= _tail; index++)
                    {
                        yield return _items[index];
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
