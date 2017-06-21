using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chisl_Resh_Integral
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, h, p, t, s, o;

            a = Double.Parse(textBox_a.Text.Replace('.',','));
            b = Double.Parse(textBox_b.Text.Replace('.', ','));
            h = Double.Parse(textBox_h.Text.Replace('.', ','));
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show(this.Owner, "Выберите функцию для начала", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            p = Priam(a, b, h);
            t = Trap(a, b, h);
            s = Simpson(a, b, h);
            o = Original(a, b);

            label8.Text = p.ToString();
            label9.Text = t.ToString();
            label10.Text = s.ToString();
            label12.Text = o.ToString();

            label14.Text = (o - p).ToString();
            label15.Text = (o - t).ToString();
            label16.Text = (o - s).ToString();
            label17.Text = (o - o).ToString();

            p = toch_met(a, b, h, "p");
            t = toch_met(a, b, h, "t");
            s = toch_met(a, b, h, "s");


            if (!double.IsNaN(p))
            {
                label19.Text = p.ToString();
            }
            else
            {
                label19.Text = "Точность максимальная";
            }

            if (!double.IsNaN(t))
            {
                label20.Text = t.ToString();
            }
            else
            {
                label20.Text = "Точность максимальная";
            }

            if (!double.IsNaN(s))
            {
                label21.Text = s.ToString();
            }
            else
            {
                label21.Text = "Точность максимальная";
            }
        }

        private double Trap(double a, double b, double h)
        {
            double sum = 0;

            for (double i = a; i < b; i += h)
            {
                sum += ((Fnc(i) + Fnc(i + h)) / 2) * h;
            }

            return sum;
        }

        private double Priam(double a, double b, double h)
        {
            double sum = 0;

            for (double i = a; i < b; i += h)
            {
                sum += Fnc(i) * h;
            }

            return sum;
        }


        private double Simpson(double a, double b, double h)
        {
            double sum = 0;

            for (double i = a; i < b; i += h)
            {
                sum += ((Fnc(i) + 4 * Fnc(i + h / 2) + Fnc(i + h)) / 6) * h;
            }

            return sum;
        }

        private double Original(double a, double b)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0: return (2 * Math.Pow(b, 3 / 2) / 3) - (2 * Math.Pow(a, 3 / 2) / 3);
                case 1: return (b * b * b) / 3 - (a * a * a) / 3;
                case 2: return Math.Exp(b) - Math.Exp(a);
                case 3: return -Math.Cos(b) + Math.Cos(a);
                case 4: return -Math.Log(Math.Abs(Math.Cos(b))) + Math.Log(Math.Abs(Math.Cos(a)));
            }
            return 0;
            
        }

        private double toch_met(double a, double b, double h, string str)
        {
            double q = 2;
            if (str == "p")
                return 1 / Math.Log(q) * Math.Log((Priam(a, b, h * q * q) - Priam(a, b, h * q)) / (Priam(a, b, h * q) - Priam(a, b, h)));
            else if (str == "t")
                return 1/Math.Log(q) * Math.Log((Trap(a, b, h * q * q) - Trap(a, b, h * q)) / (Trap(a, b, h *q) - Trap(a, b, h)));
            else if (str == "s")
                return 1 / Math.Log(q) * Math.Log((Simpson(a, b, h * q * q) - Simpson(a, b, h * q)) / (Simpson(a, b, h * q) - Simpson(a, b, h)));
            return -9999999;

        }

            
        private double Fnc(double x)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0: return Math.Sqrt(x);
                case 1: return x * x;
                case 2: return Math.Exp(x);
                case 3: return Math.Sin(x);
                case 4: return Math.Tan(x);
            }
            return -1;
        }
    }
}
