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
    public partial class NewLevelAdd : Form
    {
        public string levelName { get { return LevelNameTextBox.Text.Trim(); } }
        public int levelFloor { get { return Convert.ToInt32(FloorTextBox.Text); } }
        public bool ContinueFlag { get; set; }

        public NewLevelAdd(string levelName = "", int levelFloor = -1)
        {
            InitializeComponent();

            LevelNameTextBox.Text = levelName;
            if (levelFloor != -1) FloorTextBox.Text = Convert.ToString(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LevelNameTextBox.Text.Trim().Length != 0)
            {
                ContinueFlag = true;
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля");
        }
    }
}
