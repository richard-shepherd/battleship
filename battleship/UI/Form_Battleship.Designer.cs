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
            this.ctrlTurnTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrlLog = new UI.Control_Log();
            this.lblLog = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ctrlAIs = new System.Windows.Forms.ListBox();
            this.lblAIList = new System.Windows.Forms.Label();
            this.ctrlBoard1 = new UI.Control_Board();
            this.ctrlBoard2 = new UI.Control_Board();
            this.ctrlStartGame = new System.Windows.Forms.Button();
            this.ctrlPlayTurn = new System.Windows.Forms.Button();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlTurnTimer
            // 
            this.ctrlTurnTimer.Enabled = true;
            this.ctrlTurnTimer.Interval = 50;
            this.ctrlTurnTimer.Tick += new System.EventHandler(this.ctrlTurnTimer_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblLog);
            this.splitContainer1.Panel2.Controls.Add(this.ctrlLog);
            this.splitContainer1.Size = new System.Drawing.Size(1377, 766);
            this.splitContainer1.SplitterDistance = 576;
            this.splitContainer1.TabIndex = 9;
            // 
            // ctrlLog
            // 
            this.ctrlLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlLog.Location = new System.Drawing.Point(12, 20);
            this.ctrlLog.Name = "ctrlLog";
            this.ctrlLog.Size = new System.Drawing.Size(1353, 154);
            this.ctrlLog.TabIndex = 3;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLog.Location = new System.Drawing.Point(12, 2);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(27, 15);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Log";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblAIList);
            this.splitContainer2.Panel1.Controls.Add(this.ctrlAIs);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblPlayer2);
            this.splitContainer2.Panel2.Controls.Add(this.lblPlayer1);
            this.splitContainer2.Panel2.Controls.Add(this.ctrlPlayTurn);
            this.splitContainer2.Panel2.Controls.Add(this.ctrlStartGame);
            this.splitContainer2.Panel2.Controls.Add(this.ctrlBoard2);
            this.splitContainer2.Panel2.Controls.Add(this.ctrlBoard1);
            this.splitContainer2.Size = new System.Drawing.Size(1377, 576);
            this.splitContainer2.SplitterDistance = 352;
            this.splitContainer2.TabIndex = 0;
            // 
            // ctrlAIs
            // 
            this.ctrlAIs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlAIs.FormattingEnabled = true;
            this.ctrlAIs.ItemHeight = 15;
            this.ctrlAIs.Location = new System.Drawing.Point(12, 27);
            this.ctrlAIs.Name = "ctrlAIs";
            this.ctrlAIs.Size = new System.Drawing.Size(337, 544);
            this.ctrlAIs.TabIndex = 6;
            // 
            // lblAIList
            // 
            this.lblAIList.AutoSize = true;
            this.lblAIList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAIList.Location = new System.Drawing.Point(12, 9);
            this.lblAIList.Name = "lblAIList";
            this.lblAIList.Size = new System.Drawing.Size(23, 15);
            this.lblAIList.TabIndex = 7;
            this.lblAIList.Text = "AIs";
            // 
            // ctrlBoard1
            // 
            this.ctrlBoard1.BoardSize = 30;
            this.ctrlBoard1.GridColor = System.Drawing.Color.LightGray;
            this.ctrlBoard1.Location = new System.Drawing.Point(3, 29);
            this.ctrlBoard1.Name = "ctrlBoard1";
            this.ctrlBoard1.PlayerColor = System.Drawing.Color.Blue;
            this.ctrlBoard1.Size = new System.Drawing.Size(500, 500);
            this.ctrlBoard1.TabIndex = 1;
            // 
            // ctrlBoard2
            // 
            this.ctrlBoard2.BoardSize = 30;
            this.ctrlBoard2.GridColor = System.Drawing.Color.LightGray;
            this.ctrlBoard2.Location = new System.Drawing.Point(509, 29);
            this.ctrlBoard2.Name = "ctrlBoard2";
            this.ctrlBoard2.PlayerColor = System.Drawing.Color.Red;
            this.ctrlBoard2.Size = new System.Drawing.Size(500, 500);
            this.ctrlBoard2.TabIndex = 2;
            // 
            // ctrlStartGame
            // 
            this.ctrlStartGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlStartGame.Location = new System.Drawing.Point(3, 535);
            this.ctrlStartGame.Name = "ctrlStartGame";
            this.ctrlStartGame.Size = new System.Drawing.Size(131, 36);
            this.ctrlStartGame.TabIndex = 4;
            this.ctrlStartGame.Text = "Start game";
            this.ctrlStartGame.UseVisualStyleBackColor = true;
            this.ctrlStartGame.Click += new System.EventHandler(this.ctrlStartGame_Click);
            // 
            // ctrlPlayTurn
            // 
            this.ctrlPlayTurn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlPlayTurn.Location = new System.Drawing.Point(140, 535);
            this.ctrlPlayTurn.Name = "ctrlPlayTurn";
            this.ctrlPlayTurn.Size = new System.Drawing.Size(128, 36);
            this.ctrlPlayTurn.TabIndex = 5;
            this.ctrlPlayTurn.Text = "Play turn";
            this.ctrlPlayTurn.UseVisualStyleBackColor = true;
            this.ctrlPlayTurn.Click += new System.EventHandler(this.ctrlPlayTurn_Click);
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlayer1.Location = new System.Drawing.Point(3, 9);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(65, 17);
            this.lblPlayer1.TabIndex = 8;
            this.lblPlayer1.Text = "Player 1: ";
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.AutoSize = true;
            this.lblPlayer2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlayer2.ForeColor = System.Drawing.Color.Red;
            this.lblPlayer2.Location = new System.Drawing.Point(509, 7);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(65, 17);
            this.lblPlayer2.TabIndex = 9;
            this.lblPlayer2.Text = "Player 2: ";
            // 
            // Form_Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 766);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Blue;
            this.Name = "Form_Battleship";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ctrlTurnTimer;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label lblAIList;
        private ListBox ctrlAIs;
        private Label lblPlayer2;
        private Label lblPlayer1;
        private Button ctrlPlayTurn;
        private Button ctrlStartGame;
        private Control_Board ctrlBoard2;
        private Control_Board ctrlBoard1;
        private Label lblLog;
        private Control_Log ctrlLog;
    }
}