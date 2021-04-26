using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;
using NavProject_Navigator.CalcFunctions.Algoritms;
using System.Threading;

namespace NavProject_Navigator.CalcFunctions
{
    /// <summary>
    /// Class that provide shortest pathfinding between different connectivity components of building
    /// </summary>
    public class LevelPath
    {
        private Map map;
        private List<ConnectivityComponents> connectivityCompsList;
        private ConnectivityComponents conCompStart;
        private ConnectivityComponents conCompEnd;
        public LevelPath(Map _map, ConnectivityComponents _connectivityCompStart, ConnectivityComponents _connectivityCompEnd)
        {
            map = _map;
            connectivityCompsList = new List<ConnectivityComponents>();

            foreach (Level i in map.GetFloorsList().Values)
                foreach (ConnectivityComponents conn in i.GetConnectivityComponentsList())
                    connectivityCompsList.Add(conn);

            conCompStart = _connectivityCompStart;
            conCompEnd = _connectivityCompEnd;
        }
        private List<ConnectivityComponents> FormResult(Dictionary<ConnectivityComponents, ConnectivityComponents> previousConComp)
        {
            ConnectivityComponents currentNode = conCompEnd;
            List<ConnectivityComponents> result = new List<ConnectivityComponents>();
            while (!currentNode.Equals(conCompStart))
            {
                result.Add(currentNode);
                currentNode = previousConComp[currentNode];
            }
            result.Add(conCompStart);
            result.Reverse();
            return result;
        }
        private ConnectivityComponents MinimumDistance(Dictionary<ConnectivityComponents, int> distance, Dictionary<ConnectivityComponents, bool> isFixedConComp)
        {
            int min = int.MaxValue;
            ConnectivityComponents minIndex = null;

            foreach (ConnectivityComponents conn in distance.Keys)
            {
                if (!isFixedConComp[conn] && distance[conn] <= min)
                {
                    if (minIndex != null && distance[minIndex] == distance[conn] && minIndex.Equals(conCompEnd))
                        continue;

                    min = distance[conn];
                    minIndex = conn;
                }
            }
            return minIndex;
        }
        public List<ConnectivityComponents> Calc()
        {
            Dictionary<ConnectivityComponents, int> distance = new Dictionary<ConnectivityComponents, int>();
            Dictionary<ConnectivityComponents, bool> isFixedConComp = new Dictionary<ConnectivityComponents, bool>();
            Dictionary<ConnectivityComponents, ConnectivityComponents> previousConComp = new Dictionary<ConnectivityComponents, ConnectivityComponents>();

            foreach (var i in connectivityCompsList)
            {
                distance.Add(i, int.MaxValue);
                isFixedConComp.Add(i, false);
                previousConComp.Add(i, null);
            }

            distance[conCompStart] = 0;
            isFixedConComp[conCompStart] = true;
            ConnectivityComponents u = conCompStart;

            while (!u.Equals(conCompEnd))
            {
                foreach (Node nd in u.GetLadders())
                    foreach (ConnectivityComponents j in map.GetConnectivities(nd))
                        if (!isFixedConComp[j] && distance[u] != int.MaxValue && distance[u] + 1 < distance[j])
                        {
                            distance[j] = distance[u] + 1;
                            previousConComp[j] = u;
                        }

                u = MinimumDistance(distance, isFixedConComp);
                isFixedConComp[u] = true;
            }

            return FormResult(previousConComp);
        }
    }
}
