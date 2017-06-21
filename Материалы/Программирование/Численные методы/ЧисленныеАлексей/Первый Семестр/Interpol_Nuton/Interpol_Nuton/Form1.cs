using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpol_Nuton
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToResizeColumns = false;
            dataGridView2.AllowUserToResizeRows = false;
            
            
        }

        #region Переменные
        int col_x, col1, ind;
        string func;
        double[] x, y;
        double[,] ms;
        double shag;
        int pp = 0;

        #endregion

        #region События

        private void button1_Click(object sender, EventArgs e)
        {
            pp = 0;
            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();
            ind = 0;


            if (func == null)
            {
                MessageBox.Show("Выберите функцию для начала");
                return;
            }
            try
            {
                col_x = int.Parse(textBox_Tochki.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поле \"Указать кол - во точек\"");
                return;
            }

            try
            {
                shag = double.Parse(textBox_X.Text.Replace('.', ','));
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поле \"Ограничение по оси X\"");
                return;
            }
            x = new double[col_x];
            y = new double[col_x];
            ms = new double[col_x, col_x + 1];

            try
            {
                for (int i = 0; i < col_x + 1; i++)
                {
                    if (i == 0)
                    {
                        dataGridView2.Columns.Add("column" + i.ToString(), "x");
                    }
                    else
                    {
                        dataGridView2.Columns.Add("column" + i.ToString(), "y" + (i - 1).ToString());
                        dataGridView2.Columns[i].Width = 60;
                    }
                    if (i < col_x)
                    {
                        dataGridView2.Rows.Add();
                        x[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Replace('.', ','));
                        ms[i, ind] = x[i];
                    }
                   
                }
                ind++;
                for (int i = 0; i < col_x; i++)
                {
                    y[i] = Get_Znach(func, x[i]);
                    ms[i, ind] = Math.Round(y[i], 3);
                }
                ind++;
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поля ввода таблицы X");
                
                return;
            }

            chart1.Series[0].Name = func;
            chart1.Series[1].Name = func + " интерполяция";
            chart1.Series[2].Name = "Значения " + func + " в X";
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
                            
            for (double i = -shag; i < shag; i += 0.1)
            {
               
                chart1.Series[0].Points.AddXY(i, Get_Znach(func, i));
              //chart1.Series[1].Points.AddXY(i, Newton(x, y, i));              
               chart1.Series[1].Points.AddXY(i, Newtn(x, y, i));
            }

            for (int i = 0; i < col_x; i++)
            {
                chart1.Series[2].Points.AddXY(x[i], (Get_Znach(func, x[i])));
            }

            Vivod_mass();
          
        }

        private void textBox_Tochki_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Tochki.Text == null || textBox_Tochki.Text == "") { return; }
            try
            {
                col_x = int.Parse(textBox_Tochki.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поле \"Указать кол - во точек\"");
                return;
            }
           
            if (col1 > col_x)
            {
                for (int i = col1 - 1; i >= col_x; i--)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
            else
            {
                for (int i = col1; i < col_x; i++)
                {
                    dataGridView1.Rows.Add();
                }
            }
            col1 = col_x;
        }

     

        #endregion     

        #region Функции

        private double Get_Znach(string func, double x)
        {
            switch(func)
            {
                case "sin(x)": return Math.Sin(x);
                case "cos(x)": return Math.Cos(x);
                case "tg(x)": return Math.Tan(x);
                case "ctg(x)": return (-1 * Math.Tan(x));
                case "e^x": return Math.Exp(x);
                default: return 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private double Pn(double x, double[] xx, int k )
        {
            double p = 1;
            for (int i = 0; i < k; i++)
            {
                p *= (x - xx[i]);
            }
            return p;
        }

         private double Newtn(double[] x, double[] y, double x0)
        {
            double L, P;

            L = ms[0, 1];
         
            for (int k = 1; k < col_x; k++)
            {
                P = Pn(x0, x, k);
                if (pp == 0)
                {
                    for (int i = 0; i < col_x - k; i++)
                    {
                        y[i] = (y[i + 1] - y[i]) / (x[i + k] - x[i]);
                            ms[i, ind] = y[i];
                    }
                        ind++;
                    
                }

                L += P * ms[0, k + 1];
            }
            pp = 1;
            
            return L;

        }
        

        private void Vivod_mass()
        {
      
           
            for (int i = 0; i < col_x; i++)
            {
                for (int j = 0; j < col_x + 1; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = ms[i, j];                     
                }          
            }
        }


        #endregion


    }
}
