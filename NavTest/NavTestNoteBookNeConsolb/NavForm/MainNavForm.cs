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
        private int panelX=0;
        private int panelY=0;
        private int radius = 10;
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
            foreach (Level i in map.Floors.Values)
                ChooseLevelComboBox.Items.Add(i.Name);
        }

        private void DownloadFromDB(string buildingName)
        {
            DataFromDB db = new DataFromDB(buildingName);
            int uselessCounter=-1;
            map = db.DownloadFromDB(ref uselessCounter, true);
        }

        private void LoadLevel()
        {
            Level floor = map.Floors[ChooseLevelComboBox.Text];
            pictureBox1.Image = new Bitmap(floor.screenResX, floor.screenResY);
            panelX = floor.screenResX;
            panelY = floor.screenResY;

            foreach (Node nodeIter in floor.nodeListOnFloor.Keys) // draw all Nodes
                DrawNode(floor.nodeListOnFloor[nodeIter][0], floor.nodeListOnFloor[nodeIter][1], 255, ((nodeIter.type == 0) ? "" : nodeIter.name)); // ternary operator to not draw text on corridor nodes
            Dictionary<Node, List<Node>> edgesCopy = new Dictionary<Node, List<Node>>(map.Floors[ChooseLevelComboBox.Text].edges);
            foreach (Node FirstNodeLine in edgesCopy.Keys) // draw All 
                foreach (Node SecondNodeLine in edgesCopy[FirstNodeLine])
                {
                    DrawLine(floor.nodeListOnFloor[FirstNodeLine][0], floor.nodeListOnFloor[FirstNodeLine][1], floor.nodeListOnFloor[SecondNodeLine][0], floor.nodeListOnFloor[SecondNodeLine][1]);
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
            List<Node> nodeList = map.SearchNode(ChooseLevelComboBox.Text, X1, Y1, X2, Y2);
            string nodeName = (nodeList[0].type == 0) ? "" : nodeList[0].name;
            DrawNode(map.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[0]][0], map.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[0]][1], 255, nodeName);
            nodeName = (nodeList[1].type == 0) ? "" : nodeList[1].name;
            DrawNode(map.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[1]][0], map.Floors[ChooseLevelComboBox.Text].nodeListOnFloor[nodeList[1]][1], 255, nodeName);
        }


        #region // RootSelection
        private void StartPointButton_Click(object sender, EventArgs e)
        {

        }

        private void EndPointButton_Click(object sender, EventArgs e)
        {

        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void ChooseLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLevel();
        }
    }
}
