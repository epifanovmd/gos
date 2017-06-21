using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Vvod_Element : Form
    {
        public Vvod_Element()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }

        public int k;
        public double[,] mass;
        private bool can = false;

        private void Vvod_Element_Load(object sender, EventArgs e)
        {
            mass = new double[k, k + 1];          
            for (int i = 0; i < k; i++ )
            {
                dataGridView1.Columns.Add(i.ToString(), "");
                dataGridView1.Rows.Add();
                
            }
            dataGridView1.Columns.Add(k.ToString(), "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            can = true;
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                for (int j = 0; j < mass.GetLength(1); j++)
                {
                    try
                    {
                        mass[i, j] = double.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString().Replace('.',','));
                    }
                    catch(Exception ex)
                    {                           
                        MessageBox.Show(ex.Message, "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;            
                    }
                }
            }
            this.Close();
        }

        private void Vvod_Element_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!can) e.Cancel = true;
        }


    }
}
