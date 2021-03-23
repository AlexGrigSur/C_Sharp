using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;
using System.Threading;

namespace NavProject_Navigator.CalcFunctions
{
    public struct A_Star_Point
    {
        public Node previousNode;
        public int currentDistance;
        public int heuristic;
        public A_Star_Point(Node _previousNode, int _currentDistance, int _heuristic)
        {
            previousNode = _previousNode;
            currentDistance = _currentDistance;
            heuristic = _heuristic;
        }
    }
    public class A_Star //: IRouteCalc
    {
        //private Map map;
        //private ConnectivityComponents curConComp;
        //private Dictionary<Node, int> heuristic = new Dictionary<Node, int>();

        //Dictionary<Node, A_Star_Point> openSet = new Dictionary<Node, A_Star_Point>();
        //Dictionary<Node, A_Star_Point> closedSet = new Dictionary<Node, A_Star_Point>();

        //private Node startNode;
        //private Node endNode;

        //public A_Star(ref Map _map, ConnectivityComponents _currentConComp, Node _startNode, Node _endNode)
        //{
        //    map = _map;
        //    curConComp = _currentConComp;
        //    startNode = _startNode;
        //    endNode = _endNode;
        //}



        //public Dictionary<ConnectivityComponents, List<Node>> GetRoute();

        //private void getPriority(ref List<TaskToCalc> calc)
        //{
        //    for (int i = 0; i < calc.Count - 1; ++i)
        //    {
        //        int min = int.MaxValue;
        //        Node ladrIndex = new Node();
        //        Point baseCoord = map.GetCoordOfNode(calc[i].CurrConnComp.GetFloor(), calc[i].startNode);
        //        Point ladderCoord;

        //        foreach (Node ladr in calc[i].CurrConnComp.GetLadderList())
        //        {
        //            if (!map.GetConnectivities(ladr).Contains(calc[i + 1].CurrConnComp))
        //                continue;
        //            ladderCoord = map.GetCoordOfNode(calc[i].CurrConnComp.GetFloor(), ladr);
        //            int distance = Convert.ToInt32(Math.Sqrt((ladderCoord.X - baseCoord.X) * (ladderCoord.X - baseCoord.X) + (ladderCoord.Y - baseCoord.Y) * (ladderCoord.Y - baseCoord.Y)));
        //            if (distance < min)
        //            {
        //                min = distance;
        //                ladrIndex = ladr;
        //            }
        //        }
        //        calc[i] = new TaskToCalc(calc[i].CurrConnComp, calc[i].startNode, ladrIndex);
        //        calc[i + 1] = new TaskToCalc(calc[i + 1].CurrConnComp, ladrIndex, calc[i + 1].endNode);
        //    }
        //}

        //private Node FindMin(ref Dictionary<Node, A_Star_Point> openSet)
        //{
        //    Node minNode = openSet.Keys.First();
        //    int min_distance_plus_heuristic = openSet[minNode].currentDistance + openSet[minNode].heuristic;

        //    foreach (Node i in openSet.Keys)
        //    {
        //        if (openSet[i].currentDistance + openSet[i].heuristic < min_distance_plus_heuristic)
        //        {
        //            minNode = i;
        //            min_distance_plus_heuristic = openSet[i].currentDistance + openSet[i].heuristic;
        //        }
        //    }
        //    return minNode;
        //}

        //public List<Node> Calc()
        //{
        //    List<Node> result = new List<Node>();

        //    GetHeuristicToAllNodes();

        //    openSet.Add(startNode, new A_Star_Point(startNode, 0, heuristic[startNode]));

        //    while (openSet.Count > 0)
        //    {
        //        Node currentPoint = FindMin(ref openSet);

        //        closedSet.Add(currentPoint, openSet[currentPoint]);
        //        openSet.Remove(currentPoint);

        //        if (currentPoint.Equals(endNode))
        //            return PrintPath(currentPoint);


        //        foreach (var neighbourNode in GetNeighbours(currentPoint))
        //        {
        //            if (closedSet.ContainsKey(neighbourNode))
        //                continue;

        //            if (!openSet.ContainsKey(neighbourNode))//!isFound)
        //                openSet.Add(neighbourNode, new A_Star_Point(currentPoint, closedSet[currentPoint].currentDistance + GetDistanceBetweenTwoPoints(currentPoint, neighbourNode), heuristic[neighbourNode]));
        //            else
        //            {
        //                int distance = closedSet[currentPoint].currentDistance + GetDistanceBetweenTwoPoints(currentPoint, neighbourNode);
        //                if (openSet[neighbourNode].currentDistance > distance)
        //                    openSet[neighbourNode] = new A_Star_Point(currentPoint, distance, heuristic[neighbourNode]);
        //            }
        //        }
        //    }
        //    return null;
        //}

        //private List<Node> PrintPath(Node EndNode)
        //{
        //    var result = new List<Node>();

        //    Node currentPoint = EndNode;

        //    while (!currentPoint.Equals(closedSet[currentPoint].previousNode))
        //    {
        //        result.Add(currentPoint);
        //        currentPoint = closedSet[currentPoint].previousNode;
        //    }
        //    result.Add(currentPoint);
        //    result.Reverse();
        //    return result;
        //}

        //private List<Node> GetNeighbours(Node currentPoint)
        //{
        //    List<Node> neighbours = new List<Node>();

        //    foreach (Node i in map.GetFloor(curConComp.GetFloor()).GetEdge(currentPoint))
        //        neighbours.Add(i);

        //    return neighbours;
        //}

        //private void GetHeuristicToAllNodes()
        //{
        //    foreach (Node i in curConComp.GetAllNodesList())
        //        heuristic.Add(i, GetDistanceBetweenTwoPoints(i, endNode));
        //}
        //private int GetDistanceBetweenTwoPoints(Node startPoint, Node EndPoint) // also using to calc heuristic (distance between node -> goal)
        //{
        //    Point start = map.GetCoordOfNode(curConComp.GetFloor(), startPoint);
        //    Point end = map.GetCoordOfNode(curConComp.GetFloor(), EndPoint);
        //    return Convert.ToInt32(Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y)));
        //}
    }
}