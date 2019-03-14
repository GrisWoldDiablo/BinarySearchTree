using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_March_2019_BinarySearchTree
{
    class Program
    {
        static Random rand = new Random(2019);
        static List<int> numberList = new List<int>();
        static void Main(string[] args)
        {
            BST<int> myTree= new BST<int>();
            //myTree.InsertValue(10);
            //myTree.InsertValue(5);
            //myTree.InsertValue(20);
            //myTree.InsertValue(1);
            //myTree.InsertValue(4);
            //myTree.InsertValue(15);
            //myTree.InsertValue(31);
            int h = rand.Next(10, 20);
            for (int i = 0; i < h; i++)
            {
                int v = rand.Next(100);
                myTree.InsertValue(v);
                Console.Write($"{v}, ");
            }
            Console.WriteLine();
            Console.WriteLine("IN ORDER");
            myTree.InOrder();
            Console.WriteLine();
            Console.WriteLine("LEVEL ORDER");
            myTree.LevelOrder();
            Console.WriteLine();
            Console.WriteLine($"Height: {myTree.GetHeight()}");
            Console.WriteLine();
            var foundNode = myTree.SearchTree(15);
            if (foundNode != null)
            {
                Console.WriteLine(foundNode.key);
            }
            Console.WriteLine();
            int value = 93;
            foundNode = myTree.SearchTree(value);
            if (foundNode != null)
            {
                Console.WriteLine($"Successor of {value} : { myTree.SearchTree(value).GetSuccessor().key}");
            }
            value = rand.Next(34);
            foundNode = myTree.SearchTree(value);

            if (foundNode != null)
            {
                Console.WriteLine($"Delete {value}");
                myTree.DeleteNode(foundNode);
            }
            
            Console.WriteLine("IN ORDER");
            myTree.InOrder();
            Console.WriteLine();
            Console.WriteLine("POST ORDER");
            myTree.PostOrder();
            Console.WriteLine();
            Console.WriteLine("PRE ORDER");
            myTree.PreOrder();
            Console.WriteLine();
            Console.WriteLine("LEVEL ORDER");
            myTree.LevelOrder();
            Console.WriteLine($"Height: {myTree.GetHeight()}");

            Shuffle(myTree.nodes);
            foreach (var item in myTree.nodes)
            {
                Console.Write($"{item},");
            }
            Console.WriteLine();
        }

        private static void Shuffle(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                int j = rand.Next(0, i + 1);
                var temp = numbers[i];
                numbers[i] = numbers[j];
                numbers[j] = temp;
            }
        }
    }
}
