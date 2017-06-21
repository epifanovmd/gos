using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SistemProgramm_F3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string patternSum = @"(\s*\d+(?:[.|,]\d+)?\s*)[+](\s*\d+(?:[.|,]\d+)?\s*)";

        private void button1_Click(object sender, EventArgs e)
        {
            label_Result.Text = Regex.Replace(textBox_Text.Text, patternSum, (t) => (double.Parse(t.Groups[1].Value.Replace('.', ',')) +
                                                                        double.Parse(t.Groups[2].Value.Replace('.', ','))).ToString());
        }

  
    }
}
