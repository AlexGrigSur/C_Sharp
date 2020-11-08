using NavTest;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using NavTestNoteBookNeConsolb.DrawingForms;
using System.Threading;

namespace NavTestNoteBookNeConsolb
{
    public partial class DrawingForm : Form
    {
        private DB DataBase = new DB("Plans");
        private Map obj;
        private string buildingName = "";

        private int Mode = -1; // -1 - ObserverMode, 0-AddNode, 1-EditNode, 2-DeleteNode, 3-AddEdge, 4 - Delete Edge

        private int radius = 10;
        private List<int> FirstPoint = new List<int>(); // for AddEdge/DeleteEdge

        private int panelX = 0;
        private int panelY = 0;

        private bool isChanges = false;
        private void Changes(bool flag)
        {
            if (!isChanges && flag) this.Text = this.Text.Substring(1);
            if (isChanges && !flag) this.Text = "*" + this.Text;
            isChanges = flag;
        }
        private bool resizeToLeft = false;
        private bool isGreyMode = false;
        private Bitmap SecondLayer = null;
        private Node FoundNode;
        public DrawingForm(string BuildingName)
        {
            this.Text = BuildingName + " drawing";
            obj = new Map();
            InitializeComponent();
            buildingName = BuildingName;
            ObservereMode();

            updateFromDB();
            updateLevelList();

            panelX = panel1.Width;
            panelY = panel1.Height;

            pictureBox1.Parent = panel1;
            pictureBox1.Location = Point.Empty;
            pictureBox1.Image = new Bitmap(panelX, panelY);
            pictureBox1.ClientSize = pictureBox1.Image.Size;

            isChanges = false;
        }

        private void updateLevelList()
        {
            ChooseLevelComboBox.Items.Clear();
            ChooseLevelComboBox.SelectedIndex = -1;
            foreach (Level i in obj.Floors.Values)
            {
                ChooseLevelComboBox.Items.Add(i.Name);
            }
            ChooseLevelComboBox.Items.Add("Новый этаж");
        }
        private void ChooseLevelComboBox_SelectedIndexChanged(object sender, EventArgs e) // addFloor
        {
            if (ChooseLevelComboBox.SelectedIndex == ChooseLevelComboBox.Items.Count - 1)
            {
                using (NewLevelAdd form = new NewLevelAdd())
                {
                    form.ShowDialog();
                    if (form.ContinueFlag)
                    {
                        obj.AddFloor(form.levelName, form.levelFloor);
                        obj.Floors[form.levelName].screenResX = panelX;
                        obj.Floors[form.levelName].screenResY = panelY;
                        updateLevelList();
                        ChooseLevelComboBox.SelectedIndex = ChooseLevelComboBox.Items.Count - 2;
                        isChanges = true;
                    }
                }
            }
            else
                LoadLevel();
        }

        private void LoadLevel()
        {
            Level floor = obj.Floors[ChooseLevelComboBox.Text];
            pictureBox1.Image = new Bitmap(floor.screenResX, floor.screenResY);
            panelX = floor.screenResX;
            panelY = floor.screenResY;

            foreach (Node nodeIter in floor.nodeListOnFloor.Keys) // draw all Nodes
                DrawNode(floor.nodeListOnFloor[nodeIter][0], floor.nodeListOnFloor[nodeIter][1], 0, 255, nodeIter.name);
            foreach (Node FirstNodeLine in obj.Floors[ChooseLevelComboBox.Text].edges.Keys) // draw All 
                foreach (Node SecondNodeLine in obj.Floors[ChooseLevelComboBox.Text].edges[FirstNodeLine])
                    DrawLine(floor.nodeListOnFloor[FirstNodeLine][0], floor.nodeListOnFloor[FirstNodeLine][1], floor.nodeListOnFloor[SecondNodeLine][0], floor.nodeListOnFloor[SecondNodeLine][1]);
            pictureBox1.Invalidate();
            ObservereMode();
            GreyMode(false);
        }
        private void updateFromDB()
        {
            obj = new Map();
            obj.name = buildingName;
            //// BETA
            #region // Levels
            using (MySqlDataReader reader = DataBase.ExecuteReader($"select `levelName`,`levelFloor`,`levelScreenResX`,`levelScreenResY` from `Levels` where `building_ID`=(select `id` from `Buildings` where `buildingName`='{buildingName}')"))
            {
                while (reader.Read())
                {
                    obj.Floors.Add(reader.GetString(0), new Level(reader.GetString(0), reader.GetInt32(1)));
                    obj.Floors[reader.GetString(0)].screenResX = reader.GetInt32(2);
                    obj.Floors[reader.GetString(0)].screenResY = reader.GetInt32(3);
                }
            }
            #endregion
            #region // Nodes
            using (MySqlDataReader reader = DataBase.ExecuteReader($"select `NodeName`,`NodeType`,`NodeDescription` from `Nodes` where `building_ID`=(select `id` from `Buildings` where `buildingName`='{buildingName}')"))
            {
                while (reader.Read())
                {
                    Node tempNode = new Node(reader.GetString(0), reader.GetInt32(1), reader.GetString(2));
                    obj.NodeList.Add(tempNode.name, tempNode);
                }
            }
            #endregion
            #region // LevelNodes/Edges
            foreach (Level i in obj.Floors.Values)
            {
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `Nds`.`NodeName`,`levelNodeCoordX`,`levelNodeCoordY` from `LevelNodes` `LN` inner join `Nodes` `Nds` on `Nds`.`id`=`LN`.`Node_ID` where `level_ID`=(select `id` from `Levels` where `levelName`='{i.Name}')"))
                {
                    while (reader.Read())
                    {
                        i.AddNode(obj.NodeList[reader.GetString(0)], reader.GetInt32(1), reader.GetInt32(2));
                        if (obj.NodeList[reader.GetString(0)].type >= 2)
                            obj.AddHyperGraphByConn(obj.NodeList[reader.GetString(0)]);
                    }
                }
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `Nds`.`NodeName`,`levelNodeCoordX`,`levelNodeCoordY` from `LevelNodes` `LN` inner join `Nodes` `Nds` on `Nds`.`id`=`LN`.`Node_ID` where `level_ID`=(select `id` from `Levels` where `levelName`='{i.Name}')"))
                {
                    while (reader.Read())
                    {
                        i.AddNode(obj.NodeList[reader.GetString(0)], reader.GetInt32(1), reader.GetInt32(2));
                        if (obj.NodeList[reader.GetString(0)].type >= 2)
                            obj.AddHyperGraphByConn(obj.NodeList[reader.GetString(0)]);
                    }
                }
                using (MySqlDataReader reader = DataBase.ExecuteReader($"select `First`.`NodeName`,`Second`.`NodeName` from `Edges` `EDG` inner join `Nodes` `First` on `First`.`id`=`EDG`.`startNode_ID` inner join `Nodes` `Second` on `Second`.`id`=`EDG`.`endNode_ID` where `level_ID`=(select `id` from `Levels` where `levelName`='{i.Name}')"))
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
            }
            #endregion
        }
        private void updateDB()
        {
            if (isChanges)
            {
                obj.NodesOptimizer();
                NavSavePrepear prepear = new NavSavePrepear(ref obj);
                #region // Удаление старых данных 
                DataBase.ExecuteCommand($"delete from `Buildings` where `buildingName`='{buildingName}'");
                #endregion
                #region // вставить здание
                DataBase.ExecuteCommand($"insert into `Buildings` values(null,'{buildingName}','{((prepear.isNavAble == 0) ? 1 : 0)}')"); // Вставить здание
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
                    //Dictionary<ConnectivityComp, int> ConnectivityComponentsList = new Dictionary<ConnectivityComp, int>();
                    #region // вставить этажи
                    DataBase.ExecuteCommand($"insert into `Levels` values(null,'{building_ID}','{i.Name}','{i.floor}','{i.screenResX}','{i.screenResY}')"); // добавить этаж
                    using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Levels` where `building_ID`='{buildingName}' and `levelName`='{i.Name}'")) // Вытащить этот id
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
                        using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{nodeKey}'"))
                        {
                            if (reader.Read()) startNode_ID = reader.GetInt32(0);
                        }
                        foreach (Node connectedNode in tempDictionary[nodeKey])
                        {
                            using (MySqlDataReader reader = DataBase.ExecuteReader($"select `id` from `Nodes` where `building_ID`='{building_ID}' and `NodeName`='{connectedNode}'"))
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
                LoadLevel();
                Changes(false);

                switch (prepear.isNavAble)
                {
                    case 0:
                        break;
                    case 1:
                        {
                            MessageBox.Show("Введенный план не является связным. Навигация пока невозможна");
                            break;
                        }
                    case 2:
                        {
                            MessageBox.Show("Введённый план не имеет входов. Навигация пока невозможна");
                            break;
                        }
                    case 3:
                        {
                            MessageBox.Show("Введенный план не является связным и не имеет выходов. Навигация пока невозможна");
                            break;
                        }
                }
            }

        }
        private void HighlighterNode(int X, int Y)
        {
            using (Graphics G = Graphics.FromImage(pictureBox1.Image))
            {
                Pen pen = new Pen(Color.Red);
                G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
            }
            pictureBox1.Invalidate();
        }
        private void HighlighterNode(List<int> coord)
        {
            using (Graphics G = Graphics.FromImage(pictureBox1.Image))
            {
                Pen pen = new Pen(Color.Red);
                G.DrawEllipse(pen, coord[0] - radius, coord[1] - radius, 2 * radius, 2 * radius);
            }
            pictureBox1.Invalidate();
        }

        private void DrawNode(int X, int Y, int mode = 0, int transparent = 255, string nodeName = "")
        {
            using (Graphics G = Graphics.FromImage(pictureBox1.Image))
            {
                Color colorFirst, colorSecond;
                if (mode == 0)
                {
                    colorFirst = Color.FromArgb(transparent, Color.Black);
                    colorSecond = Color.FromArgb(transparent, Color.Orange);
                }
                else
                {
                    colorFirst = Color.FromArgb(255, 255, 225); //Color.White;
                    colorSecond = colorFirst;
                }
                Pen pen = new Pen(colorFirst);
                Brush brush = new SolidBrush(colorFirst);
                G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
                G.FillEllipse(brush, X - radius, Y - radius, 2 * radius, 2 * radius);
                brush = new SolidBrush(colorSecond);
                G.FillRectangle(brush, X, Y, 1, 1);
                if (transparent == 255) G.DrawString(nodeName.Substring(0, ((nodeName.Length < 4) ? nodeName.Length : 4)), new Font("Microsoft Sans Serif", 15f), new SolidBrush(colorFirst), X + radius + 2, Y - radius);
            }
            pictureBox1.Invalidate();
        }
        private void DrawLine(int X1, int Y1, int X2, int Y2, int mode = 0)
        {
            using (Graphics G = Graphics.FromImage(pictureBox1.Image))
            {
                Color lineColor;
                if (mode == 0)
                    lineColor = Color.Purple;
                else
                    lineColor = Color.FromArgb(255, 255, 225);
                G.DrawLine(new Pen(lineColor), X1, Y1, X2, Y2);
            }
            List<Node> nodeList = obj.SearchNode(ChooseLevelComboBox.Text, X1, Y1, X2, Y2);
            DrawNode(obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[0]][0], obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[0]][1], 0, 255, nodeList[0].name);
            DrawNode(obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[1]][0], obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[1]][1], 0, 255, nodeList[1].name);
        }
        #region // Mode selection
        private void ObservereMode()
        {
            if (isGreyMode)
            {
                isGreyMode = false;
                GreyMode(false);
            }
            ModeStatusLable.Text = "Mode: Observere";
            PanelActivate(false);
            Mode = -1;
        }
        private void GreyMode(bool activated)
        {
            if (activated)
            {
                isGreyMode = true;
                SecondLayer = new Bitmap(pictureBox1.Image);
            }
            else
            {
                isGreyMode = false;
                if (SecondLayer != null) pictureBox1.Image = new Bitmap(SecondLayer);
                SecondLayer = null;
                pictureBox1.Invalidate();
            }
        }
        private void CreateNode_Click(object sender, EventArgs e)
        {
            if (Mode != 0)
            {
                PanelActivate(true);
                Mode = 0;
                ModeStatusLable.Text = "Mode: CreateNode";
                GreyMode(false);
                button1.Text = "Добавить Вершину";
            }
            else
                ObservereMode();
        }
        private void EditNode_Click(object sender, EventArgs e)
        {
            if (Mode != 1)
            {
                Mode = 1;
                PanelActivate(true);
                ModeStatusLable.Text = "Mode: EditNode";
                GreyMode(false);
                button1.Text = "Изменить Вершину";
            }
            else
                ObservereMode();
        }
        private void DeleteNode_Click(object sender, EventArgs e)
        {
            if (Mode != 2)
            {
                PanelActivate(false);
                Mode = 2;
                GreyMode(false);
                ModeStatusLable.Text = "Mode: DeleteNode";
                button1.Text = "Удалить Вершину";
            }
            else
                ObservereMode();
        }
        private void CreateEdge_Click(object sender, EventArgs e)
        {
            if (Mode != 3)
            {
                PanelActivate(false);
                Mode = 3;
                FirstPoint.Clear();
                if (isGreyMode) GreyMode(false);
                ModeStatusLable.Text = "Mode: CreateEdge";
                button1.Text = "Добавить Ребро";
            }
            else
                ObservereMode();
        }
        private void DeleteEdge_Click(object sender, EventArgs e)
        {
            if (Mode != 4)
            {
                PanelActivate(false);
                Mode = 4;
                FirstPoint.Clear();
                GreyMode(false);
                ModeStatusLable.Text = "Mode: DeleteEdge";
                button1.Text = "Удалить Ребро";
            }
            else
                ObservereMode();
        }
        #endregion

        private void SearchNodeInBase(List<int> observerNode)
        {
            if (observerNode.Count == 2)
            {
                Node tempNode = obj.SearchNode(ChooseLevelComboBox.Text, observerNode[0], observerNode[1])[0];
                textBox1.Text = tempNode.name;
                typeTB.Text = comboBox1.Items[tempNode.type].ToString();
                textBox2.Text = tempNode.description;
                textBox3.Text = Convert.ToString(observerNode[0]);
                textBox4.Text = Convert.ToString(observerNode[1]);
                if (Mode >= 1) FoundNode = tempNode;
            }
            else
            {
                textBox1.Text = "";
                typeTB.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) // Основная функция
        {
            if (ChooseLevelComboBox.SelectedIndex == -1 || ChooseLevelComboBox.SelectedIndex == ChooseLevelComboBox.Items.Count - 1)
            {
                MessageBox.Show("Работа без добавления новых уровней невозможна");
                return;
            }
            switch (Mode)
            {
                case -1:
                    {
                        SearchNodeInBase(SearchNodesOnScreen(e, radius));
                        break;
                    }
                case 0: // add Node // done
                    {
                        if (SearchNodesOnScreen(e, radius).Count != 2)
                        {
                            if (!isGreyMode)
                                GreyMode(true);
                            else
                                pictureBox1.Image = new Bitmap(SecondLayer);
                            pictureBox1.Invalidate();
                            DrawNode(e.X, e.Y, 0, 155);
                            textBox3.Text = e.X.ToString();
                            textBox4.Text = e.Y.ToString();
                        }
                        else
                            MessageBox.Show("На выбранном месте уже существует вершина");
                        break;
                    }
                case 1: // edit Node
                    {
                        List<int> nodeByCoord = SearchNodesOnScreen(e, radius);
                        if (nodeByCoord.Count == 2) // chooseNodeToEdit
                        {
                            if (!isGreyMode)
                            {
                                //isGreyMode = true;
                                SearchNodeInBase(nodeByCoord);
                                GreyMode(true);
                                pictureBox1.Invalidate();
                            }
                            else
                            {
                                pictureBox1.Image = new Bitmap(SecondLayer);
                                pictureBox1.Invalidate();
                                SearchNodeInBase(nodeByCoord);
                            }
                            HighlighterNode(nodeByCoord[0], nodeByCoord[1]);
                        }
                        else
                        {
                            if (textBox1.Text.Trim() != "") // if node was already choosen
                            {
                                pictureBox1.Image = new Bitmap(SecondLayer);
                                pictureBox1.Invalidate();
                                HighlighterNode(obj.GetCoordOfNode(ChooseLevelComboBox.Text, FoundNode));
                                DrawNode(e.X, e.Y, 0, 155);
                                textBox3.Text = e.X.ToString();
                                textBox4.Text = e.Y.ToString();
                            }
                        }
                        break;
                    }
                case 2: // delete node
                    {
                        List<int> nodeCoord = SearchNodesOnScreen(e, radius);
                        if (nodeCoord.Count != 0)
                        {
                            if (isGreyMode)
                            {
                                pictureBox1.Image = new Bitmap(SecondLayer);
                                HighlighterNode(nodeCoord);
                            }
                            else
                            {
                                GreyMode(true);
                                HighlighterNode(nodeCoord);
                            }
                            SearchNodeInBase(nodeCoord);
                        }
                        break;
                    }
                case 3:
                case 4: // add/delete edge
                    {
                        List<int> FindNode = SearchNodesOnScreen(e, radius);
                        if (FindNode.Count == 0) // Если не найдено вершин
                            return;
                        if (FirstPoint.Count == 0) // если первая не выбрана
                        {
                            FirstPoint = FindNode;

                            if (isGreyMode)
                                pictureBox1.Image = new Bitmap(SecondLayer);
                            else
                                GreyMode(true);

                            SearchNodeInBase(FindNode);

                            HighlighterNode(FirstPoint);

                            return;
                        }
                        else
                        {
                            if (FindNode[0] == FirstPoint[0] && FindNode[1] == FirstPoint[1])
                            {
                                FirstPoint.Clear();
                                GreyMode(false);
                                return;
                            }
                            else
                            {
                                pictureBox1.Image = new Bitmap(SecondLayer); // костыль
                                SearchNodeInBase(FindNode);

                                HighlighterNode(FirstPoint); // костыль №2
                                HighlighterNode(FindNode);
                            }
                        }
                        break;
                    }
            }
        }

        private void PanelActivate(bool isPanelActivated)
        {
            if (isPanelActivated)
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                if (Mode != 1)
                {
                    typeTB.Enabled = false;
                    typeTB.Visible = false;
                    comboBox1.Enabled = true;
                    comboBox1.Visible = true;
                }
                else
                {
                    typeTB.Enabled = true;
                    typeTB.Visible = true;
                    comboBox1.Enabled = false;
                    comboBox1.Visible = false;
                }
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                typeTB.Enabled = true;
                typeTB.Visible = true;
                typeTB.Text = "";
                comboBox1.Enabled = false;
                comboBox1.Visible = false;
            }
        }
        private List<int> SearchNodesOnScreen(MouseEventArgs e, int radius)
        {
            List<int> coord = new List<int>();
            bool returnFlag = false;
            using (Bitmap bmp = new Bitmap(pictureBox1.Image, panelX, panelY))
            {
                for (int i = e.Y - radius; i >= 0 && i <= panelY && i <= e.Y + radius && !returnFlag; ++i)
                    for (int j = e.X - radius; j >= 0 && j <= panelX && j <= e.X + radius; ++j)
                        if (bmp.GetPixel(j, i).ToArgb() == Color.Orange.ToArgb())
                        {
                            coord.Add(j);
                            coord.Add(i);
                            returnFlag = true;
                            break;
                        }
            }
            return coord;
        }
        #region // Form Events
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            bool ReWrite = false;
            if (panel1.Height > panelY)
            {
                panelY = panel1.Height;
                ReWrite = true;
            }
            if (panel1.Width > panelX)
            {
                panelX = panel1.Width;
                ReWrite = true;
            }
            if (ReWrite)
            {
                Bitmap bmp = new Bitmap(panelX, panelY);
                if (ChooseLevelComboBox.SelectedIndex != -1 && ChooseLevelComboBox.SelectedIndex != ChooseLevelComboBox.Items.Count - 1)
                {
                    obj.Floors[ChooseLevelComboBox.Text].screenResX = panelX;
                    obj.Floors[ChooseLevelComboBox.Text].screenResY = panelY;
                }
                using (Graphics G = Graphics.FromImage(bmp))
                {
                    if (isGreyMode)
                    {
                        Bitmap bmpSecond = new Bitmap(panelX, panelY);
                        using (Graphics G1 = Graphics.FromImage(bmpSecond))
                        {
                            if (resizeToLeft) G1.DrawImage(SecondLayer, panelX - pictureBox1.Image.Width, panelY - pictureBox1.Image.Height, pictureBox1.Width, pictureBox1.Height);
                            else G1.DrawImage(SecondLayer, 0, 0, pictureBox1.Width, pictureBox1.Height);
                            SecondLayer = bmpSecond;
                        }
                    }
                    if (resizeToLeft)
                    {
                        G.DrawImage(pictureBox1.Image, panelX - pictureBox1.Image.Width, panelY - pictureBox1.Image.Height, pictureBox1.Width, pictureBox1.Height);
                        resizeToLeft = false;
                    }
                    else
                        G.DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Width, pictureBox1.Height);
                }
                pictureBox1.Image = bmp;
            }
            if (pictureBox1.ClientSize.Width < panelX || pictureBox1.ClientSize.Height < panelY) pictureBox1.ClientSize = new Size(panelX, panelY);
            panel1.Invalidate();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, Point.Empty);
        }
        private void DrawToLeftToolStripButton_Click(object sender, EventArgs e) => resizeToLeft = !resizeToLeft;
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) => updateDB();
        #endregion

        private bool isNewLadder = true;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                List<Node> localLadderList = new List<Node>(obj.HyperGraphByConnectivity.Keys);
                for (int i = 0; i < localLadderList.Count; ++i)
                    if (obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor.ContainsKey(localLadderList[i]))
                    {
                        localLadderList.Remove(localLadderList[i]);
                        --i;
                    }
                LadderChoose form = new LadderChoose(localLadderList);
                form.ShowDialog();
                if (form.ContinueFlag)
                {
                    if (form.SelectedLadder == "Новая Лестница")
                        isNewLadder = true;
                    else
                    {
                        FoundNode = obj.NodeList[form.SelectedLadder];
                        textBox1.Text = FoundNode.name;
                        textBox1.ReadOnly = true;
                        comboBox1.SelectedIndex = FoundNode.type;
                        textBox2.Text = FoundNode.description;
                        textBox2.ReadOnly = true;
                    }
                }
            }
            else
                isNewLadder = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case 0: // addNode
                    {
                        if (textBox1.Text.Trim() == "" || comboBox1.SelectedIndex == -1)
                        {
                            MessageBox.Show("Заполните поля названия и/или типа");
                            return;
                        }
                        if (obj.NodeList.ContainsKey(textBox1.Text) && (comboBox1.SelectedIndex != 2 || isNewLadder))
                        {
                            MessageBox.Show("Вершина с данным именем уже существует. Измените его для продолжения работы");
                            return;
                        }
                        if (textBox3.Text.Trim() == "")
                        {
                            MessageBox.Show("Для вершины не заданы координаты. Отмена операции");
                            return;
                        }
                        if (comboBox1.SelectedIndex == 2 && !isNewLadder)
                        {
                            obj.AddNode(ChooseLevelComboBox.Text, FoundNode, Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
                            comboBox1.SelectedIndex = -1;
                        }
                        else
                        {
                            Node newNode = new Node(textBox1.Text, comboBox1.SelectedIndex, textBox2.Text);
                            obj.AddNode(ChooseLevelComboBox.Text, newNode, Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
                            comboBox1.SelectedIndex = -1;
                        }
                        pictureBox1.Image = new Bitmap(SecondLayer);
                        DrawNode(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text), 0, 255, textBox1.Text);
                        SecondLayer = new Bitmap(pictureBox1.Image);
                        Changes(true);
                        PanelActivate(true);
                        break;
                    }
                case 1: // EditNode
                    {
                        bool changeFlag = false;
                        string name, descr;
                        Node NewNode = FoundNode;
                        if (textBox1.Text != FoundNode.name)
                        {
                            if (obj.NodeList.ContainsKey(textBox1.Text.Trim()))
                            {
                                MessageBox.Show("Данный план уже содержит вершину с таким именем");
                                return;
                            }
                            name = textBox1.Text;
                            changeFlag = true;
                        }
                        else
                            name = FoundNode.name;
                        if (textBox2.Text != FoundNode.description)
                        {
                            descr = textBox2.Text;
                            changeFlag = true;
                        }
                        else
                            descr = FoundNode.description;

                        if (changeFlag)
                        {
                            NewNode = new Node(name, FoundNode.type, descr);
                            obj.EditNode(ChooseLevelComboBox.Text, FoundNode.name, NewNode);
                        }

                        if (isGreyMode)
                        {
                            List<int> tempCoord = obj.GetCoordOfNode(ChooseLevelComboBox.Text, NewNode);
                            if (tempCoord[0] != Convert.ToInt32(textBox3.Text) || tempCoord[1] != Convert.ToInt32(textBox4.Text))
                                obj.Floors[ChooseLevelComboBox.Text].NodeCoordChange(NewNode, Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
                            changeFlag = true;
                        }
                        if (changeFlag)
                        {
                            Changes(true);
                            LoadLevel();
                            //SecondLayer = new Bitmap(pictureBox1.Image);
                            ///*///////////////////////////////////////////////////*/pictureBox1.Invalidate(); /////////////////////////////////////////////////////////////////////
                        }
                        break;
                    }
                case 2: // deleteNode
                    {
                        if (isGreyMode)
                        {
                            Changes(true);
                            //Node tempNode = obj.SearchNode(ChooseLevelComboBox.Text, FoundNode[0], nodeCoord[1])[0];
                            GreyMode(false);
                            obj.RemoveNode(ChooseLevelComboBox.Text, FoundNode);//tempNode.name);
                            LoadLevel();
                            textBox1.Text = "";
                            typeTB.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                        }
                        break;
                    }
                case 3: // AddEdge
                case 4: // DeleteEdge
                    {
                        if (FirstPoint.Count != 0)
                        {
                            List<int> foundNodeCoord = obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[FoundNode]; // second Node
                            if (foundNodeCoord[0] != FirstPoint[0] && foundNodeCoord[1] != FirstPoint[1])
                            {
                                Node FirstNode = obj.SearchNode(ChooseLevelComboBox.Text, FirstPoint[0], FirstPoint[1])[0];
                                if (Mode == 3 && !obj.isEdgeExists(ChooseLevelComboBox.Text, FirstNode, FoundNode))
                                {
                                    GreyMode(false);
                                    obj.AddEdge(ChooseLevelComboBox.Text, new List<Node> { FirstNode, FoundNode });
                                    DrawLine(FirstPoint[0], FirstPoint[1], foundNodeCoord[0], foundNodeCoord[1]);
                                    FirstPoint.Clear();
                                    Changes(true);
                                }
                                else
                                {
                                    if (Mode == 4 && obj.isEdgeExists(ChooseLevelComboBox.Text, FirstNode, FoundNode))
                                    {
                                        GreyMode(false);
                                        obj.RemoveEdge(ChooseLevelComboBox.Text, new List<Node> { FirstNode, FoundNode });
                                        LoadLevel();//DrawLine(FirstPoint[0], FirstPoint[1], foundNodeCoord[0], foundNodeCoord[1]);
                                        FirstPoint.Clear();
                                        Changes(true);
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) // Удаление уровня
        {
            if (ChooseLevelComboBox.SelectedIndex != -1)
            {
                DialogResult result = MessageBox.Show("Вы уверены?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    isChanges = true;
                    obj.Floors.Remove(ChooseLevelComboBox.Text);
                    ChooseLevelComboBox.Items.Remove(ChooseLevelComboBox.Text);
                    if (ChooseLevelComboBox.Items.Count > 0)
                        ChooseLevelComboBox.SelectedIndex = 0;
                    else
                    {
                        ObservereMode();
                        GreyMode(false);
                        //updateFromDB();

                        updateLevelList();

                        panelX = panel1.Width;
                        panelY = panel1.Height;

                        pictureBox1.Parent = panel1;
                        pictureBox1.Location = Point.Empty;
                        pictureBox1.Image = new Bitmap(panelX, panelY);
                        pictureBox1.ClientSize = pictureBox1.Image.Size;
                    }
                }
            }
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e) // Clear
        {
            DialogResult result = MessageBox.Show("Вы уверены?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (Node i in obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor.Keys)
                {
                    if (i.type >= 2)
                        obj.RemoveNode(ChooseLevelComboBox.Text, i);
                }
                obj.Floors[ChooseLevelComboBox.Text].nodeListOnFloor.Clear();
                obj.Floors[ChooseLevelComboBox.Text].edges.Clear();
                isChanges = true;
            }
        }
    }
}