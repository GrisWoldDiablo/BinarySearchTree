using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_March_2019_BinarySearchTree
{
    public class RBT<T> where T : IComparable
    {
        public NodeRBT<T> nil;
        public NodeRBT<T> root;

        public RBT()
        {
            nil = new NodeRBT<T>();
            root = nil;
        }

        private void Search()
        {

        }

        public void InsertValue(T k)
        {
            Insert(new NodeRBT<T>(k));
        }

        public void InOrder()
        {
            InOrder(this.root);
            Console.WriteLine();
        }

        private void InOrder(NodeRBT<T> root)
        {
            if (root != nil)
            {
                InOrder(root.left);
                Console.Write($"{root.key},");
                InOrder(root.right);
            }
        }

        public void PreOrder()
        {
            PreOrder(root);
            Console.WriteLine();
        }

        private void PreOrder(NodeRBT<T> root)
        {
            if (root != nil)
            {
                Console.Write($"{root.key},");
                PreOrder(root.left);
                PreOrder(root.right);
            }
        }

        public void PostOrder()
        {
            PostOrder(root);
            Console.WriteLine();
        }

        private void PostOrder(NodeRBT<T> root)
        {
            if (root != nil)
            {
                PostOrder(root.left);
                PostOrder(root.right);
                Console.Write($"{root.key},");
            }
        }

        public void LevelOrder()
        {
            LevelOrder(root);
            Console.WriteLine();
        }

        private void LevelOrder(NodeRBT<T> root)
        {
            int level = 0;
            int height = GetHeight(root);
            var queue = new Queue<NodeRBT<T>>();
            Queue<int> queueLevel = new Queue<int>();
            if (root != nil)
            {
                // enqueue current root
                queue.Enqueue(root);
                queueLevel.Enqueue(0);
                int currentLevel = 0;
                // while there are nodes to process
                while (queue.Count != 0)
                {
                    // dequeue next node
                    NodeRBT<T> node = queue.Dequeue();
                    level = queueLevel.Dequeue();
                    if (currentLevel != level)
                    {
                        currentLevel = level;
                        Console.WriteLine();
                    }
                    Console.Write($"{node.key}:L:{level},");

                    // enqueue child elements from next level in order
                    if (node.left != nil/* tree has non-empty left subtree*/ )
                    {
                        queue.Enqueue(node.left);
                        queueLevel.Enqueue(level + 1);
                    }
                    if (node.right != nil/*tree has non-empty right subtree */)
                    {
                        queue.Enqueue(node.right);
                        queueLevel.Enqueue(level + 1);
                    }
                }
                Console.WriteLine();
            }

        }

        private void Insert(NodeRBT<T> z)
        {
            NodeRBT<T> y = this.nil;
            NodeRBT<T> x = this.root;

            while (x != this.nil)
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

            z.p = y;

            if (y == this.nil)
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
            z.left = this.nil;
            z.right = this.nil;
            z.color = Color.RED;
            InsertFixUp(z);

        }

        private void InsertFixUp(NodeRBT<T> z)
        {
            while (z.p.color == Color.RED)
            {
                if (z.p == z.p.p.left)
                {
                    NodeRBT<T> y = z.p.p.right;
                    if (y.color == Color.RED)
                    {
                        z.p.color = Color.BLACK;
                        y.color = Color.BLACK;
                        z.p.p.color = Color.RED;
                        z = z.p.p;
                    }
                    else
                    {
                        if (z == z.p.right)
                        {
                            z = z.p;
                            LeftRotate(z);
                        }
                        z.p.color = Color.BLACK;
                        z.p.p.color = Color.RED;
                        RightRotate(z.p.p);
                    }
                }
                else
                {
                    NodeRBT<T> y = z.p.p.left;
                    if (y.color == Color.RED)
                    {
                        z.p.color = Color.BLACK;
                        y.color = Color.BLACK;
                        z.p.p.color = Color.RED;
                        z = z.p.p;
                    }
                    else
                    {
                        if (z == z.p.left)
                        {
                            z = z.p;
                            RightRotate(z);
                        }
                        z.p.color = Color.BLACK;
                        z.p.p.color = Color.RED;
                        LeftRotate(z.p.p);
                    }
                }
            }
            this.root.color = Color.BLACK;
        }

        private void Delete()
        {

        }

        private void LeftRotate(NodeRBT<T> x)
        {
            NodeRBT<T> y = x.right;
            x.right = y.left;
            if (y.left != this.nil)
            {
                y.left.p = x;
            }
            y.p = x.p;
            if (x.p == this.nil)
            {
                this.root = y;
            }
            else if (x == x.p.left)
            {
                x.p.left = y;
            }
            else
            {
                x.p.right = y;
            }
            y.left = x;
            x.p = y;
        }

        private void RightRotate(NodeRBT<T> x)
        {
            NodeRBT<T> y = x.left;
            x.left = y.right;
            if (y.right != this.nil)
            {
                y.right.p = x;
            }
            y.p = x.p;
            if (x.p == this.nil)
            {
                this.root = y;
            }
            else if (x == x.p.right)
            {
                x.p.right = y;
            }
            else
            {
                x.p.left = y;
            }
            y.right = x;
            x.p = y;
        }

        public int GetHeight()
        {
            return GetHeight(root);
        }

        private int GetHeight(NodeRBT<T> x)
        {
            if (x == nil)
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

    } // Class
} // namespace
