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
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.blinkCountBox = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.allowableErrorBox = new System.Windows.Forms.NumericUpDown();
            this.Button_blink = new System.Windows.Forms.Button();
            this.blankDGV = new System.Windows.Forms.DataGridView();
            this.maxFrameBox = new System.Windows.Forms.NumericUpDown();
            this.minFrameBox = new System.Windows.Forms.NumericUpDown();
            this.BlinkResultDGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.blinkCoolTimeBox = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SwitchCaptureFrameVisibleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CaptureTestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.breakingTimeBox = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.targetSeedBox = new XDReader.SeedBox();
            this.currentSeedBox = new XDReader.SeedBox();
            this.indexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blinkResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.blankMagnificationBox = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ShootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.blinkCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.allowableErrorBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blankDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxFrameBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFrameBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlinkResultDGV)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkCoolTimeBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.breakingTimeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blankMagnificationBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ShootMenuItem
            // 
            ShootMenuItem.Name = "ShootMenuItem";
            ShootMenuItem.Size = new System.Drawing.Size(67, 20);
            ShootMenuItem.Text = "画像保存";
            ShootMenuItem.Click += new System.EventHandler(this.ShootMenuItem_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 51);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 60;
            this.label15.Text = "目標seed";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(471, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 12);
            this.label10.TabIndex = 58;
            this.label10.Text = "瞬き回数";
            // 
            // blinkCountBox
            // 
            this.blinkCountBox.Location = new System.Drawing.Point(471, 137);
            this.blinkCountBox.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.blinkCountBox.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.blinkCountBox.Name = "blinkCountBox";
            this.blinkCountBox.Size = new System.Drawing.Size(55, 19);
            this.blinkCountBox.TabIndex = 57;
            this.blinkCountBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 56;
            this.label8.Text = "現在seed";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 55;
            this.label7.Text = "～";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 54;
            this.label6.Text = "許容誤差";
            // 
            // allowableErrorBox
            // 
            this.allowableErrorBox.Location = new System.Drawing.Point(250, 98);
            this.allowableErrorBox.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.allowableErrorBox.Name = "allowableErrorBox";
            this.allowableErrorBox.Size = new System.Drawing.Size(55, 19);
            this.allowableErrorBox.TabIndex = 53;
            this.allowableErrorBox.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // Button_blink
            // 
            this.Button_blink.Location = new System.Drawing.Point(476, 162);
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
            this.blankDGV.Size = new System.Drawing.Size(111, 362);
            this.blankDGV.TabIndex = 51;
            // 
            // maxFrameBox
            // 
            this.maxFrameBox.Location = new System.Drawing.Point(77, 98);
            this.maxFrameBox.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.maxFrameBox.Name = "maxFrameBox";
            this.maxFrameBox.Size = new System.Drawing.Size(100, 19);
            this.maxFrameBox.TabIndex = 50;
            this.maxFrameBox.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // minFrameBox
            // 
            this.minFrameBox.Location = new System.Drawing.Point(77, 73);
            this.minFrameBox.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.minFrameBox.Name = "minFrameBox";
            this.minFrameBox.Size = new System.Drawing.Size(99, 19);
            this.minFrameBox.TabIndex = 49;
            // 
            // BlinkResultDGV
            // 
            this.BlinkResultDGV.AllowUserToAddRows = false;
            this.BlinkResultDGV.AllowUserToDeleteRows = false;
            this.BlinkResultDGV.AllowUserToResizeColumns = false;
            this.BlinkResultDGV.AllowUserToResizeRows = false;
            this.BlinkResultDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BlinkResultDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn6,
            this.Column7});
            this.BlinkResultDGV.Location = new System.Drawing.Point(133, 204);
            this.BlinkResultDGV.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.BlinkResultDGV.Name = "BlinkResultDGV";
            this.BlinkResultDGV.ReadOnly = true;
            this.BlinkResultDGV.RowHeadersVisible = false;
            this.BlinkResultDGV.RowHeadersWidth = 82;
            this.BlinkResultDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.BlinkResultDGV.RowTemplate.Height = 33;
            this.BlinkResultDGV.Size = new System.Drawing.Size(316, 184);
            this.BlinkResultDGV.TabIndex = 47;
            this.BlinkResultDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BlinkResultDGV_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "[F]";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn8.Width = 88;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "seed";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 10;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 110;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "残り消費数";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.blankMagnificationBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.targetSeedBox);
            this.groupBox1.Controls.Add(this.currentSeedBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.blinkCoolTimeBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.minFrameBox);
            this.groupBox1.Controls.Add(this.allowableErrorBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.maxFrameBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Location = new System.Drawing.Point(133, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 172);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "条件入力";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(535, 57);
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
            this.numericUpDown2.Location = new System.Drawing.Point(471, 54);
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
            this.label11.Location = new System.Drawing.Point(471, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 78;
            this.label11.Text = "画面拡大率";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(471, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 75;
            this.label4.Text = "閾値";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(471, 95);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(55, 19);
            this.numericUpDown1.TabIndex = 74;
            this.numericUpDown1.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 73;
            this.label3.Text = "瞬き間隔";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 70;
            this.label2.Text = "検索範囲";
            // 
            // blinkCoolTimeBox
            // 
            this.blinkCoolTimeBox.Location = new System.Drawing.Point(250, 73);
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
            4,
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
            this.menuStrip1.Size = new System.Drawing.Size(566, 24);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(459, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 77;
            this.label5.Text = "制動時間";
            // 
            // breakingTimeBox
            // 
            this.breakingTimeBox.Location = new System.Drawing.Point(496, 365);
            this.breakingTimeBox.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.breakingTimeBox.Name = "breakingTimeBox";
            this.breakingTimeBox.Size = new System.Drawing.Size(55, 19);
            this.breakingTimeBox.TabIndex = 76;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(459, 304);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 24);
            this.label12.TabIndex = 78;
            this.label12.Text = "セルをダブルクリックで\r\nタイマー起動";
            // 
            // targetSeedBox
            // 
            this.targetSeedBox.Location = new System.Drawing.Point(77, 48);
            this.targetSeedBox.MaxLength = 8;
            this.targetSeedBox.Name = "targetSeedBox";
            this.targetSeedBox.Size = new System.Drawing.Size(64, 19);
            this.targetSeedBox.TabIndex = 73;
            this.targetSeedBox.Text = "19F0033A";
            this.targetSeedBox.ZeroPadding = false;
            // 
            // currentSeedBox
            // 
            this.currentSeedBox.Location = new System.Drawing.Point(77, 23);
            this.currentSeedBox.MaxLength = 8;
            this.currentSeedBox.Name = "currentSeedBox";
            this.currentSeedBox.Size = new System.Drawing.Size(64, 19);
            this.currentSeedBox.TabIndex = 72;
            this.currentSeedBox.Text = "787AE45";
            this.currentSeedBox.ZeroPadding = false;
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
            // blankMagnificationBox
            // 
            this.blankMagnificationBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.blankMagnificationBox.Location = new System.Drawing.Point(250, 123);
            this.blankMagnificationBox.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.blankMagnificationBox.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.blankMagnificationBox.Name = "blankMagnificationBox";
            this.blankMagnificationBox.Size = new System.Drawing.Size(55, 19);
            this.blankMagnificationBox.TabIndex = 74;
            this.blankMagnificationBox.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(191, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 75;
            this.label13.Text = "延長倍率";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(311, 126);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 81;
            this.label14.Text = "%";
            // 
            // BlinkReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 396);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.blankDGV);
            this.Controls.Add(this.BlinkResultDGV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.breakingTimeBox);
            this.Controls.Add(this.Button_blink);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.blinkCountBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "BlinkReader";
            this.ShowIcon = false;
            this.Text = "BlinkReader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlinkReader_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.blinkCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.allowableErrorBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blankDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxFrameBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFrameBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlinkResultDGV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkCoolTimeBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.breakingTimeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blinkResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blankMagnificationBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown blinkCountBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown allowableErrorBox;
        private System.Windows.Forms.Button Button_blink;
        private System.Windows.Forms.DataGridView blankDGV;
        private System.Windows.Forms.NumericUpDown maxFrameBox;
        private System.Windows.Forms.NumericUpDown minFrameBox;
        private System.Windows.Forms.DataGridView BlinkResultDGV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blankDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem SwitchCaptureFrameVisibleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CaptureTestMenuItem;
        private System.Windows.Forms.BindingSource blinkResultBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown blinkCoolTimeBox;
        private SeedBox targetSeedBox;
        private SeedBox currentSeedBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown breakingTimeBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown blankMagnificationBox;
        private System.Windows.Forms.Label label13;
    }
}