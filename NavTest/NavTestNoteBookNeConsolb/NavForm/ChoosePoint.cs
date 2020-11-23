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

namespace NavTestNoteBookNeConsolb.NavForm
{
    public partial class ChoosePoint : Form
    {
        public ChoosePoint(ref Dictionary<Node,ConnectivityComp> avaliableList)
        {
            InitializeComponent();
            foreach (Node i in avaliableList.Keys)
                dataGridView1.Rows.Add(i.name, i.description, avaliableList[i].GetFloor());
            ContinueFlag = false;
        }
        public string SelectedNode { get; set; }
        public bool ContinueFlag { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count==1)
            {
                SelectedNode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                ContinueFlag = true;
            }
        }
    }
}
