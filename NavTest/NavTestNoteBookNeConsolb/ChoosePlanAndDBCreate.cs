using System;
using MySqlConnector;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTestNoteBookNeConsolb
{
    public partial class ChoosePlanAndDBCreate : Form
    {
        string BuildingName = "";
        List<bool> isAvaliableList = new List<bool>();
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
            using (DB DataBase = new DB("Plans"))
            {
                using (MySqlDataReader reader = DataBase.ExecuteReader("select `buildingName`,`buildingIsNavAble` from `Buildings`"))
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                        isAvaliableList.Add(reader.GetBoolean(1));
                    }
                }
            }
            comboBox1.Items.Add("New Building");
        }

        //private async void DBCreateAsynk()
        //{
        //    await Task.Run(() => DBCreate());
        //}
        private void DBCreate()
        {
            using (DB DataBase = new DB())
            {
                DataBase.ExecuteCommand("create database if not exists `Plans`");
                DataBase.NewConnection("Plans");
                DataBase.ExecuteCommand("create table if not exists `Buildings` (`id` int(11) not null primary key auto_increment, `buildingName` varchar(150) not null unique, `buildingIsNavAble` boolean not null)");
                DataBase.ExecuteCommand("create table if not exists `Levels`(`id` int(11) primary key not null auto_increment, `building_ID` int(11) not null,`levelFloor` int(11) not null, `levelScreenResX` int(11) not null, `levelScreenResY` int(11) not null, constraint `NoSameLevels` UNIQUE(`building_ID`,`levelFloor`), constraint `building_id_FK` foreign key(`building_ID`) references `Buildings`(`id`) on delete cascade)");
                DataBase.ExecuteCommand("create table if not exists `Nodes` (`id` int(11) primary key not null auto_increment, `building_ID` int(11) not null,`NodeName` varchar(150) not null,`NodeType` int(11) not null, `NodeDescription` varchar(150), constraint `building_id_FK_1` foreign key(`building_ID`) references `Buildings`(`id`) on delete cascade)");
                DataBase.ExecuteCommand("create table if not exists `ConnectivityComponents` (`level_ID` int(11) not null, `connectivityComponentIndex` int(11) not null, `node_ID` int(11) not null, constraint `UniqueInConnComp` primary key(`level_ID`,`connectivityComponentIndex`,`node_ID`), constraint `level_ID_FK` foreign key(`level_ID`) references `Levels`(`id`) on delete cascade, constraint `node_ID_FK` foreign key(`node_ID`) references `Nodes`(`id`) on delete cascade)");
                DataBase.ExecuteCommand("create table if not exists `LevelNodes` (`id` int(11) primary key not null auto_increment, `level_ID` int(11) not null, `node_ID` int(11) not null, `levelNodeCoordX` int(11) not null, `levelNodeCoordY` int(11) not null, constraint `level_ID_FK_1` foreign key(`level_ID`) references `Levels`(`id`) on delete cascade, constraint `node_ID_FK_1` foreign key(`node_ID`) references `Nodes`(`id`) on delete cascade)");
                DataBase.ExecuteCommand("create table if not exists `Edges` (`level_ID` int(11) not null, `startNode_ID` int(11) not null, `endNode_ID` int(11) not null, constraint `NoMultiGraphs` primary key(`level_ID`,`startNode_ID`,`endNode_ID`), constraint `level_ID_FK_2` foreign key(`level_ID`) references `Levels`(`id`) on delete cascade ,constraint `startNode_ID_FK` foreign key(`startNode_ID`) references `LevelNodes`(`id`) on delete cascade, constraint `endNode_ID_FK` foreign key(`endNode_ID`) references `LevelNodes`(`id`) on delete cascade)");
            }
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
                using (DB DataBase = new DB("Plans"))
                {
                    DataBase.ExecuteCommand($"INSERT INTO `Buildings` VALUES (NULL, '{textBoxNameInOutput.Text}','0')");
                }
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
            new NavForm.MainNavForm(comboBox1.Text).ShowDialog();
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
            using (DB DataBase = new DB("Plans"))
            {
                DataBase.ExecuteCommand($"delete from `Buildings` where `buildingName`='{comboBox1.SelectedItem.ToString()}'");
            }
            SelectBuildings();
        }

        #region // DropDB
        private void DropDB()
        {
            using (DB DataBase = new DB("Plans"))
            {
                DataBase.ExecuteCommand("drop database `Plans`");
            }
            DBCreate();
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
