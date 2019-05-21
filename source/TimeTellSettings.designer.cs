namespace TimeTell
{
    partial class TimeTellSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.RecentLabel = new System.Windows.Forms.Label();
            this.PercentOfAttempts = new System.Windows.Forms.RadioButton();
            this.FixedAttempts = new System.Windows.Forms.RadioButton();
            this.AttemptCountBox = new System.Windows.Forms.NumericUpDown();
            this.CreditsLabel = new System.Windows.Forms.Label();
            this.MaxPrecisionCountBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.alt_comp_mode_cb = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timeTellComponentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AttemptCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxPrecisionCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTellComponentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // RecentLabel
            // 
            this.RecentLabel.AutoSize = true;
            this.RecentLabel.Location = new System.Drawing.Point(3, 41);
            this.RecentLabel.Name = "RecentLabel";
            this.RecentLabel.Size = new System.Drawing.Size(87, 13);
            this.RecentLabel.TabIndex = 1;
            this.RecentLabel.Text = "Use most recent:";
            // 
            // PercentOfAttempts
            // 
            this.PercentOfAttempts.AutoSize = true;
            this.PercentOfAttempts.Checked = true;
            this.PercentOfAttempts.Location = new System.Drawing.Point(150, 32);
            this.PercentOfAttempts.Name = "PercentOfAttempts";
            this.PercentOfAttempts.Size = new System.Drawing.Size(102, 17);
            this.PercentOfAttempts.TabIndex = 2;
            this.PercentOfAttempts.TabStop = true;
            this.PercentOfAttempts.Text = "Percent of Splits";
            this.PercentOfAttempts.UseVisualStyleBackColor = true;
            // 
            // FixedAttempts
            // 
            this.FixedAttempts.AutoSize = true;
            this.FixedAttempts.Location = new System.Drawing.Point(150, 47);
            this.FixedAttempts.Name = "FixedAttempts";
            this.FixedAttempts.Size = new System.Drawing.Size(50, 17);
            this.FixedAttempts.TabIndex = 3;
            this.FixedAttempts.TabStop = true;
            this.FixedAttempts.Text = "Splits";
            this.FixedAttempts.UseVisualStyleBackColor = true;
            // 
            // AttemptCountBox
            // 
            this.AttemptCountBox.Location = new System.Drawing.Point(93, 39);
            this.AttemptCountBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.AttemptCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AttemptCountBox.Name = "AttemptCountBox";
            this.AttemptCountBox.Size = new System.Drawing.Size(51, 20);
            this.AttemptCountBox.TabIndex = 1;
            this.AttemptCountBox.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // CreditsLabel
            // 
            this.CreditsLabel.AutoSize = true;
            this.CreditsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreditsLabel.Location = new System.Drawing.Point(3, 13);
            this.CreditsLabel.Name = "CreditsLabel";
            this.CreditsLabel.Size = new System.Drawing.Size(215, 13);
            this.CreditsLabel.TabIndex = 4;
            this.CreditsLabel.Text = "TimeTell by JimmSlimm         (ver1.0)";
            // 
            // MaxPrecisionCountBox
            // 
            this.MaxPrecisionCountBox.Location = new System.Drawing.Point(93, 86);
            this.MaxPrecisionCountBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.MaxPrecisionCountBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MaxPrecisionCountBox.Name = "MaxPrecisionCountBox";
            this.MaxPrecisionCountBox.Size = new System.Drawing.Size(69, 20);
            this.MaxPrecisionCountBox.TabIndex = 5;
            this.MaxPrecisionCountBox.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Max precision:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(168, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Higher gives better results,\r\nbut increases computation time";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(125, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "COMPUTE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 220);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(322, 281);
            this.dataGridView1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(329, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "This is a complicated calculation, it may take some time.\r\nHistory size of attemp" +
    "ts and \"Max precision\" affects the time it takes.";
            // 
            // alt_comp_mode_cb
            // 
            this.alt_comp_mode_cb.AutoSize = true;
            this.alt_comp_mode_cb.Location = new System.Drawing.Point(159, 125);
            this.alt_comp_mode_cb.Name = "alt_comp_mode_cb";
            this.alt_comp_mode_cb.Size = new System.Drawing.Size(15, 14);
            this.alt_comp_mode_cb.TabIndex = 12;
            this.alt_comp_mode_cb.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Alternative computation mode:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(183, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Faster, but less accurate";
            // 
            // timeTellComponentBindingSource
            // 
            this.timeTellComponentBindingSource.DataSource = typeof(TimeTell.TimeTellComponent);
            // 
            // TimeTellSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.alt_comp_mode_cb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MaxPrecisionCountBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CreditsLabel);
            this.Controls.Add(this.AttemptCountBox);
            this.Controls.Add(this.FixedAttempts);
            this.Controls.Add(this.PercentOfAttempts);
            this.Controls.Add(this.RecentLabel);
            this.Name = "TimeTellSettings";
            this.Size = new System.Drawing.Size(331, 504);
            ((System.ComponentModel.ISupportInitialize)(this.AttemptCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxPrecisionCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTellComponentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label RecentLabel;
        private System.Windows.Forms.RadioButton PercentOfAttempts;
        private System.Windows.Forms.RadioButton FixedAttempts;
        private System.Windows.Forms.NumericUpDown AttemptCountBox;
        private System.Windows.Forms.Label CreditsLabel;
        private System.Windows.Forms.NumericUpDown MaxPrecisionCountBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource timeTellComponentBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox alt_comp_mode_cb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
