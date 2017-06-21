using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Metod_Eilera_Sistemi_Dif
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x0, y0, y1, xn, h;
            double lambda0 = 0, lambda1 = 3;
            x0 = double.Parse(textBox_x0.Text.Replace('.', ','));
            y0 = double.Parse(textBox_y0.Text.Replace('.', ','));
            y1 = double.Parse(textBox_y1.Text.Replace('.', ','));
            xn = double.Parse(textBox_xn.Text.Replace('.', ','));
            h = double.Parse(textBox_h.Text.Replace('.', ','));
            int kol = (int)(xn / h);
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart3.Series[1].Points.Clear();
            chart4.Series[0].Points.Clear();
            chart4.Series[1].Points.Clear();
            chart5.Series[0].Points.Clear();
            chart5.Series[1].Points.Clear();
            //Eiler(x0, xn, y0, y1, h);
            Modify_Eiler(x0, xn, y0, y1, h);
          //  Runge_Kutta(x0, xn, y0, y1, h);
           // Adams(x0, xn, y0, y1, h);
           // Adams_Parametrs(x0, xn, y0, y1, h, lambda0, lambda1);
         
        }


        private double[] func(double x, double[] y)
        {
            double[] p = new double[2];
            p[0] = y[1]; 
            p[1] = -(1-x*y[1])/(x*x);
            return p;
        }

        private double[] func1(double x, double[] y, double lambda)
        {
            double[] p = new double[2];
            p[0] = y[1];
            // p[1] = -(1 / x) * y[1] - y[0];
            p[1] = -((1 - x * y[1]) / (x * x))*lambda;
            return p;
        }

        private void Eiler(double x0, double xn, double y0, double y1, double h)
        {
            double[] dy = new double[2];
            double[] y = new double[2];
            y[0] = y0;
            y[1] = y1;
            for (double i = x0; i <= xn; i += h)
            {
                dy = func(i, y);
                for (int j = 0; j < 2; j++)
                {
                    y[j] = y[j] + h * dy[j];
                }
                chart1.Series[0].Points.AddXY(i, y[0]);
                chart1.Series[1].Points.AddXY(i, y[1]);
            }
            return;
        }

        private void Runge_Kutta(double x0, double xn, double y0, double y1, double h)
        {
            double[] y = new double[2];
            double[] y_1 = new double[2];
            double[] k1 = new double[2];
            double[] k2 = new double[2];
            double[] k3 = new double[2];
            double[] k4 = new double[2];
            y[0] = y0;
            y[1] = y1;
            for (double i = x0; i <= xn; i += h)
            {
                for (int k = 0; k < 2; k++)
                {
                    y_1[k] = y[k];
                }
                k1 = func(i, y);
                for (int k = 0; k < 2; k++)
                {
                    y[k] = y_1[k] + k1[k]*h/2;
                }
                k2 = func(i + h / 2, y);
                for (int k = 0; k < 2; k++)
                {
                    y[k] = y_1[k] + k2[k] * h / 2;
                }
                k3 = func(i + h / 2, y);
                for (int k = 0; k < 2; k++)
                {
                    y[k] = y_1[k] + k3[k] * h;
                }
                k4 = func(i + h, y);
                for (int k = 0; k < 2; k++)
                {
                    y[k] = y_1[k] + h * (k1[k] + 2*k2[k] + 2*k3[k] + k4[k]) / 6;
                }
                chart3.Series[0].Points.AddXY(i, y[0]);
                chart3.Series[1].Points.AddXY(i, y[1]);
            }
            return;
        }

        private void Modify_Eiler(double x0, double xn, double y0, double y1, double h)
        {
            double[] dy = new double[2];
            double[] dy1 = new double[2];
            double[] y = new double[2];
            double[] y_1 = new double[2];
            y[0] = y0;
            y[1] = y1;
            for (double i = x0; i <= xn; i += h)
            {
                for (int k = 0; k < 2; k++)
                {
                    y_1[k] = y[k];
                }
                dy1 = func(i, y);
                for (int j = 0; j < 2; j++)
                {
                    y[j] = y_1[j] + h * dy1[j];
                }
                dy = func(i, y);
                for (int j = 0; j < 2; j++)
                {
                    y[j] = y_1[j] + h * (dy1[j] + dy[j]) / 2;
                }
                chart2.Series[0].Points.AddXY(i, y[0]);
                chart2.Series[1].Points.AddXY(i, y[1]);
            }
            return;
        }

        private void Adams(double x0, double xn, double y0, double y1, double h)
        {
            
            double[] y = new double[2];
            double[] y_1 = new double[2];
            double[] dy = new double[2];
            double[] dy1 = new double[2];
            double[] dy2 = new double[2];
            double[] dy3 = new double[2];
            double[] dy4 = new double[2];
            y[0] = y0;
            y[1] = y1;
            
            
            for (double x = x0; x < xn; x += h)
            {
                for (int i = 0; i <= 1; i++)
                {
                    dy4[i] = dy3[i];
                    dy3[i] = dy2[i];
                    dy2[i] = dy1[i];
                    dy1[i] = dy[i];
                }
                dy = func(x, y);
                for (int i = 0; i <= 1; i++)
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
                chart4.Series[0].Points.AddXY(x, y[0]);
                chart4.Series[1].Points.AddXY(x, y[1]);
            }
            return;
        }

        private void Adams_Parametrs(double x0, double xn, double y0, double y1, double h, double lambada0, double lambda1)
        {
            StreamWriter X = new StreamWriter(@"D:\x.txt");
            StreamWriter Y = new StreamWriter(@"D:\y.txt");
            StreamWriter Z = new StreamWriter(@"D:\z.txt");
            double[] y = new double[2];
            double[] y_1 = new double[2];
            double[] dy = new double[2];
            double[] dy1 = new double[2];
            double[] dy2 = new double[2];
            double[] dy3 = new double[2];
            double[] dy4 = new double[2];

            for (double lambda = lambada0; lambda < lambda1; lambda+=h)
            {
                y[0] = y0;
                y[1] = y1;
                for (double x = x0; x < xn; x += h)
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        dy4[i] = dy3[i];
                        dy3[i] = dy2[i];
                        dy2[i] = dy1[i];
                        dy1[i] = dy[i];
                    }
                    dy = func1(x, y, lambda);
                    for (int i = 0; i <= 1; i++)
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
                    chart5.Series[0].Points.AddXY(x, y[0]);
                    chart5.Series[1].Points.AddXY(x, y[1]);
                    X.Write(x + " ");
                    Y.Write(lambda + " ");
                    Z.Write(y[0] + " ");
                }
                if (lambda + h < lambda1)
                {
                    X.Write("\n");
                    Y.Write("\n");
                    Z.Write("\n");
                }
            }
            X.Close();
            Y.Close();
            Z.Close();
            return;
        }

       
    }
}
