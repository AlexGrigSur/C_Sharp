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
        public int levelFloor { get { return Convert.ToInt32(FloorTextBox.Text); } }
        public bool ContinueFlag { get; set; }

        public NewLevelAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FloorTextBox.Text.Trim().Length != 0)
            {
                ContinueFlag = true;
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля");
        }

        private void FloorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar == '-')
            {
                e.Handled = true;
            }

        }
    }
}
