namespace Soldador
{
    partial class Form1
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
            this.Status = new System.Windows.Forms.Label();
            this.BConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BProg = new System.Windows.Forms.Button();
            this.Zval = new System.Windows.Forms.TextBox();
            this.Xval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBox = new System.Windows.Forms.ComboBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Roda = new System.Windows.Forms.Button();
            this.MM = new System.Windows.Forms.RadioButton();
            this.TProg = new System.Windows.Forms.Timer(this.components);
            this.Matriz = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(22, 20);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(118, 13);
            this.Status.TabIndex = 0;
            this.Status.Text = "Status: Não Conectado";
            // 
            // BConnect
            // 
            this.BConnect.Location = new System.Drawing.Point(22, 47);
            this.BConnect.Name = "BConnect";
            this.BConnect.Size = new System.Drawing.Size(94, 29);
            this.BConnect.TabIndex = 1;
            this.BConnect.Text = "Start";
            this.BConnect.UseVisualStyleBackColor = true;
            this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BProg);
            this.groupBox1.Controls.Add(this.Zval);
            this.groupBox1.Controls.Add(this.Xval);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CBox);
            this.groupBox1.Location = new System.Drawing.Point(19, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 99);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comandos";
            // 
            // BProg
            // 
            this.BProg.Location = new System.Drawing.Point(6, 63);
            this.BProg.Name = "BProg";
            this.BProg.Size = new System.Drawing.Size(303, 23);
            this.BProg.TabIndex = 4;
            this.BProg.Text = "Programa";
            this.BProg.UseVisualStyleBackColor = true;
            this.BProg.Click += new System.EventHandler(this.BProg_Click);
            // 
            // Zval
            // 
            this.Zval.Enabled = false;
            this.Zval.Location = new System.Drawing.Point(260, 37);
            this.Zval.Name = "Zval";
            this.Zval.Size = new System.Drawing.Size(50, 20);
            this.Zval.TabIndex = 3;
            this.Zval.Leave += new System.EventHandler(this.Zval_Leave);
            // 
            // Xval
            // 
            this.Xval.Enabled = false;
            this.Xval.Location = new System.Drawing.Point(204, 37);
            this.Xval.Name = "Xval";
            this.Xval.Size = new System.Drawing.Size(50, 20);
            this.Xval.TabIndex = 2;
            this.Xval.Leave += new System.EventHandler(this.Xval_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Posição [X, Y/Z] :";
            // 
            // CBox
            // 
            this.CBox.FormattingEnabled = true;
            this.CBox.Items.AddRange(new object[] {
            "0 - NOP",
            "1 - MOVEJ",
            "2 - MOVEL",
            "3 - ZMOVE",
            "4 - DELAY_1S"});
            this.CBox.Location = new System.Drawing.Point(6, 32);
            this.CBox.Name = "CBox";
            this.CBox.Size = new System.Drawing.Size(101, 21);
            this.CBox.TabIndex = 0;
            this.CBox.Text = "0 - NOP";
            this.CBox.UseWaitCursor = true;
            this.CBox.SelectedIndexChanged += new System.EventHandler(this.CBox_SelectedIndexChanged);
            // 
            // Timer
            // 
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Matriz);
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Controls.Add(this.Roda);
            this.groupBox2.Location = new System.Drawing.Point(19, 191);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 338);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Programação";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(320, 195);
            this.richTextBox1.TabIndex = 36;
            this.richTextBox1.Text = "";
            // 
            // Roda
            // 
            this.Roda.Location = new System.Drawing.Point(4, 200);
            this.Roda.Name = "Roda";
            this.Roda.Size = new System.Drawing.Size(305, 23);
            this.Roda.TabIndex = 35;
            this.Roda.Text = "Roda";
            this.Roda.UseVisualStyleBackColor = true;
            this.Roda.Click += new System.EventHandler(this.Roda_Click);
            // 
            // MM
            // 
            this.MM.AutoSize = true;
            this.MM.Location = new System.Drawing.Point(211, 53);
            this.MM.Name = "MM";
            this.MM.Size = new System.Drawing.Size(95, 17);
            this.MM.TabIndex = 4;
            this.MM.TabStop = true;
            this.MM.Text = "Movimentando";
            this.MM.UseVisualStyleBackColor = true;
            // 
            // TProg
            // 
            this.TProg.Interval = 500;
            this.TProg.Tick += new System.EventHandler(this.TProg_Tick);
            // 
            // Matriz
            // 
            this.Matriz.AcceptsReturn = true;
            this.Matriz.AcceptsTab = true;
            this.Matriz.AllowDrop = true;
            this.Matriz.Location = new System.Drawing.Point(3, 249);
            this.Matriz.Multiline = true;
            this.Matriz.Name = "Matriz";
            this.Matriz.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Matriz.Size = new System.Drawing.Size(306, 81);
            this.Matriz.TabIndex = 5;
            this.Matriz.Click += new System.EventHandler(this.Matriz_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Matriz :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 541);
            this.Controls.Add(this.MM);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BConnect);
            this.Controls.Add(this.Status);
            this.Name = "Form1";
            this.Text = "Programa SCARA";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Button BConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BProg;
        private System.Windows.Forms.TextBox Zval;
        private System.Windows.Forms.TextBox Xval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBox;
        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton MM;
        private System.Windows.Forms.Timer TProg;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button Roda;
        private System.Windows.Forms.TextBox Matriz;
        private System.Windows.Forms.Label label2;
    }
}

