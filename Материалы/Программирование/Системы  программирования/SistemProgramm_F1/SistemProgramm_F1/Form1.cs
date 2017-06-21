using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemProgramm_F1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x1, y1, x2, y2, x3, y3;
            x1 = double.Parse(textBox_x1.Text.Replace('.', ','));
            x2 = double.Parse(textBox_x2.Text.Replace('.', ','));
            x3 = double.Parse(textBox_x3.Text.Replace('.', ','));
            y1 = double.Parse(textBox_y1.Text.Replace('.', ','));
            y2 = double.Parse(textBox_y2.Text.Replace('.', ','));
            y3 = double.Parse(textBox_y3.Text.Replace('.', ','));
            label_Result.Text = FindPerimetr(FindLenght(x1, y1, x2, y2), FindLenght(x2, y2, x3, y3), FindLenght(x3, y3, x1,y1)).ToString();

        }

        private double FindLenght(double x, double y, double x1, double y1)
        {
            return Math.Sqrt(Math.Pow(x1 -x, 2)+ Math.Pow(y1-y,2));
        }
        
        private double FindPerimetr(double a, double b, double c)
        {
            return (a + b + c);
        }
    }
}
