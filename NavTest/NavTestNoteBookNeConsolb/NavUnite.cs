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
        bool isExitExists = false;
        public void SplitByConnectivity(ref NavTest.Map map)
        {
            foreach (var i in map.Floors.Keys)
            {
                map.Floors[i].connectivityComponents.Clear();
                isСonnectivityAll(map.Floors[i]);
            }
           //if (isExitExists) return true;
        }

        private void isСonnectivityAll(Level level)
        {
            Dictionary<NavTest.Node, int> nodesToBeVisited = new Dictionary<NavTest.Node, int>(); // 0-notVisited ,1-reachable, 2-visited
            foreach (var i in level.nodeListOnFloor.Keys)
                nodesToBeVisited.Add(i, 0);
            while (nodesToBeVisited.Count > 0)
            {
                int visitedNodesValue = 0;
                int reachableNodesValue = 0;
                Reccur(ref level, ref nodesToBeVisited, nodesToBeVisited.First().Key, ref reachableNodesValue, ref visitedNodesValue, ref isExitExists);
                level.connectivityComponents.Add(new ConnectivityComp());
                foreach (var i in nodesToBeVisited.Keys)
                {
                    if (nodesToBeVisited[i] > 0)
                    {
                        level.connectivityComponents.Last().add(i);
                        nodesToBeVisited.Remove(i);
                    }
                }
            }
        }

        private void Reccur(ref Level level,ref Dictionary<NavTest.Node, int> nodesToBeVisited, NavTest.Node currentNode, ref int reachableNodesValue, ref int visitedNodesValue, ref bool isExitExists) // simple version
        {
            nodesToBeVisited[currentNode] = 2;
            if (reachableNodesValue == nodesToBeVisited.Count )
                return; // all visited

            foreach (var i in level.edges[currentNode]) // reach all nodes
            {
                if (nodesToBeVisited[i] == 0) // if not reachable
                {
                    nodesToBeVisited[i] = 1;
                    reachableNodesValue += 1;
                }
                if (i.type == 4) isExitExists = true;
            }
            foreach (var i in level.edges[currentNode]) // move 
            {
                if (nodesToBeVisited[i] != 2)
                {
                    visitedNodesValue += 1;
                    /*return*/
                    Reccur(ref level, ref nodesToBeVisited, currentNode, ref reachableNodesValue, ref visitedNodesValue, ref isExitExists);
                }
            }

            if (reachableNodesValue == visitedNodesValue)
                return;
        }

    }
}

