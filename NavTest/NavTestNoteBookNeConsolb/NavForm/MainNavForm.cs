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
        static private int radius = 10;

        private Dictionary<Node, ConnectivityComp> avaliableNodes;
        public MainNavForm(string buildingName)
        {
            InitializeComponent();

            panelX = 900;
            panelY = 700;

            pictureBox1.Parent = panel1;
            pictureBox1.Location = Point.Empty;
            pictureBox1.Image = new Bitmap(panelX, panelY);
            pictureBox1.ClientSize = pictureBox1.Image.Size;

            DownloadFromDB(buildingName);
            updateLevelList();
            ChooseLevelComboBox.SelectedIndex = 0;
            LoadLevel();
        }
        DrawClass draw = new DrawClass(radius);
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
            avaliableNodes = new Dictionary<Node, ConnectivityComp>();
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
            pictureBox1.Image = new Bitmap(draw.LoadLevel(Convert.ToInt32(ChooseLevelComboBox.Text), ref map, out panelX, out panelY));
            pictureBox1.Invalidate();
        }
        #region // RootSelection
        private void ChoosePointMenu(Button button)
        {
            ChoosePoint form = new ChoosePoint(ref avaliableNodes);
            form.ShowDialog();
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
                    NavCalc navCalc = new NavCalc(map, map.GetNode(StartPointButton.Text), map.GetNode(EndPointButton.Text));
                    navCalc.startCalc();
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
            if (avaliableNodes.Count == 0)
            {
                MessageBox.Show("План не имеет доступных точек для расчёта маршрута");
                return;
            }
            LoadLevel();
        }
        private void RefreshRouteButton_Click(object sender, EventArgs e)
        {
            StartPointButton.Text = "Выберите точку";
            EndPointButton.Text = "Выберите точку";
        }
    }
}
