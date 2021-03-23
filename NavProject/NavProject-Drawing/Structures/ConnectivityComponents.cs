using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Drawing.Structures
{
    /// <summary>
    /// Represent class that contains all connected by same level nodes 
    /// </summary>
    public class ConnectivityComponents
    {
        private int floor;
        private List<Node> allNodes = new List<Node>();
        /// <summary>
        /// Nodes that connect different levels
        /// </summary>
        private List<Node> ladders = new List<Node>();
        public ConnectivityComponents(int Floor) => floor = Floor;
        public int GetFloor() => floor;
        public List<Node> GetAllNodes() => allNodes;
        public List<Node> GetLadders() => ladders;
        public bool IsContains(Node obj) => allNodes.Contains(obj);
        public void Add(Node obj)
        {
            allNodes.Add(obj);
            if (obj.type == 2) ladders.Add(obj);
        }
    }
}
