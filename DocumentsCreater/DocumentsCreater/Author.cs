using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndrewWithInterface
{
    public partial class Author : Form
    {
        public Author()
        {
            InitializeComponent();
            textBox1.Text = $"Программа выполнена студентом 2 курса ФКТиПМ КУБГУ {Environment.NewLine}Направления ФИИТ (02.03.02){Environment.NewLine}Григоряном Александром{Environment.NewLine}";
            textBox1.Text += $"20.07.2020{Environment.NewLine}{Environment.NewLine}";
            textBox1.Text += $"При возникновении вопросов можете обращаться на почту: {Environment.NewLine}alex.grig.sur@gmail.com";
            textBox1.ReadOnly = true;
            textBox1.BorderStyle = 0;
            textBox1.BackColor = this.BackColor;
            textBox1.TabStop = false;
            textBox1.Multiline = true;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
