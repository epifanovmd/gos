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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public double f(double k)
        {
            return Math.Sin(k);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double a, b, eps;
            double c, c1, l, d;

            a = Convert.ToDouble(textBox1.Text);
            b = Convert.ToDouble(textBox2.Text);
            eps = Convert.ToDouble(textBox3.Text);

            if (f(a) * f(b) < 0)
            {
                do
                {
                    c = a;
                    c1 = c;
                    l = ((f(a) - f(b)) / (a - b));
                    d = f(a) - l * a;
                    c = -d / l;

                    if (f(a) * f(c) < 0)
                    {
                        b = c;
                    }
                    else
                    {
                        a = c;
                    }
                } while (Math.Abs(c1 - c) > eps);
                label5.Text = Convert.ToString(c);
                label7.Text = Convert.ToString(f(c));
            }
            else
            {
                MessageBox.Show("Введите корректные данные");
            }

        }


    }
}
