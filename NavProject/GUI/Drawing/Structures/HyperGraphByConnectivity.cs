using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavProject_Drawing.Structures
{
    /// <summary>
    /// Class that represent connections between different levels
    /// </summary>
    [Serializable]
    public class HyperGraphByConnectivity
    {
        private Dictionary<Node, List<ConnectivityComponents>> hyperGraph = new Dictionary<Node, List<ConnectivityComponents>>();
        public void Clear() => hyperGraph.Clear();
        public bool ContainsKey(Node nd) => hyperGraph.ContainsKey(nd);
        public List<ConnectivityComponents> GetConnectivities(Node node) => hyperGraph[node];
        public List<Node> GetKeys() => hyperGraph.Keys.ToList();
        public void Add(Node nd, List<ConnectivityComponents> value) 
        {
            if (!hyperGraph.ContainsKey(nd)) 
                hyperGraph.Add(nd, value);
        }
        public void Add(Node nd, ConnectivityComponents value) => hyperGraph[nd].Add(value);
        public void Remove(Node nd) => hyperGraph.Remove(nd);
    }
}
