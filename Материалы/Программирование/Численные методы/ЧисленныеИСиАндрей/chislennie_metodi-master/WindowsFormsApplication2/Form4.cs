using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public double f(double k)
        {
            return Math.Sin(k);
        }

        public double df(double k)
        {
            return Math.Cos(k);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double x0, x, eps, a, b, shag = 0.1;
            //   double x_nach; //Раскомментировать и указать примерно корень по графику(если нужно найти корень по графику)
            List<double> krn = new List<double>();
            a = double.Parse(textBox_a.Text.Replace('.',',').Replace('.', ','));
            b = double.Parse(textBox_b.Text.Replace('.', ',').Replace('.', ','));
            eps = double.Parse(textBox2.Text.Replace('.', ',').Replace('.', ','));
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "Sin(x)";
            textBox_Rezult.Text = null;
            for (double xx = a; xx <= b; xx +=0.1)
            {
                chart1.Series[0].Points.AddXY(xx, f(xx));
            }
            for (double i = a; i <= b; i+=shag) //Убрать цикл(если нужно найти корень по графику)
            {
                if (Math.Abs(f(i)) < shag) //Убрать условие(если нужно найти корень по графику)   
                {
                    x0 = i;   //Вставить x_nach(если нужно найти корень по графику)   
                    do
                    {
                        x = x0;
                        x0 = x - f(x) / df(x);
                    }
                    while (Math.Abs(x0 - x) > eps);

                    if (IsRepeatKoren(x0, krn))  //Убрать условие(если нужно найти корень по графику)                         
                    {                       
                        krn.Add(x0);
                    }
                   
                }

            }

            for (int i = 0; i < krn.Count; i++)
            {
                textBox_Rezult.Text += string.Format("x{0} = {1};\r\n", i, krn[i]);
            }
          
         
        }


        private bool IsRepeatKoren(double x, List<double> krn)
        {
            if (krn.Count == 0)
                return true;
            int sovp = 0;
            foreach (double kor in krn)
            {
                if (Math.Abs(kor - x) < 1E-20)
                    sovp++;
                if (Math.Abs(kor - x) < 1E-3 ||  sovp > 1)
                    return false;
            }
            return true;
        }
    }
}
