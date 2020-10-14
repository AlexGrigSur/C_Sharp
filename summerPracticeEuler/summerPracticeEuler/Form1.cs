using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Math;
namespace summerPracticeEuler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }
        private void Calc()
        {
            chart1.Series.Clear();
            
            EulerCalc CalcModuleApprox = new EulerCalc(0,1);
            EulerCalcAccurate CalcModuleAccur = new EulerCalcAccurate(0, 1);
            
            double leftBorder = 0;
            double rightBorder = 1;
            int N=Convert.ToInt32(textBoxStep.Text);
            double step = (rightBorder-leftBorder)/N;

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("X", "Y\\X");
            dataGridView1.Rows.Add("Y_Approximate");
            dataGridView1.Rows.Add("Y_Accurate");

            List<List<double>> resultApprox = CalcModuleApprox.Calc(leftBorder,rightBorder,step);
            List<List<double>> resultAccur = CalcModuleAccur.Calc(leftBorder, rightBorder, step);
            Series s1 = new Series("Approximate");
            Series s2 = new Series("Accurate");
            s1.ChartType = SeriesChartType.Line;
            s2.ChartType = SeriesChartType.Line;
            
            double maxDifference = 0;
            
            for (int i = 0; i < resultApprox.Count; ++i)
            {
                dataGridView1.Columns.Add($"X[{i}]", $"{Math.Round(resultApprox[i][0],5)}");
                dataGridView1.Rows[0].Cells[dataGridView1.Columns.Count-1].Value = Math.Round(resultApprox[i][1],5);
                dataGridView1.Rows[1].Cells[dataGridView1.Columns.Count - 1].Value = Math.Round(resultAccur[i][1],5);

                s1.Points.AddXY(resultApprox[i][0], resultApprox[i][1]);
                s2.Points.AddXY(resultAccur[i][0], resultAccur[i][1]);
                
                if (Abs(resultApprox[i][1] - resultAccur[i][1]) > maxDifference)
                    maxDifference = Abs(resultApprox[i][1] - resultAccur[i][1]);
            }
            chart1.Series.Add(s1);
            chart1.Series.Add(s2);
            textBoxDifference.Text = Math.Round(maxDifference, 5).ToString();
        }
        private void textBoxStep_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStep.TextLength > 0)
                Calc();
            else
                chart1.Series.Clear();
        }

        private void textBoxStep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    class EulerCalc
    {
        protected double x;
        protected double y;

        // 2y'+3ycosx=e^(2x)(2+3cosx)y^-1
        public EulerCalc(double X, double Y)
        {
            x = X;
            y = Y;
        }
        protected virtual double function(double x, double y)
        {
            return (Exp(2 * x) * (2 + 3 * Cos(x)) * (1 / y) - 3 * y * Cos(x)) / 2;
        }
        public List<List<double>> Calc(double leftBorder, double rightBorder, double step)
        {
            List<List<double>> Result = new List<List<double>>();
            for (double i = leftBorder; i <= rightBorder; i += step)
            {
                List<double> XY = new List<double>();
                XY.Clear();
                XY.Add(x);
                XY.Add(y);
                Result.Add(XY);
                y += step * function(x, y);
                x += step;
            }
            return Result;
        }
    }
    class EulerCalcAccurate : EulerCalc
    {
        public EulerCalcAccurate(double X, double Y) : base(X, Y)
        {
        }
        protected override double function(double x, double y)
        {
            return Sqrt(Exp(2 * x));
        }
    }
}
