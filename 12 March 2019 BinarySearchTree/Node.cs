using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_March_2019_BinarySearchTree
{
    public class Node<T> : IComparable where T : IComparable
    {
        public Node<T> left, right, parent;
        public T key;

        public Node(T key)
        {
            this.key = key;
            left = right = parent = null;
        }

        public Node<T> GetSuccessor()
        {
            return GetSuccessor(this);
        }

        public static Node<T> GetSuccessor(Node<T> x)
        {
            if (x.right != null)
            {
                return BST<T>.GetMinNode(x.right);
            }
            Node<T> y = x.parent;
            while (y != null && x == y.right)
            {
                x = y;
                y = y.parent;
            }
            return y;
        }

        public int CompareTo(object obj)
        {
            Node<T> otherNode = obj as Node<T>;

            if (otherNode != null)
                return this.key.CompareTo(otherNode.key);
            else
                return -1;
        }
    }
}
