using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication2
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        double[] f(double x, double[] y)
        {
            double[] dy = new double[2];
            dy[0] = y[1];
            dy[1] = (-(1 / x) * y[1]) - y[0];
            return dy;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double x0, xl, h;
            int i;
            double[] y = new double[2];
            double[] dy = new double[2];
            chart1.Series.Clear();
            chart1.Series.Add(" y0 ");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series.Add(" y1 ");
            chart1.Series[1].ChartType = SeriesChartType.Line;
            x0 = Convert.ToDouble(textBox1.Text);
            xl = Convert.ToDouble(textBox2.Text);
            h = Convert.ToDouble(textBox3.Text);
            y[0] = Convert.ToDouble(textBox4.Text);
            y[1] = Convert.ToDouble(textBox5.Text);
            // Метод Эйлера
            for (double x = x0; x <= xl; x = x + h)
            {
                dy = f(x, y);
                for (i = 0; i <= 1; i++)
                {
                    y[i] = y[i] + dy[i] * h;
                }
                chart1.Series[0].Points.AddXY(x, y[0]);
                chart1.Series[1].Points.AddXY(x, y[1]);
            }


        }
    }
}
