using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
             
        }

        #region Переменные

        private double a,b,c = 0, eps, d = 0, x, x1, x0;
        private bool flag = false;       

        #endregion

        #region Вспомогательные функции


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private double Func(double x)
        {
            return Math.Cos(x) + x - 2;
        }

        private double FuncPr(double x)
        {
            return -Math.Sin(x) + 1;                                                                                                              
        }

        #endregion

        #region Метод деления пополам

        private void button1_Click(object sender, EventArgs e)
        {

            a = Double.Parse(textBox_a.Text.Replace('.', ','));
            b = Double.Parse(textBox_b.Text.Replace('.', ','));
            eps = Double.Parse(textBox_c.Text.Replace('.', ','));
            Chart_Init1();
            Solve();


        }

        private void Solve()
        {
            d = 0;
            if (Func(a) * Func(b) > 0)
            {
                MessageBox.Show("Корней нет на этом отрезке!");
            }
            else
            {
                do
                {
                    c = (a + b) / 2;
                    d++;
                    if (Func(c) * Func(b) < 0)
                    {
                        a = c;
                    }
                    else
                    {
                        b = c;
                    }

                }
                while (Math.Abs(b - a) > eps);
                labelResult.Text = c.ToString();
                label_fx.Text = Func(c).ToString();
                MessageBox.Show("Корень найден за " + d.ToString() + " шагов");
                label15_shag1.Text = d.ToString();
            }
        }

        private void Chart_Init1()
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                chart1.Series[0].Points.AddXY(i, Func(i));
            }
        }

        #endregion

        #region Метод Секущих

        private void button2_Click(object sender, EventArgs e)
        {
            a = Double.Parse(textBoxas.Text.Replace('.', ','));
            b = Double.Parse(textBoxbs.Text.Replace('.', ','));
            eps = Double.Parse(textBoxcs.Text.Replace('.', ','));
            Chart_Init2();
            Solve_Sec();

        }

        private void Solve_Sec()
        {
            d = 0;
             if (Func(a) * Func(b) > 0)
            {
                MessageBox.Show("Корней нет на этом отрезке!");
            }
            else
            {
                x = a;
                do
                {
                    x0 = x;
                    d++;
                    x = x0 - (Func(x0) / (Func(x0) - Func(b))) * (x0 - b);
                }
                while (Math.Abs(x - x0) > eps);
                label_RezX.Text = x.ToString();
                label_RezFx.Text = Func(x).ToString();
                MessageBox.Show("Корень найден за " + d.ToString() + " шагов");
                label15_shag2.Text = d.ToString();
             }
        }

        private void Chart_Init2()
        {
            chart2.Series[0].Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                chart2.Series[0].Points.AddXY(i, Func(i));
            }
        }

        #endregion

        #region Метод Ньютона

        private void button3_Click(object sender, EventArgs e)
        {
            Init_Nuton();
            Chart_Init3();
            Nuton();


        }

        private void Init_Nuton()
        {
            x = Double.Parse(textBox1_x0.Text.Replace('.',','));
            eps = Double.Parse(textBox2_eps.Text.Replace('.', ','));
        }

        private void Nuton()
        {
            d = 0;
            flag = false;
            do
            {
                if (flag)
                {
                    x = x1;
                }
                x1 = x - Func(x) / FuncPr(x);
                flag = true;
                d++;
            }
            while (Math.Abs(x1 - x) > eps);
            label14_Otvet.Text = x.ToString();
            MessageBox.Show("Корень найден за " + d.ToString() + " шагов");
            label15_shag3.Text = d.ToString();
        }               

        private void Chart_Init3()
        {
            chart3.Series[0].Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                chart3.Series[0].Points.AddXY(i, Func(i));
            }
        }



        #endregion

        #region Решение системы уравнений

        private double[,] sistema, sistema1;
        private int n, m, znak = 2, kol_korn = 0;
        string str, str1;

        private void button4_Click(object sender, EventArgs e)
        {
            kol_korn = n;
            double[] kr = new double[n];
            try
            {
                n = int.Parse(textBox_n.Text.Replace('.', ','));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            m = n + 1;
            Get_Sistema();
            sistema1 = (double[,])sistema.Clone();
            Vivod_Sistema(1, sistema);
            kr = Diag(sistema, ref str, ref str1);
            Vivod_Sistema(666, sistema);
            if (str == null)
                Vivod_Korn(kr);
            else
                label_OtvetX.Text = str;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kol_korn = n;
            double[] kr = new double[n];
            try
            {
                n = int.Parse(textBox_n.Text.Replace('.', ','));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            m = n + 1;
            Vvod_Element v = new Vvod_Element();
            v.k = n;
            v.ShowDialog();
            sistema = (double[,])v.mass.Clone();
            sistema1 = (double[,])sistema.Clone();
            Vivod_Sistema(1, sistema);
            kr = Diag(sistema, ref str, ref str1);
            Vivod_Sistema(666, sistema);
            if (str == null)
                Vivod_Korn(kr);
            else
                label_OtvetX.Text = str;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
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
            if (n > 0)
            {
                double[] kr = new double[n];
                sistema = (double[,])sistema1.Clone();
                Vivod_Sistema(1, sistema);
                kr = Diag(sistema, ref str, ref str1);
                Vivod_Sistema(666, sistema);
                if (str == null)
                    Vivod_Korn(kr);
                else
                    label_OtvetX.Text = str;
            }
        }

        private void Get_Sistema()
        {
            sistema = new double[n, m];
            Random rnd = new Random();
            for (int i = 0; i < sistema.GetLength(0); i++)
            {
                for (int j = 0; j < sistema.GetLength(1); j++)
                {
                    sistema[i, j] = (double)rnd.Next(0, 21);
                }
            }
        }

        private double[] Diag(double[,] sistema, ref string str, ref string str1)
        {
            str = null;
            str1 = null;
            double shag, rr;
            double[] xx = new double[n];
            int pp;
            bool zn = false;

            for (int i = 0; i < sistema.GetLength(0); i++)
            {
                
                shag = sistema[i, i];
                if (shag == 0)
                {
                    for (int z = i + 1; z < sistema.GetLength(0); z++)
                    {
                        if (sistema[z,i] != 0)
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
                for (int j = i + 1; j < sistema.GetLength(1) - 1; j++)
                {
                    shag = sistema[j, i];
                    for (pp = sistema.GetLength(0); pp >= i; pp--)
                    {
                        if (sistema[j, pp] == 0) 
                        { 
                            continue; 
                        }
                        sistema[j, pp] -= shag * sistema[i, pp];
                    }
                }
            }

                xx[sistema.GetLength(0) - 1] = sistema[sistema.GetLength(0) - 1, sistema.GetLength(1) - 1];

                for (int i = sistema.GetLength(0) - 2; i >= 0; i--)
                {
                    xx[i] = sistema[i, sistema.GetLength(1) - 1];

                    for (int j = i + 1; j < sistema.GetLength(0); j++)
                    {
                        xx[i] -= sistema[i, j] * xx[j];
                        sistema[i, j] = 0;
                    }
                    sistema[i, sistema.GetLength(1) - 1] = xx[i];
                }
            str1 = "Система линейно зависима, сделаем замену для корней:\n";
                for (int i = 0; i < sistema.GetLength(0); i++)
                {

                    if (sistema[i, i] == 0)
                    {
                        if (sistema[i, sistema.GetLength(1) - 1] == 0)
                        {
                            if (!zn)
                            zn = true;
                            xx[i] = 0;
                            str1 += "x" + (i + 1).ToString() + " = 0; ";
                        }
                        else
                        {
                            str = "Система не имеет решений";
                        }

                    }

                }
                if (!zn) str1 = null;
                else
                {
                    str1 = str1.Remove(str1.Length - 2, 1);
                    str1 += '\n';
                }
           
            
            return xx;
        }

        private string Vivod_Sistema(int k, double[,] sistema)
        {
            string str = null;
            for (int i = 0; i < sistema.GetLength(0); i++)
            {
                for (int j = 0; j < sistema.GetLength(1); j++)
                {
                    
                    if (j == sistema.GetLength(0))
                    {
                        str += '|' + Math.Round(sistema[i, j], znak).ToString().Replace(',','.');
                    }
                    else if (j == sistema.GetLength(0) - 1)
                    {
                        str += Math.Round(sistema[i, j], znak).ToString().Replace(',', '.');
                    }
                    else
                    {
                        str += Math.Round(sistema[i, j], znak).ToString().Replace(',', '.') + ',';
                    }
                }
                str += '\n';
            }
            if (k == 1)
            {
                label_Sistema.Text = str;
            }
            else if (k == 666)
            {
                label_Stupenki.Text = str;
            }
            return str;
        }

        private void Vivod_Korn(double[] krn)
        {
            string str = null;
            
                for (int i = 0; i < n; i++)
                {
                    str += 'x' + (i + 1).ToString() + " = " + Math.Round(krn[i], znak).ToString() + '\n';
                }
                if (str1 != null)
                {
                    label_OtvetX.Text = str1 + "-----------------------------------------------------\n";
                    label_OtvetX.Text += str;
                }
                else
                {
                    label_OtvetX.Text = str;
                }
        }



      

        #endregion      

        #region Метод прогонки

        private double[] aa, bb, cc, dd;
        private int kk, znak1;
        private bool key = false;

        private void button6_Click(object sender, EventArgs e)
        {
            double[] xx = new double[kk];
            if (!key)
            znak1 = 5;          
            try
            {
                kk = int.Parse(textBox1.Text.Replace('.', ','));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            key = true;
            aa = new double[kk];
            bb = new double[kk];
            cc = new double[kk];
            dd = new double[kk];
            Zadanie_Rnd();
            Vivod_Vector();
            label_Uslovie.Text = Proverka_Ustoi();
            xx = Progonka(aa, bb, cc, dd);
            Vivod_Kr(xx);
            Proverka(xx);
            
        }

        private void Zadanie_Rnd()
        {
 
            Random rnd = new Random();
              for (int j = 0; j < kk; j++)
                {
                    if (j == 0)
                    {

                        goto m1;
                    }
                    else
                    {
                        aa[j] = rnd.Next(0, 25);
                    }
              m1: if (j == kk - 1)
                    {
                        goto m2;
                    }
                    else
                    {
                        bb[j] = rnd.Next(0, 25);
                    }
              m2:cc[j] = rnd.Next(0, 25);
                    dd[j] = rnd.Next(0, 25);
                }
           
        }

        

        private void Vivod_Vector()
        {
            label_Matrix.Text = null;
            string sr = "a = { ";
            for (int i = 1; i < kk; i++)
            {
                if (i < kk - 1)
                sr += aa[i].ToString() + ", ";
                else
                    sr += aa[i].ToString();
            }
            sr += " }\nb = { ";
            for (int i = 0; i < kk - 1; i++)
            {
                if (i < kk - 2)
                    sr += bb[i].ToString() + ", ";
                else
                    sr += bb[i].ToString();
            }
            sr += " }\nc = { ";
            for (int i = 0; i < kk; i++)
            {
                if (i < kk - 1)
                    sr += cc[i].ToString() + ", ";
                else
                    sr += cc[i].ToString();
            }
            sr += " }\nd = { ";
            for (int i = 0; i < kk; i++)
            {
                if (i < kk - 1)
                    sr += dd[i].ToString() + ", ";
                else
                    sr += dd[i].ToString();
            }
            label_Matrix.Text = sr + " }";
        }

        private double[] Progonka(double[] aa, double[] bb, double[] cc, double[] dd)
        {
            int nn = 0;
            double[] xx = new double[kk];
            double[] p = new double[kk - 1];
            double[] q = new double[kk - 1];

            if (cc[0] == 0)
            {
                p[0] = 0;
                q[0] = 0;
            }
            else
            {
                p[0] = bb[0] / (-cc[0]);
                q[0] = -dd[0] / (-cc[0]);
            }

            aa[0] = 0; bb[kk - 1] = 0;

            for (int i = 1; i < kk - 1; i++)
            {
                if (-cc[i] - aa[i] * p[i - 1] == 0)
                {
                    p[i] = 0;
                    q[i] = 0;
                }
                else
                {
                    p[i] = bb[i] / (-cc[i] - aa[i] * p[i - 1]);
                    q[i] = (aa[i] * q[i - 1] - dd[i]) / (-cc[i] - aa[i] * p[i - 1]);
                }
            }
            if (-cc[kk - 1] - aa[kk - 1] * p[kk - 2] == 0)
            {
                xx[0] = 0;
            }
            else
            {
                 xx[0] = (aa[kk - 1] * q[kk - 2] - dd[kk - 1]) / (-cc[kk - 1] - aa[kk - 1] * p[kk - 2]);
            }

            for (int i = kk - 2; i >= 0; i--)
            {
                xx[kk - 1 - i] = p[i] * xx[nn] + q[i];
                nn++;
            }
            Array.Reverse(xx);
            return xx;
        }

       private void Vivod_Kr(double[] xx)
        {
           label_MOtvet.Text = null;
           label_MOtvet.Text = "x = { ";
           for (int i = 0; i < kk; i++)
           {
               if (i < kk - 1)
                   label_MOtvet.Text += Math.Round(xx[i], znak1).ToString().Replace(',','.') + ",\n      ";
               else
                   label_MOtvet.Text += Math.Round(xx[i], znak1).ToString().Replace(',', '.') + " }";
           }
        }


       private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
       {
           switch (comboBox2.SelectedIndex)
           {
               case 0: znak1 = 1; break;
               case 1: znak1 = 2; break;
               case 2: znak1 = 3; break;
               case 3: znak1 = 4; break;
               case 4: znak1 = 5; break;
               case 5: znak1 = 6; break;
               case 6: znak1 = 7; break;
               case 7: znak1 = 8; break;
               case 8: znak1 = 9; break;
               case 9: znak1 = 10; break;
           }

           if (key) Vivod_Kr(Progonka(aa, bb, cc, dd));
       }

       private void button7_Click(object sender, EventArgs e)
       {
           double[] xx = new double[kk];
           try
           {
               kk = int.Parse(textBox1.Text);
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               return;
           }
           Vectors v = new Vectors();
           v.n = kk;
           v.ShowDialog();
           aa = (double[])v.a1.Clone();
           sdvig(aa);
           bb = (double[])v.b1.Clone();
           cc = (double[])v.c1.Clone();
           dd = (double[])v.d1.Clone();
           Vivod_Vector();
           label_Uslovie.Text = Proverka_Ustoi();
           xx = Progonka(aa, bb, cc, dd);
           Vivod_Kr(xx);
           Proverka(xx);
       }

       private void sdvig(double[] ms)
       {

           for (int i = ms.GetLength(0) - 1; i > 0; i--)
           {
               ms[i] = ms[i - 1];
           }
       }

        private void Proverka(double[] xx)
       {
           label_Proverka.Text = null;
           for (int i = 0; i < xx.GetLength(0); i++)
           {
               if (i == 0)
               {
                   if (cc[i]* xx[i] + bb[i]*xx[i + 1] == dd[i])
                   {
                       label_Proverka.Text += "x[" + i.ToString() + "] - Проверку прошёл успешно\n";
                   }
                   else
                   {
                       label_Proverka.Text += "x[" + i.ToString() + "] - Проверку НЕ прошёл\n";
                   }
                  
               }
               else if (i == kk - 1)
               {
                   if (aa[i]*xx[i -1] + cc[i] * xx[i] == dd[i])
                   {
                       label_Proverka.Text += "x[" + i.ToString() + "] - Проверку прошёл успешно\n";
                   }
                   else
                   {
                       label_Proverka.Text += "x[" + i.ToString() + "] - Проверку НЕ прошёл\n";
                   }
               }
               else
               {
                   if (aa[i] * xx[i - 1] + cc[i] * xx[i] + bb[i] * xx[i + 1] == dd[i])
                   {
                       label_Proverka.Text += "x[" + i.ToString() + "] - Проверку прошёл успешно\n";
                   }
                   else
                   {
                       label_Proverka.Text += "x[" + i.ToString() + "] - Проверку НЕ прошёл\n";
                   }
               }
           }
           
       }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = -1;

        }

        private string Proverka_Ustoi()
        {
            for (int i = 0; i < kk; i++)
            {
                if (i == 0)
                {
                    if (Math.Abs(cc[i]) < Math.Abs(bb[i]))
                    {
                        return "Диагональные элементы НЕ преобладают\n в данной системе векторов";
                    }
                }
                else if (i == kk - 1)
                {
                    if (Math.Abs(cc[i]) < Math.Abs(aa[i]))
                    {
                        return "Диагональные элементы НЕ преобладают\n в данной системе векторов";
                    }
                }
                else
                {
                    if (Math.Abs(cc[i]) < Math.Abs(aa[i]) + Math.Abs(bb[i]))
                    {
                        return "Диагональные элементы НЕ преобладают\n в данной системе векторов";
                    }

                }
            }
            return "Диагональные элементы Преобладают\n в данной системе векторов";
        }


        #endregion






    


    }
}
