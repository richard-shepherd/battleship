namespace UI
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
            this.control_Board2 = new UI.Control_Board();
            this.SuspendLayout();
            // 
            // control_Board1
            // 
            this.control_Board1.BoardSize = 30;
            this.control_Board1.GridColor = System.Drawing.Color.LightGray;
            this.control_Board1.Location = new System.Drawing.Point(12, 12);
            this.control_Board1.Name = "control_Board1";
            this.control_Board1.PlayerColor = System.Drawing.Color.Blue;
            this.control_Board1.Size = new System.Drawing.Size(497, 469);
            this.control_Board1.TabIndex = 0;
            // 
            // control_Board2
            // 
            this.control_Board2.BoardSize = 30;
            this.control_Board2.GridColor = System.Drawing.Color.LightGray;
            this.control_Board2.Location = new System.Drawing.Point(515, 12);
            this.control_Board2.Name = "control_Board2";
            this.control_Board2.PlayerColor = System.Drawing.Color.Red;
            this.control_Board2.Size = new System.Drawing.Size(497, 469);
            this.control_Board2.TabIndex = 1;
            // 
            // Form_Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 659);
            this.Controls.Add(this.control_Board2);
            this.Controls.Add(this.control_Board1);
            this.DoubleBuffered = true;
            this.Name = "Form_Battleship";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Control_Board control_Board1;
        private Control_Board control_Board2;
    }
}