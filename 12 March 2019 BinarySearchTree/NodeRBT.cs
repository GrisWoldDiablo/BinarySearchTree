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
        public NodeRBT<T> gp;
        public NodeRBT<T> u;

        public NodeRBT()
        {
            color = Color.BLACK;
        }

        public NodeRBT(T k)
        {
            key = k;
        }
    }
}
