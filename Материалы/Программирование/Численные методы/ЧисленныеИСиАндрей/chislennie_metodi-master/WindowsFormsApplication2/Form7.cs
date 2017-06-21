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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        double[] f(double x, double[] y, double Lamda)
        {

            double[] dy = new double[2];
            dy[0] = y[1];
            dy[1] = -Lamda * y[0];
            return dy;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double x0, xl, h;
            int i;
            double[] y = new double[2];
            double[] dy = new double[2];
            double[] dy1 = new double[2];
            double[] dy2 = new double[2];
            double[] dy3 = new double[2];
            double[] dy4 = new double[2];
            double[] k1 = new double[2];
            double[] k2 = new double[2];
            double[] k3 = new double[2];
            double[] k4 = new double[2];
            //StreamWriter xx = new StreamWriter(@"d:\\303\xx.txt");
            //StreamWriter ll = new StreamWriter(@"d:\\303\ll.txt");
            //StreamWriter yy = new StreamWriter(@"d:\\303\yy.txt");


            chart1.Series.Clear();
            chart1.Series.Add(" y0 ");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series.Add(" y1 ");
            chart1.Series[1].ChartType = SeriesChartType.Line;
            x0 = Convert.ToDouble(textBox1.Text);
            xl = Convert.ToDouble(textBox2.Text);
            h = Convert.ToDouble(textBox3.Text);
            for (double Lamda = -2; Lamda <= 10; Lamda++)
            {


                y[0] = Convert.ToDouble(textBox4.Text);
                y[1] = Convert.ToDouble(textBox5.Text);

                // Метод Эйлера (Адамса)
                for (double x = x0; x <= xl; x = x + h)
                {
                    for (i = 0; i <= 1; i++)
                    {
                        dy4[i] = dy3[i];
                        dy3[i] = dy2[i];
                        dy2[i] = dy1[i];
                        dy1[i] = dy[i];
                    }
                    dy = f(x, y, Lamda);
                    for (i = 0; i <= 1; i++)
                    {
                        if (x < x0 + 4 * h)
                        {
                            y[i] = y[i] + dy[i] * h;
                        }
                        else
                        {
                            y[i] = y[i] + (h / 24.0) * (55 * dy[i] - 59 * dy1[i] + 37 * dy2[i] - 9 * dy3[i]);
                        }
                    }
                    chart1.Series[0].Points.AddXY(x, y[0]);
                    chart1.Series[1].Points.AddXY(x, y[1]);
                    //xx.Write(x + " ");
                    //ll.Write(Lamda + " ");
                    //yy.Write(y[0] + " ");
                }
                if (Lamda < 10)
                {
                    //xx.Write("\n ");
                    //ll.Write("\n ");
                    //yy.Write("\n ");
                }
            }
            //xx.Close();
            //ll.Close();
            //yy.Close();
        }
    }
}
