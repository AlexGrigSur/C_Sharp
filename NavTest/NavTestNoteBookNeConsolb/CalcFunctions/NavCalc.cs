using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavTest;

namespace NavTestNoteBookNeConsolb
{
    class NavCalc
    {
        static int Vertex = 9;
        int minDistance(int[] dist, bool[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < Vertex; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        void dijkstra(int[,] graph, int src)
        {
            int[] dist = new int[Vertex];
            bool[] sptSet = new bool[Vertex];

            for (int i = 0; i < Vertex; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            dist[src] = 0;
            for (int count = 0; count < Vertex - 1; count++)
            {
                int u = minDistance(dist, sptSet);
                sptSet[u] = true;
                for (int v = 0; v < Vertex; v++)
                    if (!sptSet[v] && graph[u, v] != 0 &&
                        dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }
            //printSolution(dist, Vertex);
        }
    }

    public class Dijkstra
    {
        private Map map;
        private ConnectivityComp conCompStart;
        private ConnectivityComp conCompEnd;
        public Dijkstra(ref Map _map, ref ConnectivityComp _connectivityCompStart, ref ConnectivityComp _connectivityCompEnd)
        {
            map = _map;
            conCompStart=_connectivityCompStart;
            conCompEnd=_connectivityCompEnd;
        }
        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        private static void Print(int[] distance, int verticesCount)
        {
            Console.WriteLine("Vertex    Distance from source");

            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i, distance[i]);
        }

        public static void DijkstraAlgo(ConnectivityComp comp)
        {
            //int[] distance = new int[];
            //bool[] shortestPathTreeSet = new bool[verticesCount];

            //for (int i = 0; i < verticesCount; ++i)
            //{
            //    distance[i] = int.MaxValue;
            //    shortestPathTreeSet[i] = false;
            //}

            //distance[source] = 0;

            //for (int count = 0; count < verticesCount - 1; ++count)
            //{
            //    int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            //    shortestPathTreeSet[u] = true;

            //    for (int v = 0; v < verticesCount; ++v)
            //        if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
            //            distance[v] = distance[u] + graph[u, v];
            //}

            //Print(distance, verticesCount);
        }

        class A_Star
        {
            Map map;
            ConnectivityComp curConComp;
            public A_Star(ref Map _map, ConnectivityComp _currentConComp)
            {
                map = _map;
                curConComp = _currentConComp;
            }
            private int GetHeuristicPathLength(Node startPoint, Node EndPoint)
            {
                List<int> start = map.GetCoordOfNode(curConComp.GetFloor(), startPoint);
                List<int> end = map.GetCoordOfNode(curConComp.GetFloor(), EndPoint);
                return Math.Abs(start[0] - end[0]) + Math.Abs(start[1] - end[1]);
            }
            private static List<Node> GetPathForNode(Node startNode, Node endNode)
            {
                List<Node> result = new List<Node>();
                //var currentNode = pathNode;
                //while (currentNode != null)
                //{
                //    result.Add(currentNode.Position);
                //    currentNode = currentNode.CameFrom;
                //}
                result.Reverse();
                return result;
            }
        }
    }
}
