using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NavProject_Navigator.Structures;

namespace NavProject_Navigator.CalcFunctions
{
    interface IRouteCalc
    {
        public Dictionary<ConnectivityComponents, List<Node>> GetRoute(List<ComponentsToCalc> toCalc);
    }
    //class RouteCalc
    //{
    //    public Dictionary<ConnectivityComponents, List<Node>> GetRoute(Map map, Node startNode, Node endNode, IRouteCalc routeMethod)
    //    {

    //        ConnectivityComponents startConComp = map.FindConnectivityComponentsByNode(startNode);
    //        ConnectivityComponents endConComp = map.FindConnectivityComponentsByNode(endNode);

    //        List<ComponentsToCalc> calc = new List<ComponentsToCalc>();

    //        if (startConComp.Equals(endConComp))
    //            calc.Add(new ComponentsToCalc(startConComp, startNode, endNode));
    //        else
    //        {
    //            List<ConnectivityComponents> ConnectivityComponentsList = new List<ConnectivityComponents>();
    //            foreach (Level i in map.GetFloorsList().Values)
    //                foreach (ConnectivityComponents comp in i.GetConnectivityComponentsList())
    //                    ConnectivityComponentsList.Add(comp);

    //            calc = new Dijkstra(ref map, ref startConComp, ref endConComp).Calc();

    //            calc.Reverse();
    //            calc[0] = new ComponentsToCalc(calc[0].CurrConnComp, startNode, new Node());
    //            calc[calc.Count - 1] = new ComponentsToCalc(calc[calc.Count - 1].CurrConnComp, new Node(), endNode);
    //        }

    //        return routeMethod.GetRoute();
    //    }
    //}
    //public class DijkstraForConn
    //{
    //    private Map map;
    //    private ConnectivityComponents curConComp;

    //    private Node startNode;
    //    private Node endNode;

    //    public DijkstraForConn(ref Map _map, ConnectivityComponents _currentConComp, Node _startNode, Node _endNode)
    //    {
    //        map = _map;
    //        curConComp = _currentConComp;
    //        startNode = _startNode;
    //        endNode = _endNode;
    //    }

    //    private Node MinimumDistance(ref Dictionary<Node, int> distance, ref Dictionary<Node, bool> isFixedConComp)
    //    {
    //        int min = int.MaxValue;
    //        Node nullNode = new Node();
    //        Node minIndex = nullNode;
    //        foreach (Node conn in distance.Keys)
    //        {
    //            if (!isFixedConComp[conn] && distance[conn] <= min)
    //            {
    //                if (!minIndex.Equals(nullNode) && distance[minIndex] == distance[conn] && minIndex.Equals(endNode))
    //                    continue;

    //                min = distance[conn];
    //                minIndex = conn;
    //            }
    //        }
    //        return minIndex;
    //    }

    //    private int GetDistanceBetweenTwoPoints(Node startPoint, Node EndPoint) // also using to calc heuristic (distance between node -> goal)
    //    {
    //        Point start = map.GetCoordOfNode(curConComp.GetFloor(), startPoint);
    //        Point end = map.GetCoordOfNode(curConComp.GetFloor(), EndPoint);
    //        return Convert.ToInt32(Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y)));
    //    }
    //    public List<Node> Calc()
    //    {
    //        Dictionary<Node, int> distance = new Dictionary<Node, int>();
    //        Dictionary<Node, bool> isFixedConComp = new Dictionary<Node, bool>();
    //        Dictionary<Node, Node> previousConComp = new Dictionary<Node, Node>();

    //        foreach (Node i in curConComp.GetAllNodes())
    //        {
    //            distance.Add(i, int.MaxValue);
    //            isFixedConComp.Add(i, false);
    //        }

    //        distance[startNode] = 0;
    //        isFixedConComp[startNode] = true;
    //        previousConComp.Add(startNode, startNode);
    //        Node u = startNode;
    //        while (!u.Equals(endNode))
    //        {
    //            foreach (Node nd in map.GetLevel(curConComp.GetFloor()).GetEdge(u))
    //                if (!isFixedConComp[nd] && distance[u] != int.MaxValue && distance[u] + GetDistanceBetweenTwoPoints(u, nd) < distance[nd])
    //                {
    //                    distance[nd] = distance[u] + GetDistanceBetweenTwoPoints(u, nd);
    //                    if (previousConComp.ContainsKey(nd))
    //                        previousConComp[nd] = u;
    //                    else
    //                        previousConComp.Add(nd, u);
    //                }

    //            u = MinimumDistance(ref distance, ref isFixedConComp);
    //            isFixedConComp[u] = true;
    //        }

    //        return PrintPath(ref previousConComp);
    //    }

    //    private List<Node> PrintPath(ref Dictionary<Node, Node> previousConComp)
    //    {
    //        var result = new List<Node>();

    //        Node currentNode = endNode;
    //        while (!currentNode.Equals(startNode))
    //        {
    //            result.Add(currentNode);
    //            currentNode = previousConComp[currentNode];
    //        }
    //        result.Add(startNode);
    //        result.Reverse();
    //        return result;
    //    }
    //}
}
