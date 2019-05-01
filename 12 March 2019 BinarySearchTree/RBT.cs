using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_March_2019_BinarySearchTree
{
    // Lg(n)
    public class RBT<T> where T : IComparable
    {
        public static NodeRBT<T> nil;
        public NodeRBT<T> root;

        public RBT()
        {
            nil = new NodeRBT<T>();
            root = nil;
        }

        public void Delete(NodeRBT<T> z)
        {//RB - DELETE(T, z)
            NodeRBT<T> x;
            //1.y = z
            NodeRBT<T> y = z;
            //2.y - original - color = y.color
            Color yOGColor = y.color;
            //3. if z.left == T.nil
            if (z.left == nil)
            {
                //4.     x = z.right
                x = z.right;
                //5.     RB - TRANSPLANT(T, z, z.right) 
                Transplant(z, z.right);
            }
            else if(z.right == nil) //6. elseif z.right == T.nil
            {
                //7.     x = z.left
                x = z.left;
                //8.     RB - TRANSPLANT(T, z, z.left) 
                Transplant(z, z.left);
            }
            else //9. else 
            {
                //   y = TREE - MINIMUM(z.right)
                y = GetMinNode(z.right);
                //10.y - original - color = y.color
                yOGColor = y.color;
                //11.x = y.right
                x = y.right;
                if (y.p == z)//12.   if y.p == z
                {
                    //13.       x.p = y
                    x.p = y;
                }
                //14.   else
                else
                {
                    //          RB - TRANSPLANT(T, y, y.right)
                    Transplant(y, y.right);
                    //15.   y.right = z.right
                    y.right = z.right;
                    //16.   y.right.p = y
                    y.right.p = y;
                }
                //17.RB - TRANSPLANT(T, z, y)
                Transplant(z, y);
                //18.y.left = z.left
                y.left = z.left;
                //19.y.left.p = y
                y.left.p = y;
                //20.y.color = z.color
                y.color = z.color;
            }
            
            if (yOGColor == Color.BLACK) //21.if y - original - color == BLACK
            {
                //22.    RB - DELETE - FIXUP(T, x) 
                DeleteFixup(x);
            }
        }

        private void DeleteFixup(NodeRBT<T> x)
        { //RB - DELETE - FIXUP(T, x)
            while (x != this.root && x.color == Color.BLACK) //1. while x ≠ T and x.color == BLACK
            {
                if (x == x.p.left) //2. if x == x.p.left
                {
                    //3.w = x.p.right
                    NodeRBT<T> w = x.p.right;
                    if (w.color == Color.RED)//4. if w.color == RED
                    {
                        //5.w.color = BLACK // case 1
                        w.color = Color.BLACK;
                        //6.x.p.color = RED // case 1
                        x.p.color = Color.RED;
                        //7.LEFT - ROTATE(T, x.p) // case 1
                        LeftRotate(x.p);
                        //8.w = x.p.right // case 1 
                        w = x.p.right;
                    }
                    if (w.left.color == Color.BLACK && w.right.color == Color.BLACK) //9. if w.left.color == BLACK and w.right.color == BLACK
                    {
                        //10.w.color = RED // case 2
                        w.color = Color.RED;
                        //11.x = x.p // case 2 
                        x = x.p;
                    }
                    else //12. else
                    {
                        if (w.right.color == Color.BLACK) //13. if w.right.color == BLACK
                        {
                            //14.w.left.color = BLACK // case 3
                            w.left.color = Color.BLACK;
                            //15.w.color = RED // case 3
                            w.color = Color.RED;
                            //16.RIGHT - ROTATE(T, w) // case 3
                            RightRotate(w);
                            //17.w = x.p.right // case 3  
                            w = x.p.right;
                        }
                    }
                    //18.w.color = x.p.color // case 4
                    w.color = x.p.color;
                    //19.x.p.color = BLACK // case 4
                    x.p.color = Color.BLACK;
                    //20.w.right.color = BLACK // case 4
                    w.right.color = Color.BLACK;
                    //21.LEFT - ROTATE(T, x.p) // case 4
                    LeftRotate(x.p);
                    //22.x = T.root // case 4
                    x = this.root;

                }
                else //23. else (same as then clause, line 3, with right and left exchanged)
                {
                    //3.w = x.p.right
                    NodeRBT<T> w = x.p.left;
                    if (w.color == Color.RED)//4. if w.color == RED
                    {
                        //5.w.color = BLACK // case 1
                        w.color = Color.BLACK;
                        //6.x.p.color = RED // case 1
                        x.p.color = Color.RED;
                        //7.LEFT - ROTATE(T, x.p) // case 1
                        RightRotate(x.p);
                        //8.w = x.p.right // case 1 
                        w = x.p.left;
                    }
                    if (w.right.color == Color.BLACK && w.left.color == Color.BLACK) //9. if w.left.color == BLACK and w.right.color == BLACK
                    {
                        //10.w.color = RED // case 2
                        w.color = Color.RED;
                        //11.x = x.p // case 2 
                        x = x.p;
                    }
                    else //12. else
                    {
                        if (w.left.color == Color.BLACK) //13. if w.right.color == BLACK
                        {
                            //14.w.left.color = BLACK // case 3
                            w.right.color = Color.BLACK;
                            //15.w.color = RED // case 3
                            w.color = Color.RED;
                            //16.RIGHT - ROTATE(T, w) // case 3
                            LeftRotate(w);
                            //17.w = x.p.right // case 3  
                            w = x.p.left;
                        }
                    }
                    //18.w.color = x.p.color // case 4
                    w.color = x.p.color;
                    //19.x.p.color = BLACK // case 4
                    x.p.color = Color.BLACK;
                    //20.w.right.color = BLACK // case 4
                    w.left.color = Color.BLACK;
                    //21.LEFT - ROTATE(T, x.p) // case 4
                    RightRotate(x.p);
                    //22.x = T.root // case 4
                    x = this.root;
                }
            }
            //24.x.color = BLACK
            x.color = Color.BLACK;
        }

        public static NodeRBT<T> GetMinNode(NodeRBT<T> x)
        {
            while (x.left != nil)
            {
                x = x.left;
            }
            return x;
        }

        public NodeRBT<T> GetMinNode()
        {
            NodeRBT<T> x = this.root;
            while (x.left != nil)
            {
                x = x.left;
            }
            return x;
        }

        private void Transplant(NodeRBT<T> u, NodeRBT<T> v)
        {
            //RB-TRANSPLANT(T,u,v)
            //  1. if u.p == T.nil
            if (u.p == nil)
            {
                //  2. T.root = v
                this.root = v;
            }
            else if (u == u.p.left)//  3. elseif u == u.p.left
            {
                //  4. u.p.left = v
                u.p.left = v;
            }
            else
            { //  5. else u.p.right = v
                u.p.right = v;
            }
            //  6. v.p = u.p
            v.p = u.p;
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
            LevelOrder(this);
            Console.WriteLine();
        }

        private void LevelOrder(RBT<T> T) // Breadth
        {
            int level = 0;
            int height = T.GetHeight(T.root);
            var queue = new Queue<NodeRBT<T>>();
            Queue<int> queueLevel = new Queue<int>();
            if (T.root != nil)
            {
                // enqueue current root
                queue.Enqueue(T.root);
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
                    Console.Write($"{(node.color == Color.RED?"[":"(")}{node.key}{(node.color == Color.RED ? "]" : ")")}:L:{level}({node.left},{node.right}),");

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

        public void InsertValue(T k)
        {
            Insert(new NodeRBT<T>(k));
        }

        private void Insert(NodeRBT<T> z)
        {
            NodeRBT<T> y = nil;
            NodeRBT<T> x = this.root;

            while (x != nil)
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

            if (y == nil)
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
            z.left = nil;
            z.right = nil;
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
       

        private void LeftRotate(NodeRBT<T> x)
        {
            NodeRBT<T> y = x.right;
            x.right = y.left;
            if (y.left != nil)
            {
                y.left.p = x;
            }
            y.p = x.p;
            if (x.p == nil)
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
            if (y.right != nil)
            {
                y.right.p = x;
            }
            y.p = x.p;
            if (x.p == nil)
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

        private string PrintNode(NodeRBT<T> x)
        {
            return x == nil ? "@" : x.ToString();
        }

        public NodeRBT<T> Search(T k)
        {
            return Search(root, k);
        }

        private NodeRBT<T> Search(NodeRBT<T> x, T k)
        {
            if (x == nil || k.CompareTo(x.key) == 0)
            {
                return x;
            }
            if (k.CompareTo(x.key) < 0)
            {
                return Search(x.left, k);
            }
            else
            {
                return Search(x.right, k);
            }
        }
    } // Class
} // namespace
