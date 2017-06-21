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
    public partial class Form3 : Form
    {
        public Form3()
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
            double c;

            a = Convert.ToDouble(textBox1.Text);
            b = Convert.ToDouble(textBox2.Text);
            eps = Convert.ToDouble(textBox3.Text);

            if (f(a) - f(b) < 0)
            {
                do
                {
                    c = (a + b) / 2;
                    if (f(c) * f(a) < 0)
                    {
                        b = c;
                    }
                    else
                    {
                        a = c;
                    }
                } while (Math.Abs(b - a) > eps);
                label5.Text = Convert.ToString(c);
                label7.Text = Convert.ToString(f(c));

            }
            else
            {
                MessageBox.Show("Введите корректные данные", "Ошибка");
            }

        }


    }
}
