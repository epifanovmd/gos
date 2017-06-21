using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema_NoLine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int n = 2, m = 2, znak = 5, sh;
        double x0, y0, eps, mx;
        double[,] W, W_Obr, F, xx;

        private void button1_Click(object sender, EventArgs e)
        {
            mx = 0;
            try
            {
                x0 = double.Parse(textBox_x0.Text.Replace('.', ','));
                y0 = double.Parse(textBox_y0.Text.Replace('.', ','));
                eps = double.Parse(textBox_eps.Text.Replace('.', ','));
            }
            catch(FormatException)
            {
                MessageBox.Show(this.Owner, "Ошибка при вводе числа", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show(this.Owner, "Ошибка при вводе числа", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            W = new double[n, 2*m];
            string str = null;
            F = new double[n, 1];
            sh = 0;
            double[,] F1 = new double[n, 1];
            xx = new double[,] { {x0}, 
                                 {y0} };
            W_Obr = new double[n, m];            
            do
            {
                W = Func_D1(W, xx[0,0], xx[1,0]); //Нахождение значений функции по матрице Якобиана
                Zap();  //Добавление единичной матрицы в конец массива W
                W_Obr = Diag(W); //Обратная матрица W
                F = Func(xx[0, 0], xx[1, 0]); //Матрица значений функции в итерационных корнях
                F1 = Umn(W_Obr, F); //Умножение обратной матрицы с матрицой F
                xx = Razn(xx, F1); //Вычитание из итерационных корней некие дельта из F1
                sh++;
            }
            while (Find_Max(F1, ref str));
            //if (str == "1")
           // {
                Vivod();
           // }
           /* else if (str == null)
            {
                label_Krn.Text = "None";
                label_Korni.Text = "Решение расходится";
            }
            else
            {
                label_Krn.Text = "None";
                label_Korni.Text = str;
            }*/
            
        }

        private double[,] Func(double x, double y)// Начальная система (независимая)
        {
            double[,] w = new double[n, 1];
            //w[0, 0] = x*x + x - y*y - 0.15;
            //w[1, 0] = x*x - y + y*y + 0.17;
            w[0, 0] = Math.Sin(x) + y - 2;
            w[1, 0] = x + Math.Sin(y) + 3;
            return w;
        }


        private double[,] Func_D1(double[,] w, double x0, double y0)// Якобиан
        {
            //w[0, 0] = 2*x0 + 1;      w[0, 1] = -2*y0;
            //w[1, 0] = 2*x0;          w[1, 1] = 2*y0 - 1;

            w[0, 0] = Math.Cos(x0); w[0, 1] = 1;
            w[1, 0] = 1; w[1, 1] = Math.Cos(y0);
           
            return w;
        }


        private double[,] Umn(double[,] w, double[,] w1)
        {
            double[,] w2 = new double[w1.GetLength(0), w1.GetLength(1)];
            int p = 0, p1 = 0;
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    w2[p1, p] += w[i, j] * w1[j, p];
                }
                if (p < w1.GetLength(1) - 1) { p++; }
                if (p1 < w1.GetLength(0) - 1) { p1++; }
            }
            return w2;
        }
        private bool Find_Max(double[,] w, ref string str)
        {
          
            double max = Math.Abs(w[0, 0]);
            for (int i = 0; i < w.GetLength(0); i++)
            {
                for (int j = 0; j < w.GetLength(1); j++)
                {
                    if (Math.Abs(w[i, j]) > max)
                    {
                        max = Math.Abs(w[i, j]);
                    }
                }
            }
            //if (mx < max && str != null)
           // {
             //   str = null;
             //   str = "Решение расходится";
             //   return false;
           // }
            if (max >= eps)
            {
               // mx = max;
              //  str = "1";
                return true;
            }
            else
            {
                return false;
            }
        }

       private double[,] Razn(double[,] F, double[,] w1)
        {
            double[,] w2 = new double[w1.GetLength(0), w1.GetLength(1)];
            for (int i = 0; i < w2.GetLength(0); i++)
            {
                for (int j = 0; j < w2.GetLength(1); j++)
                {
                    w2[i, j] = F[i, j] - w1[i, j];
                }
            }
            return w2;
        }

        private double[,] Diag(double[,] sistema)
        {

            double shag, rr;
            double[ , ] xx = new double[n, m];
            int pp;

            for (int i = 0; i < sistema.GetLength(0); i++)
            {

                shag = sistema[i, i];
                if (shag == 0)
                {
                    for (int z = i + 1; z < sistema.GetLength(0); z++)
                    {
                        if (sistema[z, i] != 0)
                        {
                            shag = sistema[z, i];
                            for (int j = 0; j < sistema.GetLength(1); j++)
                            {
                                rr = sistema[i, j];
                                sistema[i, j] = sistema[z, j];
                                sistema[z, j] = rr;
                            }
                            break;
                        }
                    }

                }
                if (shag != 1 && shag != 0)
                {
                    for (int j = sistema.GetLength(1) - 1; j >= i; j--)
                    {
                        sistema[i, j] /= shag;
                    }
                }
                for (int j = i + 1; j < sistema.GetLength(0); j++)
                {
                    shag = sistema[j, i];
                    for (pp = sistema.GetLength(1) - 1; pp >= i; pp--)
                    {
                      
                        sistema[j, pp] -= shag * sistema[i, pp];
                    }
                }
            }

            for (int z = 0; z < m - 1; z++)
            {
                for (int i = sistema.GetLength(0) - 2 - z; i >= 0; i--)
                {
                    shag = sistema[i, m - 1 - z];
                    for (int j = m - 1 - z; j < sistema.GetLength(1); j++)
                    {
                        
                        sistema[i, j] -= shag * sistema[sistema.GetLength(0) - 1 - z, j];
                    }
                }
            }

            for (int i = 0; i < xx.GetLength(0); i++)
            {
                for (int j = 0; j < xx.GetLength(1); j++)
                {
                    xx[i, j] = sistema[i, j + m];
                }
            }
            return xx;
        }

        private void Zap()
        {
            //Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < W.GetLength(1); j++)
                {

                    if (j < m)
                    {
                        // W[i, j] = (double)rnd.Next(1, 10);
                        continue;
                    }
                    else
                    {
                        if (i == j - m)
                        {
                            W[i, j] = 1;
                        }
                        else
                        {
                            W[i, j] = 0;
                        }
                    }
                }
            }
        }

        private void Vivod()
        {
            label_Krn.Text = "Корни были найдены за " + sh.ToString() + " шагов";
            label_Korni.Text = "x = " + Math.Round(xx[0, 0], znak).ToString();
            label_Korni.Text += "\ny = " + Math.Round(xx[1, 0], znak).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0: znak = 1; break;
                case 1: znak = 2; break;
                case 2: znak = 3; break;
                case 3: znak = 4; break;
                case 4: znak = 5; break;
                case 5: znak = 6; break;
                case 6: znak = 7; break;
                case 7: znak = 8; break;
                case 8: znak = 9; break;
                case 9: znak = 10; break;
            }
            if (label_Korni.Text != "")
            {
                Vivod();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }
    }
}
