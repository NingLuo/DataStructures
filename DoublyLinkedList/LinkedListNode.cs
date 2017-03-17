using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    /// <summary>
    /// A node in the DoublyLinkedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedListNode<T>
    {
        ///<summary>
        ///Constructs a new node with specified value.
        /// </summary>
        public LinkedListNode(T value)
        {
            Value = value;
        }

        ///<summary>
        ///The node value
        ///</summary>
        public T Value { get; set; }
        
        /// <summary>
        /// The next node in the linked list (null if last node)
        /// </summary>
        public LinkedListNode<T> Next { get; set; }

        ///<summary>
        /// The previous node in the linked list (null if first node)
        /// </summary>
        public LinkedListNode<T> Previous { get; set; }
    }
}
