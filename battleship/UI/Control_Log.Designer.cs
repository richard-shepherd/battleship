namespace UI
{
    partial class Control_Log
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
            this.ctrlLogText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ctrlLogText
            // 
            this.ctrlLogText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlLogText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ctrlLogText.Location = new System.Drawing.Point(0, 0);
            this.ctrlLogText.Multiline = true;
            this.ctrlLogText.Name = "ctrlLogText";
            this.ctrlLogText.Size = new System.Drawing.Size(737, 215);
            this.ctrlLogText.TabIndex = 0;
            // 
            // Control_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlLogText);
            this.Name = "Control_Log";
            this.Size = new System.Drawing.Size(737, 215);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox ctrlLogText;
    }
}
