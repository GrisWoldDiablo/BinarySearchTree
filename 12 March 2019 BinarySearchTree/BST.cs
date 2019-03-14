using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_March_2019_BinarySearchTree
{
    public class BST<T> where T : IComparable
    {
        public Node<T> root;
        public List<T> nodes = null;
        public BST()
        {
            this.root = null;
        }

        public void InsertNode(Node<T> z)
        {
            Node<T> y = null;
            Node<T> x = this.root;

            while (x != null)
            {
                y = x;
                if (z.key.CompareTo(x.key) < 0)
                {
                    x = x.left;
                }
                else
                {
                    x = x.right;
                }
            }

            z.parent = y;

            if (y == null)
            {
                this.root = z;
            }
            else if (z.key.CompareTo(y.key) < 0)
            {
                y.left = z;
            }
            else
            {
                y.right = z;
            }
        }

        public void InsertValue(T v)
        {
            Node<T> z = new Node<T>(v);
            Node<T> y = null;
            Node<T> x = this.root;

            while (x != null)
            {
                y = x;
                if (z.key.CompareTo(x.key) < 0)
                {
                    x = x.left;
                }
                else
                {
                    x = x.right;
                }
            }

            z.parent = y;

            if (y == null)
            {
                this.root = z;
            }
            else if (z.key.CompareTo(y.key) < 0)
            {
                y.left = z;
            }
            else
            {
                y.right = z;
            }
        }

        public static Node<T> GetMinNode(Node<T> root)
        {
            while (root.left != null)
            {
                root = root.left;
            }
            return root;
        }

        public static Node<T> GetMaxNode(Node<T> root)
        {
            while (root.right != null)
            {
                root = root.right;
            }
            return root;
        }

        public void InOrder()
        {
            nodes = new List<T>();
            InOrder(root);
        }

        public void PostOrder()
        {
            PostOrder(root);
        }

        public void PreOrder()
        {
            PreOrder(root);
        }

        private void InOrder(Node<T> root)
        {
            if (root != null)
            {
                InOrder(root.left);
                Console.Write($"{root.key},");
                nodes.Add(root.key);
                InOrder(root.right);
            }
        }

        private void PreOrder(Node<T> root)
        {
            if (root != null)
            {
                Console.Write($"{root.key},");
                InOrder(root.left);
                InOrder(root.right);
            }
        }

        private void PostOrder(Node<T> root)
        {
            if (root != null)
            {
                InOrder(root.left);
                InOrder(root.right);
                Console.Write($"{root.key},");
            }
        }

        public void LevelOrder()
        {
            LevelOrder(root);
        }

        static void LevelOrder(Node<T> root)
        {
            int level = 0;
            int height = GetHeight(root);
            Queue<Node<T>> queue = new Queue<Node<T>>();
            Queue<int> queueLevel = new Queue<int>();
            if (root != null)
            {
                // enqueue current root
                queue.Enqueue(root);
                queueLevel.Enqueue(0);
                int currentLevel = 0;
                // while there are nodes to process
                while (queue.Count != 0)
                {
                    // dequeue next node
                    Node<T> node = queue.Dequeue();
                    level = queueLevel.Dequeue();
                    if (currentLevel != level)
                    {
                        currentLevel = level;
                        Console.WriteLine();
                    }
                    Console.Write($"{node.key}:L:{level},"); 
                    
                    // enqueue child elements from next level in order
                    if (node.left != null/* tree has non-empty left subtree*/ )
                    {
                        queue.Enqueue(node.left);
                        queueLevel.Enqueue(level + 1);
                    }
                    if (node.right != null/*tree has non-empty right subtree */)
                    {
                        queue.Enqueue(node.right);
                        queueLevel.Enqueue(level + 1);
                    }
                }
                Console.WriteLine();
            }

        }

        public Node<T> SearchTree(T k)
        {
            return SearchTree(root, k);
        }

        private Node<T> SearchTree(Node<T> x, T k)
        {
            //1. if x == NIL or k == x.key
            //2.    return x
            //3. if k < x.key
            //4.    return TREE - SEARCH(x.left, k)
            //5. else 
            //      return TREE - SEARCH(x.right, k)
            if (x == null || k.CompareTo(x.key) == 0)
            {
                return x;
            }
            if (k.CompareTo(x.key) < 0)
            {
                return SearchTree(x.left, k);
            }
            else
            {
                return SearchTree(x.right, k);
            }
        }

        private void Transplant(Node<T> u, Node<T> v)
        {
            //1. if u.p == NIL
            //2.    T.root = v
            //3. elseif u == u.p.left
            //4.    u.p.left = v
            //5. else 
            //      u.p.right = v
            //6. if v ≠ NIL
            //7.    v.p = u.p

            if (u.parent == null)
            {
                this.root = v;
            }
            else if (u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }

            if (v != null)
            {
                v.parent = u.parent;
            }
        }

        public void DeleteNode(Node<T> z)
        {
            //1. if z.left == NIL
            //2.    TRANSPLANT(T, z, z.right)
            //3. else if z.right == NIL
            //4.    TRANSPLANT(T, z, z.left)
            //5. else 
            //      y = TREE - MINIMUM(z.right)
            //6.    if y.p ≠ z
            //7.        TRANSPLANT(T, y, y.right)
            //8.        y.right = z.right
            //9.        y.right.p = y
            //10    TRANSPLANT(T, z, y)
            //11    y.left = z.left
            //12    y.left.p = y
            if (z.left == null)
            {
                Transplant(z, z.right);
            }
            else if (z.right == null)
            {
                Transplant(z, z.left);
            }
            else {
                Node<T> y = GetMinNode(z.right);
                if (y.parent != z)
                {
                    Transplant(y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }
                Transplant(z, y);
                y.left = z.left;
                y.left.parent = y;
            }
            
        }

        //public int GetHeight()
        //{
        //    return GetHeight(root);
        //}

        //private static int GetHeight(Node<T> root)
        //{
        //    int furRight = 0;
        //    int furLeft = 0;
        //    Node<T> current = root;
        //    while (current.right != null)
        //    {
        //        furRight++;
        //        current = current.right;
        //        if (current.right == null && current.left != null)
        //        {
        //            furRight += GetHeight(current);
        //        }
        //    }
        //    current = root;
        //    while (current.left != null)
        //    {
        //        furLeft++;
        //        current = current.left;
        //        if (current.left == null && current.right != null)
        //        {
        //            furLeft += GetHeight(current);
        //        }
        //    }

        //    return furRight > furLeft ? furRight : furLeft;
        //}

        public int GetHeight()
        {
            return GetHeight(root);
        }

        private static int GetHeight(Node<T> x)
        {
            if (x == null)
            {
                return -1;
            }

            int lefth = GetHeight(x.left);
            int righth = GetHeight(x.right);

            if (lefth > righth)
            {
                return lefth + 1;
            }
            else
            {
                return righth + 1;
            }
        }
    }
}
