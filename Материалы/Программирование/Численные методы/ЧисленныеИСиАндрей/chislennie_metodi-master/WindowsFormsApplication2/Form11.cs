using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }
        double intf(double x)
        { return (2 / 3.0) * Math.Pow(x, 3 / 2.0); }
        double f(double x)
        { return Math.Sqrt(x); }
        //Формулa прямоугольников. 
        double F1(double a, double b, double h)
        {
            double x, s = 0;
            for (x = a; x < b; x = x + h) s = s + f(x) * h;
            return s;
        }
        //Формула трапеций.
        double F2(double a, double b, double h)
        {
            double x, s = 0;
            for (x = a; x < b; x = x + h) s = s + (f(x) + f(x + h)) * h / 2;
            return s;
        }
        //Формула Симпсона.
        double F3(double a, double b, double h)
        {
            double x, s = 0;
            for (x = a; x < b; x = x + h) s = s + (f(x) + 4 * f(x + h / 2) + f(x + h)) * h / 6;
            return s;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, h, g, gg, ggg, gggg;
            a = Convert.ToDouble(textBox1.Text);
            b = Convert.ToDouble(textBox2.Text);
            h = Convert.ToDouble(textBox3.Text);
            g = intf(b) - intf(a);
            gg = g - F1(a, b, h);
            ggg = g - F2(a, b, h);
            gggg = g - F3(a, b, h);
            label4.Text = "S(Формулa прямоугольников)= " + F1(a, b, h);
            label5.Text = "S(Формула трапеций)= " + F2(a, b, h);
            label6.Text = "S(Формула Симпсона)= " + F3(a, b, h);
            label7.Text = "S=" + g;
            label8.Text = "S1=" + gg;
            label9.Text = "S2=" + ggg;
            label10.Text = "S3=" + gggg;
            double p1 = (1 / Math.Log(2)) * Math.Log((F1(a, b, 4 * h) - F1(a, b, 2 * h)) / (F1(a, b, 2 * h) - F1(a, b, h)));
            label11.Text = "" + p1;
            double p2 = (1 / Math.Log(2)) * Math.Log((F2(a, b, 4 * h) - F2(a, b, 2 * h)) / (F2(a, b, 2 * h) - F2(a, b, h)));
            label12.Text = "" + p2;
            double p3 = (1 / Math.Log(2)) * Math.Log((F3(a, b, 4 * h) - F3(a, b, 2 * h)) / (F3(a, b, 2 * h) - F3(a, b, h)));
            label13.Text = "" + p3;

        }
    }
}
