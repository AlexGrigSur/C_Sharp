using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NavTest.Level;

namespace NavTestNoteBookNeConsolb
{
    class NavUnite
    {
        Dictionary<NavTest.Node, List<NavTest.Node>> Graph = new Dictionary<NavTest.Node, List<NavTest.Node>>();
        NavUnite(List<NavTest.Level> LevelList)
        {
            Unite(LevelList);
        }

        private void Unite(List<NavTest.Level> LevelList)
        {
            foreach (var i in LevelList)
            {
                foreach (var j in i.edges) // Try Join
                {
                    if (j.Key.type >= 3)
                    {
                        if (Graph.ContainsKey(j.Key))
                        {
                            Graph[j.Key].AddRange(j.Value);
                            continue;
                        }
                    }
                    Graph.Add(j.Key, j.Value);
                }
            }
        }

        private bool isСonnectivityAll()
        {
            Dictionary<NavTest.Node, int> nodesToBeVisited = new Dictionary<NavTest.Node, int>(); // 0-notVisited ,1-reachable, 2-visited
            foreach (var i in Graph.Keys)
                nodesToBeVisited.Add(i, 0);
            int visitedNodesValue = 0;
            int reachableNodesValue = 0;
            bool isExitExists = false;
            return Reccur(ref nodesToBeVisited, nodesToBeVisited.First().Key, ref reachableNodesValue, ref visitedNodesValue, ref isExitExists);
        }
        
        private bool Reccur(ref Dictionary<NavTest.Node, int> nodesToBeVisited, NavTest.Node currentNode, ref int reachableNodesValue, ref int visitedNodesValue, ref bool isExitExists) // simple version
        {
            nodesToBeVisited[currentNode] = 2;
            if (reachableNodesValue == nodesToBeVisited.Count)
            {
                if(isExitExists) return true;
                else
                {
                    MessageBox.Show("План не имеет ни одного выхода/входа");
                    return false;
                }
            }
            if (reachableNodesValue == visitedNodesValue && reachableNodesValue != nodesToBeVisited.Count)
            {
                MessageBox.Show("План имеет недостижимые точки");
                return false;
            }


            foreach (var i in Graph[currentNode])
            {
                if (nodesToBeVisited[i] == 0) // if nodesToBeVisited[i]==false 
                {
                    nodesToBeVisited[i] = 1;
                    reachableNodesValue += 1;
                }
                if (i.type == 4) isExitExists = true;
            }
            foreach (var i in Graph[currentNode])
            {
                if (nodesToBeVisited[i] != 2)
                {
                    visitedNodesValue += 1;
                    /*return*/
                    Reccur(ref nodesToBeVisited, currentNode, ref reachableNodesValue, ref visitedNodesValue, ref isExitExists);
                }
            }
            return false;
        }
    }
}
