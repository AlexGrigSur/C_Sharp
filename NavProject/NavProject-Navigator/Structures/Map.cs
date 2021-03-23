using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Navigator.Structures
{
    [Serializable]
    public class Map
    {
        private string name;
        private int corridorCounter;
        private Dictionary<int, Level> Floors = new Dictionary<int, Level>(); // список этажей
        private Dictionary<string, Node> NodeList = new Dictionary<string, Node>(); // Хранит список вершин
        HyperGraphByConnectivity hyperGraph = new HyperGraphByConnectivity();
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public int corridorCouner => corridorCounter;
        public HyperGraphByConnectivity HyperGraphInstance => hyperGraph;

        public Dictionary<int, Level> GetFloorsList() => Floors;
        public Level GetFloor(int floorIndex) => Floors[floorIndex];
        public Level GetLevel(int floorIndex) => Floors[floorIndex];
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
        #endregion 
        #region // add/edit/delete floor
        public void AddFloor(int floor) => Floors.Add(floor, new Level(floor));

        public Node GetNode(string nodeName) => NodeList[nodeName];
        #endregion
        #region //Nodes
        public Dictionary<string, Node> GetNodeList() => NodeList;
        public void AddNode(int floorIndex, Node obj, int x, int y)
        {
            if (!NodeList.ContainsKey(obj.name)) NodeList.Add(obj.name, obj);
            if (obj.type == 2) AddHyperGraphByConn(obj);
            Floors[floorIndex].AddNode(obj, x, y);
        }
        public void EditNode(int floorIndex, string oldName, string NewName, string NewDescription, int x = -1, int y = -1)
        {
            Node nd = NodeList[oldName];
            if (nd.name != NewName || nd.description != NewDescription)
            {
                if (nd.description != NewDescription) nd.description = NewDescription;
                if (nd.name != NewName)
                {
                    NodeList.Add(NewName, nd);
                    NodeList.Remove(nd.name);
                    nd.name = NewName;
                }
            }
            if (x != -1) Floors[floorIndex].NodeCoordChange(nd, x, y);
        }
        public ConnectivityComponents FindConnectivityCompByNode(Node nd)
        {
            foreach (Level i in Floors.Values)
            {
                foreach (ConnectivityComponents ConComp in i.GetConnectivityComponentsList())
                    if (ConComp.IsContains(nd))
                        return ConComp;
            }
            return null;
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
        public Point GetCoordOfNode(int floorIndex, Node obj)
        {
            return Floors[floorIndex].GetNodeOnFloor(obj);
        }
        public void ClearAllNodes()
        {
            NodeList.Clear();
            hyperGraph.Clear();
            foreach (Level i in Floors.Values)
            {
                i.ClearNodeListOnFloor();
                i.ClearEdges();
            }
        }
        #endregion
        #region // Edges
        public bool isEdgeExists(int floorIndex, Node n1, Node n2) => Floors[floorIndex].EdgeExists(n1, n2);
        public void AddEdge(int floorIndex, List<Node> nodes) => Floors[floorIndex].AddExistingEdge(nodes);
        public void RemoveEdge(int floorIndex, List<Node> nodes) => Floors[floorIndex].RemoveEdge(nodes);
        #endregion
        #region // HyperGraph
        public void ClearHyperGraphByConnectivity() => hyperGraph.Clear();
        public void ClearConnectivityComponentsOnLevel(int levelIndex) => Floors[levelIndex].ClearConnectivityComponents();
        public List<ConnectivityComponents> GetConnectivities(Node node) => hyperGraph.GetConnectivities(node);
        public void AddInExistingHyperGraphByConnectivity(Node nd, ConnectivityComponents comp) => hyperGraph.AddInExistingHyperGraphByConnectivity(nd, comp);
        public void AddHyperGraphByConn(Node obj)
        {
            if (!hyperGraph.ContainsKey(obj)) hyperGraph.Add(obj, new List<ConnectivityComponents>());
        }
        public void RemoveHyperGraphByConn(Node obj)
        {
            foreach (Level i in Floors.Values)
                if (i.GetNodeListOnFloor().ContainsKey(obj))
                    return;
            hyperGraph.Remove(obj);
        }
        #endregion
        public void NodesOptimizer()
        {
            foreach (int floorIndex in Floors.Keys)
                Floors[floorIndex].NodesOptimizer();
        }
    }
}
