using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Vectors : Form
    {
        public Vectors()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView4.AllowUserToAddRows = false;
        }

        public int n;
        public double[] a1, b1, c1, d1;
        private bool can = false;

        private void button1_Click(object sender, EventArgs e)
        {
            can = true;
            for (int i = 0; i < n; i++)
            {
                try
                {
                    if (i != n - 1)
                    {
                        a1[i] = double.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString().Replace('.', ','));
                        b1[i] = double.Parse(dataGridView2.Rows[0].Cells[i].Value.ToString().Replace('.', ','));
                    }
                    c1[i] = double.Parse(dataGridView3.Rows[0].Cells[i].Value.ToString().Replace('.', ','));
                    d1[i] = double.Parse(dataGridView4.Rows[0].Cells[i].Value.ToString().Replace('.', ','));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            this.Close();
        }

        private void Vectors_Load(object sender, EventArgs e)
        {
            a1 = new double[n];
            b1 = new double[n];
            c1 = new double[n];
            d1 = new double[n];
            
            for (int i = 0; i < n; i++)
            {
                if (i != n - 1)
                {

                    dataGridView1.Columns.Add(i.ToString(), i.ToString());
                    dataGridView2.Columns.Add(i.ToString(), i.ToString());
                }
                dataGridView3.Columns.Add(i.ToString(), i.ToString());
                dataGridView4.Columns.Add(i.ToString(), i.ToString());
            }
            dataGridView1.Rows.Add();
            dataGridView2.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView4.Rows.Add();
        }

        private void Vectors_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!can) e.Cancel = true;
        }
    }
}
