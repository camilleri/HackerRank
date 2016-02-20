using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTree
{
    // https://www.hackerrank.com/challenges/even-tree?utm-source=booking-hack-a-holiday-reminder&utm-medium=email&utm-campaign=booking-hack-a-holiday
    // You are given a tree (a simple connected graph with no cycles). You have to remove as many edges from the tree as possible to obtain a forest with the condition that : 
    // Each connected

    class Node
    {
        public int value, nodesBelow;
        public List<int> children;

        public Node()
        {
            value = nodesBelow = 0;
            children = new List<int>();
        }

    }

    class Tree
    {
        Node[] nodes;

        public Tree(int n)
        {
            nodes = new Node[n];

            for(int i=0; i<n; ++i)
            {
                nodes[i] = new Node();
            }
        }

        public void addEdge(int child, int parent)
        {
            nodes[parent].children.Add(child);        
        }

        public int forest(int parent)
        {
            int res = 0;
            foreach (int child in nodes[parent].children)
            {
                int subTreeNodes = 1 + nodes[child].nodesBelow;
                if (subTreeNodes >= 2) // explore children
                {
                    if (subTreeNodes % 2 == 0) // we can cut off this edge
                    {
                        // Console.WriteLine("Cutting off edge " + parent + " -> " + child);
                        res += 1 + forest(child);
                    }
                    else // can't cut off
                    {
                        res += forest(child);
                    }
                }
            }

            return res;
        }

        public int countNodesBelow(int p)
        {
            int childrenNodesBelow = 0;
            foreach (int child in nodes[p].children)
            {
                childrenNodesBelow += countNodesBelow(child);
            }

            nodes[p].nodesBelow = nodes[p].children.Count + childrenNodesBelow;

            return nodes[p].nodesBelow;
        }

        public void print(int n)
        {
            foreach (int child in nodes[n].children)
            {
                print(child);
            }

            Console.WriteLine(n + " [" + nodes[n].nodesBelow + "]");
        }
    }

    class Solution
    {

        static void Main(string[] args)
        {
            string[] line = Console.ReadLine().Split();
            int N = int.Parse(line[0]);
            int M = int.Parse(line[1]);

            Tree t = new Tree(N);
            for (int i = 0; i < M; ++i)
            {
                line = Console.ReadLine().Split();
                int c = int.Parse(line[0]) - 1;
                int p = int.Parse(line[1]) - 1;

                t.addEdge(c, p); // nodes are indexed from 1
            }

            t.countNodesBelow(0); // start with the root and count the nodes below
            //t.print(0);
            Console.WriteLine(t.forest(0)); // initial root is 0
        }
    }
}
