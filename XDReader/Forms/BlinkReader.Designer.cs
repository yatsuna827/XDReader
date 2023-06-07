namespace XDReader
{
    partial class BlinkReader
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
            System.Windows.Forms.ToolStripMenuItem ShootMenuItem;
            this.Button_blink = new System.Windows.Forms.Button();
            this.blankDGV = new System.Windows.Forms.DataGridView();
            this.indexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blinkResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.blinkThresholdBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.blinkCoolTimeBox = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SwitchCaptureFrameVisibleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CaptureTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            ShootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.blankDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkThresholdBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkCoolTimeBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShootMenuItem
            // 
            ShootMenuItem.Name = "ShootMenuItem";
            ShootMenuItem.Size = new System.Drawing.Size(67, 20);
            ShootMenuItem.Text = "画像保存";
            ShootMenuItem.Click += new System.EventHandler(this.ShootMenuItem_Click);
            // 
            // Button_blink
            // 
            this.Button_blink.Location = new System.Drawing.Point(184, 102);
            this.Button_blink.Name = "Button_blink";
            this.Button_blink.Size = new System.Drawing.Size(75, 37);
            this.Button_blink.TabIndex = 52;
            this.Button_blink.Text = "開始";
            this.Button_blink.UseVisualStyleBackColor = true;
            this.Button_blink.Click += new System.EventHandler(this.Button_blink_Click);
            // 
            // blankDGV
            // 
            this.blankDGV.AllowUserToAddRows = false;
            this.blankDGV.AllowUserToDeleteRows = false;
            this.blankDGV.AllowUserToResizeColumns = false;
            this.blankDGV.AllowUserToResizeRows = false;
            this.blankDGV.AutoGenerateColumns = false;
            this.blankDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blankDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexDataGridViewTextBoxColumn,
            this.blankDataGridViewTextBoxColumn});
            this.blankDGV.DataSource = this.blinkResultBindingSource;
            this.blankDGV.Location = new System.Drawing.Point(10, 26);
            this.blankDGV.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.blankDGV.Name = "blankDGV";
            this.blankDGV.ReadOnly = true;
            this.blankDGV.RowHeadersVisible = false;
            this.blankDGV.RowHeadersWidth = 82;
            this.blankDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.blankDGV.RowTemplate.Height = 20;
            this.blankDGV.Size = new System.Drawing.Size(111, 359);
            this.blankDGV.TabIndex = 51;
            // 
            // indexDataGridViewTextBoxColumn
            // 
            this.indexDataGridViewTextBoxColumn.DataPropertyName = "Index";
            this.indexDataGridViewTextBoxColumn.HeaderText = "";
            this.indexDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.indexDataGridViewTextBoxColumn.Name = "indexDataGridViewTextBoxColumn";
            this.indexDataGridViewTextBoxColumn.ReadOnly = true;
            this.indexDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.indexDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.indexDataGridViewTextBoxColumn.Width = 30;
            // 
            // blankDataGridViewTextBoxColumn
            // 
            this.blankDataGridViewTextBoxColumn.DataPropertyName = "Blank";
            this.blankDataGridViewTextBoxColumn.HeaderText = "Blank";
            this.blankDataGridViewTextBoxColumn.MinimumWidth = 10;
            this.blankDataGridViewTextBoxColumn.Name = "blankDataGridViewTextBoxColumn";
            this.blankDataGridViewTextBoxColumn.ReadOnly = true;
            this.blankDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.blankDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.blankDataGridViewTextBoxColumn.Width = 50;
            // 
            // blinkResultBindingSource
            // 
            this.blinkResultBindingSource.DataSource = typeof(XDReader.BlinkResult);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(268, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 80;
            this.label9.Text = "%";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown2.Location = new System.Drawing.Point(204, 52);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(55, 19);
            this.numericUpDown2.TabIndex = 79;
            this.numericUpDown2.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(133, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 78;
            this.label11.Text = "画面拡大率";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 75;
            this.label4.Text = "閾値";
            // 
            // blinkThresholdBox
            // 
            this.blinkThresholdBox.Location = new System.Drawing.Point(204, 27);
            this.blinkThresholdBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.blinkThresholdBox.Name = "blinkThresholdBox";
            this.blinkThresholdBox.Size = new System.Drawing.Size(55, 19);
            this.blinkThresholdBox.TabIndex = 74;
            this.blinkThresholdBox.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 73;
            this.label3.Text = "瞬き間隔";
            // 
            // blinkCoolTimeBox
            // 
            this.blinkCoolTimeBox.Location = new System.Drawing.Point(204, 77);
            this.blinkCoolTimeBox.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.blinkCoolTimeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.blinkCoolTimeBox.Name = "blinkCoolTimeBox";
            this.blinkCoolTimeBox.Size = new System.Drawing.Size(55, 19);
            this.blinkCoolTimeBox.TabIndex = 72;
            this.blinkCoolTimeBox.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SwitchCaptureFrameVisibleMenuItem,
            this.CaptureTestMenuItem,
            ShootMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(297, 24);
            this.menuStrip1.TabIndex = 70;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SwitchCaptureFrameVisibleMenuItem
            // 
            this.SwitchCaptureFrameVisibleMenuItem.Name = "SwitchCaptureFrameVisibleMenuItem";
            this.SwitchCaptureFrameVisibleMenuItem.Size = new System.Drawing.Size(97, 20);
            this.SwitchCaptureFrameVisibleMenuItem.Text = "キャプチャ枠表示";
            this.SwitchCaptureFrameVisibleMenuItem.Click += new System.EventHandler(this.SwitchCaptureFrameVisibleMenuItem_Click);
            // 
            // CaptureTestMenuItem
            // 
            this.CaptureTestMenuItem.Name = "CaptureTestMenuItem";
            this.CaptureTestMenuItem.Size = new System.Drawing.Size(111, 20);
            this.CaptureTestMenuItem.Text = "キャプチャテスト開始";
            this.CaptureTestMenuItem.Click += new System.EventHandler(this.CaptureTestMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 37);
            this.button1.TabIndex = 81;
            this.button1.Text = "結果を保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BlinkReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 396);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.blankDGV);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Button_blink);
            this.Controls.Add(this.blinkThresholdBox);
            this.Controls.Add(this.blinkCoolTimeBox);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BlinkReader";
            this.ShowIcon = false;
            this.Text = "BlinkObserver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlinkReader_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.blankDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkThresholdBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkCoolTimeBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Button_blink;
        private System.Windows.Forms.DataGridView blankDGV;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blankDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem SwitchCaptureFrameVisibleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CaptureTestMenuItem;
        private System.Windows.Forms.BindingSource blinkResultBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown blinkCoolTimeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown blinkThresholdBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
    }
}