namespace Kontrolkatemp1
{
    partial class Polaczenie
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bttn_polaczenie = new System.Windows.Forms.Button();
            this.txt_testuj_polaczenie = new System.Windows.Forms.Button();
            this.txt_nr_portu = new System.Windows.Forms.TextBox();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.okres_nmrc = new System.Windows.Forms.NumericUpDown();
            this.bttn_stop = new System.Windows.Forms.Button();
            this.bttn_start = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_polaczenia_tlstrp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.baza_tlstrp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.uzytkownik_tlstrp = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1_odczyt = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2_zapis = new System.ComponentModel.BackgroundWorker();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.okres_nmrc)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.bttn_polaczenie);
            this.groupBox1.Controls.Add(this.txt_testuj_polaczenie);
            this.groupBox1.Controls.Add(this.txt_nr_portu);
            this.groupBox1.Controls.Add(this.txt_ip);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(21, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Konfiguracja po³¹czenia";
            // 
            // bttn_polaczenie
            // 
            this.bttn_polaczenie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bttn_polaczenie.Location = new System.Drawing.Point(41, 173);
            this.bttn_polaczenie.Name = "bttn_polaczenie";
            this.bttn_polaczenie.Size = new System.Drawing.Size(271, 40);
            this.bttn_polaczenie.TabIndex = 5;
            this.bttn_polaczenie.Text = "Po³¹cznie";
            this.bttn_polaczenie.UseVisualStyleBackColor = true;
            this.bttn_polaczenie.Click += new System.EventHandler(this.bttn_polaczenie_Click);
            // 
            // txt_testuj_polaczenie
            // 
            this.txt_testuj_polaczenie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_testuj_polaczenie.Location = new System.Drawing.Point(41, 129);
            this.txt_testuj_polaczenie.Name = "txt_testuj_polaczenie";
            this.txt_testuj_polaczenie.Size = new System.Drawing.Size(271, 40);
            this.txt_testuj_polaczenie.TabIndex = 4;
            this.txt_testuj_polaczenie.Text = "Ping";
            this.txt_testuj_polaczenie.UseVisualStyleBackColor = true;
            this.txt_testuj_polaczenie.Click += new System.EventHandler(this.txt_testuj_polaczenie_Click);
            // 
            // txt_nr_portu
            // 
            this.txt_nr_portu.Enabled = false;
            this.txt_nr_portu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_nr_portu.Location = new System.Drawing.Point(192, 88);
            this.txt_nr_portu.Name = "txt_nr_portu";
            this.txt_nr_portu.Size = new System.Drawing.Size(100, 26);
            this.txt_nr_portu.TabIndex = 3;
            // 
            // txt_ip
            // 
            this.txt_ip.Enabled = false;
            this.txt_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txt_ip.Location = new System.Drawing.Point(192, 53);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(100, 26);
            this.txt_ip.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(60, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numer portu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(93, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adres ip:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.okres_nmrc);
            this.groupBox2.Controls.Add(this.bttn_stop);
            this.groupBox2.Controls.Add(this.bttn_start);
            this.groupBox2.Location = new System.Drawing.Point(21, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 140);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pomiar w czasie rzeczywistym";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(60, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Okres T [s]:";
            // 
            // okres_nmrc
            // 
            this.okres_nmrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.okres_nmrc.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.okres_nmrc.Location = new System.Drawing.Point(192, 42);
            this.okres_nmrc.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.okres_nmrc.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.okres_nmrc.Name = "okres_nmrc";
            this.okres_nmrc.Size = new System.Drawing.Size(120, 26);
            this.okres_nmrc.TabIndex = 7;
            this.okres_nmrc.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // bttn_stop
            // 
            this.bttn_stop.Enabled = false;
            this.bttn_stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bttn_stop.Location = new System.Drawing.Point(192, 81);
            this.bttn_stop.Name = "bttn_stop";
            this.bttn_stop.Size = new System.Drawing.Size(138, 40);
            this.bttn_stop.TabIndex = 6;
            this.bttn_stop.Text = "Stop";
            this.bttn_stop.UseVisualStyleBackColor = true;
            this.bttn_stop.Click += new System.EventHandler(this.bttn_stop_Click);
            // 
            // bttn_start
            // 
            this.bttn_start.Enabled = false;
            this.bttn_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bttn_start.Location = new System.Drawing.Point(21, 81);
            this.bttn_start.Name = "bttn_start";
            this.bttn_start.Size = new System.Drawing.Size(138, 40);
            this.bttn_start.TabIndex = 5;
            this.bttn_start.Text = "Start";
            this.bttn_start.UseVisualStyleBackColor = true;
            this.bttn_start.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.status_polaczenia_tlstrp,
            this.toolStripStatusLabel2,
            this.baza_tlstrp,
            this.toolStripStatusLabel3,
            this.uzytkownik_tlstrp});
            this.statusStrip1.Location = new System.Drawing.Point(0, 425);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(406, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(101, 17);
            this.toolStripStatusLabel1.Text = "Status po³¹czenia:";
            // 
            // status_polaczenia_tlstrp
            // 
            this.status_polaczenia_tlstrp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.status_polaczenia_tlstrp.Name = "status_polaczenia_tlstrp";
            this.status_polaczenia_tlstrp.Size = new System.Drawing.Size(74, 17);
            this.status_polaczenia_tlstrp.Text = "Nieaktywne";
            this.status_polaczenia_tlstrp.ToolTipText = "Nieaktywne";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabel2.Text = "Baza:";
            // 
            // baza_tlstrp
            // 
            this.baza_tlstrp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.baza_tlstrp.Name = "baza_tlstrp";
            this.baza_tlstrp.Size = new System.Drawing.Size(16, 17);
            this.baza_tlstrp.Text = "...";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabel3.Text = "U¿ytkownik:";
            // 
            // uzytkownik_tlstrp
            // 
            this.uzytkownik_tlstrp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.uzytkownik_tlstrp.Name = "uzytkownik_tlstrp";
            this.uzytkownik_tlstrp.Size = new System.Drawing.Size(16, 17);
            this.uzytkownik_tlstrp.Text = "...";
            // 
            // backgroundWorker1_odczyt
            // 
            this.backgroundWorker1_odczyt.WorkerReportsProgress = true;
            this.backgroundWorker1_odczyt.WorkerSupportsCancellation = true;
            this.backgroundWorker1_odczyt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_odczyt_DoWork);
            this.backgroundWorker1_odczyt.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_odczyt_RunWorkerCompleted);
            // 
            // backgroundWorker2_zapis
            // 
            this.backgroundWorker2_zapis.WorkerReportsProgress = true;
            this.backgroundWorker2_zapis.WorkerSupportsCancellation = true;
            this.backgroundWorker2_zapis.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_zapis_DoWork);
            this.backgroundWorker2_zapis.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_zapis_RunWorkerCompleted);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Polaczenie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 447);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Polaczenie";
            this.Text = "Po³¹czenie ";
            this.Load += new System.EventHandler(this.Polaczenie_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Polaczenie_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.okres_nmrc)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button txt_testuj_polaczenie;
        private System.Windows.Forms.TextBox txt_nr_portu;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bttn_stop;
        private System.Windows.Forms.Button bttn_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown okres_nmrc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel status_polaczenia_tlstrp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel uzytkownik_tlstrp;
        private System.Windows.Forms.ToolStripStatusLabel baza_tlstrp;
        private System.Windows.Forms.Button bttn_polaczenie;
        private System.ComponentModel.BackgroundWorker backgroundWorker1_odczyt;
        private System.ComponentModel.BackgroundWorker backgroundWorker2_zapis;
        private System.Windows.Forms.Timer timer2;
    }
}