using NavTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NavTest.Level;

namespace NavTestNoteBookNeConsolb
{
    class NavSavePrepear
    {
        public bool isNavAble { get; set; }
        public NavSavePrepear(ref Map map)
        {
            isNavAble = true;
            SplitByConnectivity(ref map);
            IsMapConnectivity(ref map);
        }
        public void SplitByConnectivity(ref Map map)
        {
            foreach (string floorName in map.Floors.Keys)
            {
                map.Floors[floorName].connectivityComponents.Clear();
                Level currentLevel = map.Floors[floorName];
                if (map.Floors[floorName].Equals(currentLevel));
                Dictionary<NavTest.Node, int> nodesToBeVisited = new Dictionary<NavTest.Node, int>(); // 0-notVisited ,1-reachable, 2-visited
                foreach (Node j in currentLevel.nodeListOnFloor.Keys)
                    nodesToBeVisited.Add(j, 0);
                while (nodesToBeVisited.Count > 0)
                {
                    bool exit = false;
                    int visitedNodesValue = 0;
                    int reachableNodesValue = 1;
                    ReccurConnectivityComponents(ref currentLevel, ref nodesToBeVisited, nodesToBeVisited.First().Key, ref reachableNodesValue, ref visitedNodesValue, ref exit);
                    currentLevel.connectivityComponents.Add(new ConnectivityComp());
                    foreach (Node j in nodesToBeVisited.Keys)
                        if (nodesToBeVisited[j] > 0) currentLevel.connectivityComponents.Last().add(j);

                    currentLevel.connectivityComponents.Last().FloorName = currentLevel.Name;

                    foreach (Node j in currentLevel.connectivityComponents.Last().GetAllNodesList())
                    {
                        if (j.type == 2) map.HyperGraphByConnectivity[j].Add(currentLevel.connectivityComponents.Last());
                        nodesToBeVisited.Remove(j);
                    }
                }
            }
        }
        private void ReccurConnectivityComponents(ref Level level, ref Dictionary<NavTest.Node, int> nodesToBeVisited, NavTest.Node currentNode, ref int reachableNodesValue, ref int visitedNodesValue, ref bool exit) // simple version
        {
            visitedNodesValue += 1;
            nodesToBeVisited[currentNode] = 2;
            if (reachableNodesValue == nodesToBeVisited.Count)
            {
                exit = true;
                return; // all visited
            }
            foreach (Node i in level.edges[currentNode]) // reach all nodes
            {
                if (nodesToBeVisited[i] == 0) // if not reachable
                {
                    nodesToBeVisited[i] = 1;
                    reachableNodesValue += 1;
                }
            }
            foreach (Node i in level.edges[currentNode]) // move 
            {
                if (nodesToBeVisited[i] != 2)
                {
                    ReccurConnectivityComponents(ref level, ref nodesToBeVisited, i, ref reachableNodesValue, ref visitedNodesValue, ref exit);
                    if (exit) return;
                }
            }
            if (reachableNodesValue == visitedNodesValue)
            {
                exit = true;
                return;
            }
        }
        
        private void IsMapConnectivity(ref Map map)
        {
            Dictionary<ConnectivityComp, int> ConnectivityComponentsList = new Dictionary<ConnectivityComp, int>();
            foreach (Level i in map.Floors.Values)
            {
                foreach (ConnectivityComp j in i.connectivityComponents)
                    ConnectivityComponentsList.Add(j, 0);
            }
            bool exit = false;
            int visitedNodesValue = 0;
            int reachableNodesValue = 1;
            ReccurMapConnectivity(ref map.HyperGraphByConnectivity, ref ConnectivityComponentsList, map.Floors.First().Value.connectivityComponents.First(), ref reachableNodesValue, ref visitedNodesValue, ref exit);
            if (reachableNodesValue != ConnectivityComponentsList.Count) isNavAble = false;
        }
        private void ReccurMapConnectivity(ref Dictionary<Node, List<ConnectivityComp>> hyperGraphByConnectivity, ref Dictionary<ConnectivityComp, int> nodesToBeVisited, ConnectivityComp currentNode, ref int reachableNodesValue, ref int visitedNodesValue, ref bool exit) // simple version
        {
            visitedNodesValue += 1;
            nodesToBeVisited[currentNode] = 2;
            if (reachableNodesValue == nodesToBeVisited.Count)
            {
                exit = true;
                return; // all visited
            }
            foreach (Node i in currentNode.GetLadderList()) // reach all nodes
            {
                foreach (ConnectivityComp j in hyperGraphByConnectivity[i])
                    if (nodesToBeVisited[j] == 0) // if not reachable
                    {
                        nodesToBeVisited[j] = 1;
                        reachableNodesValue += 1;
                    }
            }
            foreach (Node i in currentNode.GetLadderList()) // reach all nodes
            {
                foreach (ConnectivityComp j in hyperGraphByConnectivity[i])
                    if (nodesToBeVisited[j] != 2)
                    {
                        ReccurMapConnectivity(ref hyperGraphByConnectivity, ref nodesToBeVisited, j, ref reachableNodesValue, ref visitedNodesValue, ref exit);
                        if (exit) return;
                    }
            }

            if (reachableNodesValue == visitedNodesValue)
            {
                exit = true;
                return;
            }
        }

    }
}