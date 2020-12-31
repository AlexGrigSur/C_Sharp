using System;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTest
{
    public partial class ChoosePlanAndDBCreate : Form
    {
        string BuildingName = "";
        List<bool> isAvaliableList = new List<bool>();
        DBInitialize DBInit = new DBInitialize();
        public ChoosePlanAndDBCreate()
        {
            InitializeComponent();
        }
        private void ChoosePlan_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            label2.Enabled = false;
            textBoxNameInOutput.Visible = false;
            textBoxNameInOutput.Enabled = false;
            comboBox1.Items.Clear();
            DBCreate();
            SelectBuildings();
        }

        private void SelectBuildings()
        {
            comboBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;
            List<string> buildingList = new List<string>();
            DBInit.SelectBuilding(ref buildingList, ref isAvaliableList);
            comboBox1.Items.AddRange(buildingList.ToArray());
            comboBox1.Items.Add("New Building");
        }

        private void DBCreate()
        {
            DBInit.InitDB();
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите любой пункт");
                return;
            }
            if (comboBox1.SelectedIndex == comboBox1.Items.Count - 1)
            {
                if (textBoxNameInOutput.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Введите название плана здания");
                    return;
                }
                DBInit.InsertBuilding(textBoxNameInOutput.Text);
                BuildingName = textBoxNameInOutput.Text;
                CallBuilder();
            }
            else
            {
                BuildingName = comboBox1.SelectedText;
                if (!radioButton1.Checked && !radioButton2.Checked)
                {
                    MessageBox.Show("Выберите один из режимов");
                    return;
                }
                if (radioButton1.Checked)
                    CallBuilder();
                else
                    CallNav();
            }
            comboBox1.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == comboBox1.Items.Count - 1)
            {
                label2.Visible = true;
                textBoxNameInOutput.Visible = true;
                label2.Enabled = true;
                textBoxNameInOutput.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
            else
            {
                label2.Visible = false;
                textBoxNameInOutput.Visible = false;
                label2.Enabled = false;
                textBoxNameInOutput.Enabled = false;
                radioButton1.Enabled = true;
                radioButton2.Enabled = isAvaliableList[comboBox1.SelectedIndex];
            }
        }

        private void CallBuilder()
        {
            this.Hide();
            string buildingName = (comboBox1.SelectedIndex == comboBox1.Items.Count - 1) ? textBoxNameInOutput.Text : comboBox1.Text;
            new DrawingForm(buildingName).ShowDialog();
            this.Show();
            ChoosePlan_Load(null, null);
            comboBox1.SelectedIndex = -1;
        }
        private void CallNav()
        {
            this.Hide();
            new MainNavForm(comboBox1.Text).ShowDialog();
            this.Show();
            ChoosePlan_Load(null, null);
            comboBox1.SelectedIndex = -1;
        }
        private void DeleteBuilding_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите здание для удаления");
                return;
            }
            if (comboBox1.SelectedIndex == comboBox1.Items.Count - 1)
            {
                MessageBox.Show("Невозможно удалить ещё не созданное здание");
                return;
            }
            DBInit.DeleteBuilding(comboBox1.SelectedItem.ToString());

            SelectBuildings();
        }

        #region // DropDB
        private async void DropDB()
        {
            await Task.Run(() => { DBInit.DropDB(); });
            SelectBuildings();
        }
        private /*async*/ void DropDBButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Уверен?", "CAUTION", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                DropDB();//await Task.Run(() => DropDB());
            else
                return;
        }
        #endregion
    }
}
