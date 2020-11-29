using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using NavTest;

namespace NavTestNoteBookNeConsolb
{
    class DataFromDB
    {
        private string buildingName;
        public DataFromDB(string BuildingName)
        {
            buildingName = BuildingName;
        }
        public Map DownloadFromDB(ref int corridorCounter, bool isNav)
        {
            Map map = new Map();
            corridorCounter = -1;
            using (DB DataBase = new DB("Plans"))
            {
                string maxCorrName = "";
                map.Name = buildingName;
                //// BETA
                #region // Levels
                int building_ID = -1;
                List<int> level_ID = new List<int>();
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Buildings` " +
                    $"where `buildingName`='{buildingName}'"))
                {
                    if (reader.Read())
                        building_ID = reader.GetInt32(0);
                }
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id`,`levelFloor`,`levelScreenResX`,`levelScreenResY` from `Levels` " +
                    $"where `building_ID`='{building_ID}'"))
                {
                    while (reader.Read())
                    {
                        level_ID.Add(reader.GetInt32(0));
                        map.AddFloor(reader.GetInt32(1));
                        map.GetFloor(reader.GetInt32(1)).ScreenResX = reader.GetInt32(2);
                        map.GetFloor(reader.GetInt32(1)).ScreenResY = reader.GetInt32(3);
                    }
                }
                #endregion
                #region // Nodes
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `NodeName`,`NodeType`,`NodeDescription` from `Nodes` " +
                    $"where `building_ID`='{building_ID}'"))
                {
                    while (reader.Read())
                    {
                        Node tempNode = new Node(reader.GetString(0), reader.GetInt32(1), reader.GetString(2));
                        map.AddInNodeList(tempNode);
                        if (tempNode.type == 0) maxCorrName = tempNode.name;
                    }
                }

                #endregion
                #region // LevelNodes/Edges
                int iterator = 0;
                foreach (Level i in map.GetFloorsList().Values)
                {
                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `Nds`.`NodeName`,`levelNodeCoordX`,`levelNodeCoordY` from `LevelNodes` `LN` " +
                        $"inner join `Nodes` `Nds` on `Nds`.`id`=`LN`.`Node_ID` " +
                        $"where `level_ID`='{level_ID[iterator]}'"))
                    {
                        while (reader.Read())
                        {
                            i.AddNode(map.GetNode(reader.GetString(0)), reader.GetInt32(1), reader.GetInt32(2));
                            if (map.GetNode(reader.GetString(0)).type == 2)
                                map.AddHyperGraphByConn(map.GetNode(reader.GetString(0)));
                        }
                    }

                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `FirstNodeName`.`NodeName`,`SecondNodeName`.`NodeName` from `Edges` `EDG` " +
                        $"inner join `LevelNodes` `FirstLN` on `FirstLN`.`id`=`EDG`.`startNode_ID` " +
                        $"inner join `LevelNodes` `SecondLN` on `SecondLN`.`id`=`EDG`.`endNode_ID` " +
                        $"inner join `Nodes` `FirstNodeName` on `FirstNodeName`.`id`=`FirstLN`.`node_ID` " +
                        $"inner join `Nodes` `SecondNodeName` on `SecondNodeName`.`id`=`SecondLN`.`node_ID` " +
                        $"where `EDG`.`level_ID`='{level_ID[iterator]}'"))
                    {
                        while (reader.Read())
                        {
                            if (!map.GetFloor(i.FloorIndex).GetEdgesList().ContainsKey(map.GetNode(reader.GetString(0))))
                                map.GetFloor(i.FloorIndex).AddEdge(map.GetNode(reader.GetString(0)));

                            map.GetFloor(i.FloorIndex).GetEdge(map.GetNode(reader.GetString(0))).Add(map.GetNode(reader.GetString(1)));

                            if (!map.GetFloor(i.FloorIndex).GetEdgesList().ContainsKey(map.GetNode(reader.GetString(1))))
                                map.GetFloor(i.FloorIndex).AddEdge(map.GetNode(reader.GetString(1)));

                            map.GetFloor(i.FloorIndex).GetEdge(map.GetNode(reader.GetString(1))).Add(map.GetNode(reader.GetString(0)));
                        }
                    }
                    if (isNav)
                    {
                        int connectivityCompIndex = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `connectivityComponentIndex`, `Nd`.`NodeName`, `Lvl`.`levelFloor` from `ConnectivityComponents` `CC` " +
                            $"inner join `Levels` `Lvl` on `CC`.`level_ID`=`Lvl`.`id` " +
                            $"inner join `Nodes` `Nd` on `Nd`.`id`=`CC`.`node_ID` " +
                            $"where `level_ID`='{level_ID[iterator]}' " +
                            $"order by `level_ID`,`connectivityComponentIndex` "))
                        {
                            while (reader.Read())
                            {
                                if (connectivityCompIndex != reader.GetInt32(0))
                                {
                                    if (connectivityCompIndex!=-1)//ladders != null)
                                    {
                                        foreach (Node nd in i.GetConnectivityComponentsList().Last().GetLadderList())
                                            map.AddInExistingHyperGraphByConnectivity(nd, i.GetConnectivityComponentsList().Last());
                                    }                                    
                                    i.AddConnectivityComponents(reader.GetInt32(2));
                                    connectivityCompIndex = reader.GetInt32(0);
                                }
                                i.GetConnectivityComponentsList().Last().add(map.GetNode(reader.GetString(1)));
                            }
                            foreach (Node nd in i.GetConnectivityComponentsList().Last().GetLadderList())
                                map.AddInExistingHyperGraphByConnectivity(nd, i.GetConnectivityComponentsList().Last());
                        }
                    }
                    ++iterator;
                }
                if (!isNav) corridorCounter = (maxCorrName != "") ? Convert.ToInt32(maxCorrName.Split('_')[1]) + 1 : 0;
                #endregion
            }
            return map;
        }
    }

    class DataToDB
    {
        private Map map;
        public DataToDB(ref Map mapToDownload)
        {
            map = mapToDownload;
        }
        public void updateDB(bool isNavAble)
        {
            string buildingName = map.Name;
            using (DB DataBase = new DB("Plans"))
            {
                #region // Удаление старых данных 
                DataBase.ExecuteCommand($"delete from `Buildings` where `buildingName`='{buildingName}'");
                #endregion
                #region // вставить здание
                DataBase.ExecuteCommand($"insert into `Buildings` values(null,'{buildingName}','{((isNavAble) ? 1 : 0)}')"); // Вставить здание
                int building_ID = -1;
                int level_ID = -1;
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Buildings` where `buildingName`='{buildingName}'")) // Вытащить id этого здания
                {
                    if (reader.Read()) building_ID = reader.GetInt32(0);
                }
                #endregion
                #region // вставить Nodes
                foreach (Node tempNode in map.GetNodeList().Values)
                    DataBase.ExecuteCommand($"insert into `Nodes` values(null,'{building_ID}','{tempNode.name}','{tempNode.type}','{tempNode.description}')"); // вставить CommonNodes
                #endregion
                List<int> coords;
                Dictionary<Node, List<Node>> tempDictionary;
                Dictionary<Node, List<Node>> removed;
                foreach (Level i in map.GetFloorsList().Values) // Floors
                {
                    #region // вставить этажи
                    int resX = (i.ScreenResX > 10000) ? 900 : i.ScreenResX;
                    int resY = (i.ScreenResY > 10000) ? 700 : i.ScreenResY;
                    DataBase.ExecuteCommand($"insert into `Levels` values(null,'{building_ID}','{i.FloorIndex}','{resX}','{resY}')"); // добавить этаж
                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Levels` where `building_ID`='{building_ID}' and `levelFloor`='{i.FloorIndex}'")) // Вытащить этот id
                    {
                        if (reader.Read()) level_ID = reader.GetInt32(0);
                    }
                    #endregion
                    #region // вставить вершины из этажей
                    foreach (Node tempNode in i.GetNodeListOnFloor().Keys)
                    {
                        int node_ID = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{tempNode.name}'"))
                        {
                            if (reader.Read()) node_ID = reader.GetInt32(0);
                        }
                        coords = map.GetCoordOfNode(i.FloorIndex, tempNode);
                        DataBase.ExecuteCommand($"insert into `LevelNodes` values (null,'{level_ID}','{node_ID}','{coords[0]}','{coords[1]}')");
                        int iterator = 0;
                        foreach (ConnectivityComp tempConnComp in i.GetConnectivityComponentsList())
                        {
                            if (tempConnComp.isContains(tempNode))
                                break;
                            ++iterator;
                        }
                        DataBase.ExecuteCommand($"insert into `ConnectivityComponents` values ('{level_ID}','{iterator}','{node_ID}')");
                    }
                    #endregion
                    #region // вставить рёбра
                    tempDictionary = new Dictionary<Node, List<Node>>(i.GetEdgesList());
                    removed = new Dictionary<Node, List<Node>>();
                    foreach (Node nodeKey in tempDictionary.Keys)
                    {
                        int startNode_ID = -1;
                        int endNode_ID = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `LevelNodes`.`id` from `LevelNodes` " +
                            $"inner join `Nodes` on `LevelNodes`.`node_ID`=`Nodes`.`id` " +
                            $"where `level_ID`='{level_ID}' and `Nodes`.`NodeName`='{nodeKey.name}'"))
                        {
                            if (reader.Read()) startNode_ID = reader.GetInt32(0);
                        }
                        foreach (Node connectedNode in tempDictionary[nodeKey])
                        {
                            using (MySqlDataReader reader = DataBase.ExecuteReader($"select `LevelNodes`.`id` from `LevelNodes` " +
                                $"inner join `Nodes` on `LevelNodes`.`node_ID`=`Nodes`.`id` " +
                                $"where `level_ID`='{level_ID}' and `Nodes`.`NodeName`='{connectedNode.name}'"))
                            {
                                if (reader.Read()) endNode_ID = reader.GetInt32(0);
                            }
                            DataBase.ExecuteCommand($"insert into `Edges` values ('{level_ID}','{startNode_ID}','{endNode_ID}')");
                            tempDictionary[connectedNode].Remove(nodeKey);
                            if (!removed.ContainsKey(connectedNode)) removed.Add(connectedNode, new List<Node>());
                            removed[connectedNode].Add(nodeKey);
                        }
                    }
                    tempDictionary.Clear();
                    foreach (Node z in removed.Keys)
                        foreach (Node j in removed[z])
                            i.AddSingleEdge(z, j);
                    removed.Clear();

                    #endregion
                }
            }
        }
    }
}
