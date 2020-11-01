﻿using System;
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
    class ConnectivityComp // will always reCalculated in case of changings
    {
        private int Floor { get; set; }
        private List<Node> allNodes = new List<Node>();
        private List<Node> ladders = new List<Node>();
        public bool isContains(Node obj)
        {
            return allNodes.Contains(obj);
        }
        public void add(Node obj)
        {
            allNodes.Add(obj);
            if (obj.type >= 3) ladders.Add(obj);
        }
    }
    class Level
    {
        public string Name;
        public int floor { get; set; }
        public int screenResX { get; set; }
        public int screenResY { get; set; }
        public List<ConnectivityComp> connectivityComponents = new List<ConnectivityComp>();
        public Dictionary<Node, List<int>> nodeListOnFloor = new Dictionary<Node, List<int>>();
        public Dictionary<Node, List<Node>> edges = new Dictionary<Node, List<Node>>();
        public Level(string _Name, int Floor)
        {
            Name = _Name;
            floor = Floor;
        }

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
                    i[0] -= minX + 10;
                    i[1] -= minY + 10;
                }
            if ((maxX + 10 != screenResX) || (maxY + 10 != screenResY))
            {
                screenResX = maxX - minX + 10;
                screenResY = maxY - minY + 10;
            }
        }

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
        #region // Add/Remove Node
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
        #region // add/Remove edge
        public bool EdgeExists(List<Node> obj) => edges[obj[0]].Contains(obj[1]);
        public void AddEdge(List<Node> obj)
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

    class Map
    {
        public string name { get; set; }
        public Dictionary<string, Level> Floors = new Dictionary<string, Level>(); // список этажей
        public Dictionary<string, Node> NodeList = new Dictionary<string, Node>(); // Хранит список вершин
        public Dictionary<Node, List<ConnectivityComp>> HyperGraphByConnectivity = new Dictionary<Node, List<ConnectivityComp>>();
        
        #region // поиск вершин
        public List<Node> SearchNode(string floorName, int x, int y, string Name = "")
        {
            List<Node> result = new List<Node>();
            if (Name != "")
            {
                result.Add(NodeList[Name]);
                return result;
            }
            else
                return Floors[floorName].SearchNode(x, y);
        }
        public List<Node> SearchNode(string floorName, int x1, int y1, int x2, int y2, string Name1 = "", string Name2 = " ")
        {
            List<Node> result = new List<Node>();
            if (Name1 != "")
            {
                result.Add(NodeList[Name1]);
                result.Add(NodeList[Name2]);
                return result;
            }
            else
                return Floors[floorName].SearchNode(x1, y1, x2, y2);
        }
        #endregion
        
        #region // add/edit/delete floor
        public void AddFloor(string name, int floor) => Floors.Add(name, new Level(name, floor));
        public void EditFloor(string oldName, Level floor)
        {
            Floors.Add(floor.Name, floor);
            Floors.Remove(oldName);
        }
        public void DeleteFloor(string name) => Floors.Remove(name);
        #endregion

        public void AddNode(string floorName, Node obj, int x, int y) 
        {
            if (!NodeList.ContainsKey(obj.name)) NodeList.Add(obj.name, obj);
            if (obj.type >= 3) AddHyperGraphByConn(obj);
            Floors[floorName].AddNode(obj, x, y);
        }
        public void EditNode(string floorName, string oldName, Node obj, int x = -1, int y = -1)
        {
            if (NodeList[oldName].GetHashCode() != obj.GetHashCode())
            {
                if (NodeList[oldName].type != obj.type)
                {
                    if (NodeList[oldName].type < 3 && obj.type >= 3) // add ladder
                    {

                    }
                    if (NodeList[oldName].type >= 3 && obj.type < 3) // delete from ladder
                    {

                    }
                }
                NodeList.Remove(oldName);
                NodeList.Add(obj.name, obj);
            }
            if (x != -1) Floors[floorName].NodeCoordChange(obj, x, y);
        }
        public void RemoveNode(string floorName, string nodeName)
        {
            if (NodeList[nodeName].type >= 3)
            {
                Floors[floorName].RemoveNode(NodeList[nodeName]);
                RemoveHyperGraphByConn(NodeList[nodeName]);
            }
            else
            {
                Floors[floorName].RemoveNode(NodeList[nodeName]);
                NodeList.Remove(nodeName);
            }
        }

        public bool isEdgeExists(string floorName, List<Node> nodes) => Floors[floorName].EdgeExists(nodes);
        public void AddEdge(string floorName, List<Node> nodes) => Floors[floorName].AddEdge(nodes);
        public void RemoveEdge(string floorName, List<Node> nodes) => Floors[floorName].RemoveEdge(nodes);

        public void AddHyperGraphByConn(Node obj)
        {
            if (!HyperGraphByConnectivity.ContainsKey(obj)) HyperGraphByConnectivity.Add(obj, new List<ConnectivityComp>());
        }
        public void RemoveHyperGraphByConn(Node obj)
        {
            foreach (Level i in Floors.Values)
                if (i.nodeListOnFloor.ContainsKey(obj))
                    return;
            HyperGraphByConnectivity.Remove(obj);
        }
    }
}