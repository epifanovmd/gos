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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        double[] f(double x, double[] y)
        {
            double[] dy = new double[2];
            dy[0] = y[1];
            dy[1] = -y[0];
            return dy;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double x0, xl, h;
            int i;
            double[] y = new double[2];
            double[] y0 = new double[2];
            double[] k1 = new double[2];
            double[] k2 = new double[2];
            double[] k3 = new double[2];
            double[] k4 = new double[2];
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
            // Метод Эйлера (Рунге Кутта)
            for (double x = x0; x <= xl; x = x + h)
            {
                for (i = 0; i <= 1; i++)
                    y0[i] = y[i];
                k1 = f(x, y);
                for (i = 0; i <= 1; i++)
                    y[i] = y0[i] + k1[i] * h / 2;
                k2 = f(x + h / 2, y);
                for (i = 0; i <= 1; i++)
                    y[i] = y0[i] + k2[i] * h / 2;
                k3 = f(x + h / 2, y);
                for (i = 0; i <= 1; i++)
                    y[i] = y0[i] + k3[i] * h;
                k4 = f(x + h, y);
                for (i = 0; i <= 1; i++)
                {
                    y[i] = y0[i] + h * (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]) / 6;
                }
                chart1.Series[0].Points.AddXY(x, y[0]);
                chart1.Series[1].Points.AddXY(x, y[1]);
            }
        }
    }
}
