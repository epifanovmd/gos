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
    public partial class Form2 : Form
    {
        public double[] a = new double[10];
        public double[] b = new double[10];
        public Form2()
        {
            InitializeComponent();


            chart1.Series.Clear();
            int k = 0;
            chart1.Series.Add("sin(x)");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            for (double i = 1; i < 10; i++)
            {
                
                chart1.Series[0].Points.AddXY(i, Math.Sin(i));
              
            }
            chart1.Series.Add("cos(x)");
            chart1.Series[1].ChartType = SeriesChartType.Line;
            //  chart1.Series[1].ChartType = SeriesChartType.Line;
            for (double i = 0.5; i < 10; i += 0.5)
            {
                k++;
                chart1.Series[1].Points.AddXY(i, Math.Cos(i));

            }
            chart1.Series.Add("etalon");

            chart1.Series[2].ChartType = SeriesChartType.Line;
            for (double i = -1; i < 11; i += 0.1)
            {
                chart1.Series[2].Points.AddXY(i, l(i));
            }


        }
        public double l(double x)
        {
            double s = 0, p;
            for (int i = 0; i < a.Length; i++)
            {
                p = 1;
                for (int j = 0; j < a.Length; j++)
                {
                    if (i != j)
                    {
                        p *= (x - a[j]) / (a[i] - a[j]);
                    }

                }
                s += p * b[i];
            }
            return s;
        }
    }
}
