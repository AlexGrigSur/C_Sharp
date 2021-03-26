using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using NavProject_Navigator.CalcFunctions.Algoritms;
using NavProject_Navigator.Structures;

namespace NavProject_Navigator.CalcFunctions
{
    public struct ComponentsToCalc
    {
        public ConnectivityComponents CurrConnComp;
        public Node startNode;
        public Node endNode;
        public ComponentsToCalc(ConnectivityComponents _ConnectivityComp, Node _startNode, Node _EndNode)
        {
            CurrConnComp = _ConnectivityComp;
            startNode = _startNode;
            endNode = _EndNode;
        }
    }
    class MainRouteHandler
    {
        private Map map;
        private Node startNode;
        private Node endNode;
        public MainRouteHandler(Map _map, Node _startNode, Node _endNode)
        {
            map = _map;
            startNode = _startNode;
            endNode = _endNode;
        }
        public Dictionary<ConnectivityComponents, List<Node>> GetRoute(IRouteAlg algorithm)
        {
            ConnectivityComponents startConComp = map.FindConnectivityCompByNode(startNode);
            ConnectivityComponents endConComp = map.FindConnectivityCompByNode(endNode);

            List<ComponentsToCalc> ToCalcList = new List<ComponentsToCalc>();

            SetComponentsPath(ToCalcList, startConComp, endConComp);

            Dictionary<ConnectivityComponents, List<Node>> result = new Dictionary<ConnectivityComponents, List<Node>>();
            List<Thread> threadsList = new List<Thread>();

            foreach (ComponentsToCalc i in ToCalcList)
            {
                result.Add(i.CurrConnComp, null);
                threadsList.Add(new Thread(() => { result[i.CurrConnComp] = algorithm.Calc(map, i.CurrConnComp, i.startNode, i.endNode); }));
                threadsList.Last().Start();
            }
            foreach (var i in threadsList)
                i.Join();

            return result;
        }

        private void SetComponentsPath(List<ComponentsToCalc> compToCalc, ConnectivityComponents startConComp, ConnectivityComponents endConComp)
        {
            if (startConComp.Equals(endConComp))
                compToCalc.Add(new ComponentsToCalc(startConComp, startNode, endNode));
            else
            {
                List<ConnectivityComponents> connectivityCompsList = new List<ConnectivityComponents>();
                foreach (Level i in map.GetFloorsList().Values)
                    foreach (var comp in i.GetConnectivityComponentsList())
                        connectivityCompsList.Add(comp);
                List<ConnectivityComponents> ToCalcList = new LevelPath(map, startConComp, endConComp).Calc();
                compToCalc = new List<ComponentsToCalc>(ToCalcList.Count);
                GetStartEndNodePriority(compToCalc, ToCalcList);
            }
        }

        private void GetStartEndNodePriority(List<ComponentsToCalc> compToCalc, List<ConnectivityComponents> components)
        {
            compToCalc[0] = new ComponentsToCalc(components.First(), startNode, new Node());
            compToCalc[compToCalc.Count - 1] = new ComponentsToCalc(components.Last(), new Node(), endNode);

            for (int i = 0; i < compToCalc.Count - 1; ++i)
            {
                int min = int.MaxValue;
                Node ladrIndex = new Node();
                Point baseCoord = map.GetCoordOfNode(components[i].GetFloor(), compToCalc[i].startNode);
                Point ladderCoord;

                foreach (Node ladr in components[i].GetLadders())
                {
                    if (!map.GetConnectivities(ladr).Contains(components[i + 1]))
                        continue;
                    ladderCoord = map.GetCoordOfNode(components[i].GetFloor(), ladr);
                    int distance = Convert.ToInt32(Math.Sqrt((ladderCoord.X - baseCoord.X) * (ladderCoord.X - baseCoord.X) + (ladderCoord.Y - baseCoord.Y) * (ladderCoord.Y - baseCoord.Y)));
                    if (distance < min)
                    {
                        min = distance;
                        ladrIndex = ladr;
                    }
                }
                compToCalc[i] = new ComponentsToCalc(components[i], compToCalc[i].startNode, ladrIndex);
                compToCalc[i + 1] = new ComponentsToCalc(components[i + 1], ladrIndex, compToCalc[i + 1].endNode);
            }
        }
    }
}
