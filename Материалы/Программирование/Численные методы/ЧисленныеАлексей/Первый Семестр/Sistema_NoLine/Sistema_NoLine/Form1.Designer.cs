namespace Sistema_NoLine
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Solve = new System.Windows.Forms.Button();
            this.textBox_eps = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_x0 = new System.Windows.Forms.TextBox();
            this.textBox_y0 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_Sistema = new System.Windows.Forms.Label();
            this.label_Iakob = new System.Windows.Forms.Label();
            this.label_Korni = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_Krn = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Solve
            // 
            this.button_Solve.Location = new System.Drawing.Point(409, 12);
            this.button_Solve.Name = "button_Solve";
            this.button_Solve.Size = new System.Drawing.Size(94, 39);
            this.button_Solve.TabIndex = 0;
            this.button_Solve.Text = "Решить";
            this.button_Solve.UseVisualStyleBackColor = true;
            this.button_Solve.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_eps
            // 
            this.textBox_eps.Location = new System.Drawing.Point(44, 22);
            this.textBox_eps.Name = "textBox_eps";
            this.textBox_eps.Size = new System.Drawing.Size(100, 20);
            this.textBox_eps.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Погрешность";
            // 
            // textBox_x0
            // 
            this.textBox_x0.Location = new System.Drawing.Point(255, 12);
            this.textBox_x0.Name = "textBox_x0";
            this.textBox_x0.Size = new System.Drawing.Size(100, 20);
            this.textBox_x0.TabIndex = 3;
            // 
            // textBox_y0
            // 
            this.textBox_y0.Location = new System.Drawing.Point(255, 53);
            this.textBox_y0.Name = "textBox_y0";
            this.textBox_y0.Size = new System.Drawing.Size(100, 20);
            this.textBox_y0.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "x0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "y0";
            // 
            // label_Sistema
            // 
            this.label_Sistema.AutoSize = true;
            this.label_Sistema.Location = new System.Drawing.Point(41, 117);
            this.label_Sistema.Name = "label_Sistema";
            this.label_Sistema.Size = new System.Drawing.Size(84, 26);
            this.label_Sistema.TabIndex = 7;
            this.label_Sistema.Text = "sin(x) + y - 2 = 0\r\nx + sin(y) + 3 = 0";
            // 
            // label_Iakob
            // 
            this.label_Iakob.AutoSize = true;
            this.label_Iakob.Location = new System.Drawing.Point(185, 117);
            this.label_Iakob.Name = "label_Iakob";
            this.label_Iakob.Size = new System.Drawing.Size(74, 26);
            this.label_Iakob.TabIndex = 8;
            this.label_Iakob.Text = "cos(x)     1\r\n    1       cos(x)";
            // 
            // label_Korni
            // 
            this.label_Korni.AutoSize = true;
            this.label_Korni.Location = new System.Drawing.Point(44, 198);
            this.label_Korni.Name = "label_Korni";
            this.label_Korni.Size = new System.Drawing.Size(0, 13);
            this.label_Korni.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Начальная система уравнений";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(185, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Якобиан системы";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Корни системы";
            // 
            // label_Krn
            // 
            this.label_Krn.AutoSize = true;
            this.label_Krn.Location = new System.Drawing.Point(13, 150);
            this.label_Krn.Name = "label_Krn";
            this.label_Krn.Size = new System.Drawing.Size(0, 13);
            this.label_Krn.TabIndex = 13;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "до 1-го знака",
            "до 2-го знака",
            "до 3-го знака",
            "до 4-го знака",
            "до 5-го знака",
            "до 6-го знака",
            "до 7-го знака",
            "до 8-го знака",
            "до 9-го знака",
            "до 10-го знака"});
            this.comboBox1.Location = new System.Drawing.Point(384, 121);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(384, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Округление";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 45);
            this.button1.TabIndex = 16;
            this.button1.Text = "Построить\r\nграфики";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 354);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label_Krn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_Korni);
            this.Controls.Add(this.label_Iakob);
            this.Controls.Add(this.label_Sistema);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_y0);
            this.Controls.Add(this.textBox_x0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_eps);
            this.Controls.Add(this.button_Solve);
            this.Name = "Form1";
            this.Text = "Решение системы нелинейных уравнений";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Solve;
        private System.Windows.Forms.TextBox textBox_eps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_x0;
        private System.Windows.Forms.TextBox textBox_y0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_Sistema;
        private System.Windows.Forms.Label label_Iakob;
        private System.Windows.Forms.Label label_Korni;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_Krn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
    }
}

