using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavProject_Drawing.Structures;

namespace NavProject_Drawing.MainForm
{
    class MainFormHandler
    {
        public Point? SearchNodeOnScreen()
        {
            return null;
        }
    }

    public partial class DrawingForm
    {
        static private int radius = 10;
        private DrawClass draw = new DrawClass(radius);
        private Map map;
        private string buildingName = "";

        private int Mode = -1; // -1 - ObserverMode, 0-AddNode, 1-EditNode, 2-DeleteNode, 3-AddEdge, 4 - Delete Edge
        private int currentLevel = -1;

        private List<int> NodeCoordList = new List<int>(); // for AddEdge/DeleteEdge

        private int panelX = 0;
        private int panelY = 0;

        private bool isChanges = false;
        private void Changes(bool flag)
        {
            //if (!flag && this.Text[0] == '*') this.Text = this.Text.Substring(1);
            //if (flag && this.Text[0] != '*') this.Text = '*' + this.Text;
            isChanges = flag;
        }

        private bool resizeToLeft = false;
        private bool isGreyMode = false;

        public DrawingForm(string BuildingName)
        {
            map = new Map();

            buildingName = BuildingName;
            //ObservereMode();
            //updateLevelList();
        }
        //    panelX = panel1.Width;
        //    panelY = panel1.Height;

        //    nodeGeneratorToolStripMenuItem.Visible = false;
        //    DrawToLeftToolStripButton.Visible = false;

        //    pictureBox1.Parent = panel1;
        //    pictureBox1.Location = Point.Empty;
        //    pictureBox1.Image = new Bitmap(panelX, panelY);
        //    pictureBox1.ClientSize = pictureBox1.Image.Size;

        //    this.Text = BuildingName + " drawing";

        //    if (ChooseLevelComboBox.Items.Count > 1)
        //        ChooseLevelComboBox.SelectedIndex = 0;

        //    MainActivityButton.Text = "";
        //}

        //private void updateLevelList()
        //{
        //    ChooseLevelComboBox.Items.Clear();
        //    ChooseLevelComboBox.SelectedIndex = -1;
        //    List<int> levelListToSort = new List<int>();

        //    foreach (Level i in map.GetFloorsList().Values)
        //        levelListToSort.Add(i.FloorIndex);

        //    levelListToSort.Sort();

        //    foreach (int i in levelListToSort)
        //        ChooseLevelComboBox.Items.Add(i);

        //    ChooseLevelComboBox.Items.Add("Новый этаж");
        //}

        //private void ChooseLevelComboBox_SelectedIndexChanged(object sender, EventArgs e) // addFloor
        //{
        //    if (ChooseLevelComboBox.SelectedIndex == ChooseLevelComboBox.Items.Count - 1)
        //    {
        //        List<int> currentLevelList = new List<int>();
        //        foreach (Level i in map.GetFloorsList().Values)
        //            currentLevelList.Add(i.FloorIndex);
        //        using (NewLevelAdd form = new NewLevelAdd(currentLevelList))
        //        {
        //            form.ShowDialog();
        //            if (form.ContinueFlag)
        //            {
        //                map.AddFloor(form.levelFloor);
        //                map.GetFloor(form.levelFloor).ScreenResX = panelX;
        //                map.GetFloor(form.levelFloor).ScreenResY = panelY;
        //                updateLevelList();
        //                ChooseLevelComboBox.SelectedIndex = ChooseLevelComboBox.Items.Count - 2;
        //                Changes(true);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        currentLevel = Convert.ToInt32(ChooseLevelComboBox.Text);
        //        LoadLevel();
        //    }
        //}
        //private void LoadLevel()
        //{
        //    pictureBox1.Image = new Bitmap(draw.LoadLevel(currentLevel, ref map, out panelX, out panelY));
        //    pictureBox1.Invalidate();
        //    SecondLayer = new Bitmap(pictureBox1.Image);
        //    GreyMode(false);
        //    ObservereMode();
        //    if (panelX < pictureBox1.Width || panelY < pictureBox1.Height) Form1_ResizeEnd(null, null);
        //    GC.Collect();
        //}

        //#region //Drawing
        ////private void HighlighterNode(int X, int Y)
        ////{
        ////    pictureBox1.Image = new Bitmap(draw.HighlighterNode(pictureBox1.Image, X, Y));
        ////    pictureBox1.Invalidate();
        ////}
        ////private void HighlighterNode(Point coord) => HighlighterNode(coord.X, coord.Y);
        ////private void DrawNode(int X, int Y, int transparent = 255, string nodeName = "")
        ////{
        ////    pictureBox1.Image = new Bitmap(draw.DrawNode(pictureBox1.Image, X, Y, transparent, nodeName));
        ////    pictureBox1.Invalidate();
        ////}
        ////private void DrawLine(int X1, int Y1, Node firstNode, int X2, int Y2, Node secondNode)
        ////{

        ////    string firstNodeName = (firstNode.type != 0) ? firstNode.name : "";
        ////    string secondNodeName = (secondNode.type != 0) ? secondNode.name : "";

        ////    pictureBox1.Image = new Bitmap(draw.DrawLine(pictureBox1.Image, X1, Y1, firstNodeName, X2, Y2, secondNodeName));
        ////    pictureBox1.Invalidate();
        ////}
        //#endregion
        //#region // Mode selection //????//
        //private void ObservereMode()
        //{
        //    if (isGreyMode)
        //    {
        //        isGreyMode = false;
        //        GreyMode(false);
        //    }
        //    ClearFields();
        //    ModeStatusLable.Text = "Mode: Observere";
        //    PanelActivate(false);
        //    Mode = -1;
        //}
        //private void GreyMode(bool activated)
        //{
        //    if (activated)
        //    {
        //        isGreyMode = true;
        //        SecondLayer = new Bitmap(pictureBox1.Image);
        //    }
        //    else
        //    {
        //        isGreyMode = false;
        //        if (SecondLayer != null) pictureBox1.Image = new Bitmap(SecondLayer);
        //        SecondLayer = null;
        //        pictureBox1.Invalidate();
        //    }
        //}
        //private void CreateNode_Click(object sender, EventArgs e)
        //{
        //    if (Mode != 0)
        //    {
        //        PanelActivate(true);
        //        Mode = 0;
        //        ModeStatusLable.Text = "Mode: CreateNode";
        //        GreyMode(false);
        //        MainActivityButton.Text = "Добавить Вершину";
        //    }
        //    else
        //        ObservereMode();
        //}
        //private void EditNode_Click(object sender, EventArgs e)
        //{
        //    if (Mode != 1)
        //    {
        //        Mode = 1;
        //        PanelActivate(true);
        //        ModeStatusLable.Text = "Mode: EditNode";
        //        GreyMode(false);
        //        MainActivityButton.Text = "Изменить Вершину";
        //    }
        //    else
        //        ObservereMode();
        //}
        //private void DeleteNode_Click(object sender, EventArgs e)
        //{
        //    if (Mode != 2)
        //    {
        //        PanelActivate(false);
        //        Mode = 2;
        //        GreyMode(false);
        //        ModeStatusLable.Text = "Mode: DeleteNode";
        //        MainActivityButton.Text = "Удалить Вершину";
        //    }
        //    else
        //        ObservereMode();
        //}
        //private void CreateEdge_Click(object sender, EventArgs e)
        //{
        //    if (Mode != 3)
        //    {
        //        PanelActivate(false);
        //        Mode = 3;
        //        NodeCoordList.Clear();
        //        if (isGreyMode) GreyMode(false);
        //        ModeStatusLable.Text = "Mode: CreateEdge";
        //        MainActivityButton.Text = "Добавить Ребро";
        //    }
        //    else
        //        ObservereMode();
        //}
        //private void DeleteEdge_Click(object sender, EventArgs e)
        //{
        //    if (Mode != 4)
        //    {
        //        PanelActivate(false);
        //        Mode = 4;
        //        NodeCoordList.Clear();
        //        GreyMode(false);
        //        ModeStatusLable.Text = "Mode: DeleteEdge";
        //        MainActivityButton.Text = "Удалить Ребро";
        //    }
        //    else
        //        ObservereMode();
        //}
        //#endregion

        //private void NameDescriptionFieldsVisible(bool flag)
        //{
        //    labelNameInOutput.Visible = flag;
        //    labelDescriptionInOutput.Visible = flag;
        //    textBoxNameInOutput.Visible = flag;
        //    textBoxDescriptionInOutput.Visible = flag;
        //}
        //private void AllFieldsReadOnly(bool flag)
        //{
        //    textBoxNameInOutput.ReadOnly = flag;
        //    textBoxDescriptionInOutput.ReadOnly = flag;
        //    textBoxXInOutput.ReadOnly = flag;
        //    textBoxYInOutput.ReadOnly = flag;
        //}
        //private void ClearFields()
        //{
        //    textBoxNameInOutput.Text = "";
        //    textBoxTypeInOutput.Text = "";
        //    textBoxDescriptionInOutput.Text = "";
        //    textBoxXInOutput.Text = "";
        //    textBoxYInOutput.Text = "";
        //}
        //private void SearchNodeInBase(List<int> observerNode)
        //{

        //    if (observerNode.Count == 2)
        //    {
        //        Node tempNode = map.SearchNode(currentLevel, observerNode[0], observerNode[1])[0];
        //        if (tempNode.type == 0)
        //            NameDescriptionFieldsVisible(false);
        //        else
        //            NameDescriptionFieldsVisible(true);
        //        if (Mode >= 1) FoundNode = tempNode;
        //    }
        //    else
        //        ClearFields();
        //}
        //private void pictureBox1_MouseClick(object sender, MouseEventArgs e) // Основная функция
        //{
        //    if (ChooseLevelComboBox.SelectedIndex == -1 || ChooseLevelComboBox.SelectedIndex == ChooseLevelComboBox.Items.Count - 1)
        //    {
        //        MessageBox.Show("Работа без добавления новых уровней невозможна");
        //        return;
        //    }
        //    switch (Mode)
        //    {
        //        case -1:
        //            {
        //                SearchNodeInBase(SearchNodesOnScreen(e, radius));
        //                break;
        //            }
        //        case 0: // add Node // done
        //            {
        //                if (SearchNodesOnScreen(e, radius).Count != 2)
        //                {
        //                    if (!isGreyMode)
        //                        GreyMode(true);
        //                    else
        //                        pictureBox1.Image = new Bitmap(SecondLayer);
        //                    pictureBox1.Invalidate();
        //                    DrawNode(e.X, e.Y, 155);
        //                    textBoxXInOutput.Text = e.X.ToString();
        //                    textBoxYInOutput.Text = e.Y.ToString();
        //                }
        //                else
        //                    MessageBox.Show("На выбранном месте уже существует вершина");
        //                break;
        //            }
        //        case 1: // edit Node
        //            {
        //                List<int> nodeByCoord = SearchNodesOnScreen(e, radius);
        //                if (nodeByCoord.Count == 2) // chooseNodeToEdit
        //                {
        //                    if (!isGreyMode)
        //                    {
        //                        SearchNodeInBase(nodeByCoord);
        //                        GreyMode(true);
        //                        pictureBox1.Invalidate();
        //                    }
        //                    else
        //                    {
        //                        pictureBox1.Image = new Bitmap(SecondLayer);
        //                        pictureBox1.Invalidate();
        //                        SearchNodeInBase(nodeByCoord);
        //                    }
        //                    HighlighterNode(nodeByCoord[0], nodeByCoord[1]);
        //                }
        //                else
        //                {
        //                    if (textBoxNameInOutput.Text.Trim() != "") // if node was already choosen
        //                    {
        //                        pictureBox1.Image = new Bitmap(SecondLayer);
        //                        pictureBox1.Invalidate();
        //                        HighlighterNode(map.GetCoordOfNode(currentLevel, FoundNode));
        //                        DrawNode(e.X, e.Y, 155);
        //                        textBoxXInOutput.Text = e.X.ToString();
        //                        textBoxYInOutput.Text = e.Y.ToString();
        //                    }
        //                }
        //                break;
        //            }
        //        case 2: // delete node
        //            {
        //                List<int> nodeCoord = SearchNodesOnScreen(e, radius);
        //                if (nodeCoord.Count != 0)
        //                {
        //                    if (isGreyMode)
        //                    {
        //                        pictureBox1.Image = new Bitmap(SecondLayer);
        //                        HighlighterNode(nodeCoord[0], nodeCoord[1]);
        //                    }
        //                    else
        //                    {
        //                        GreyMode(true);
        //                        HighlighterNode(nodeCoord[0], nodeCoord[1]);
        //                    }
        //                    SearchNodeInBase(nodeCoord);
        //                }
        //                break;
        //            }
        //        case 3:
        //        case 4: // add/delete edge
        //            {
        //                List<int> FindNode = SearchNodesOnScreen(e, radius);
        //                if (FindNode.Count == 0) // Если не найдено вершин
        //                    return;
        //                if (NodeCoordList.Count == 0) // если первая не выбрана
        //                {
        //                    NodeCoordList = FindNode;

        //                    if (isGreyMode)
        //                        pictureBox1.Image = new Bitmap(SecondLayer);
        //                    else
        //                        GreyMode(true);

        //                    SearchNodeInBase(FindNode);

        //                    HighlighterNode(NodeCoordList[0], NodeCoordList[1]);

        //                    return;
        //                }
        //                else
        //                {
        //                    if (FindNode[0] == NodeCoordList[0] && FindNode[1] == NodeCoordList[1])
        //                    {
        //                        NodeCoordList.Clear();
        //                        GreyMode(false);
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        pictureBox1.Image = new Bitmap(SecondLayer);
        //                        SearchNodeInBase(FindNode);

        //                        HighlighterNode(NodeCoordList[0], NodeCoordList[1]);
        //                        HighlighterNode(FindNode[0], FindNode[1]);
        //                    }
        //                }
        //                break;
        //            }
        //    }
        //}
        //private void PanelActivate(bool isPanelActivated)
        //{
        //    void IsCheckBoxVisible(bool flag)
        //    {
        //        comboBoxTypeInOutput.Visible = flag;
        //        textBoxTypeInOutput.Visible = !flag;
        //    }

        //    NameDescriptionFieldsVisible(true);
        //    if (isPanelActivated)
        //    {
        //        AllFieldsReadOnly(false);
        //        ClearFields();
        //        IsCheckBoxVisible(Mode != 1);
        //    }
        //    else
        //    {
        //        AllFieldsReadOnly(true);
        //        IsCheckBoxVisible(false);
        //        textBoxTypeInOutput.Text = "";
        //    }
        //} //Стало получше// Чуть меньший Ужас// Ужас /////////////////
        //private List<int> SearchNodesOnScreen(MouseEventArgs e, int radius) => draw.SearchNodesOnScreen(pictureBox1.Image, e.X, e.Y, radius);
        //#region // Form Events
        //private void Form1_ResizeEnd(object sender, EventArgs e)
        //{
        //    bool ReWrite = false;
        //    if (panel1.Height > panelY)
        //    {
        //        panelY = panel1.Height;
        //        ReWrite = true;
        //    }
        //    if (panel1.Width > panelX)
        //    {
        //        panelX = panel1.Width;
        //        ReWrite = true;
        //    }
        //    if (ReWrite || isNodeGenerator)
        //    {
        //        Bitmap bmp = new Bitmap(panelX, panelY);
        //        if (ChooseLevelComboBox.SelectedIndex != -1 && ChooseLevelComboBox.SelectedIndex != ChooseLevelComboBox.Items.Count - 1)
        //        {
        //            map.GetFloor(currentLevel).ScreenResX = panelX;
        //            map.GetFloor(currentLevel).ScreenResY = panelY;
        //        }
        //        using (Graphics G = Graphics.FromImage(bmp))
        //        {
        //            if (isGreyMode)
        //            {
        //                Bitmap bmpSecond = new Bitmap(panelX, panelY);
        //                using (Graphics G1 = Graphics.FromImage(bmpSecond))
        //                {
        //                    if (resizeToLeft) G1.DrawImage(SecondLayer, panelX - pictureBox1.Image.Width, panelY - pictureBox1.Image.Height, pictureBox1.Width, pictureBox1.Height);
        //                    else G1.DrawImage(SecondLayer, 0, 0, pictureBox1.Width, pictureBox1.Height);
        //                    SecondLayer = bmpSecond;
        //                }
        //            }
        //            if (resizeToLeft)
        //            {
        //                List<Node> NodeList = new List<Node>(map.GetFloor(currentLevel).GetNodeListOnFloor().Keys);
        //                foreach (Node i in NodeList)
        //                {
        //                    Point coords = map.GetFloor(currentLevel).GetNodeOnFloor(i);
        //                    map.EditNode(currentLevel, i.name, /*i, */ i.name, i.description, coords.X + (panelX - pictureBox1.Width), coords.Y + (panelY - pictureBox1.Height));
        //                }
        //                G.DrawImage(pictureBox1.Image, panelX - pictureBox1.Image.Width, panelY - pictureBox1.Image.Height, pictureBox1.Width, pictureBox1.Height);
        //                resizeToLeft = false;
        //            }
        //            else
        //                G.DrawImage(pictureBox1.Image, 0, 0, pictureBox1.Width, pictureBox1.Height);
        //        }
        //        pictureBox1.Image = bmp;
        //    }
        //    if (pictureBox1.ClientSize.Width < panelX || pictureBox1.ClientSize.Height < panelY) pictureBox1.ClientSize = new Size(panelX, panelY);
        //    panel1.Invalidate();
        //}
        //private void panel1_Paint(object sender, PaintEventArgs e) => e.Graphics.DrawImage(pictureBox1.Image, Point.Empty);
        //private void DrawToLeftToolStripButton_Click(object sender, EventArgs e) => resizeToLeft = !resizeToLeft;
        ////private void saveToolStripMenuItem_Click(object sender, EventArgs e) => UploadToDB();
        //#endregion
        //private bool isNewLadder = true;
        //private void comboBoxTypeInOutput_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (comboBoxTypeInOutput.SelectedIndex == 0)
        //        NameDescriptionFieldsVisible(false);
        //    else
        //    {
        //        NameDescriptionFieldsVisible(true);

        //        if (comboBoxTypeInOutput.SelectedIndex == 2)
        //        {
        //            List<Node> localLadderList = new List<Node>(map.HyperGraphInstance.GetKeys());
        //            for (int i = 0; i < localLadderList.Count; ++i)
        //                if (map.GetFloor(currentLevel).IsNodeContains(localLadderList[i]))//(map.GetFloor(currentLevel).GetNodeListOnFloor().ContainsKey(localLadderList[i]))
        //                {
        //                    localLadderList.Remove(localLadderList[i]);
        //                    --i;
        //                }
        //            LadderChoose form = new LadderChoose(localLadderList);
        //            form.ShowDialog();
        //            if (form.ContinueFlag)
        //            {
        //                if (form.SelectedLadder == "Новая Лестница")
        //                    isNewLadder = true;
        //                else
        //                {
        //                    FoundNode = map.GetNode(form.SelectedLadder);
        //                    textBoxNameInOutput.Text = FoundNode.name;
        //                    textBoxNameInOutput.ReadOnly = true;
        //                    comboBoxTypeInOutput.SelectedIndex = FoundNode.type;
        //                    textBoxDescriptionInOutput.Text = FoundNode.description;
        //                    textBoxDescriptionInOutput.ReadOnly = true;
        //                    isNewLadder = false;
        //                }
        //            }
        //            else
        //                comboBoxTypeInOutput.SelectedIndex = -1;
        //        }
        //        else
        //            isNewLadder = false;
        //    }
        //}
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    switch (Mode)
        //    {
        //        case 0: // addNode
        //            {
        //                if (comboBoxTypeInOutput.SelectedIndex != 0)
        //                {
        //                    if (textBoxNameInOutput.Text.Trim() == "" || comboBoxTypeInOutput.SelectedIndex == -1)
        //                    {
        //                        MessageBox.Show("Заполните поля названия и/или типа");
        //                        return;
        //                    }
        //                    if (map.GetNodeList().ContainsKey(textBoxNameInOutput.Text) && (comboBoxTypeInOutput.SelectedIndex != 2 || isNewLadder) && !(comboBoxTypeInOutput.SelectedIndex == 2 && !isNewLadder))
        //                    {
        //                        MessageBox.Show("Вершина с данным именем уже существует. Измените его для продолжения работы");
        //                        return;
        //                    }
        //                    if (textBoxXInOutput.Text.Trim() == "")
        //                    {
        //                        MessageBox.Show("Для вершины не заданы координаты. Отмена операции");
        //                        return;
        //                    }
        //                }
        //                if (comboBoxTypeInOutput.SelectedIndex == 2 && !isNewLadder)
        //                {
        //                    map.AddNode(currentLevel, FoundNode, Convert.ToInt32(textBoxXInOutput.Text), Convert.ToInt32(textBoxYInOutput.Text));
        //                    comboBoxTypeInOutput.SelectedIndex = -1;
        //                }
        //                else
        //                {
        //                    string NodeName = "";
        //                    string description = "";
        //                    if (comboBoxTypeInOutput.SelectedIndex == 0)
        //                        NodeName = "CorridorStandartName#_" + map.corridorCouner;
        //                    else
        //                    {
        //                        NodeName = textBoxNameInOutput.Text;
        //                        description = textBoxDescriptionInOutput.Text;
        //                    }
        //                    Node newNode = new Node(NodeName, comboBoxTypeInOutput.SelectedIndex, description);
        //                    map.AddNode(currentLevel, newNode, Convert.ToInt32(textBoxXInOutput.Text), Convert.ToInt32(textBoxYInOutput.Text));
        //                    comboBoxTypeInOutput.SelectedIndex = -1;
        //                }
        //                pictureBox1.Image = new Bitmap(SecondLayer);

        //                if (comboBoxTypeInOutput.SelectedIndex == 0)
        //                    DrawNode(Convert.ToInt32(textBoxXInOutput.Text), Convert.ToInt32(textBoxYInOutput.Text), 255);
        //                else
        //                    DrawNode(Convert.ToInt32(textBoxXInOutput.Text), Convert.ToInt32(textBoxYInOutput.Text), 255, textBoxNameInOutput.Text);
        //                SecondLayer = new Bitmap(pictureBox1.Image);
        //                Changes(true);
        //                PanelActivate(true);
        //                break;
        //            }
        //        case 1: // EditNode
        //            {
        //                bool changeFlag = false;
        //                string name, descr;
        //                Node NewNode = FoundNode;
        //                if (textBoxNameInOutput.Text != FoundNode.name)
        //                {
        //                    if (map.GetNodeList().ContainsKey(textBoxNameInOutput.Text.Trim()))
        //                    {
        //                        MessageBox.Show("Данный план уже содержит вершину с таким именем");
        //                        return;
        //                    }
        //                    name = textBoxNameInOutput.Text;
        //                    changeFlag = true;
        //                }
        //                else
        //                    name = FoundNode.name;
        //                if (textBoxDescriptionInOutput.Text != FoundNode.description)
        //                {
        //                    descr = textBoxDescriptionInOutput.Text;
        //                    changeFlag = true;
        //                }
        //                else
        //                    descr = FoundNode.description;

        //                if (changeFlag)
        //                    map.EditNode(currentLevel, name, FoundNode.name, descr);

        //                if (isGreyMode)
        //                {
        //                    Point tempCoord = map.GetCoordOfNode(currentLevel, NewNode);
        //                    if (tempCoord.X != Convert.ToInt32(textBoxXInOutput.Text) || tempCoord.Y != Convert.ToInt32(textBoxYInOutput.Text))
        //                        map.GetFloor(currentLevel).NodeCoordChange(NewNode, Convert.ToInt32(textBoxXInOutput.Text), Convert.ToInt32(textBoxYInOutput.Text));
        //                    changeFlag = true;
        //                }
        //                if (changeFlag)
        //                {
        //                    LoadLevel();
        //                    SecondLayer = new Bitmap(pictureBox1.Image);
        //                    Changes(true);
        //                }
        //                break;
        //            }
        //        case 2: // deleteNode
        //            {
        //                if (isGreyMode)
        //                {
        //                    Changes(true);
        //                    GreyMode(false);
        //                    map.RemoveNode(currentLevel, FoundNode);
        //                    LoadLevel();
        //                }
        //                break;
        //            }
        //        case 3: // AddEdge
        //        case 4: // DeleteEdge
        //            {
        //                if (NodeCoordList.Count != 0)
        //                {
        //                    Point foundNodeCoord = map.GetFloor(currentLevel).GetNodeOnFloor(FoundNode); // second Node
        //                    if (foundNodeCoord.X != NodeCoordList[0] || foundNodeCoord.Y != NodeCoordList[1])
        //                    {
        //                        Node FirstNode = map.SearchNode(currentLevel, NodeCoordList[0], NodeCoordList[1])[0];
        //                        if (Mode == 3 && !map.isEdgeExists(currentLevel, FirstNode, FoundNode))
        //                        {
        //                            GreyMode(false);
        //                            map.AddEdge(currentLevel, new List<Node> { FirstNode, FoundNode });
        //                            DrawLine(NodeCoordList[0], NodeCoordList[1], FirstNode, foundNodeCoord.X, foundNodeCoord.Y, FoundNode);
        //                            NodeCoordList.Clear();
        //                            Changes(true);
        //                        }
        //                        else
        //                        {
        //                            if (Mode == 4 && map.isEdgeExists(currentLevel, FirstNode, FoundNode))
        //                            {
        //                                GreyMode(false);
        //                                map.RemoveEdge(currentLevel, new List<Node> { FirstNode, FoundNode });
        //                                LoadLevel();
        //                                NodeCoordList.Clear();
        //                                Changes(true);
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //    }
        //}

        //private bool isNodeGenerator = false;
        //private async void nodeGeneratorToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    void blockControl(bool flag)
        //    {
        //        ObservereMode();

        //        DeleteLevelToolStripButton.Enabled = !flag;
        //        clearToolStripMenuItem.Enabled = !flag;
        //        saveToolStripMenuItem.Enabled = !flag;
        //        CreateEdge.Enabled = !flag;
        //        CreateNode.Enabled = !flag;
        //        EditNode.Enabled = !flag;
        //        DeleteNode.Enabled = !flag;
        //        DeleteEdge.Enabled = !flag;
        //        MainActivityButton.Enabled = !flag;
        //        ChooseLevelComboBox.Enabled = !flag;
                
        //        if (flag) ModeStatusLable.Text = "Происходит процесс генерации плана. Подождите, пожалуйста";
        //    }

        //    if (ChooseLevelComboBox.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Добавьте уровень для продолжения");
        //        return;
        //    }
        //    int count = 3600;
        //    if (count > 0) Changes(true);
        //    int sqrt = Convert.ToInt32(Math.Sqrt(count));

        //    panelX = sqrt * 50 + 200;
        //    panelY = panelX;

        //    isNodeGenerator = true;
        //    Form1_ResizeEnd(null, null);
        //    isNodeGenerator = false;

        //    blockControl(true);
        //    await Task.Run(() =>
        //    {
        //        for (int i = 0; i < sqrt; ++i)
        //        {
        //            for (int j = 0; j < sqrt; ++j)
        //            {
        //                Node newNode = new Node($"CorridorStandartName#_{map.corridorCouner}", 0, "");
        //                map.AddNode(currentLevel, newNode, 50 + i * 50, 50 + j * 50);
        //            }
        //        }

        //        for (int i = 0; i < sqrt; ++i)
        //        {
        //            for (int j = 0; j < sqrt; ++j)
        //            {
        //                NodeCoordList = new List<int>() { 50 + i * 50, 50 + j * 50 };
        //                Node FirstNode = map.SearchNode(currentLevel, NodeCoordList[0], NodeCoordList[1])[0];
        //                if (i != sqrt - 1)
        //                {
        //                    FoundNode = map.SearchNode(currentLevel, 50 + (i + 1) * 50, 50 + j * 50)[0];
        //                    map.AddEdge(currentLevel, new List<Node> { FirstNode, FoundNode });
        //                }
        //                if (j != sqrt - 1)
        //                {
        //                    NodeCoordList = new List<int>() { 50 + i * 50, 50 + j * 50 };
        //                    FoundNode = map.SearchNode(currentLevel, 50 + i * 50, 50 + (j + 1) * 50)[0];
        //                    map.AddEdge(currentLevel, new List<Node> { FirstNode, FoundNode });
        //                }
        //            }
        //        }
        //    });
        //    LoadLevel();
        //    blockControl(false);
        //}

        //private void clearToolStripMenuItem_Click(object sender, EventArgs e) // Clear
        //{
        //    DialogResult result = MessageBox.Show("Вы уверены?", "Warning", MessageBoxButtons.YesNo);
        //    if (result == DialogResult.Yes)
        //    {
        //        map.ClearAllNodes();
        //        Changes(true);
        //        LoadLevel();
        //    }
        //}


        //private void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.S)
        //    {
        //        saveToolStripMenuItem_Click(null, null);
        //        return;
        //    }
        //    if (e.Control && e.Shift && e.KeyCode == Keys.T)
        //    {
        //        nodeGeneratorToolStripMenuItem_Click_1(null, null);
        //        return;
        //    }
        //    if (e.Control && e.KeyCode == Keys.L)
        //    {
        //        DrawToLeftToolStripButton_Click(null, null);
        //        return;
        //    }
        //}
        //private void DeleteLevelToolStripButton_Click(object sender, EventArgs e)
        //{
        //    if (ChooseLevelComboBox.SelectedIndex != -1)
        //    {
        //        DialogResult result = MessageBox.Show("Вы уверены?", "Warning", MessageBoxButtons.YesNo);
        //        if (result == DialogResult.Yes)
        //        {
        //            Changes(true);
        //            map.RemoveFromFloors(currentLevel);
        //            ChooseLevelComboBox.Items.Remove(currentLevel);
        //            if (ChooseLevelComboBox.Items.Count > 0)
        //                ChooseLevelComboBox.SelectedIndex = 0;
        //            else
        //            {
        //                ObservereMode();
        //                GreyMode(false);

        //                updateLevelList();

        //                panelX = panel1.Width;
        //                panelY = panel1.Height;

        //                pictureBox1.Parent = panel1;
        //                pictureBox1.Location = Point.Empty;
        //                pictureBox1.Image = new Bitmap(panelX, panelY);
        //                pictureBox1.ClientSize = pictureBox1.Image.Size;
        //            }
        //        }
        //    }
        //}
    }
}
