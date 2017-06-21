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
    public partial class Form10 : Form
    {
        double[] x = new double[100];
        double[] y = new double[100];
        double[,] A = new double[100, 100];
        public Form10()
        {
            InitializeComponent();

            chart1.Series.Clear();
            chart1.Series.Add("sin(x)");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series.Add("etalon");

            chart1.Series[1].ChartType = SeriesChartType.Line;
            for (int i = 0; i < 10; i++)
            {
                x[i] = i;
                y[i] = Math.Sin(i);
                A[0, i] = y[i];
                chart1.Series[0].Points.AddXY(i, Math.Sin(i));


            }
            label1.Text = "";
            for (int k = 1; k < 10; k++)
            {
                for (int j = 0; j < 9 - k; j++)
                {
                    A[k, j] = (A[k - 1, j] - A[k - 1, j + 1]) / (x[j] - x[j + k]);
                }

            }
            for (int k = 1; k < 10; k++)
            {

                label1.Text += x[k].ToString() + "   " + y[k].ToString() + " ";
                for (int j = 0; j < 9 - k; j++)
                {
                    label1.Text += A[j, k].ToString() + "       ";
                }
                label1.Text += "\n";
            }


            for (double i = -1; i < 11; i += 0.1)
            {
                chart1.Series[1].Points.AddXY(i, P(i));
            }


        }

        double P(double xx)
        {
            double p;
            int i = 1; double s = y[0];
            for (int j = 0; j < 10; j++)
            {
                p = 1;
                for (i = 0; i <= j; i++)
                {
                    p = p * (xx - x[i]);
                }
                s = s + p * A[j + 1, 0];
            }
            return s;
        }
    }
}
