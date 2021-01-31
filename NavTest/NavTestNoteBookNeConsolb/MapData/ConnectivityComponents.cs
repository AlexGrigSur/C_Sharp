using System;
using System.Collections.Generic;
using NavTest;

namespace NavTest
{ 
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
}
