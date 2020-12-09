using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace NavTest
{
    class NavCalc
    {
        private Map map;
        private Node startNode;
        private Node endNode;

        public string timeToCalc { get; set; }
        public NavCalc(Map _map, Node _startNode, Node _endNode)
        {
            map = _map;
            startNode = _startNode;
            endNode = _endNode;
        }

        public Dictionary<ConnectivityComp, List<Node>> startCalc()
        {
            Stopwatch time = new Stopwatch();

            ConnectivityComp startConComp = map.FindConnectivityCompByNode(startNode);
            ConnectivityComp endConComp = map.FindConnectivityCompByNode(endNode);

            List<TaskToCalc> calc = new List<TaskToCalc>();

            timeToCalc = "";
            if (startConComp.Equals(endConComp))
            {
                timeToCalc += "Алгоритм Дейкстры выполнен за 0 миллисекунд(не требовался)\n";
                calc.Add(new TaskToCalc(startConComp, startNode, endNode));
            }
            else
            {
                List<ConnectivityComp> connectivityCompsList = new List<ConnectivityComp>();
                foreach (Level i in map.GetFloorsList().Values)
                    foreach (ConnectivityComp comp in i.GetConnectivityComponentsList())
                        connectivityCompsList.Add(comp);

                time.Start();
                calc = new Dijkstra(ref map, ref startConComp, ref endConComp).DijkstraAlgo();
                time.Stop();
                timeToCalc += $"Алгоритм Дейкстры выполнен за {time.ElapsedMilliseconds} миллисекунд\n";

                calc.Reverse();
                calc[0] = new TaskToCalc(calc[0].CurrConnComp, startNode, new Node());
                calc[calc.Count - 1] = new TaskToCalc(calc[calc.Count - 1].CurrConnComp, new Node(), endNode);

                getPriorityForAStar(ref calc);
            }

            Dictionary<ConnectivityComp, List<Node>> finalPath = new Dictionary<ConnectivityComp, List<Node>>();
            time = new Stopwatch();
            time.Start();
            foreach (TaskToCalc i in calc)
                finalPath.Add(i.CurrConnComp, new A_Star(ref map, i.CurrConnComp, i.startNode, i.endNode).Calc());
            time.Stop();
            timeToCalc += $"Алгоритм A* выполнен за {time.ElapsedMilliseconds} миллисекунд";

            return finalPath;
        }

        private void getPriorityForAStar(ref List<TaskToCalc> calc)
        {
            for (int i = 0; i < calc.Count - 1; ++i)
            {
                int min = int.MaxValue;
                Node ladrIndex = new Node();
                List<int> baseCoord = map.GetCoordOfNode(calc[i].CurrConnComp.GetFloor(), calc[i].startNode);
                List<int> ladderCoord;

                foreach (Node ladr in calc[i].CurrConnComp.GetLadderList())
                {
                    if (!map.GetConnectivities(ladr).Contains(calc[i + 1].CurrConnComp))
                        continue;
                    ladderCoord = map.GetCoordOfNode(calc[i].CurrConnComp.GetFloor(), ladr);
                    int distance = Convert.ToInt32(Math.Sqrt(Math.Pow(ladderCoord[0] - baseCoord[0], 2) + Math.Pow(ladderCoord[1] - baseCoord[1], 2)));
                    if (distance < min)
                    {
                        min = distance;
                        ladrIndex = ladr;
                    }
                }
                calc[i] = new TaskToCalc(calc[i].CurrConnComp, calc[i].startNode, ladrIndex);
                calc[i + 1] = new TaskToCalc(calc[i + 1].CurrConnComp, ladrIndex, calc[i + 1].endNode);
            }
        }
    }

    #region // Dijkstra
    public struct TaskToCalc
    {
        public ConnectivityComp CurrConnComp;
        public Node startNode;
        public Node endNode;
        public TaskToCalc(ConnectivityComp _ConnectivityComp, Node _startNode, Node _EndNode)
        {
            CurrConnComp = _ConnectivityComp;
            startNode = _startNode;
            endNode = _EndNode;
        }
    }
    public class Dijkstra
    {
        private Map map;
        private List<ConnectivityComp> connectivityCompsList;
        private ConnectivityComp conCompStart;
        private ConnectivityComp conCompEnd;
        public Dijkstra(ref Map _map, ref ConnectivityComp _connectivityCompStart, ref ConnectivityComp _connectivityCompEnd)
        {
            map = _map;
            connectivityCompsList = new List<ConnectivityComp>();

            foreach (Level i in map.GetFloorsList().Values)
                foreach (ConnectivityComp conn in i.GetConnectivityComponentsList())
                    connectivityCompsList.Add(conn);

            conCompStart = _connectivityCompStart;
            conCompEnd = _connectivityCompEnd;
        }
        private List<TaskToCalc> FormResult(ref Dictionary<ConnectivityComp, ConnectivityComp> previousConComp)
        {
            ConnectivityComp currentNode = conCompEnd;
            List<TaskToCalc> result = new List<TaskToCalc>();
            while (!currentNode.Equals(conCompStart))
            {
                result.Add(new TaskToCalc(currentNode, new Node(), new Node()));
                currentNode = previousConComp[currentNode];
            }
            result.Add(new TaskToCalc(conCompStart, new Node(), new Node()));
            return result;
        }
        private ConnectivityComp MinimumDistance(ref Dictionary<ConnectivityComp, int> distance, ref Dictionary<ConnectivityComp, bool> isFixedConComp)
        {
            int min = int.MaxValue;
            ConnectivityComp minIndex = null;

            foreach (ConnectivityComp conn in distance.Keys)
            {
                if (!isFixedConComp[conn] && distance[conn] <= min)
                {
                    if (minIndex != null && distance[minIndex] == distance[conn] && minIndex.Equals(conCompEnd))
                        continue;

                    min = distance[conn];
                    minIndex = conn;
                }
            }
            return minIndex;
        }
        public List<TaskToCalc> DijkstraAlgo()
        {
            Dictionary<ConnectivityComp, int> distance = new Dictionary<ConnectivityComp, int>();
            Dictionary<ConnectivityComp, bool> isFixedConComp = new Dictionary<ConnectivityComp, bool>();
            Dictionary<ConnectivityComp, ConnectivityComp> previousConComp = new Dictionary<ConnectivityComp, ConnectivityComp>();

            foreach (ConnectivityComp i in connectivityCompsList)
            {
                distance.Add(i, int.MaxValue);
                isFixedConComp.Add(i, false);
                previousConComp.Add(i, null);
            }

            distance[conCompStart] = 0;
            isFixedConComp[conCompStart] = true;
            ConnectivityComp u = conCompStart;
            while (!u.Equals(conCompEnd))
            {
                foreach (Node nd in u.GetLadderList())
                    foreach (ConnectivityComp j in map.GetConnectivities(nd))
                        if (!isFixedConComp[j] && distance[u] != int.MaxValue && distance[u] + 1 < distance[j])
                        {
                            distance[j] = distance[u] + 1;
                            previousConComp[j] = u;
                        }

                u = MinimumDistance(ref distance, ref isFixedConComp);
                isFixedConComp[u] = true;
            }

            return FormResult(ref previousConComp);
        }
    }
    #endregion

    #region //A_Star
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
    public class A_Star
    {
        private Map map;
        private ConnectivityComp curConComp;
        private Dictionary<Node, int> heuristic = new Dictionary<Node, int>();

        Dictionary<Node, A_Star_Point> openSet = new Dictionary<Node, A_Star_Point>();
        Dictionary<Node, A_Star_Point> closedSet = new Dictionary<Node, A_Star_Point>();

        private Node startNode;
        private Node endNode;

        public A_Star(ref Map _map, ConnectivityComp _currentConComp, Node _startNode, Node _endNode)
        {
            map = _map;
            curConComp = _currentConComp;
            startNode = _startNode;
            endNode = _endNode;
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

        public List<Node> Calc()
        {
            List<Node> result = new List<Node>();

            GetHeuristicToAllNodes();

            openSet.Add(startNode, new A_Star_Point(startNode, 0, heuristic[startNode]));

            while (openSet.Count > 0)
            {
                Node currentPoint = FindMin(ref openSet);

                closedSet.Add(currentPoint, openSet[currentPoint]);
                openSet.Remove(currentPoint);

                if (currentPoint.Equals(endNode))
                    return PrintPath(currentPoint);


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

        private List<Node> PrintPath(Node EndNode)
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

        private List<Node> GetNeighbours(Node currentPoint)
        {
            List<Node> neighbours = new List<Node>();

            foreach (Node i in map.GetFloor(curConComp.GetFloor()).GetEdge(currentPoint))
                neighbours.Add(i);

            return neighbours;
        }

        private void GetHeuristicToAllNodes()
        {
            foreach (Node i in curConComp.GetAllNodesList())
                heuristic.Add(i, GetDistanceBetweenTwoPoints(i, endNode));
        }
        private int GetDistanceBetweenTwoPoints(Node startPoint, Node EndPoint) // also using to calc heuristic (distance between node -> goal)
        {
            List<int> start = map.GetCoordOfNode(curConComp.GetFloor(), startPoint);
            List<int> end = map.GetCoordOfNode(curConComp.GetFloor(), EndPoint);
            return Convert.ToInt32(Math.Sqrt(Math.Pow(start[0] - end[0], 2) + Math.Pow(start[1] - end[1], 2)));
        }
    }
    #endregion
}
