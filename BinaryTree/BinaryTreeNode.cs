using System;

namespace BinaryTree
{
    /// <summary>
    /// A binary tree node class - encapsulates the value and left/right pointers
    /// </summary>
    class BinaryTreeNode<TNode> : IComparable<TNode>
        where TNode : IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }

        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }
        public TNode Value { get; private set; }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}
