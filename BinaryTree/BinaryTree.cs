using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class BinaryTree<T> : IEnumerable<T>  //the binary tree itself is enumerable
        where T : IComparable<T>  // the value we store in the binary tree is comparable
    {
        private BinaryTreeNode<T> _head;
        private int _count;

        #region Add
        /// <summary>
        /// Adds the provided value to the binary tree
        /// </summary>
        /// <returns></returns>
        public void Add(T value)
        {
            //Case 1: The tree is empty - allocate the head
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            //Case 2: The tree is not empty so find the right location to insert
            else
            {
                AddTo(_head, value);
            }

            _count++;
        }

        //Recursive add algorithm
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            // Case 1: value is less than the current node value
            if (value.CompareTo(node.Value) < 0)
            {
                // if there is no left child then make this the new left
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // otherwise add it to the left node and compare with the left node
                    AddTo(node.Left, value);
                }
            }
            // Case 2: Value is equal to or greater than the current value
            else
            {
                // if there is no right child then make this the new right child
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // otherwise add it to the right node and compare with the right node
                    AddTo(node.Right, value);
                }
            }
        }
        #endregion

        /// <summary>
        /// Determines if the specified value exists in the binary tree.
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <returns>True if the tree contains the value, false otherwise</returns>
        public bool Contains(T value)
        {
            // defer to the node search helper function
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        /// <summary>
        /// Finds and returns the frist node containing the specified value. If the value
        /// is not found, returns null. Also returns the parent of the found node(or null)
        /// which is used in remove.
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <param name="parent">The parent of the found node (or null)</param>
        /// <returns>The found node (or null)</returns>
        /// 
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = _head;
            parent = null;

            //while we don't have a match
            while(current != null)
            {
                int result = current.CompareTo(value);

                if (result > 0)
                {
                    // if the value is less than current, go left
                    parent = current;
                    current = current.Left;                   
                }
                else if (result < 0)
                {
                    // if the value is more than current, go right
                    parent = current;
                    current = current.Right;                    
                }
                else
                {
                    // We hava a match!
                    break;
                }
            }

            return current;
        }

        #region Remove
        /// <summary>
        /// Removes the first occurence of the specified value from the tree
        /// </summary>
        /// <param name="value">The value to remove</param>
        /// <returns>True if value is removed, flase otherwise</returns>
        public bool Remove(T value)
        {
            BinaryTreeNode<T> current, parent;

            current = FindWithParent(value, out parent);

            if (current == null)
            {
                return false;
            }

            _count--;

            // Case 1: If current has no right child, then current's left replaces current
            if (current.Right == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if parent value is greater than current
                        // meaning the current is parent's left child
                        // then make the current's left child a left child of parent
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // if parent value is less than current
                        // meaning the current is parent's right child
                        // then make the current's left child a right child of parent
                        parent.Right = current.Left;
                    }
                }
            }
            // Case 2: If current's right child has no left child, 
            // then current's right child replaces current
            else if (current.Right.Left == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        //if parent value is larger than current
                        //meaning current is parent's left child
                        //then make current's right child a left child of parent
                        //and make current's left child a left child of current's right child
                        parent.Left = current.Right;
                        current.Right.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        //if parent value is less than current
                        //meaning current is parent's right child
                        //then make current's right child a right child of parent
                        //and make current's left child a left child of current's right child
                        parent.Right = current.Right;
                        current.Right.Left = current.Left;
                    }
                }
            }
            // Case 3: If current's right child has a left child
            // then current's right child's left-most child replaces current
            else
            {
                //find the right's left-most child
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                //leftmost's right child become leftmost's parent's left child
                leftmostParent.Left = leftmost.Right;

                // assign current's left and right to leftmost's left and right
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    _head = leftmost;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // if parent value is greater than current value
                        // make leftmost the parent's left child
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // if parent value is less than current value
                        // make leftmost the parent's right child
                        parent.Right = leftmost;
                    }
                }
            }

            return true;
        }
        #endregion

        #region Pre-Order Traversal
        /// <summary>
        /// Performs the provided action on each binary tree value in pre-order traveral order
        /// </summary>
        /// <returns></returns>
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }
        
        private void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }
        #endregion

        #region Post-order Traversal
        public void PostOrderTraversal(Action<T> action)
        {
            PostOrderTraversal(action, _head);
        }

        private void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }
        #endregion

        #region In-Order Traversal
        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, _head);
        }

        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }

        /// <summary>
        /// Enumerates the values contains in the binary tree in in-order traversal order
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> InOrderTraversal()
        {
            if (_head != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                BinaryTreeNode<T> current = _head;

                bool goLeftNext = true;

                //start by pushing Head onto the stack
                stack.Push(current);
            
                while(stack.Count > 0)
                {
                    //If we are heading left...
                    if (goLeftNext)
                    {
                        //push everything but the left-most node to the stack
                        // we'll yield the left-most after this block 
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value;

                    if (current.Right != null)
                    {
                        current = current.Right;

                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }



        #endregion
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
