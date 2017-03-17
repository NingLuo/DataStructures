using LinkedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    /// <summary>
    /// A linked list collection capable of basic operations such as 
    /// Add, Remove, Find and Ennumerate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T> :
        System.Collections.Generic.ICollection<T>
    {
        //The first node in the list or null if empty
        public LinkedListNode<T> Head
        {
            get;
            private set;
        }

        //The last node in the library or null if empty
        public LinkedListNode<T> Tail
        {
            get;
            private set;
        }

        #region Add

        //Adds the specified value to the start of the list
        public void AddFirst(T value)
        {
            AddFirst(new LinkedListNode<T>(value));
        }

        //Add the node to the start of the list
        public void AddFirst(LinkedListNode<T> node)
        {
            //Save of the head node so we don't lose it
            LinkedListNode<T> tempt = Head;

            //Point head to the new node
            Head = node;

            //Insert the rest of the list behind the head
            Head.Next = tempt;

            Count++;

            if (Count == 1)
            {
                Tail = Head;
            }
        }

        //Add the value to the end of the List
        public void AddLast(T value)
        {
            AddLast(new LinkedListNode<T>(value));
        }

        //Add the node to the end of the list
        public void AddLast(LinkedListNode<T> node)
        {
            if (Count == 0)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }

            Tail = node;

            Count++;
        }
        #endregion

        #region Remove

        //Removes the first node from the list
        public void RemoveFirst()
        {
            if (Count != 0)
            {
                //Before: Head -> 3 -> 5
                //After:  Head ------> 5

                //Before: Head -> 3 -> null
                //After:  Head ------> null
                Head = Head.Next;
                Count--;

                if (Count == 0)
                {
                    Tail = null;
                }
            }
        }

        //Removes the last node from the list
        public void RemoveLast()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    //Before: Head -> 3 -> 5 -> 7
                    //        Tail = 7
                    //After:  Head -> 3 -> 5 -> null
                    //        Tail = 5
                    LinkedListNode<T> current = Head;
                    while (current.Next != Tail)
                    {
                        current = current.Next;
                    }

                    current.Next = null;
                    Tail = current;
                }

                Count--;
            }
        }
        #endregion

        #region ICollection
        //The number of items currently in the list
        public int Count
        {
            get;
            private set;
        }

        //Adds the specific value to the front of the list
        public void Add(T item)
        {
            AddFirst(item);
        }

        //Returns true if the list contains the specific item,
        //false otherwise
        public bool Contains(T item)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        //Copies the node values to a specific array
        //<param name="array">The array to copy the linked list values to </param> 
        //<param name="arrayIndex">The index in the array to start copying at </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = Head;

            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        //True if the collection is read only, false otherwise
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        // Remove the first occurance of the item from the list (searching 
        // from Head to Tail)
        // <param name="item"> The item to remove </param>
        // <returns> True if the item was found and removed, false otherwise </returns>
        public bool Remove(T item)
        {
            LinkedListNode<T> previous = null;
            LinkedListNode<T> current = Head;

            // Cases:
            // 1: Empty List - do nothing
            // 2: Single node: (previous is null) - remove first
            // 3: Many nodes
            //    a: node to remove is the first node
            //    b: node to remove is the middle or last

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    //case 3b: its a node in the middle or end
                    if (previous != null)
                    {
                        //Before: Head -> 3 -> 5 -> 7
                        //After:  Head -> 3 ------> 7
                        previous.Next = current.Next;
                        

                        //it was the end, so update Tail
                        //Before: Head -> 3 -> 5 -> null
                        //After:  Tail -> 3 ------> null
                        if (current.Next == null)
                        {
                            Tail = previous;
                        }

                        Count--;
                    }
                    else
                    {
                        //case 2 or 3a
                        RemoveFirst();
                    }

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Enumerates over the linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }


        /// <summary>
        /// A none generic enumerator who enumerates over the linked list values from Head to Tail
        /// </summary>
        /// <returns>A Head to Tail enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.Generic.IEnumerable<T>)this).GetEnumerator();
        }

        ///<summary>
        ///Removes all the nodes from the list
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        #endregion
    }
}
