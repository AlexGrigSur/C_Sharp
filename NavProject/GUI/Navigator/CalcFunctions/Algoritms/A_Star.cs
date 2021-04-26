using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;

namespace NavProject_Navigator.CalcFunctions.Algoritms
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
    public class A_Star : IRouteAlg
    {
        private Map map;
        private ConnectivityComponents curConComp;
        private Dictionary<Node, int> heuristic;
        private Node startNode;
        private Node endNode;

        private Dictionary<Node, A_Star_Point> openSet;
        private Dictionary<Node, A_Star_Point> closedSet;

        public List<Node> Calc(Map _map, ConnectivityComponents _currentComponent, Node _startNode, Node _endNode)
        {
            heuristic = new Dictionary<Node, int>();
            openSet = new Dictionary<Node, A_Star_Point>();
            closedSet = new Dictionary<Node, A_Star_Point>();

            map = _map;
            curConComp = _currentComponent;
            startNode = _startNode;
            endNode = _endNode;

            List<Node> result = new List<Node>();
            GetHeuristicToAllNodes();
            openSet.Add(startNode, new A_Star_Point(startNode, 0, heuristic[startNode]));
            while (openSet.Count > 0)
            {
                Node currentPoint = FindMin(ref openSet);

                closedSet.Add(currentPoint, openSet[currentPoint]);
                openSet.Remove(currentPoint);

                if (currentPoint.Equals(endNode))
                    return GetResult(currentPoint);

                foreach (var neighbourNode in GetNeighbours(currentPoint))
                {
                    if (closedSet.ContainsKey(neighbourNode))
                        continue;

                    if (!openSet.ContainsKey(neighbourNode))//!isFound)
                        openSet.Add(neighbourNode, new A_Star_Point(currentPoint, closedSet[currentPoint].currentDistance + GetDistanceBetweenTwoPoints(currentPoint, neighbourNode), heuristic[neighbourNode]));
                    else
                    {
                        int distance = closedSet[currentPoint].currentDistance + GetDistanceBetweenTwoPoints(currentPoint, neighbourNode);
                        if (openSet[neighbourNode].currentDistance > distance)
                            openSet[neighbourNode] = new A_Star_Point(currentPoint, distance, heuristic[neighbourNode]);
                    }
                }
            }
            return null;

        }
        private Node FindMin(ref Dictionary<Node, A_Star_Point> openSet)
        {
            Node minNode = openSet.Keys.First();
            int min_distance_plus_heuristic = openSet[minNode].currentDistance + openSet[minNode].heuristic;

            foreach (Node i in openSet.Keys)
            {
                if (openSet[i].currentDistance + openSet[i].heuristic < min_distance_plus_heuristic)
                {
                    minNode = i;
                    min_distance_plus_heuristic = openSet[i].currentDistance + openSet[i].heuristic;
                }
            }
            return minNode;
        }
        private List<Node> GetResult(Node EndNode)
        {
            var result = new List<Node>();

            Node currentPoint = EndNode;

            while (!currentPoint.Equals(closedSet[currentPoint].previousNode))
            {
                result.Add(currentPoint);
                currentPoint = closedSet[currentPoint].previousNode;
            }
            result.Add(currentPoint);
            result.Reverse();
            return result;
        }
        private List<Node> GetNeighbours(Node currentPoint) => map.GetFloor(curConComp.GetFloor()).GetEdge(currentPoint);
        private void GetHeuristicToAllNodes()
        {
            foreach (Node i in curConComp.GetAllNodes())
                heuristic.Add(i, GetHeuristic(i));
        }
        private int GetHeuristic(Node node) => GetDistanceBetweenTwoPoints(node, endNode);
        private int GetDistanceBetweenTwoPoints(Node startPoint, Node EndPoint) 
        {
            Point start = map.GetCoordOfNode(curConComp.GetFloor(), startPoint);
            Point end = map.GetCoordOfNode(curConComp.GetFloor(), EndPoint);
            return Convert.ToInt32(Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y)));
        }
    }
}
