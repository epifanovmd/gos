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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private double x = 10, y = 10, shag = 0.1;

        private void Form2_Load(object sender, EventArgs e)
        {
            Viv();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            try
            {
                x = double.Parse(textBox_X.Text.Replace('.', ','));
            }
            catch(Exception)
            {
                x = 10;
            }
            try
            {
                y = double.Parse(textBox_Y.Text.Replace('.', ','));
            }
            catch(Exception)
            {
                y = 10;
            }
            try
            {
                shag = double.Parse(textBox_Shag.Text.Replace('.', ','));
            }
            catch (Exception)
            {
                shag = 0.1;
            }
                Viv();
            
            
            
        }

        private void Viv()
        {
            double j = -y;
            for (double i = -x; i < x; i += shag)
            {
                chart1.Series[0].Points.AddXY(i, 2 - Math.Sin(i));
                chart1.Series[1].Points.AddXY(-3 - Math.Sin(j), j);
                j += shag;
            }
        }
    }
}
