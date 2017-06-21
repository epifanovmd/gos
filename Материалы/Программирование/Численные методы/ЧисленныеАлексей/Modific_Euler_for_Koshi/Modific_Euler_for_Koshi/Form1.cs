using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modific_Euler_for_Koshi
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
            x0 = double.Parse(textBox_x0.Text.Replace('.', ','));
            y0 = double.Parse(textBox_y0.Text.Replace('.', ','));
            y1 = double.Parse(textBox_y1.Text.Replace('.', ','));
            xn = double.Parse(textBox_xn.Text.Replace('.', ','));
            h = double.Parse(textBox_h.Text.Replace('.', ','));
            int kol = (int)(xn / h);
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            Modify_Eiler(x0, xn, y0, y1, h);
        }

        private double[] func(double x, double[] y)
        {
            double[] p = new double[2];
            p[0] = y[1];
            p[1] = -(1 - x * y[1]) / (x * x);
            return p;
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
                chart1.Series[0].Points.AddXY(i, y[0]);
                chart1.Series[1].Points.AddXY(i, y[1]);
            }
            return;
        }
    }
}
