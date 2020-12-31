using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace NavTest
{
    public class DBInitialize
    {
        public void InitDB()
        {
            using (DB DataBase = new DB())
            {
                DataBase.ExecuteCommand("create database if not exists `Plans`");
                DataBase.NewConnection("Plans");
                DataBase.ExecuteCommand("create table if not exists `Buildings`(" +
                    "`id` int(11) not null primary key auto_increment, " +
                    "`buildingName` varchar(150) not null unique, " +
                    "`buildingIsNavAble` boolean not null)");

                DataBase.ExecuteCommand("create table if not exists `Levels`(" +
                    "`id` int(11) primary key not null auto_increment, " +
                    "`building_ID` int(11) not null," +
                    "`levelFloor` int(11) not null, " +
                    "`levelScreenResX` int(11) not null, " +
                    "`levelScreenResY` int(11) not null, " +
                    "constraint `NoSameLevels` UNIQUE(`building_ID`,`levelFloor`), " +
                    "constraint `building_id_FK` foreign key(`building_ID`) references `Buildings`(`id`) on delete cascade)");

                DataBase.ExecuteCommand("create table if not exists `Nodes` (" +
                    "`id` int(11) primary key not null auto_increment, " +
                    "`building_ID` int(11) not null," +
                    "`NodeName` varchar(150) not null," +
                    "`NodeType` int(11) not null, " +
                    "`NodeDescription` varchar(150), " +
                    "constraint `building_id_FK_1` foreign key(`building_ID`) references `Buildings`(`id`) on delete cascade)");

                DataBase.ExecuteCommand("create table if not exists `ConnectivityComponents` (" +
                    "`level_ID` int(11) not null," +
                    "`connectivityComponentIndex` int(11) not null, " +
                    "`node_ID` int(11) not null, " +
                    "constraint `UniqueInConnComp` primary key(`level_ID`,`connectivityComponentIndex`,`node_ID`), " +
                    "constraint `level_ID_FK` foreign key(`level_ID`) references `Levels`(`id`) on delete cascade, " +
                    "constraint `node_ID_FK` foreign key(`node_ID`) references `Nodes`(`id`) on delete cascade)");

                DataBase.ExecuteCommand("create table if not exists `LevelNodes` (" +
                    "`id` int(11) primary key not null auto_increment, " +
                    "`level_ID` int(11) not null, `node_ID` int(11) not null, " +
                    "`levelNodeCoordX` int(11) not null, " +
                    "`levelNodeCoordY` int(11) not null, " +
                    "constraint `level_ID_FK_1` foreign key(`level_ID`) references `Levels`(`id`) on delete cascade, " +
                    "constraint `node_ID_FK_1` foreign key(`node_ID`) references `Nodes`(`id`) on delete cascade)");

                DataBase.ExecuteCommand("create table if not exists `Edges` (" +
                    "`level_ID` int(11) not null, " +
                    "`startNode_ID` int(11) not null, " +
                    "`endNode_ID` int(11) not null, " +
                    "constraint `NoMultiGraphs` primary key(`level_ID`,`startNode_ID`,`endNode_ID`), " +
                    "constraint `level_ID_FK_2` foreign key(`level_ID`) references `Levels`(`id`) on delete cascade," +
                    "constraint `startNode_ID_FK` foreign key(`startNode_ID`) references `LevelNodes`(`id`) on delete cascade, " +
                    "constraint `endNode_ID_FK` foreign key(`endNode_ID`) references `LevelNodes`(`id`) on delete cascade)");
            }
        }

        public void DropDB()
        {
            using (DB DataBase = new DB("Plans"))
            {
                DataBase.ExecuteCommand("drop database `Plans`");
            }
            InitDB();
        }

        public void InsertBuilding(string buildingName)
        {
            using (DB DataBase = new DB("Plans"))
            {
                DataBase.ExecuteCommand($"INSERT INTO `Buildings` VALUES (NULL, '{buildingName}','0')");
            }
        }

        public void DeleteBuilding(string buildingName)
        {
            using (DB DataBase = new DB("Plans"))
            {
                DataBase.ExecuteCommand($"delete from `Buildings` where `buildingName`='{buildingName}'");
            }
        }

        public void SelectBuilding(ref List<string> buildings, ref List<bool> isNavAble)
        {
            using (DB DataBase = new DB("Plans"))
            {
                using (MySqlDataReader reader = DataBase.ExecuteReader("select `buildingName`,`buildingIsNavAble` from `Buildings`"))
                {
                    while (reader.Read())
                    {
                        buildings.Add(reader.GetString(0));
                        isNavAble.Add(reader.GetBoolean(1));
                    }
                }
            }
        }
    }
    class DBInteraction
    {
        public Map DownloadFromDB(string buildingName, ref int corridorCounter, bool isNav)
        {
            Map map = new Map();
            corridorCounter = -1;
            using (DB DataBase = new DB("Plans"))
            {
                string maxCorrName = "";
                map.Name = buildingName;
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
                                    if (connectivityCompIndex != -1)//ladders != null)
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
        public void UploadToDB(ref Map map, bool isNavAble)
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
                string command = "insert into `Nodes` values "; 
                int count = 0;
                foreach (Node tempNode in map.GetNodeList().Values) 
                {
                    ++count;
                    command += $"(null,'{building_ID}','{tempNode.name}','{tempNode.type}','{tempNode.description}'),";
                    if(count==1000)
                    {
                        DataBase.ExecuteCommand(command.Substring(0, command.Length - 1));
                        command = "insert into `Nodes` values ";
                        count = 0;
                    }
                }
                if(count!=0) DataBase.ExecuteCommand(command.Substring(0, command.Length - 1));
                #endregion
                Point coords;
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

                    command = "insert into `LevelNodes` values ";
                    string commandCC = "insert into `ConnectivityComponents` values ";
                    //List<int> nodeIDList = new List<int>();
                    count = 0;
                    int node_ID;
                    int iterator;

                    foreach (Node tempNode in i.GetNodeListOnFloor().Keys)
                    {
                        node_ID = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{tempNode.name}'"))
                        {
                            if (reader.Read()) node_ID = reader.GetInt32(0);//nodeIDList.Add(reader.GetInt32(0));//
                        }
                        coords = map.GetCoordOfNode(i.FloorIndex, tempNode);

                        ++count;
                        command += $"(null,'{level_ID}','{node_ID}','{coords.X}','{coords.Y}'),";

                        iterator = 0;
                        foreach (ConnectivityComp tempConnComp in i.GetConnectivityComponentsList())
                        {
                            if (tempConnComp.isContains(tempNode))
                                break;
                            ++iterator;
                        }
                        
                        commandCC += $"('{level_ID}','{iterator}','{node_ID}'),";

                        if(count==1000)
                        {
                            DataBase.ExecuteCommand(command.Substring(0,command.Length-1));
                            DataBase.ExecuteCommand(commandCC.Substring(0, commandCC.Length - 1));
                            command = "insert into `LevelNodes` values ";
                            commandCC = "insert into `ConnectivityComponents` values ";
                            count = 0;
                        }
                        //DataBase.ExecuteCommand($"insert into `ConnectivityComponents` values ('{level_ID}','{iterator}','{node_ID}')");
                    }
                    if(count!=0)
                    {
                        DataBase.ExecuteCommand(command.Substring(0, command.Length - 1));
                        DataBase.ExecuteCommand(commandCC.Substring(0, commandCC.Length - 1));
                    }
                    #endregion
                    #region // вставить рёбра
                    tempDictionary = new Dictionary<Node, List<Node>>(i.GetEdgesList());
                    removed = new Dictionary<Node, List<Node>>();

                    command = "insert into `Edges` values ";
                    count = 0;
                    foreach (Node nodeKey in tempDictionary.Keys)
                    {
                        int startNode_ID = -1;
                        //int endNode_ID = -1;
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `LevelNodes`.`id` from `LevelNodes` " +
                            $"inner join `Nodes` on `LevelNodes`.`node_ID`=`Nodes`.`id` " +
                            $"where `level_ID`='{level_ID}' and `Nodes`.`NodeName`='{nodeKey.name}'"))
                        {
                            if (reader.Read()) startNode_ID = reader.GetInt32(0);
                        }
                        List<int> connectedWithEdgeNodesList = new List<int>();
                        foreach (Node connectedNode in tempDictionary[nodeKey])
                        {
                            using (MySqlDataReader reader = DataBase.ExecuteReader($"select `LevelNodes`.`id` from `LevelNodes` " +
                                $"inner join `Nodes` on `LevelNodes`.`node_ID`=`Nodes`.`id` " +
                                $"where `level_ID`='{level_ID}' and `Nodes`.`NodeName`='{connectedNode.name}'"))
                            {
                                if (reader.Read()) /*endNode_ID = reader.GetInt32(0);*/ connectedWithEdgeNodesList.Add(reader.GetInt32(0));
                            }
                            tempDictionary[connectedNode].Remove(nodeKey);
                            if (!removed.ContainsKey(connectedNode)) removed.Add(connectedNode, new List<Node>());
                            removed[connectedNode].Add(nodeKey);
                        }

                        foreach (int endNode in connectedWithEdgeNodesList)
                        {
                            ++count;
                            command += $"('{level_ID}','{startNode_ID}','{endNode}'),";
                            if(count==1000)
                            {
                                DataBase.ExecuteCommand(command.Substring(0, command.Length - 1));
                                command = "insert into `Edges` values ";
                                count = 0;
                            }
                        }
                    }
                    if (count != 0) DataBase.ExecuteCommand(command.Substring(0, command.Length - 1));
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
