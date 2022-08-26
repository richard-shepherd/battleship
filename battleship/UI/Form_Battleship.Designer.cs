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
            this.components = new System.ComponentModel.Container();
            this.ctrlBoard1 = new UI.Control_Board();
            this.ctrlBoard2 = new UI.Control_Board();
            this.ctrlLog = new UI.Control_Log();
            this.ctrlStartGame = new System.Windows.Forms.Button();
            this.ctrlPlayTurn = new System.Windows.Forms.Button();
            this.ctrlTurnTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ctrlBoard1
            // 
            this.ctrlBoard1.BoardSize = 30;
            this.ctrlBoard1.GridColor = System.Drawing.Color.LightGray;
            this.ctrlBoard1.Location = new System.Drawing.Point(12, 12);
            this.ctrlBoard1.Name = "ctrlBoard1";
            this.ctrlBoard1.PlayerColor = System.Drawing.Color.Blue;
            this.ctrlBoard1.Size = new System.Drawing.Size(500, 500);
            this.ctrlBoard1.TabIndex = 0;
            // 
            // ctrlBoard2
            // 
            this.ctrlBoard2.BoardSize = 30;
            this.ctrlBoard2.GridColor = System.Drawing.Color.LightGray;
            this.ctrlBoard2.Location = new System.Drawing.Point(518, 12);
            this.ctrlBoard2.Name = "ctrlBoard2";
            this.ctrlBoard2.PlayerColor = System.Drawing.Color.Red;
            this.ctrlBoard2.Size = new System.Drawing.Size(500, 500);
            this.ctrlBoard2.TabIndex = 1;
            // 
            // ctrlLog
            // 
            this.ctrlLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlLog.Location = new System.Drawing.Point(12, 637);
            this.ctrlLog.Name = "ctrlLog";
            this.ctrlLog.Size = new System.Drawing.Size(1002, 117);
            this.ctrlLog.TabIndex = 2;
            // 
            // ctrlStartGame
            // 
            this.ctrlStartGame.Location = new System.Drawing.Point(12, 518);
            this.ctrlStartGame.Name = "ctrlStartGame";
            this.ctrlStartGame.Size = new System.Drawing.Size(174, 47);
            this.ctrlStartGame.TabIndex = 3;
            this.ctrlStartGame.Text = "Start game";
            this.ctrlStartGame.UseVisualStyleBackColor = true;
            this.ctrlStartGame.Click += new System.EventHandler(this.ctrlStartGame_Click);
            // 
            // ctrlPlayTurn
            // 
            this.ctrlPlayTurn.Location = new System.Drawing.Point(192, 518);
            this.ctrlPlayTurn.Name = "ctrlPlayTurn";
            this.ctrlPlayTurn.Size = new System.Drawing.Size(174, 47);
            this.ctrlPlayTurn.TabIndex = 4;
            this.ctrlPlayTurn.Text = "Play turn";
            this.ctrlPlayTurn.UseVisualStyleBackColor = true;
            this.ctrlPlayTurn.Click += new System.EventHandler(this.ctrlPlayTurn_Click);
            // 
            // ctrlTurnTimer
            // 
            this.ctrlTurnTimer.Enabled = true;
            this.ctrlTurnTimer.Tick += new System.EventHandler(this.ctrlTurnTimer_Tick);
            // 
            // Form_Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 766);
            this.Controls.Add(this.ctrlPlayTurn);
            this.Controls.Add(this.ctrlStartGame);
            this.Controls.Add(this.ctrlLog);
            this.Controls.Add(this.ctrlBoard2);
            this.Controls.Add(this.ctrlBoard1);
            this.DoubleBuffered = true;
            this.Name = "Form_Battleship";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Control_Board ctrlBoard1;
        private Control_Board ctrlBoard2;
        private Control_Log ctrlLog;
        private Button ctrlStartGame;
        private Button ctrlPlayTurn;
        private System.Windows.Forms.Timer ctrlTurnTimer;
    }
}