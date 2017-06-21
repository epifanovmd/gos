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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form4().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Form5().ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Form6().ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Form7().ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Form8().ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Form9().ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new Form10().ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new Form11().ShowDialog();
        }
    }
}
