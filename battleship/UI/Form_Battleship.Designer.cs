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
            this.components = new System.ComponentModel.Container();
            this.ctrlTurnTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ctrlAIs = new System.Windows.Forms.CheckedListBox();
            this.ctrlAIs_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrlAIs_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlAIs_SelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlAIs_Load = new System.Windows.Forms.Button();
            this.ctrlAIFolder = new System.Windows.Forms.TextBox();
            this.lblAIList = new System.Windows.Forms.Label();
            this.lblBoardSize = new System.Windows.Forms.Label();
            this.ctrlBoardSize = new System.Windows.Forms.ComboBox();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.ctrlPlayTurn = new System.Windows.Forms.Button();
            this.ctrlStartGame = new System.Windows.Forms.Button();
            this.ctrlBoard2 = new UI.Control_Board();
            this.ctrlBoard1 = new UI.Control_Board();
            this.lblLog = new System.Windows.Forms.Label();
            this.ctrlLog = new UI.Control_Log();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ctrlAIs_ContextMenu.SuspendLayout();
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ctrlAIs);
            this.splitContainer2.Panel1.Controls.Add(this.ctrlAIs_Load);
            this.splitContainer2.Panel1.Controls.Add(this.ctrlAIFolder);
            this.splitContainer2.Panel1.Controls.Add(this.lblAIList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblBoardSize);
            this.splitContainer2.Panel2.Controls.Add(this.ctrlBoardSize);
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
            this.ctrlAIs.CheckOnClick = true;
            this.ctrlAIs.ContextMenuStrip = this.ctrlAIs_ContextMenu;
            this.ctrlAIs.FormattingEnabled = true;
            this.ctrlAIs.Location = new System.Drawing.Point(12, 58);
            this.ctrlAIs.Name = "ctrlAIs";
            this.ctrlAIs.Size = new System.Drawing.Size(337, 508);
            this.ctrlAIs.TabIndex = 12;
            // 
            // ctrlAIs_ContextMenu
            // 
            this.ctrlAIs_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctrlAIs_SelectAll,
            this.ctrlAIs_SelectNone});
            this.ctrlAIs_ContextMenu.Name = "contextMenuStrip1";
            this.ctrlAIs_ContextMenu.Size = new System.Drawing.Size(136, 48);
            // 
            // ctrlAIs_SelectAll
            // 
            this.ctrlAIs_SelectAll.Name = "ctrlAIs_SelectAll";
            this.ctrlAIs_SelectAll.Size = new System.Drawing.Size(135, 22);
            this.ctrlAIs_SelectAll.Text = "Select all";
            this.ctrlAIs_SelectAll.Click += new System.EventHandler(this.ctrlAIs_SelectAll_Click);
            // 
            // ctrlAIs_SelectNone
            // 
            this.ctrlAIs_SelectNone.Name = "ctrlAIs_SelectNone";
            this.ctrlAIs_SelectNone.Size = new System.Drawing.Size(135, 22);
            this.ctrlAIs_SelectNone.Text = "Select none";
            this.ctrlAIs_SelectNone.Click += new System.EventHandler(this.ctrlAIs_SelectNone_Click);
            // 
            // ctrlAIs_Load
            // 
            this.ctrlAIs_Load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlAIs_Load.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlAIs_Load.Location = new System.Drawing.Point(296, 29);
            this.ctrlAIs_Load.Name = "ctrlAIs_Load";
            this.ctrlAIs_Load.Size = new System.Drawing.Size(53, 23);
            this.ctrlAIs_Load.TabIndex = 9;
            this.ctrlAIs_Load.Text = "Load";
            this.ctrlAIs_Load.UseVisualStyleBackColor = true;
            this.ctrlAIs_Load.Click += new System.EventHandler(this.ctrlAIs_Load_Click);
            // 
            // ctrlAIFolder
            // 
            this.ctrlAIFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlAIFolder.Location = new System.Drawing.Point(12, 29);
            this.ctrlAIFolder.Name = "ctrlAIFolder";
            this.ctrlAIFolder.Size = new System.Drawing.Size(278, 23);
            this.ctrlAIFolder.TabIndex = 8;
            this.ctrlAIFolder.Text = "D:\\code\\battleship\\AIs";
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
            // lblBoardSize
            // 
            this.lblBoardSize.AutoSize = true;
            this.lblBoardSize.Location = new System.Drawing.Point(3, 530);
            this.lblBoardSize.Name = "lblBoardSize";
            this.lblBoardSize.Size = new System.Drawing.Size(60, 15);
            this.lblBoardSize.TabIndex = 11;
            this.lblBoardSize.Text = "Board size";
            // 
            // ctrlBoardSize
            // 
            this.ctrlBoardSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctrlBoardSize.FormattingEnabled = true;
            this.ctrlBoardSize.Items.AddRange(new object[] {
            "10x10",
            "20x20",
            "30x30",
            "40x40",
            "50x50",
            "80x80",
            "100x100",
            "200x200",
            "500x500",
            "1000x1000"});
            this.ctrlBoardSize.Location = new System.Drawing.Point(3, 548);
            this.ctrlBoardSize.Name = "ctrlBoardSize";
            this.ctrlBoardSize.Size = new System.Drawing.Size(91, 23);
            this.ctrlBoardSize.TabIndex = 10;
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
            // lblPlayer1
            // 
            this.lblPlayer1.AutoSize = true;
            this.lblPlayer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlayer1.ForeColor = System.Drawing.Color.Blue;
            this.lblPlayer1.Location = new System.Drawing.Point(3, 9);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(65, 17);
            this.lblPlayer1.TabIndex = 8;
            this.lblPlayer1.Text = "Player 1: ";
            // 
            // ctrlPlayTurn
            // 
            this.ctrlPlayTurn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlPlayTurn.Location = new System.Drawing.Point(751, 535);
            this.ctrlPlayTurn.Name = "ctrlPlayTurn";
            this.ctrlPlayTurn.Size = new System.Drawing.Size(128, 36);
            this.ctrlPlayTurn.TabIndex = 5;
            this.ctrlPlayTurn.Text = "Play turn";
            this.ctrlPlayTurn.UseVisualStyleBackColor = true;
            this.ctrlPlayTurn.Click += new System.EventHandler(this.ctrlPlayTurn_Click);
            // 
            // ctrlStartGame
            // 
            this.ctrlStartGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlStartGame.Location = new System.Drawing.Point(614, 535);
            this.ctrlStartGame.Name = "ctrlStartGame";
            this.ctrlStartGame.Size = new System.Drawing.Size(131, 36);
            this.ctrlStartGame.TabIndex = 4;
            this.ctrlStartGame.Text = "Start game";
            this.ctrlStartGame.UseVisualStyleBackColor = true;
            this.ctrlStartGame.Click += new System.EventHandler(this.ctrlStartGame_Click);
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
            // Form_Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 766);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
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
            this.ctrlAIs_ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ctrlTurnTimer;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label lblAIList;
        private Label lblPlayer2;
        private Label lblPlayer1;
        private Button ctrlPlayTurn;
        private Button ctrlStartGame;
        private Control_Board ctrlBoard2;
        private Control_Board ctrlBoard1;
        private Label lblLog;
        private Control_Log ctrlLog;
        private TextBox ctrlAIFolder;
        private Button ctrlAIs_Load;
        private CheckedListBox ctrlAIs;
        private ContextMenuStrip ctrlAIs_ContextMenu;
        private ToolStripMenuItem ctrlAIs_SelectAll;
        private ToolStripMenuItem ctrlAIs_SelectNone;
        private Label lblBoardSize;
        private ComboBox ctrlBoardSize;
    }
}