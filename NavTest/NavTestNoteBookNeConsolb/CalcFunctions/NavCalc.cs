using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavTest;

namespace NavTestNoteBookNeConsolb
{
    public struct TaskToCalc
    {
        ConnectivityComp CurrConnComp;
        Node startNode;
        Node endNode;
    }
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
        private List<ConnectivityComp> connectivityCompsList;
        private ConnectivityComp conCompStart;
        private ConnectivityComp conCompEnd;
        public Dijkstra(ref Map _map, ref List<ConnectivityComp> _connectivityCompsList, ref ConnectivityComp _connectivityCompStart, ref ConnectivityComp _connectivityCompEnd)
        {
            map = _map;
            connectivityCompsList = _connectivityCompsList;
            conCompStart = _connectivityCompStart;
            conCompEnd = _connectivityCompEnd;
        }
        private ConnectivityComp MinimumDistance(ConnectivityComp currentConComp, ref Dictionary<ConnectivityComp, int> distance, ref Dictionary<ConnectivityComp, bool> isFixedConComp)
        {
            int min = int.MaxValue;
            ConnectivityComp minIndex=null;
            foreach (Node nds in currentConComp.GetLadderList())
            {
                foreach (ConnectivityComp conn in map.GetConnectivities(nds))
                {
                    if (!isFixedConComp[conn] && distance[conn] <= min)
                    {
                        min = distance[conn];
                        minIndex = conn;
                    }
                }
            }
            return minIndex;
        }

        public void DijkstraAlgo(ConnectivityComp comp)
        {
            Dictionary<ConnectivityComp, int> distance = new Dictionary<ConnectivityComp, int>();
            Dictionary<ConnectivityComp, bool> isFixedConComp = new Dictionary<ConnectivityComp, bool>();

            foreach (ConnectivityComp i in connectivityCompsList)
            {
                distance.Add(i, int.MaxValue);
                isFixedConComp.Add(i, false);
            }
            distance[conCompStart] = 0;
            foreach(ConnectivityComp i in connectivityCompsList)
            {
                ConnectivityComp u = MinimumDistance(i, ref distance, ref isFixedConComp);
                isFixedConComp[u] = true;
                foreach(Node nd in i.GetLadderList())
                foreach(ConnectivityComp j in map.GetConnectivities(nd))
                    if (!isFixedConComp[j] && distance[u] != int.MaxValue && distance[u] + 1 < distance[j])
                        distance[j] = distance[u] + 1;
            }
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
                result.Reverse();
                return result;
            }
        }
    }
}
