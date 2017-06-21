using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interpol_Lagrang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        #region Переменные

        int col_x;
        string func;
        double[] x;
        double[] y;
        double shag = 0.1, ogr_x = 10;

        #endregion

        #region События

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                shag = double.Parse(textBox_Shag.Text.Replace('.', ','));
                ogr_x = double.Parse(textBox_X.Text.Replace('.', ','));
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поля ввода: Шаг графиков и Ограничение графиков");
                return;
            }
            if (func == null)
            {
                MessageBox.Show("Выберите функцию");
                return;
            }
            Init_chart();
            x = new double[col_x];
            y = new double[col_x];
            try
            {
                for (int i = 0; i < col_x; i++)
                {
                    x[i] = double.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString().Replace('.', ','));
                    y[i] = Get_Znachenie(func, x[i]);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Введено недопустимое X в таблицу");
                return;
            }

            draw_graphics();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            func = comboBox1.SelectedItem.ToString();

        }

        private void textBox_Tochki_TextChanged(object sender, EventArgs e)
        {
            try
            {
                col_x = int.Parse(textBox_Tochki.Text == "" || textBox_Tochki.Text == null ? "0" : textBox_Tochki.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поля ввода");
                return;
            }
            dataGridView1.Rows.Clear();
            Init_Grid();
           
        }

        #endregion

        #region Функции

        private void Init_chart()
        {
            chart1.Series[0].Name = func;
            chart1.Series[1].Name = func + " интерполяция";
            chart1.Series[2].Name = "Значения " + func + " в X";
        }

        private void Init_Grid()
        {
            for (int i = 0; i < col_x; i++)
            {
                dataGridView1.Rows.Add();
            }
        }

        private double Get_Znachenie(string func, double x)
        {
            switch(func)
            {
                case "sin(x)": return Math.Sin(x);
                case "cos(x)": return Math.Cos(x);
                case "tg(x)": return Math.Tan(x);
                case "ctg(x)": return -1*Math.Tan(x);
                case "e^x": return Math.Exp(x);
                default: return 0;
            }
        }

        private double Lagrange(double t, double[] x, double[] y)
        {
            double f, l = 0;

            for (int i = 0; i < col_x; i++)
            {
                f = 1;
                for (int j = 0; j < col_x; j++)
                {
                    if (i != j)
                    {
                        f *= (t - x[j]) / (x[i] - x[j]);
                    }
                }
                f *= y[i];
                l += f;
            }

            return l;
        }

        private void draw_graphics()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            for (int i = 0; i < col_x; i++)
            {
                chart1.Series[2].Points.AddXY(x[i], y[i]);
            }

            for (double i = -ogr_x; i < ogr_x; i += shag)
            {
                chart1.Series[0].Points.AddXY(i, Get_Znachenie(func, i));
                chart1.Series[1].Points.AddXY(i, Lagrange(i, x, y));
            }

           
        }

        #endregion


    }
}
