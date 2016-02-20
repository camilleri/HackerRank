using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendAway
{
    // https://www.hackerrank.com/contests/booking-hack-a-holiday/challenges/weekend-away

    class Node
    {
        public SortedList<int, List<int>> children; // weight, node

        public Node()
        {
            children = new SortedList<int, List<int>>();            
        }
    }

    class Graph
    {
        Node[] nodes;
        int numNodes;

        public Graph(int n)
        {
            nodes = new Node[n];

            for (int i = 0; i < n; ++i)
            {
                nodes[i] = new Node();
            }

            numNodes = n;
        }

        public void addRoad(int c1, int c2, int d)
        {
            List<int> bucket;
            if (nodes[c1].children.TryGetValue(d, out bucket))
            {
                bucket.Add(c2);
            }
            else
            {
                nodes[c1].children.Add(d, new List<int>() { c2 });
            }

            if (nodes[c2].children.TryGetValue(d, out bucket))
            {
                bucket.Add(c1);
            }
            else
            {
                nodes[c2].children.Add(d, new List<int>() { c1 });
            }
        }

        public int solve()
        {

            int bestDistance = int.MaxValue;
            for (int c = 1; c < numNodes; c++) // Try starting from each city
            {
                int distance = findRoute(-1, bestDistance, 0, c, 1); // breaks recursion if distance > bestDistance               

                if (distance < bestDistance)
                    bestDistance = distance;
            }

            return bestDistance;
        }

        private int findRoute(int prevCity, int bestDistance, int currDistance, int currCity, int numCities)
        {
            if (currDistance >= bestDistance)
                return int.MaxValue; // break recursion, we won't find a better route

            if (numCities >= 3) // visited 3 cities, return current distance
                return currDistance;

            // find shortest sub-route from each children
            int bestSubRoute = int.MaxValue;
            foreach (KeyValuePair<int, List<int>> pair in nodes[currCity].children)
            {
                foreach (int nextCity in pair.Value) {
                    if (nextCity != prevCity) // avoid loops
                    {
                        int distance = pair.Key + currDistance;
                        if (distance < bestSubRoute) // only explore if we can improve...
                        {
                            int subRouteDistance = findRoute(currCity, bestDistance, distance, nextCity, numCities + 1);
                            if (subRouteDistance < bestSubRoute)
                            {
                                bestSubRoute = subRouteDistance;
                            }
                        }
                    }
                }
            }

            return bestSubRoute;
        }
    }

    class Solution
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());

            for (int t = 0; t < T; ++t)
            {
                string[] line = Console.ReadLine().Split();
                int N = int.Parse(line[0]); // locations
                int M = int.Parse(line[1]); // roads

                Graph g = new Graph(N + 1); // first index is 1

                for (int road = 0; road < M; ++road)
                {
                    line = Console.ReadLine().Split();
                    int c1 = int.Parse(line[0]);
                    int c2 = int.Parse(line[1]);
                    int d = int.Parse(line[2]);
                    g.addRoad(c1, c2, d);
                }

                Console.WriteLine(g.solve());
            }            
        }
    }
}
