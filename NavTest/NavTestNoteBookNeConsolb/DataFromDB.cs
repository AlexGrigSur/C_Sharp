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
        private Map obj;
        private string buildingName;
        DataFromDB(Map map, string BuildingName)
        {
            obj = map;
            buildingName = BuildingName;
        }
        public Map DownloadFromDB(ref int corridorCounter, bool isNav)
        {
            using (DB DataBase = new DB("Plans"))
            {
                string maxCorrName = "";
                obj.name = buildingName;
                //// BETA
                #region // Levels
                int building_ID = -1;
                List<int> level_ID = new List<int>();
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Buildings` where `buildingName`='{buildingName}'"))
                {
                    if (reader.Read())
                        building_ID = reader.GetInt32(0);
                }
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id`,`levelName`,`levelFloor`,`levelScreenResX`,`levelScreenResY` from `Levels` where `building_ID`='{building_ID}'"))
                {
                    while (reader.Read())
                    {
                        level_ID.Add(reader.GetInt32(0));
                        obj.Floors.Add(reader.GetString(1), new Level(reader.GetString(1), reader.GetInt32(2)));
                        obj.Floors[reader.GetString(1)].screenResX = reader.GetInt32(3);
                        obj.Floors[reader.GetString(1)].screenResY = reader.GetInt32(4);
                    }
                }
                #endregion
                #region // Nodes
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `NodeName`,`NodeType`,`NodeDescription` from `Nodes` where `building_ID`='{building_ID}'"))
                {
                    while (reader.Read())
                    {
                        Node tempNode = new Node(reader.GetString(0), reader.GetInt32(1), reader.GetString(2));
                        obj.NodeList.Add(tempNode.name, tempNode);
                        if (tempNode.type == 0) maxCorrName = tempNode.name;
                    }
                }

                #endregion
                #region // LevelNodes/Edges
                int iterator = 0;
                foreach (Level i in obj.Floors.Values)
                {

                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `Nds`.`NodeName`,`levelNodeCoordX`,`levelNodeCoordY` from `LevelNodes` `LN` inner join `Nodes` `Nds` on `Nds`.`id`=`LN`.`Node_ID` where `level_ID`='{level_ID[iterator]}'"))
                    {
                        while (reader.Read())
                        {
                            i.AddNode(obj.NodeList[reader.GetString(0)], reader.GetInt32(1), reader.GetInt32(2));
                            if (obj.NodeList[reader.GetString(0)].type == 2)
                                obj.AddHyperGraphByConn(obj.NodeList[reader.GetString(0)]);
                        }
                    }

                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `First`.`NodeName`,`Second`.`NodeName` from `Edges` `EDG` inner join `Nodes` `First` on `First`.`id`=`EDG`.`startNode_ID` inner join `Nodes` `Second` on `Second`.`id`=`EDG`.`endNode_ID` where `level_ID`='{level_ID[iterator]}'"))
                    {
                        while (reader.Read())
                        {
                            if (!obj.Floors[i.Name].edges.ContainsKey(obj.NodeList[reader.GetString(0)]))
                                obj.Floors[i.Name].edges.Add(obj.NodeList[reader.GetString(0)], new List<Node>());
                            obj.Floors[i.Name].edges[obj.NodeList[reader.GetString(0)]].Add(obj.NodeList[reader.GetString(1)]);

                            if (!obj.Floors[i.Name].edges.ContainsKey(obj.NodeList[reader.GetString(1)]))
                                obj.Floors[i.Name].edges.Add(obj.NodeList[reader.GetString(1)], new List<Node>());
                            obj.Floors[i.Name].edges[obj.NodeList[reader.GetString(1)]].Add(obj.NodeList[reader.GetString(0)]);
                        }
                    }
                    if (isNav)
                    {
                        int connectivityCompIndex = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `connectivityComponentIndex`, `Nd`.`NodeName` from `ConnectivityComponents` `CC` inner join `Nodes` `Nd` on `Nd`.`id`=`CC`.`node_ID` where `level_ID`='{level_ID[iterator]}'"))
                        {
                            while (reader.Read())
                            {
                                if (connectivityCompIndex != reader.GetInt32(0))
                                {
                                    i.connectivityComponents.Add(new ConnectivityComp());
                                    connectivityCompIndex = reader.GetInt32(0);
                                }
                                i.connectivityComponents.Last().add(obj.NodeList[reader.GetString(1)]);
                            }
                        }
                        if (maxCorrName != "") corridorCounter = Convert.ToInt32(maxCorrName.Split('_')[1]) + 1;
                    }
                    ++iterator;
                }
                #endregion
            }
            return obj;
        }

        public void updateDB()
        {
            using (DB DataBase = new DB("Plans"))
            {
                #region // Удаление старых данных 
                DataBase.ExecuteCommand($"delete from `Buildings` where `buildingName`='{buildingName}'");
                #endregion
                #region // вставить здание
                DataBase.ExecuteCommand($"insert into `Buildings` values(null,'{buildingName}','{((prepear.isNavAble) ? 1 : 0)}')"); // Вставить здание
                int building_ID = -1;
                int level_ID = -1;
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Buildings` where `buildingName`='{buildingName}'")) // Вытащить id этого здания
                {
                    if (reader.Read()) building_ID = reader.GetInt32(0);
                }
                #endregion
                #region // вставить Nodes
                foreach (Node tempNode in obj.NodeList.Values)
                    DataBase.ExecuteCommand($"insert into `Nodes` values(null,'{building_ID}','{tempNode.name}','{tempNode.type}','{tempNode.description}')"); // вставить CommonNodes
                #endregion
                List<int> coords;
                Dictionary<Node, List<Node>> tempDictionary;
                foreach (Level i in obj.Floors.Values) // Floors
                {
                    #region // вставить этажи
                    DataBase.ExecuteCommand($"insert into `Levels` values(null,'{building_ID}','{i.Name}','{i.floor}','{i.screenResX}','{i.screenResY}')"); // добавить этаж
                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Levels` where `building_ID`='{building_ID}' and `levelName`='{i.Name}'")) // Вытащить этот id
                    {
                        if (reader.Read()) level_ID = reader.GetInt32(0);
                    }
                    #endregion
                    #region // вставить вершины из этажей
                    foreach (Node tempNode in i.nodeListOnFloor.Keys)
                    {
                        int node_ID = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{tempNode.name}'"))
                        {
                            if (reader.Read()) node_ID = reader.GetInt32(0);
                        }
                        coords = obj.GetCoordOfNode(i.Name, tempNode);
                        DataBase.ExecuteCommand($"insert into `LevelNodes` values (null,'{level_ID}','{node_ID}','{coords[0]}','{coords[1]}')");
                        int iterator = 0;
                        foreach (ConnectivityComp tempConnComp in i.connectivityComponents)
                        {
                            if (tempConnComp.isContains(tempNode))
                                break;
                            ++iterator;
                        }
                        DataBase.ExecuteCommand($"insert into `ConnectivityComponents` values ('{level_ID}','{iterator}','{node_ID}')");
                        // Отсортировать ConnectivityComponents
                    }
                    #endregion
                    #region // вставить рёбра
                    tempDictionary = new Dictionary<Node, List<Node>>(i.edges);
                    foreach (Node nodeKey in tempDictionary.Keys)
                    {
                        int startNode_ID = -1;
                        int endNode_ID = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{nodeKey.name}'"))
                        {
                            if (reader.Read()) startNode_ID = reader.GetInt32(0);
                        }
                        foreach (Node connectedNode in tempDictionary[nodeKey])
                        {
                            using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{connectedNode.name}'"))
                            {
                                if (reader.Read()) endNode_ID = reader.GetInt32(0);
                            }
                            DataBase.ExecuteCommand($"insert into `Edges` values ('{level_ID}','{startNode_ID}','{endNode_ID}')");
                            tempDictionary[connectedNode].Remove(nodeKey);
                        }
                    }
                    tempDictionary.Clear();
                    #endregion
                }
            }
        }
    }
}
