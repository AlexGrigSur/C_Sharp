using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Navigator.Structures;
using System.Threading;

namespace NavProject_Navigator.CalcFunctions
{
    class Dijkstra //: IRouteCalc
    {
        private Map map;
        private List<ConnectivityComponents> ConnectivityComponentssList;
        private ConnectivityComponents conCompStart;
        private ConnectivityComponents conCompEnd;
        public Dijkstra(ref Map _map, ref ConnectivityComponents _ConnectivityComponentsStart, ref ConnectivityComponents _ConnectivityComponentsEnd)
        {
            map = _map;
            ConnectivityComponentssList = new List<ConnectivityComponents>();

            foreach (Level i in map.GetFloorsList().Values)
                foreach (ConnectivityComponents conn in i.GetConnectivityComponentsList())
                    ConnectivityComponentssList.Add(conn);

            conCompStart = _ConnectivityComponentsStart;
            conCompEnd = _ConnectivityComponentsEnd;
        }
        private List<ComponentsToCalc> FormResult(ref Dictionary<ConnectivityComponents, ConnectivityComponents> previousConComp)
        {
            ConnectivityComponents currentNode = conCompEnd;
            List<ComponentsToCalc> result = new List<ComponentsToCalc>();
            while (!currentNode.Equals(conCompStart))
            {
                result.Add(new ComponentsToCalc(currentNode, new Node(), new Node()));
                currentNode = previousConComp[currentNode];
            }
            result.Add(new ComponentsToCalc(conCompStart, new Node(), new Node()));
            return result;
        }
        private ConnectivityComponents MinimumDistance(ref Dictionary<ConnectivityComponents, int> distance, ref Dictionary<ConnectivityComponents, bool> isFixedConComp)
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
        public List<ComponentsToCalc> Calc()
        {
            Dictionary<ConnectivityComponents, int> distance = new Dictionary<ConnectivityComponents, int>();
            Dictionary<ConnectivityComponents, bool> isFixedConComp = new Dictionary<ConnectivityComponents, bool>();
            Dictionary<ConnectivityComponents, ConnectivityComponents> previousConComp = new Dictionary<ConnectivityComponents, ConnectivityComponents>();

            foreach (ConnectivityComponents i in ConnectivityComponentssList)
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

                u = MinimumDistance(ref distance, ref isFixedConComp);
                isFixedConComp[u] = true;
            }

            return FormResult(ref previousConComp);
        }
    }
}
