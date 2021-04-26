using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;

namespace NavProject_Navigator.CalcFunctions.Algoritms
{
    public class Dijkstra : IRouteAlg
    {
        private Map map;
        private ConnectivityComponents curComponent;

        private Node startNode;
        private Node endNode;
        public List<Node> Calc(Map _map, ConnectivityComponents _currentComponent, Node _startNode, Node _endNode)
        {
            map = _map;
            curComponent = _currentComponent;
            startNode = _startNode;
            endNode = _endNode;
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            Dictionary<Node, bool> isFixedConComp = new Dictionary<Node, bool>();
            Dictionary<Node, Node> previousConComp = new Dictionary<Node, Node>();

            foreach (Node i in curComponent.GetAllNodes())
            {
                distance.Add(i, int.MaxValue);
                isFixedConComp.Add(i, false);
            }

            distance[startNode] = 0;
            isFixedConComp[startNode] = true;
            previousConComp.Add(startNode, startNode);
            Node u = startNode;
            while (!u.Equals(endNode))
            {
                foreach (Node nd in map.GetLevel(curComponent.GetFloor()).GetEdge(u))
                    if (!isFixedConComp[nd] && distance[u] != int.MaxValue && distance[u] + GetDistanceBetweenTwoPoints(u, nd) < distance[nd])
                    {
                        distance[nd] = distance[u] + GetDistanceBetweenTwoPoints(u, nd);
                        if (previousConComp.ContainsKey(nd))
                            previousConComp[nd] = u;
                        else
                            previousConComp.Add(nd, u);
                    }

                u = MinimumDistance(ref distance, ref isFixedConComp);
                isFixedConComp[u] = true;
            }

            return PrintPath(ref previousConComp);
        }
        private Node MinimumDistance(ref Dictionary<Node, int> distance, ref Dictionary<Node, bool> isFixedConComp)
        {
            int min = int.MaxValue;
            Node nullNode = new Node();
            Node minIndex = nullNode;
            foreach (Node conn in distance.Keys)
            {
                if (!isFixedConComp[conn] && distance[conn] <= min)
                {
                    if (!minIndex.Equals(nullNode) && distance[minIndex] == distance[conn] && minIndex.Equals(endNode))
                        continue;

                    min = distance[conn];
                    minIndex = conn;
                }
            }
            return minIndex;
        }
        private int GetDistanceBetweenTwoPoints(Node startPoint, Node EndPoint)
        {
            Point start = map.GetCoordOfNode(curComponent.GetFloor(), startPoint);
            Point end = map.GetCoordOfNode(curComponent.GetFloor(), EndPoint);
            return Convert.ToInt32(Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y)));
        } 
        private List<Node> PrintPath(ref Dictionary<Node, Node> previousConComp)
        {
            var result = new List<Node>();

            Node currentNode = endNode;
            while (!currentNode.Equals(startNode))
            {
                result.Add(currentNode);
                currentNode = previousConComp[currentNode];
            }
            result.Add(startNode);
            result.Reverse();
            return result;
        }

    }
}
