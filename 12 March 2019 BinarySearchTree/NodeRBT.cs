using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_March_2019_BinarySearchTree
{
    public enum Color { RED, BLACK }

    public class NodeRBT<T> where T : IComparable
    {
        
        public T key;
        public Color color;
        public NodeRBT<T> left;
        public NodeRBT<T> right;
        public NodeRBT<T> p;    

        public NodeRBT()
        {
            color = Color.BLACK;
        }

        public NodeRBT(T k)
        {
            key = k;
        }

        public override string ToString()
        {
            if (this == RBT<T>.nil)
            {
                return "@";
            }
            return key.ToString();
        }
        
        public NodeRBT<T> GetSuccessor()
        {
            NodeRBT<T> x = this;
            if (x.right != RBT<T>.nil)
            {
                return RBT<T>.GetMinNode(x.right);
            }
            NodeRBT<T> y = x.p;
            while (y != RBT<T>.nil && x == y.right)
            {
                x = y;
                y = y.p;
            }
            return y;
        }
    }
}
