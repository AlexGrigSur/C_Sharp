using System;
using System.Collections.Generic;
using System.Linq;
using NavTest;

namespace NavTest
{
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
        public void ClearHyperGraphByConnectivity() => HyperGraphByConnectivity.Clear();
        public ConnectivityComp FindConnectivityCompByNode(Node nd)
        {
            foreach (Level i in Floors.Values)
            {
                foreach (ConnectivityComp ConComp in i.GetConnectivityComponentsList())
                    if (ConComp.isContains(nd))
                        return ConComp;
            }
            return null;
        }
        public void ClearConnectivityComponentsOnLevel(int levelIndex) => Floors[levelIndex].ClearConnectivityComponents();
        public List<ConnectivityComp> GetConnectivities(Node node)
        {
            return HyperGraphByConnectivity[node];
        }

        public Dictionary<Node, List<ConnectivityComp>> GetHyperGraphByConnectivity()
        {
            return HyperGraphByConnectivity;
        }
        public void AddInExistingHyperGraphByConnectivity(Node nd, ConnectivityComp comp) => HyperGraphByConnectivity[nd].Add(comp);
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
