namespace Kontrolkatemp1
{
    partial class Archiwum
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.data = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.l_os_y = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RBgauss = new System.Windows.Forms.RadioButton();
            this.RBSREDNIA = new System.Windows.Forms.RadioButton();
            this.RBMAX = new System.Windows.Forms.RadioButton();
            this.RBMIN = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 57);
            this.panel2.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.data);
            this.groupBox1.Location = new System.Drawing.Point(21, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // data
            // 
            this.data.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.data.Location = new System.Drawing.Point(39, 15);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(200, 20);
            this.data.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(773, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 3;
            this.button1.Text = "Odœwie¿";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(21, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(827, 481);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wykres";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.l_os_y);
            this.panel1.Location = new System.Drawing.Point(8, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 398);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(763, 451);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Godzina";
            // 
            // l_os_y
            // 
            this.l_os_y.AutoSize = true;
            this.l_os_y.Location = new System.Drawing.Point(3, 10);
            this.l_os_y.Name = "l_os_y";
            this.l_os_y.Size = new System.Drawing.Size(29, 13);
            this.l_os_y.TabIndex = 3;
            this.l_os_y.Text = "Iloœæ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RBMIN);
            this.groupBox3.Controls.Add(this.RBMAX);
            this.groupBox3.Controls.Add(this.RBSREDNIA);
            this.groupBox3.Controls.Add(this.RBgauss);
            this.groupBox3.Location = new System.Drawing.Point(291, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(369, 41);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Typ wykresu";
            // 
            // RBgauss
            // 
            this.RBgauss.AutoSize = true;
            this.RBgauss.Location = new System.Drawing.Point(29, 15);
            this.RBgauss.Name = "RBgauss";
            this.RBgauss.Size = new System.Drawing.Size(61, 17);
            this.RBgauss.TabIndex = 0;
            this.RBgauss.TabStop = true;
            this.RBgauss.Text = "Gaussa";
            this.RBgauss.UseVisualStyleBackColor = true;
            // 
            // RBSREDNIA
            // 
            this.RBSREDNIA.AutoSize = true;
            this.RBSREDNIA.Location = new System.Drawing.Point(96, 15);
            this.RBSREDNIA.Name = "RBSREDNIA";
            this.RBSREDNIA.Size = new System.Drawing.Size(61, 17);
            this.RBSREDNIA.TabIndex = 1;
            this.RBSREDNIA.TabStop = true;
            this.RBSREDNIA.Text = "Œrednia";
            this.RBSREDNIA.UseVisualStyleBackColor = true;
            // 
            // RBMAX
            // 
            this.RBMAX.AutoSize = true;
            this.RBMAX.Location = new System.Drawing.Point(163, 15);
            this.RBMAX.Name = "RBMAX";
            this.RBMAX.Size = new System.Drawing.Size(84, 17);
            this.RBMAX.TabIndex = 2;
            this.RBMAX.TabStop = true;
            this.RBMAX.Text = "Maksymalna";
            this.RBMAX.UseVisualStyleBackColor = true;
            // 
            // RBMIN
            // 
            this.RBMIN.AutoSize = true;
            this.RBMIN.Location = new System.Drawing.Point(248, 15);
            this.RBMIN.Name = "RBMIN";
            this.RBMIN.Size = new System.Drawing.Size(72, 17);
            this.RBMIN.TabIndex = 3;
            this.RBMIN.TabStop = true;
            this.RBMIN.Text = "Minimalna";
            this.RBMIN.UseVisualStyleBackColor = true;
            // 
            // Archiwum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 556);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Name = "Archiwum";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Archiwum_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Archiwum_Paint);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker data;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label l_os_y;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton RBSREDNIA;
        private System.Windows.Forms.RadioButton RBgauss;
        private System.Windows.Forms.RadioButton RBMIN;
        private System.Windows.Forms.RadioButton RBMAX;

    }
}