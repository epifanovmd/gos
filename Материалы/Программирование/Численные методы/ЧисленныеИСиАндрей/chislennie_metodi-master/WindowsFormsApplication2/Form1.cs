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
    public partial class Form1 : Form
    {
        TextBox[,] t = new TextBox[10, 10 + 1];
        Label[,] l = new Label[10, 10 + 1];

        public Form1()
        {
            InitializeComponent();
            comboBox1.Text = "2";
        }
        private void matrix(int n)
        {
            panel1.Controls.Clear();
            int point1 = 0, point2 = 10;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    t[i, j] = new TextBox();
                    
                    if (j == n)
                    {
                        point1 += 10;
                        l[i, j] = new Label();
                        l[i,j].Location = new Point(point1-22, point2+2);
                        l[i, j].Text = " = ";
                        l[i, j].AutoSize = true;
                    }
                    else
                    {
                        if (j < n-1)
                        {
                            l[i, j] = new Label();
                            l[i, j].Location = new Point(point1 + 50, point2 + 2);
                            l[i, j].Text = "x" + (j + 1) + " + ";
                            l[i, j].AutoSize = true;
                        }
                        else
                        {
                            l[i, j] = new Label();
                            l[i, j].Location = new Point(point1 + 50, point2 + 2);
                            l[i, j].Text = "x" + (j + 1);
                            l[i, j].AutoSize = true;
                        }
                    }

                    t[i, j].Location = new Point(point1, point2);
                    t[i, j].Size = new Size(47, 20);

                    panel1.Controls.Add(t[i, j]);
                    panel1.Controls.Add(l[i, j]);
                    point1 += 80;
                }
                point2 += 30;
                point1 = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            matrix(Convert.ToInt32(comboBox1.Text));
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            matrix(Convert.ToInt32(comboBox1.Text));
        }
        public void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(comboBox1.Text);
            double buf;
            double[,] a = new double[n, n + 1];
            double[] x = new double[n];
            try
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n + 1; j++)
                    {
                        a[i, j] = Convert.ToDouble(t[i, j].Text);
                    }
                }
            }
            catch { MessageBox.Show("Заполните поля правильно", "Ошибка"); return; }
            //Прямой ход метода Гаусса
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    buf = a[i, i] / a[j, i];
                    for (int k = 0; k <= n; k++) a[j, k] = a[j, k] * buf - a[i, k];
                }
            //Обратный ход метода Гаусса
            x[n - 1] = a[n - 1, n] / a[n - 1, n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                buf = 0;
                for (int j = i + 1; j < n; j++) buf += a[i, j] * x[j];
                x[i] = (a[i, n] - buf) / a[i, i];
            }
            richTextBox1.Text = "";
            for (int i = 0; i < n; i++)
            {
                richTextBox1.Text += "x" + (i + 1) + " = " + x[i] + ";\n";
            }
        }
    }
}
