using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TypeChange

namespace NavTest
{
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

    public class ConnectivityComp // will always reCalculated in case of changings
    {

        private int floor;
        private List<Node> allNodes = new List<Node>();
        private List<Node> ladders = new List<Node>();

        public ConnectivityComp(int Floor)
        {
            floor = Floor;
        }
        public int GetFloor()
        {
            return floor;
        }
        public List<Node> GetAllNodesList()
        {
            return allNodes;
        }
        public List<Node> GetLadderList()
        {
            return ladders;
        }
        public bool isContains(Node obj)
        {
            return allNodes.Contains(obj);
        }
        public void add(Node obj)
        {
            allNodes.Add(obj);
            if (obj.type == 2) ladders.Add(obj);
        }
    }
    public class Level
    {
        private int floorIndex;
        private int screenResX;
        private int screenResY;
        private List<ConnectivityComp> connectivityComponents = new List<ConnectivityComp>();
        private Dictionary<Node, List<int>> nodeListOnFloor = new Dictionary<Node, List<int>>();
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
            return connectivityComponents;
        }
        public void AddConnectivityComponents(int floorIndex) => connectivityComponents.Add(new ConnectivityComp(floorIndex));
        public void ClearConnectivityComponents() => connectivityComponents.Clear();
        #endregion        
        public void NodesOptimizer()
        {
            int maxX = -1, maxY = -1, minX = Int32.MaxValue, minY = Int32.MaxValue;
            foreach (List<int> i in nodeListOnFloor.Values)
            {
                if (i[0] > maxX) maxX = i[0];
                if (i[1] > maxY) maxY = i[1];
                if (i[0] < minX) minX = i[0];
                if (i[1] < minY) minY = i[1];
            }
            if (minX != 10 || minY != 10)
                foreach (List<int> i in nodeListOnFloor.Values)
                {
                    i[0] -= minX - 15;
                    i[1] -= minY - 15;
                }
            if ((maxX + 10 != screenResX) || (maxY + 10 != screenResY))
            {
                screenResX = maxX - minX - 15;
                screenResY = maxY - minY - 15;
            }
            if (screenResX < 800) screenResX = 800;
            if (screenResY < 600) screenResY = 600;
        }

        #region // NodeList
        public Dictionary<Node, List<int>> GetNodeListOnFloor()
        {
            return nodeListOnFloor;
        }
        public List<int> GetNodeOnFloor(Node nd)
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
                if (nodeListOnFloor[tempNode][0] == x && nodeListOnFloor[tempNode][1] == y)
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
                if ((nodeListOnFloor[tempNode][0] == x1 && nodeListOnFloor[tempNode][1] == y1) || (nodeListOnFloor[tempNode][0] == x2 && nodeListOnFloor[tempNode][1] == y2))
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
            nodeListOnFloor.Add(obj, new List<int> { x, y });
            edges.Add(obj, new List<Node>());
        }
        public void NodeCoordChange(Node obj, int newX, int newY)
        {
            nodeListOnFloor[obj] = new List<int> { newX, newY };
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
            edges[obj[0]].Add(obj[1]);
            edges[obj[1]].Add(obj[0]);
        }
        public void RemoveEdge(List<Node> obj)
        {
            edges[obj[0]].Remove(obj[1]);
            edges[obj[1]].Remove(obj[0]);
        }
        #endregion
    }

    public class Map
    {
        private string name;
        private Dictionary<int, Level> Floors = new Dictionary<int, Level>(); // список этажей
        private Dictionary<string, Node> NodeList = new Dictionary<string, Node>(); // Хранит список вершин
        private Dictionary<Node, List<ConnectivityComp>> HyperGraphByConnectivity = new Dictionary<Node, List<ConnectivityComp>>();
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public Dictionary<int, Level> GetFloorsList()
        {
            return Floors;
        }
        public Level GetFloor(int floorIndex)
        {
            return Floors[floorIndex];
        }
        public Level GetLevel(int floorIndex)
        {
            return Floors[floorIndex];
        }
        public void RemoveFromFloors(int floorIndex) => Floors.Remove(floorIndex);
        #region // поиск вершин
        public List<Node> SearchNode(int floor, int x, int y, string Name = "")
        {
            List<Node> result = new List<Node>();
            if (Name != "")
            {
                result.Add(NodeList[Name]);
                return result;
            }
            else
                return Floors[floor].SearchNode(x, y);
        }
        public List<Node> SearchNode(int floor, int x1, int y1, int x2, int y2, string Name1 = "", string Name2 = " ")
        {
            List<Node> result = new List<Node>();
            if (Name1 != "")
            {
                result.Add(NodeList[Name1]);
                result.Add(NodeList[Name2]);
                return result;
            }
            else
                return Floors[floor].SearchNode(x1, y1, x2, y2);
        }
        #endregion 
        #region // add/edit/delete floor
        public void AddFloor(int floor) => Floors.Add(floor, new Level(floor));

        public Node GetNode(string nodeName)
        {
            return NodeList[nodeName];
        }
        public void EditFloor(int oldFloorIndex, Level floor)
        {
            Floors.Add(floor.FloorIndex, floor);
            Floors.Remove(oldFloorIndex);
        }
        public void DeleteFloor(int floorIndex) => Floors.Remove(floorIndex);
        #endregion
        #region //Nodes
        public void AddInNodeList(Node nd) => NodeList.Add(nd.name, nd);
        public Dictionary<string, Node> GetNodeList()
        {
            return NodeList;
        }
        public void AddNode(int floorIndex, Node obj, int x, int y)
        {
            if (!NodeList.ContainsKey(obj.name)) NodeList.Add(obj.name, obj);
            if (obj.type == 2 /*&& !HyperGraphByConnectivity.ContainsKey(obj)*/) AddHyperGraphByConn(obj);
            Floors[floorIndex].AddNode(obj, x, y);
        }
        public void EditNode(int floorIndex, string oldName, Node newNode, int x = -1, int y = -1)
        {
            if (!NodeList[oldName].Equals(newNode))//.GetHashCode() != obj.GetHashCode())
            {
                if (newNode.type == 2) // если вершина - лестница
                {
                    foreach (int i in Floors.Keys) // проход по всем этажам
                    {
                        if (Floors[i].GetNodeListOnFloor().ContainsKey(NodeList[oldName])) // Если вершина размещена на этаже
                        {
                            foreach (Node tempNode in Floors[i].GetEdge(NodeList[oldName])) //Пройтись по всем, связанным со старой вершиной, вершинам
                            {
                                Floors[i].GetEdge(tempNode).Remove(NodeList[oldName]);
                                Floors[i].GetEdge(tempNode).Add(newNode);
                            }
                            Floors[i].AddEdge(newNode, Floors[i].GetEdge(NodeList[oldName]));
                            Floors[i].GetEdgesList().Remove(NodeList[oldName]);

                            Floors[i].GetNodeListOnFloor().Add(newNode, Floors[i].GetNodeOnFloor(NodeList[oldName]));
                            Floors[i].GetNodeListOnFloor().Remove(NodeList[oldName]);
                        }
                    }
                    EditHyperGraphByConn(oldName, newNode);
                }
                else
                {
                    foreach (Node tempNode in Floors[floorIndex].GetEdge(NodeList[oldName])) //Пройтись по всем, связанным со старой вершиной, вершинам
                    {
                        Floors[floorIndex].GetEdge(tempNode).Remove(NodeList[oldName]);
                        Floors[floorIndex].GetEdge(tempNode).Add(newNode);
                    }
                    Floors[floorIndex].GetEdgesList().Add(newNode, Floors[floorIndex].GetEdge(NodeList[oldName]));
                    Floors[floorIndex].GetEdgesList().Remove(NodeList[oldName]);

                    Floors[floorIndex].GetNodeListOnFloor().Add(newNode, Floors[floorIndex].GetNodeOnFloor(NodeList[oldName]));
                    Floors[floorIndex].GetNodeListOnFloor().Remove(NodeList[oldName]);

                }
                NodeList.Remove(oldName);
                NodeList.Add(newNode.name, newNode);
            }
            if (x != -1) Floors[floorIndex].NodeCoordChange(newNode, x, y);
        }
        public void RemoveNode(int floorIndex, string nodeName)
        {
            if (NodeList[nodeName].type == 2)
            {
                Floors[floorIndex].RemoveNode(NodeList[nodeName]);
                RemoveHyperGraphByConn(NodeList[nodeName]);
            }
            else
            {
                Floors[floorIndex].RemoveNode(NodeList[nodeName]);
                NodeList.Remove(nodeName);
            }
        }
        public void RemoveNode(int floorIndex, Node node)
        {
            if (node.type == 2)
            {
                Floors[floorIndex].RemoveNode(node);
                RemoveHyperGraphByConn(node);
            }
            else
            {
                Floors[floorIndex].RemoveNode(node);
                NodeList.Remove(node.name);
            }
        }
        public List<int> GetCoordOfNode(int floorIndex, Node obj)
        {
            return Floors[floorIndex].GetNodeOnFloor(obj);
        }
        #endregion
        #region // Edges
        public bool isEdgeExists(int floorIndex, Node n1, Node n2) => Floors[floorIndex].EdgeExists(n1, n2);
        public bool isEdgeExists(int floorIndex, List<Node> nodes) => Floors[floorIndex].EdgeExists(nodes);
        public void AddEdge(int floorIndex, List<Node> nodes) => Floors[floorIndex].AddExistingEdge(nodes);
        public void RemoveEdge(int floorIndex, List<Node> nodes) => Floors[floorIndex].RemoveEdge(nodes);
        #endregion
        #region // HyperGraph
        public ConnectivityComp FindConnectivityCompByNode(Node nd)
        {
            foreach(Level i in Floors.Values)
            {
                foreach (ConnectivityComp ConComp in i.GetConnectivityComponentsList())
                    if (ConComp.isContains(nd))
                        return ConComp;
            }
            return null;
        }
        public void ClearConnectivityComponentsOnLevel(int levelIndex) => Floors[levelIndex].ClearConnectivityComponents();
        public Dictionary<Node, List<ConnectivityComp>> GetHyperGraphByConnectivity()
        {
            return HyperGraphByConnectivity;
        }
        public void AddInExistingHyperGraphByConnectivity(Node nd, ConnectivityComp comp)
        {
            HyperGraphByConnectivity[nd].Add(comp);
        }
        public void AddHyperGraphByConn(Node obj)
        {
            if (!HyperGraphByConnectivity.ContainsKey(obj)) HyperGraphByConnectivity.Add(obj, new List<ConnectivityComp>());
        }
        public void EditHyperGraphByConn(string oldName, Node obj)
        {
            Node FoundNode = HyperGraphByConnectivity.Keys.First();
            foreach (Node i in HyperGraphByConnectivity.Keys)
            {
                if (i.name == oldName)
                {
                    FoundNode = i;
                    break;
                }
            }
            HyperGraphByConnectivity.Add(obj, HyperGraphByConnectivity[FoundNode]);
            HyperGraphByConnectivity.Remove(FoundNode);

        }
        public void RemoveHyperGraphByConn(Node obj)
        {
            foreach (Level i in Floors.Values)
                if (i.GetNodeListOnFloor().ContainsKey(obj))
                    return;
            HyperGraphByConnectivity.Remove(obj);
        }
        #endregion
        public void NodesOptimizer()
        {
            foreach (int floorIndex in Floors.Keys)
                Floors[floorIndex].NodesOptimizer();
        }
    }
}