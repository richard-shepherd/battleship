﻿namespace UI
{
    partial class Form_Battleship
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
            this.control_Board1 = new UI.Control_Board();
            this.SuspendLayout();
            // 
            // control_Board1
            // 
            this.control_Board1.Location = new System.Drawing.Point(44, 22);
            this.control_Board1.Name = "control_Board1";
            this.control_Board1.Size = new System.Drawing.Size(472, 463);
            this.control_Board1.TabIndex = 0;
            // 
            // Form_Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 659);
            this.Controls.Add(this.control_Board1);
            this.DoubleBuffered = true;
            this.Name = "Form_Battleship";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Control_Board control_Board1;
    }
}