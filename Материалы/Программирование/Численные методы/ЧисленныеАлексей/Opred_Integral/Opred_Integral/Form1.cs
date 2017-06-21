using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opred_Integral
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double f(double x)
        {
            return Math.Cos(x);
        }

        private double int_f(double x)
        {
            return Math.Sin(x);
        }

        //Формулa прямоугольников. 
        double Priamougolnik(double a, double b, double h)
        {
            double x, s = 0;
            for (x = a; x < b; x += h)
            {
                s += f(x) * h;
            }
            return s;
        }
        //Формула трапеций.
        double Trapetia(double a, double b, double h)
        {
            double x, s = 0;
            for (x = a; x < b; x += h)
            {
                s += (f(x) + f(x + h)) * h / 2;
            }
            return s;
        }
        //Формула Симпсона.
        double Simpson(double a, double b, double h)
        {
            double x, s = 0;
            for (x = a; x < b; x += h)
            {
                s += (f(x) + 4 * f(x + h / 2) + f(x + h)) * h / 6;
            }
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, h;
            a = double.Parse(textBox1.Text.Replace('.',','));
            b = double.Parse(textBox2.Text.Replace('.', ','));
            h = double.Parse(textBox3.Text.Replace('.', ','));
            textBox_Rez1.Text = Simpson(a, b, h).ToString();
            textBox_Rez2.Text = (int_f(b) - int_f(a)).ToString();
        }
    }
}
