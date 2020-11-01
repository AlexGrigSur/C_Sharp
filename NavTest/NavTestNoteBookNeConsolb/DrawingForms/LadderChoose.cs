using NavTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavTestNoteBookNeConsolb.DrawingForms
{
    public partial class LadderChoose : Form
    {
        public bool ContinueFlag { get; set; }
        public string SelectedLadder { get; set; }
        public LadderChoose(List<Node> ladderList)
        {
            foreach (Node i in ladderList)
                dataGridView1.Rows.Add(i.name);
            dataGridView1.Rows.Add("Новая лестница");
            InitializeComponent();
            ContinueFlag = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count!=0)
            {
                SelectedLadder = dataGridView1.SelectedRows[0].ToString();
                ContinueFlag = true;
                this.Close();
            }
        }
    }
}
