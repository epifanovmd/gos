using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nuton_KorniUravn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private double f(double k)
        {
            return Math.Sin(k);
        }

        private double df(double k)
        {
            return Math.Cos(k);
        }


        private double Nuton(double x_nach, double eps)
        {
            double x0 = x_nach, x;
            do
            {
                x = x0;
                x0 = x - f(x) / df(x);
            }
            while (Math.Abs(x0 - x) > eps);
            return x0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            double  eps, a, b, shag = 0.1;
            double x_nach = double.Parse(textBox_Xnach.Text.Replace('.', ',')); 
            a = double.Parse(textBox_a.Text.Replace('.', ','));
            b = double.Parse(textBox_b.Text.Replace('.', ','));
            eps = double.Parse(textBox2.Text.Replace('.', ','));

            textBox_Rezult.Text += Nuton(x_nach, eps).ToString() + "\r\n";

        }


        private void button2_Click(object sender, EventArgs e)
        {
            double a, b, shag = 0.1;
            a = double.Parse(textBox_a.Text.Replace('.', ','));
            b = double.Parse(textBox_b.Text.Replace('.', ','));
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "Sin(x)";
            for (double xx = a; xx <= b; xx += shag)
            {
                chart1.Series[0].Points.AddXY(xx, f(xx));
            }
        }
    }
}
