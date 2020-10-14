using System;
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
    public partial class NodeInfo : Form
    {
        public string nodeName { get { return NameTextBox.Text.Trim(); } }
        public int nodeType { get { return TypeComboBox.SelectedIndex; } }
        public string nodeDescription { get { return DescriptionTextBox.Text; } }
        public int nodeCoordX { get { return Convert.ToInt32(XTextBox.Text); } }
        public int nodeCoordY { get { return Convert.ToInt32(YTextBox.Text); } }
        public bool ContinueFlag { get; set; }
        public NodeInfo(int x, int y)
        {
            InitializeComponent();

            ContinueFlag = false;
            TypeComboBox.Items.Add("Коридор");
            TypeComboBox.Items.Add("Кабинет");
            TypeComboBox.Items.Add("Лестница");
            TypeComboBox.Items.Add("Выход");

            XTextBox.Text = Convert.ToString(x);
            YTextBox.Text = Convert.ToString(y);
        }

        public NodeInfo(int x = 0, int y = 0,string name="", string description="", int type=-1)
        {
            NameTextBox.Text = name;
            TypeComboBox.SelectedIndex = type;
            DescriptionTextBox.Text = description;
            XTextBox.Text = x.ToString();
            YTextBox.Text = y.ToString();
        }

        private void Continue_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text.Trim().Length != 0 && TypeComboBox.SelectedIndex != -1)
            {
                ContinueFlag = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля");
                return;
            }
        }
    }
}
