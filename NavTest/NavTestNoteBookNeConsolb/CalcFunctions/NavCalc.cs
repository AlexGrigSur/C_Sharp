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
        Map map;
        public NavCalc(Map _map, Node startNode, Node endNode)
        {
            map = _map;
            var startConComp = map.FindConnectivityCompByNode(startNode);
            var endConComp = map.FindConnectivityCompByNode(endNode);
            if (!startConComp.Equals(endConComp))
            {
                List<ConnectivityComp> connectivityCompsList = new List<ConnectivityComp>();
                foreach (Level i in map.GetFloorsList().Values)
                    foreach (ConnectivityComp comp in i.GetConnectivityComponentsList())
                        connectivityCompsList.Add(comp);

                Dijkstra algo = new Dijkstra(ref map, ref connectivityCompsList, ref startConComp, ref endConComp);
            }
        }
    }

    public class Dijkstra
    {
        private Map map;
        List<ConnectivityComp> connectivityCompsList;
        private ConnectivityComp conCompStart;
        private ConnectivityComp conCompEnd;
        public Dijkstra(ref Map _map, ref List<ConnectivityComp> _connectivityCompsList, ref ConnectivityComp _connectivityCompStart, ref ConnectivityComp _connectivityCompEnd)
        {
            map = _map;
            connectivityCompsList = _connectivityCompsList;
            conCompStart = _connectivityCompStart;
            conCompEnd = _connectivityCompEnd;
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
