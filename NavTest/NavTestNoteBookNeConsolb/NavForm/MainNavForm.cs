using System;
using NavTest;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTestNoteBookNeConsolb.NavForm
{
    public partial class MainNavForm : Form
    {
        private Map map;
        private int panelX = 0;
        private int panelY = 0;
        private int radius = 10;

        private Dictionary<Node, ConnectivityComp> avaliableNodes;
        public MainNavForm(string buildingName)
        {
            InitializeComponent();

            panelX = panel1.Width;
            panelY = panel1.Height;

            pictureBox1.Parent = panel1;
            pictureBox1.Location = Point.Empty;
            pictureBox1.Image = new Bitmap(panelX, panelY);
            pictureBox1.ClientSize = pictureBox1.Image.Size;

            DownloadFromDB(buildingName);
            updateLevelList();
            ChooseLevelComboBox.SelectedIndex = 0;
            LoadLevel();
        }

        private void updateLevelList()
        {
            ChooseLevelComboBox.Items.Clear();
            ChooseLevelComboBox.SelectedIndex = -1;
            List<int> levelListToSort = new List<int>();

            foreach (Level i in map.GetFloorsList().Values)
                levelListToSort.Add(i.FloorIndex);

            levelListToSort.Sort();

            foreach (int i in levelListToSort)
                ChooseLevelComboBox.Items.Add(i);
        }

        private void DownloadFromDB(string buildingName)
        {
            DataFromDB db = new DataFromDB(buildingName);
            int uselessCounter = -1;
            map = db.DownloadFromDB(ref uselessCounter, true);
            updateAvaliableNodes();
        }
        private void updateAvaliableNodes()
        {
            foreach (Level Flr in map.GetFloorsList().Values)
            {
                foreach (ConnectivityComp CnCmp in Flr.GetConnectivityComponentsList())
                {
                    foreach (Node Nd in CnCmp.GetAllNodesList())
                        if (Nd.type == 1) avaliableNodes.Add(Nd, CnCmp);
                }
            }
        }

        private void LoadLevel()
        {
            Level floor = map.GetFloor(Convert.ToInt32(ChooseLevelComboBox.Text));
            pictureBox1.Image = new Bitmap(floor.ScreenResX, floor.ScreenResY);
            panelX = floor.ScreenResX;
            panelY = floor.ScreenResY;

            foreach (Node nodeIter in floor.GetNodeListOnFloor().Keys) // draw all Nodes
                DrawNode(floor.GetNodeOnFloor(nodeIter)[0], floor.GetNodeOnFloor(nodeIter)[1], 255, ((nodeIter.type == 0) ? "" : nodeIter.name)); // ternary operator to not draw text on corridor nodes
            Dictionary<Node, List<Node>> edgesCopy = new Dictionary<Node, List<Node>>(map.GetFloor(Convert.ToInt32(ChooseLevelComboBox.Text)).GetEdgesList());
            foreach (Node FirstNodeLine in edgesCopy.Keys) // draw All 
                foreach (Node SecondNodeLine in edgesCopy[FirstNodeLine])
                {
                    DrawLine(floor.GetNodeOnFloor(FirstNodeLine)[0], floor.GetNodeOnFloor(FirstNodeLine)[1], floor.GetNodeOnFloor(SecondNodeLine)[0], floor.GetNodeOnFloor(SecondNodeLine)[1]);
                    edgesCopy[SecondNodeLine].Remove(FirstNodeLine);
                }
            edgesCopy.Clear();
            pictureBox1.Invalidate();
        }
        private void DrawNode(int X, int Y, int transparent = 255, string nodeName = "")
        {
            using (Graphics G = Graphics.FromImage(pictureBox1.Image))
            {
                Color colorFirst, colorSecond;
                colorFirst = Color.FromArgb(transparent, Color.Black);
                colorSecond = Color.FromArgb(transparent, Color.Orange);

                Pen pen = new Pen(colorFirst);
                Brush brush = new SolidBrush(colorFirst);
                G.DrawEllipse(pen, X - radius, Y - radius, 2 * radius, 2 * radius);
                G.FillEllipse(brush, X - radius, Y - radius, 2 * radius, 2 * radius);
                brush = new SolidBrush(colorSecond);
                G.FillRectangle(brush, X, Y, 1, 1);
                if (nodeName != "") G.DrawString(nodeName.Substring(0, ((nodeName.Length < 4) ? nodeName.Length : 4)), new Font("Microsoft Sans Serif", 15f), new SolidBrush(colorFirst), X + radius + 2, Y - radius);
            }
            pictureBox1.Invalidate();
        }
        private void DrawLine(int X1, int Y1, int X2, int Y2)
        {
            using (Graphics G = Graphics.FromImage(pictureBox1.Image))
            {
                Color lineColor;
                lineColor = Color.Purple;
                G.DrawLine(new Pen(lineColor), X1, Y1, X2, Y2);
            }
            List<Node> nodeList = map.SearchNode(Convert.ToInt32(ChooseLevelComboBox.Text), X1, Y1, X2, Y2);
            string nodeName = (nodeList[0].type == 0) ? "" : nodeList[0].name;
            DrawNode(map.GetFloor(Convert.ToInt32(ChooseLevelComboBox.Text)).GetNodeOnFloor(nodeList[0])[0], map.GetFloor(Convert.ToInt32(ChooseLevelComboBox.Text)).GetNodeOnFloor(nodeList[0])[1], 255, nodeName);
            nodeName = (nodeList[1].type == 0) ? "" : nodeList[1].name;
            DrawNode(map.GetFloor(Convert.ToInt32(ChooseLevelComboBox.Text)).GetNodeOnFloor(nodeList[1])[0], map.GetFloor(Convert.ToInt32(ChooseLevelComboBox.Text)).GetNodeOnFloor(nodeList[1])[1], 255, nodeName);
        }

        #region // RootSelection
        private void ChoosePointMenu(Button button)
        {
            ChoosePoint form = new ChoosePoint(ref avaliableNodes);
            if (form.ContinueFlag)
                button.Text = form.SelectedNode;
        }
        private void StartPointButton_Click(object sender, EventArgs e) => ChoosePointMenu(StartPointButton);

        private void EndPointButton_Click(object sender, EventArgs e) => ChoosePointMenu(EndPointButton);

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (StartPointButton.Text != "Выберите точку" && EndPointButton.Text != "Выберите точку")
            {
                if (StartPointButton.Text != EndPointButton.Text)
                {
                    NavCalc navCalc = new NavCalc();
                    bool isDifferentConnectivityComp = avaliableNodes[map.GetNode(StartPointButton.Text)].Equals(avaliableNodes[map.GetNode(EndPointButton.Text)]);
                    
                }
                else
                    MessageBox.Show("Вы уже в точке назначения :)");
            }
            else
                MessageBox.Show("Выберите начальную и конечную точки");
        }
        #endregion

        private void ChooseLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLevel();
        }
    }
}
