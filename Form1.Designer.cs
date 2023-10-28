namespace P322310540TM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            dataGridView2 = new DataGridView();
            Column2 = new DataGridViewTextBoxColumn();
            dataGridView3 = new DataGridView();
            Column3 = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            label3 = new Label();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(31, 95, 97);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1 });
            dataGridView1.Enabled = false;
            dataGridView1.GridColor = Color.FromArgb(31, 95, 97);
            dataGridView1.Location = new Point(12, 103);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(267, 359);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Pila";
            Column1.Name = "Column1";
            // 
            // dataGridView2
            // 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.BackgroundColor = Color.FromArgb(31, 95, 97);
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { Column2 });
            dataGridView2.Enabled = false;
            dataGridView2.GridColor = Color.FromArgb(31, 95, 97);
            dataGridView2.Location = new Point(309, 103);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(267, 359);
            dataGridView2.TabIndex = 1;
            // 
            // Column2
            // 
            Column2.HeaderText = "Resultado";
            Column2.Name = "Column2";
            // 
            // dataGridView3
            // 
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.BackgroundColor = Color.FromArgb(31, 95, 97);
            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Columns.AddRange(new DataGridViewColumn[] { Column3 });
            dataGridView3.Enabled = false;
            dataGridView3.GridColor = Color.FromArgb(31, 95, 97);
            dataGridView3.Location = new Point(605, 103);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.Size = new Size(267, 359);
            dataGridView3.TabIndex = 2;
            // 
            // Column3
            // 
            Column3.HeaderText = "Evaluacion";
            Column3.Name = "Column3";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(31, 95, 97);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.ForeColor = Color.FromArgb(48, 38, 28);
            textBox1.Location = new Point(12, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(267, 26);
            textBox1.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(31, 95, 97);
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.FromArgb(48, 38, 28);
            button1.Location = new Point(309, 12);
            button1.Name = "button1";
            button1.Size = new Size(267, 60);
            button1.TabIndex = 4;
            button1.Text = "Convertir";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(31, 95, 97);
            button2.Enabled = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.FromArgb(48, 38, 28);
            button2.Location = new Point(605, 12);
            button2.Name = "button2";
            button2.Size = new Size(267, 60);
            button2.TabIndex = 5;
            button2.Text = "Evaluar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(48, 38, 28);
            label2.Location = new Point(12, 465);
            label2.Name = "label2";
            label2.Size = new Size(162, 25);
            label2.TabIndex = 6;
            label2.Text = "Expresión prefija: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(48, 38, 28);
            label3.Location = new Point(12, 508);
            label3.Name = "label3";
            label3.Size = new Size(184, 25);
            label3.TabIndex = 7;
            label3.Text = "Expresión evaluada: ";
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(95, 31, 97);
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.Black;
            button3.Location = new Point(12, 536);
            button3.Name = "button3";
            button3.Size = new Size(860, 60);
            button3.TabIndex = 8;
            button3.Text = "Salir";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 129, 133);
            ClientSize = new Size(884, 611);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(dataGridView3);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Convertidor";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private DataGridView dataGridView3;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Label label2;
        private Label label3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button button3;
    }
}