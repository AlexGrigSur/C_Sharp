using System;
using System.Drawing;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using NavTest;

namespace NavTest
{
    [Serializable]
    public struct Node
    {
        public string name;
        public int type; // 0-коридор, 1-кабинет, 2-лестница, 3-выход
        public string description;
        public Node(string Name, int Type, string Description)
        {
            name = Name;
            type = Type;
            description = Description;
        }
    }

    [Serializable]
    public class Level
    {
        private int floorIndex;
        private int screenResX;
        private int screenResY;
        private List<ConnectivityComp> connectivityComponentsList = new List<ConnectivityComp>();
        private Dictionary<Node, Point> nodeListOnFloor = new Dictionary<Node, Point>();
        private Dictionary<Node, List<Node>> edges = new Dictionary<Node, List<Node>>();
        public Level(int FloorIndex)
        {
            floorIndex = FloorIndex;
        }
        public int ScreenResX
        {
            set { screenResX = value; }
            get { return screenResX; }
        }
        public int ScreenResY
        {
            set { screenResY = value; }
            get { return screenResY; }
        }
        public int FloorIndex
        {
            get { return floorIndex; }
        }

        #region // ConnectivityComponents
        public List<ConnectivityComp> GetConnectivityComponentsList()
        {
            return connectivityComponentsList;
        }
        public void AddConnectivityComponents(int floorIndex) => connectivityComponentsList.Add(new ConnectivityComp(floorIndex));
        public void ClearConnectivityComponents() => connectivityComponentsList.Clear();
        #endregion        
        public void NodesOptimizer()
        {
            int maxX = -1, maxY = -1, minX = Int32.MaxValue, minY = Int32.MaxValue;
            foreach (Point i in nodeListOnFloor.Values)
            {
                if (i.X > maxX) maxX = i.X;
                if (i.Y > maxY) maxY = i.Y;
                if (i.X < minX) minX = i.X;
                if (i.Y < minY) minY = i.Y;
            }
            if (minX != 10 || minY != 10)
            {
                List<Node> nodeListOnFloorCopy = new List<Node>(nodeListOnFloor.Keys);
                foreach (Node i in nodeListOnFloorCopy)
                    nodeListOnFloor[i] = new Point(nodeListOnFloor[i].X - minX + 15, nodeListOnFloor[i].Y - minY + 15);
            }
            if ((maxX + 10 != screenResX) || (maxY + 10 != screenResY))
            {
                screenResX = maxX - minX + 100;
                screenResY = maxY - minY + 100;
            }
            if (screenResX < 900) screenResX = 900;
            if (screenResY < 700) screenResY = 700;
        }

        #region // NodeList
        public Dictionary<Node, Point> GetNodeListOnFloor()
        {
            return nodeListOnFloor;
        }
        public Point GetNodeOnFloor(Node nd)
        {
            return nodeListOnFloor[nd];
        }
        public void ClearNodeListOnFloor() => nodeListOnFloor.Clear();
        #endregion
        #region // EdgesList
        public Dictionary<Node, List<Node>> GetEdgesList()
        {
            return edges;
        }
        public List<Node> GetEdge(Node nd)
        {
            return edges[nd];
        }
        public void ClearEdges() => edges.Clear();
        #endregion
        #region // поиск вершин
        public List<Node> SearchNode(int x, int y)
        {
            List<Node> result = new List<Node>();
            foreach (Node tempNode in nodeListOnFloor.Keys)
                if (nodeListOnFloor[tempNode].X == x && nodeListOnFloor[tempNode].Y == y)
                {
                    result.Add(tempNode);
                    break;
                }
            return result;
        }
        public List<Node> SearchNode(int x1, int y1, int x2, int y2)
        {
            List<Node> result = new List<Node>();
            foreach (Node tempNode in nodeListOnFloor.Keys)
                if ((nodeListOnFloor[tempNode].X  == x1 && nodeListOnFloor[tempNode].Y == y1) || (nodeListOnFloor[tempNode].X == x2 && nodeListOnFloor[tempNode].Y == y2))
                {
                    result.Add(tempNode);
                    if (result.Count == 2)
                        break;
                }
            return result;
        }
        #endregion
        #region // Nodes
        public void AddNode(Node obj, int x, int y)
        {
            nodeListOnFloor.Add(obj, new Point(x, y));
            edges.Add(obj, new List<Node>());
        }
        public void NodeCoordChange(Node obj, int newX, int newY)
        {
            nodeListOnFloor[obj] = new Point(newX, newY);
        }
        public void RemoveNode(Node obj)
        {
            foreach (var i in edges.Values)
                if (i.Contains(obj)) i.Remove(obj);
            edges[obj].Clear();
            edges.Remove(obj);
            nodeListOnFloor.Remove(obj);
        }
        #endregion

        #region // Edges
        public bool EdgeExists(List<Node> obj) => edges[obj[0]].Contains(obj[1]);
        public bool EdgeExists(Node n1, Node n2) => edges[n1].Contains(n2);
        public void AddEdge(Node nd) => edges.Add(nd, new List<Node>());
        public void AddEdge(Node nd, List<Node> ndList) => edges.Add(nd, ndList);
        public void AddExistingEdge(List<Node> obj)
        {
            if(!EdgeExists(obj[0],obj[1])) edges[obj[0]].Add(obj[1]);
            if (!EdgeExists(obj[1], obj[0])) edges[obj[1]].Add(obj[0]);
        }
        public void AddSingleEdge(Node baseNode, Node connNode)
        {
            if (!EdgeExists(baseNode, connNode))
                edges[baseNode].Add(connNode);
        }
        public void RemoveEdge(List<Node> obj)
        {
            edges[obj[0]].Remove(obj[1]);
            edges[obj[1]].Remove(obj[0]);
        }
        #endregion
    }
}