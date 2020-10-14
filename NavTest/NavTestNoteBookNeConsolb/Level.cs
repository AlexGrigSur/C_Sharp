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
    class Level
    {
        public string Name;
        public int floor { get; set; }
        public int screenResX { get; set; }
        public int screenResY { get; set; }
        public Dictionary<Node, List<int>> nodeListOnFloor = new Dictionary<Node, List<int>>();
        public Dictionary<Node, List<Node>> edges = new Dictionary<Node, List<Node>>();
        public Level(string _Name, int Floor)
        {
            Name = _Name;
            floor = Floor;
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
        public void EditNode(Node obj, int newX, int newY)
        {
            nodeListOnFloor[obj] = new List<int> { newX, newY };
        }
        public bool RemoveApprove(Node obj)
        {
            foreach (var i in nodeListOnFloor.Keys)
                if (i.GetHashCode() != obj.GetHashCode() && (i.type == 2 || i.type == 3))
                    return true;
            return false;
        }
        public bool RemoveNode(Node obj)
        {
            if(obj.type>=3)
                if (!RemoveApprove(obj))
                {
                    MessageBox.Show("Запрет на удаление единственного выхода/лестницы");
                    return false;
                }
            foreach (var i in edges.Values)
                if(i.Contains(obj)) i.Remove(obj);
            edges[obj].Clear();
            edges.Remove(obj);
            return true;
        }
        #endregion
        #region // add/Remove edge
        public void AddEdge(List<Node> obj)
        {
            edges[obj[0]].Add(obj[1]);
            edges[obj[1]].Add(obj[0]);
        }
        public void RemoveEdge(List<Node> obj)
        {
            // remove Edge Check
            edges[obj[0]].Remove(obj[1]);
            edges[obj[1]].Remove(obj[0]);
        }
        #endregion
    }


    class Map
    {
        public string name { get; set; }
        public Dictionary<string, Level> Floor = new Dictionary<string, Level>(); // список этажей
        public Dictionary<string, Node> NodeList = new Dictionary<string, Node>(); // Хранит список вершин
        public Dictionary<Node, List<Level>> hyperGraphEdge = new Dictionary<Node, List<Level>>(); // Рёберный граф, который хранит связь между этажами и списком лестниц/проходов

        public List<Node> SearchNode(string floorName, int x, int y, string Name = "")
        {
            List<Node> result = new List<Node>();
            if (Name != "")
            {
                result.Add(NodeList[Name]);
                return result;
            }
            else
                return Floor[floorName].SearchNode(x, y);
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
                return Floor[floorName].SearchNode(x1, y1, x2, y2);
        }


        public void AddFloor(string name, int floor) => Floor.Add(name, new Level(name, floor));
        public void EditFloor(string oldName, Level floor)
        {
            Floor.Add(floor.Name,floor);
            Floor.Remove(oldName);
        }
        public void DeleteFloor(string name) => Floor.Remove(name);

        public void AddNodeList(Node obj, Level floor=null)
        {
            if(!NodeList.ContainsKey(obj.name)) NodeList.Add(obj.name, obj);
            if (obj.type >= 3 && floor!=null) AddHyperGraphLadder(obj, floor);
        }
        public void EditNode(string oldName, Node obj)
        {
            NodeList.Remove(oldName);
            NodeList.Add(obj.name, obj);
        }
        public void RemoveNode(string Name)
        {
            NodeList.Remove(Name);
        }

        public void AddHyperGraphLadder(Node obj, Level Floor)
        {
            if (!hyperGraphEdge.ContainsKey(obj)) hyperGraphEdge.Add(obj, new List<Level>());
            hyperGraphEdge[obj].Add(Floor);
        }
        public void EditHyperGraphLadder(Node oldObj, Node newObj)
        {
            hyperGraphEdge.Add(newObj, hyperGraphEdge[oldObj]);
            hyperGraphEdge.Remove(oldObj);
        }
        public void RemoveHyperGraphLadder(Node obj, Level Floor)
        {
            hyperGraphEdge[obj].Remove(Floor);
            if (hyperGraphEdge[obj].Count == 0) hyperGraphEdge.Remove(obj);
        }
    }
}