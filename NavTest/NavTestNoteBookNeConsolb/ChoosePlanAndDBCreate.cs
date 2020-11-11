﻿using System;
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
        DB DataBase = new DB();
        string BuildingName = "";
        public ChoosePlanAndDBCreate()
        {
            InitializeComponent();
        }

        private void SelectBuildings()
        {
            comboBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;
            using (MySqlDataReader reader = DataBase.ExecuteReader("select `buildingName` from `Buildings`"))
            {
                while (reader.Read())
                    comboBox1.Items.Add(reader.GetString(0));
            }
            comboBox1.Items.Add("New Building");
        }

        private void DBCreate()
        {
            DataBase.ExecuteCommand("create database if not exists `Plans`");
            DataBase = new DB("Plans");
            DataBase.ExecuteCommand("create table if not exists `Buildings` (`id` int(11) not null primary key auto_increment, `buildingName` varchar(150) not null unique, `buildingIsNavAble` boolean not null)");
            DataBase.ExecuteCommand("create table if not exists `Levels`(`id` int(11) primary key not null auto_increment, `building_ID` int(11) not null references `Buildings`(`id`) on delete cascade,`levelName` varchar(150) not null, `levelFloor` int(11) not null, `levelScreenResX` int(11) not null, `levelScreenResY` int(11) not null, constraint `NoSameLevels` UNIQUE(`building_ID`,`levelName`))");
            DataBase.ExecuteCommand("create table if not exists `Nodes` (`id` int(11) primary key not null auto_increment, `building_ID` int(11) not null references `Buildings`(`id`) on delete cascade,`NodeName` varchar(150) not null unique,`NodeType` int(11) not null, `NodeDescription` varchar(150))");
            DataBase.ExecuteCommand("create table if not exists `ConnectivityComponents` (`level_ID` int(11) not null references `Levels`(`id`) on delete cascade, `connectivityComponentIndex` int(11) not null, `node_ID` int(11) not null references `Nodes`(`id`) on delete cascade, constraint `UniqueInConnComp` primary key(`level_ID`,`connectivityComponentIndex`,`node_ID`))");
            //DataBase.ExecuteCommand("create table if not exists `HyperGraph` (`id` int(11) primary key not null auto_increment, `building_ID` int(11) not null references `Buildings`(`id`) on delete cascade, `commonnode_ID` int(11) not null references `CommonNodes`(`id`) on delete cascade, `level_ID` int(11) not null references `Levels`(`id`) on delete cascade)");
            DataBase.ExecuteCommand("create table if not exists `LevelNodes` (`id` int(11) primary key not null auto_increment, `level_ID` int(11) not null references `Levels`(`id`) on delete cascade, `node_ID` int(11) not null references `Nodes`(`id`) on delete cascade, `levelNodeCoordX` int(11) not null, `levelNodeCoordY` int(11) not null)");
            DataBase.ExecuteCommand("create table if not exists `Edges` (`level_ID` int(11) not null references `Levels`(`id`) on delete cascade, `startNode_ID` int(11) not null references `LevelNodes`(`id`) on delete cascade, `endNode_ID` int(11) not null references `LevelNodes`(`id`) on delete cascade, constraint `NoMultiGraphs` primary key(`startNode_ID`,`endNode_ID`))");
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
                DataBase.ExecuteCommand($"INSERT INTO `Buildings` VALUES (NULL, '{textBoxNameInOutput.Text}','0')");
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
                radioButton2.Enabled = true;
            }
        }

        private void CallBuilder()
        {
            this.Hide();
            string buildingName = (comboBox1.SelectedIndex == comboBox1.Items.Count - 1) ? textBoxNameInOutput.Text : comboBox1.Text;
            new DrawingForm(buildingName).ShowDialog();
            this.Show();
            ChoosePlan_Load(null,null);
        }
        private void CallNav()
        {
            this.Hide();
            // NavForm form = new NavForm(BuildingName).ShowDialog();
            this.Show();
            ChoosePlan_Load(null, null);
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

        private void DeleteBuilding_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите здание для удаления");
                return;
            }
            if(comboBox1.SelectedIndex==comboBox1.Items.Count-1)
            {
                MessageBox.Show("Невозможно удалить ещё не созданное здание");
                return;
            }
            DataBase.ExecuteCommand($"delete from `Buildings` where `buildingName`='{comboBox1.SelectedItem.ToString()}'");
            SelectBuildings();
        }

        private void DropDBButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Уверен?", "CAUTION", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataBase.ExecuteCommand("drop database `Plans`");
                DataBase = new DB();
                DBCreate();
/**************/SelectBuildings();
            }
            else
                return;        
        }
    }
}