using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cubic_inerpol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }


        #region Структуры

        // Структура, описывающая сплайн на каждом сегменте сетки
        private struct SplineTuple
        {
            public double a, b, c, d, x;
        }

        #endregion

        #region Переменные

        SplineTuple[] splines; // Сплайн
        string func;
        int col_x, col1;
        double ogr_x, shag = 0.1;
        double[] x, y;

        #endregion

        #region События

        private void button1_Click(object sender, EventArgs e)
        {
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
                ogr_x = double.Parse(textBox_X.Text.Replace('.', ','));
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте поле \"Ограничение по оси X\"");
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
                    y[i] = Get_Znach(func, x[i]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введено недопустимое X в таблицу");
                return;
            }
            draw_graphics();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: func = "sin(x)"; return;
                case 1: func = "cos(x)"; return;
                case 2: func = "tg(x)"; return;
                case 3: func = "ctg(x)"; return;
                case 4: func = "e^x"; return;
            }
        }

        #endregion

        #region Функции

        private double Get_Znach(string func, double x)
        {
            switch (func)
            {
                case "sin(x)": return Math.Sin(x);
                case "cos(x)": return Math.Cos(x);
                case "tg(x)": return Math.Tan(x);
                case "ctg(x)": return (-1 * Math.Tan(x));
                case "e^x": return Math.Exp(x);
                default: return 0;
            }
        }

        private void Init_chart()
        {
            chart1.Series[0].Name = func;
            chart1.Series[1].Name = func + " интерполяция";
            chart1.Series[2].Name = "Значения " + func + " в X";
        }

        private void draw_graphics()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            BuildSpline(x, y, col_x);
            for (int i = 0; i < col_x; i++)
            {
                chart1.Series[2].Points.AddXY(x[i], y[i]);
            }

            for (double i = -ogr_x; i < ogr_x; i += shag)
            {
                chart1.Series[0].Points.AddXY(i, Get_Znach(func, i));
                chart1.Series[1].Points.AddXY(i, Interpolate(i));
            }


        }



        // Построение сплайна
        // x - узлы сетки, должны быть упорядочены по возрастанию, кратные узлы запрещены
        // y - значения функции в узлах сетки
        // n - количество узлов сетки
        public void BuildSpline(double[] x, double[] y, int n)
        {
            // Инициализация массива сплайнов
            splines = new SplineTuple[n];
            for (int i = 0; i < n; ++i)
            {
                splines[i].x = x[i];
                splines[i].a = y[i];
            }
            splines[0].c = splines[n - 1].c = 0.0;

            // Решение СЛАУ относительно коэффициентов сплайнов c[i] методом прогонки для трехдиагональных матриц
            // Вычисление прогоночных коэффициентов - прямой ход метода прогонки
            double[] alpha = new double[n - 1];
            double[] beta = new double[n - 1];
            alpha[0] = beta[0] = 0.0;
            for (int i = 1; i < n - 1; ++i)
            {
                double hi = x[i] - x[i - 1];
                double hi1 = x[i + 1] - x[i];
                double A = hi;
                double C = 2.0 * (hi + hi1);
                double B = hi1;
                double F = 6.0 * ((y[i + 1] - y[i]) / hi1 - (y[i] - y[i - 1]) / hi);
                double z = (A * alpha[i - 1] + C);
                alpha[i] = -B / z;
                beta[i] = (F - A * beta[i - 1]) / z;
            }

            // Нахождение решения - обратный ход метода прогонки
            for (int i = n - 2; i > 0; --i)
            {
                splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
            }

            // По известным коэффициентам c[i] находим значения b[i] и d[i]
            for (int i = n - 1; i > 0; --i)
            {
                double hi = x[i] - x[i - 1];
                splines[i].d = (splines[i].c - splines[i - 1].c) / hi;
                splines[i].b = hi * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi;
            }
        }

        // Вычисление значения интерполированной функции в произвольной точке
        public double Interpolate(double x)
        {
            if (splines == null)
            {
                return double.NaN; // Если сплайны ещё не построены - возвращаем NaN
            }

            int n = splines.Length;
            SplineTuple s;

            if (x <= splines[0].x) // Если x меньше точки сетки x[0] - пользуемся первым эл-тов массива
            {
                s = splines[0];
            }
            else if (x >= splines[n - 1].x) // Если x больше точки сетки x[n - 1] - пользуемся последним эл-том массива
            {
                s = splines[n - 1];
            }
            else // Иначе x лежит между граничными точками сетки - производим бинарный поиск нужного эл-та массива
            {
                int i = 0;
                int j = n - 1;
                while (i + 1 < j)
                {
                    int k = i + (j - i) / 2;
                    if (x <= splines[k].x)
                    {
                        j = k;
                    }
                    else
                    {
                        i = k;
                    }
                }
                s = splines[j];
            }

            double dx = x - s.x;
            // Вычисляем значение сплайна в заданной точке по схеме Горнера (в принципе, "умный" компилятор применил бы схему Горнера сам, но ведь не все так умны, как кажутся)
            return s.a + (s.b + (s.c / 2.0 + s.d * dx / 6.0) * dx) * dx;
        }

        #endregion



      





    }
}
