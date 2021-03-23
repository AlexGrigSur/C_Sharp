using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Drawing.Structures
{
    [Serializable]
    public class Level
    {
        private int floorIndex;
        private int screenResX;
        private int screenResY;
        private List<ConnectivityComponents> connectivityComponentsList = new List<ConnectivityComponents>();
        private Dictionary<Node, Point> nodeListOnFloor = new Dictionary<Node, Point>();
        private Dictionary<Node, List<Node>> edges = new Dictionary<Node, List<Node>>();
        public Level(int FloorIndex) => floorIndex = FloorIndex;
        public int ScreenResX
        {
            get { return screenResX; }
            set { screenResX = value; }
        }
        public int ScreenResY
        {
            get { return screenResY; }
            set { screenResY = value; }
        }
        public int FloorIndex => floorIndex;

        #region // ConnectivityComponents
        public List<ConnectivityComponents> GetConnectivityComponentsList() => connectivityComponentsList;
        public void AddConnectivityComponents(int floorIndex) => connectivityComponentsList.Add(new ConnectivityComponents(floorIndex));
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
        public Dictionary<Node, Point> GetNodeListOnFloor() => nodeListOnFloor;
        public Point GetNodeOnFloor(Node nd) => nodeListOnFloor[nd];
        public void ClearNodeListOnFloor() => nodeListOnFloor.Clear();
        #endregion
        #region // EdgesList
        public Dictionary<Node, List<Node>> GetEdgesList() => edges;
        public List<Node> GetEdge(Node nd) => edges[nd];
        public void ClearEdges() => edges.Clear();

        #endregion
        #region // Node Search
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
                if ((nodeListOnFloor[tempNode].X == x1 && nodeListOnFloor[tempNode].Y == y1) || (nodeListOnFloor[tempNode].X == x2 && nodeListOnFloor[tempNode].Y == y2))
                {
                    result.Add(tempNode);
                    if (result.Count == 2)
                        break;
                }
            return result;
        } ///
        #endregion
        #region // Nodes
        public bool IsNodeContains(Node obj) => nodeListOnFloor.ContainsKey(obj);
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
        public bool EdgeExists(Node n1, Node n2) => edges[n1].Contains(n2);
        public void AddExistingEdge(List<Node> obj)
        {
            if (!EdgeExists(obj[0], obj[1])) edges[obj[0]].Add(obj[1]);
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
