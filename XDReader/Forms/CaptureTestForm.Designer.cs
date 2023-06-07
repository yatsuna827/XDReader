namespace XDReader
{
    partial class CaptureTestForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.faceBox = new System.Windows.Forms.TextBox();
            this.dotCountBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(431, 361);
            this.pictureBox1.TabIndex = 67;
            this.pictureBox1.TabStop = false;
            // 
            // faceBox
            // 
            this.faceBox.Location = new System.Drawing.Point(12, 12);
            this.faceBox.Name = "faceBox";
            this.faceBox.ReadOnly = true;
            this.faceBox.Size = new System.Drawing.Size(39, 19);
            this.faceBox.TabIndex = 68;
            // 
            // dotCountBox
            // 
            this.dotCountBox.Location = new System.Drawing.Point(57, 12);
            this.dotCountBox.Name = "dotCountBox";
            this.dotCountBox.ReadOnly = true;
            this.dotCountBox.Size = new System.Drawing.Size(73, 19);
            this.dotCountBox.TabIndex = 72;
            // 
            // CaptureTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 412);
            this.Controls.Add(this.dotCountBox);
            this.Controls.Add(this.faceBox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaptureTestForm";
            this.ShowIcon = false;
            this.Text = "瞬きテスト";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlinkCaptureTestForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox faceBox;
        private System.Windows.Forms.TextBox dotCountBox;
    }
}