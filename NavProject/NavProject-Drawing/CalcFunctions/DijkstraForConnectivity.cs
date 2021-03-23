//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using NavProject_Drawing.Structures;

//namespace NavProject_Drawing.CalcFunctions
//{
//    abstract class DijsktraBasic
//    {
//        abstract protected object currentNode { get; set; }
//        protected object mainStruct { get; set; }
//        Dictionary<object, int> nodesToBeVisited { get; set; }
//        protected int reachableNodesValue;
//        protected int visitedNodesValue;
//        protected bool exit;

//        abstract protected void MainHandler();
//        protected void Reccur() // simple version
//        {
//            visitedNodesValue += 1;
//            nodesToBeVisited[currentNode] = 2;
//            if (reachableNodesValue == nodesToBeVisited.Count)
//            {
//                exit = true;
//                return; // all visited
//            }
//            MainHandler();
//            if (reachableNodesValue == visitedNodesValue)
//            {
//                exit = true;
//                return;
//            }
//        }
//    }

//    class DijkstraForConnectivitySplit : DijsktraBasic
//    {
//        protected override void MainHandler()
//        {
//            Node current = currentNode as Node;
//            Level main = mainStruct as Level;
//            foreach (var i in main.GetEdge(current))// reach all nodes
//            {
//                if (nodesToBeVisited[i] == 0) // if not reachable
//                {
//                    nodesToBeVisited[i] = 1;
//                    reachableNodesValue += 1;
//                }
//            }
//            foreach (var i in main.GetEdge(current)) // move 
//            {
//                if (nodesToBeVisited[i] != 2)
//                {
//                    Reccur();
//                    if (exit) return;
//                }
//            }
//        }


//        // ОГРОМНЫЕ СОМНЕНИЯ, КАК ЭТО БУДЕТ РАБОТАТЬ В МНОГОПОТОКЕ
//        public void Run(Map map)
//        {
//            map.ClearHyperGraphByConnectivity();
//            List<Thread> threadList = new List<Thread>();
//            foreach (int floorIndex in map.GetFloorsList().Keys)
//            {
//                map.ClearConnectivityComponentsOnLevel(floorIndex);
//                Level currentLevel = map.GetFloorsList()[floorIndex];
//                Dictionary</*Node*/object, int> nodesToBeVisitedLocal = new Dictionary</*Node*/object, int>(); // 0-notVisited ,1-reachable, 2-visited

//                foreach (Node j in currentLevel.GetNodeListOnFloor().Keys)
//                    nodesToBeVisited.Add(j, 0);
//                threadList.Add(new Thread(() =>
//                {
//                    while (nodesToBeVisited.Count > 0)
//                    {
//                        mainStruct = currentLevel;
//                        nodesToBeVisited = nodesToBeVisitedLocal;
//                        currentNode = nodesToBeVisited.First().Key;
//                        exit = false;
//                        visitedNodesValue = 0;
//                        reachableNodesValue = 1;
//                        Reccur();
//                        currentLevel.AddConnectivityComponents(floorIndex);

//                        foreach (Node j in nodesToBeVisited.Keys)
//                            if (nodesToBeVisited[j] > 0) currentLevel.GetConnectivityComponentsList().Last().Add(j);

//                        foreach (Node j in currentLevel.GetConnectivityComponentsList().Last().GetAllNodes())
//                        {
//                            if (j.type == 2)
//                                map.AddHyperGraphByConn(j, currentLevel.GetConnectivityComponentsList().Last());//, currentLevel.GetConnectivityComponentsList().Last());
//                            nodesToBeVisited.Remove(j);
//                        }
//                    }
//                }));
//                threadList.Last().Start();
//            }
//            foreach (var i in threadList)
//                i.Join();
//        }
//    }
//    class DijkstraForMapConnectivity : DijsktraBasic
//    {
//        public bool Run(Map map)
//        {
//            Dictionary</*ConnectivityComponents*/object, int> ConnectivityComponentsList = new Dictionary</*ConnectivityComponents*/object, int>();
//            foreach (Level i in map.GetFloorsList().Values)
//            {
//                foreach (ConnectivityComponents j in i.GetConnectivityComponentsList())
//                    ConnectivityComponentsList.Add(j, 0);
//            }

//            // set property fields
//            mainStruct = map.HyperGraphInstance;
//            nodesToBeVisited = ConnectivityComponentsList;
//            currentNode = map.GetFloorsList().First().Value.GetConnectivityComponentsList().First();
//            exit = false;
//            visitedNodesValue = 0;
//            reachableNodesValue = 1;

//            // start recursion
//            if (ConnectivityComponentsList.Count > 0)
//            {
//                Reccur();
//                return (reachableNodesValue != ConnectivityComponentsList.Count) ? false : true;
//            }
//            else
//                return false;
//        }
//        protected override void MainHandler()
//        {
//            //onnectivityComponents comp = currentNode as ConnectivityComponents;
//            HyperGraphByConnectivity hyper = mainStruct as HyperGraphByConnectivity;
//            foreach (var i in currentNode.GetLadders()) // reach all nodes
//            {
//                foreach (var j in hyper.GetConnectivities(i))
//                {
//                    if (nodesToBeVisited[j] == 0) // if not reachable
//                    {
//                        nodesToBeVisited[j] = 1;
//                        reachableNodesValue += 1;
//                    }
//                }
//            }
//            foreach (var i in comp.GetLadders()) // reach all nodes
//            {
//                foreach (var j in hyper.GetConnectivities(i))
//                    if (nodesToBeVisited[j] != 2)
//                    {
//                        Reccur();
//                        if (exit) return;
//                    }
//            }
//        }
//    }
//}
