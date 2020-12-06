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

namespace NavTest
{
    public partial class MainNavForm : Form
    {
        private Map map;
        private int panelX = 0;
        private int panelY = 0;
        static private int radius = 10;

        private int currentRouteElem=-1;
        private Image SecondLayer;

        private Dictionary<Node, ConnectivityComp> avaliableNodes;

        private Dictionary<ConnectivityComp, List<Node>> Route;
        private List<ConnectivityComp> RouteNavigation;
        public MainNavForm(string buildingName)
        {
            InitializeComponent();

            DownloadFromDB(buildingName);
            updateLevelList();

            panelX = map.GetFloorsList().First().Value.ScreenResX;
            panelY = map.GetFloorsList().First().Value.ScreenResY;

            pictureBox1.Parent = panel1;
            pictureBox1.Location = Point.Empty;
            pictureBox1.Image = new Bitmap(panelX, panelY);
            pictureBox1.ClientSize = pictureBox1.Image.Size;

            ChooseLevelComboBox.SelectedIndex = 0;
            LoadLevel();

            Step.Visible = false;
            ContinueButton.Visible = false;
            PreviousButton.Visible = false;
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

        private void drawPath()
        {
            Step.Text = $"Шаг {currentRouteElem + 1}/{Route.Count}";
            int floor = RouteNavigation[currentRouteElem].GetFloor();
            ChooseLevelComboBox.SelectedItem = floor;
            SecondLayer = new Bitmap(pictureBox1.Image);
            List<List<int>> ToDraw = new List<List<int>>();
            foreach (Node i in Route[RouteNavigation[currentRouteElem]])
                ToDraw.Add(map.GetFloor(floor).GetNodeOnFloor(i));
            pictureBox1.Image = new DrawClass(radius).RouteBuilder(pictureBox1.Image, ToDraw);
            pictureBox1.Invalidate();
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (!ContinueButton.Enabled)
                ContinueButton.Enabled = true;

            if (currentRouteElem == 1)
                PreviousButton.Enabled = false;

            if (currentRouteElem != 0)
            {
                --currentRouteElem;
                drawPath();
            }
        }
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            if (!PreviousButton.Enabled)
                PreviousButton.Enabled = true;

            if (currentRouteElem == Route.Count - 2)
                ContinueButton.Enabled = false;

            if (currentRouteElem != Route.Count - 1)
            {
                ++currentRouteElem;
                drawPath();
            }
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

            currentRouteElem = -1;

            pictureBox1.Image = new Bitmap(SecondLayer);
            pictureBox1.Invalidate();

            Step.Visible = false;
            ContinueButton.Enabled= true;
            PreviousButton.Enabled = true;
            ContinueButton.Visible = false;
            PreviousButton.Visible = false;
        }

        private void ResetRoute()
        {
            pictureBox1.Image = SecondLayer;
            SecondLayer = null;

            Step.Text = "";
            ContinueButton.Enabled = true;
            PreviousButton.Enabled = true;
            ContinueButton.Visible = false;
            PreviousButton.Visible = false;
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            if (StartPointButton.Text != "Выберите точку" && EndPointButton.Text != "Выберите точку")
            {
                if (StartPointButton.Text != EndPointButton.Text)
                {
                    if (currentRouteElem != -1) ResetRoute();
                    NavCalc alg = new NavCalc(map, map.GetNode(StartPointButton.Text), map.GetNode(EndPointButton.Text));
                    Route = alg.startCalc();
                    RouteNavigation = Route.Keys.ToList();
                    currentRouteElem = 0;

                    if (Route.Count > 1)
                    {
                        Step.Visible = true;
                        ContinueButton.Visible = true;
                        PreviousButton.Visible = true;
                        PreviousButton.Enabled = false;
                    }

                    drawPath();
                    MessageBox.Show(alg.timeToCalc);
                }
                else
                {
                    if (currentRouteElem != -1) ResetRoute();
                    MessageBox.Show("Вы уже в точке назначения :)");
                }
            }
            else
                MessageBox.Show("Выберите начальную и конечную точки");
        }

    }
}
