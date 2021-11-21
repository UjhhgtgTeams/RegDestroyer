namespace WinUpdateTool
{
    partial class Updater
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.updateLabel = new System.Windows.Forms.Label();
            this.updateProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // updateLabel
            // 
            this.updateLabel.AutoSize = true;
            this.updateLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.updateLabel.Location = new System.Drawing.Point(13, 9);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(242, 27);
            this.updateLabel.TabIndex = 0;
            this.updateLabel.Text = "Preparing for update......";
            this.updateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateProgress
            // 
            this.updateProgress.Location = new System.Drawing.Point(18, 39);
            this.updateProgress.Name = "updateProgress";
            this.updateProgress.Size = new System.Drawing.Size(526, 23);
            this.updateProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.updateProgress.TabIndex = 1;
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 78);
            this.ControlBox = false;
            this.Controls.Add(this.updateProgress);
            this.Controls.Add(this.updateLabel);
            this.Cursor = System.Windows.Forms.Cursors.No;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "Updater";
            this.Text = "Windows Update Tool";
            this.Load += new System.EventHandler(this.Updater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label updateLabel;
        private System.Windows.Forms.ProgressBar updateProgress;
    }
}