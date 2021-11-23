namespace WinUpdateTool
{
    partial class Note
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Note));
            this.noteBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // noteBox
            // 
            this.noteBox.BackColor = System.Drawing.Color.White;
            this.noteBox.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noteBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.noteBox.Location = new System.Drawing.Point(0, 0);
            this.noteBox.Multiline = true;
            this.noteBox.Name = "noteBox";
            this.noteBox.ReadOnly = true;
            this.noteBox.ShortcutsEnabled = false;
            this.noteBox.Size = new System.Drawing.Size(639, 367);
            this.noteBox.TabIndex = 0;
            // 
            // Note
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 366);
            this.Controls.Add(this.noteBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Note";
            this.Text = "Note.txt - Notepad";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Note_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox noteBox;
    }
}