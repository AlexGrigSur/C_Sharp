using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTest//NavTestNoteBookNeConsolb
{
    public partial class NewLevelAdd : Form
    {
        public int levelFloor { get { return Convert.ToInt32(FloorTextBox.Text); } }
        public bool ContinueFlag { get; set; }
        private List<int> existingLevels;
        public NewLevelAdd(List<int> _existingLevels)
        {
            InitializeComponent();
            existingLevels = _existingLevels;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FloorTextBox.Text.Trim().Length != 0)
            {
                if (existingLevels.Contains(Convert.ToInt32(FloorTextBox.Text.Trim())))
                {
                    MessageBox.Show("Такой этаж уже существует. Добавление невозможно");
                    return;
                }
                ContinueFlag = true;
                this.Close();
            }
            else
                MessageBox.Show("Заполните все поля");
        }

        private void FloorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }
    }
}
