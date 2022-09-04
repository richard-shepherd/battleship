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
            this.ctrlSplit_Horizontal = new System.Windows.Forms.SplitContainer();
            this.ctrlSplit_Vertical = new System.Windows.Forms.SplitContainer();
            this.ctrlAIs = new System.Windows.Forms.CheckedListBox();
            this.ctrlAIs_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctrlAIs_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlAIs_SelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrlAIs_Load = new System.Windows.Forms.Button();
            this.ctrlAIFolder = new System.Windows.Forms.TextBox();
            this.lblAIList = new System.Windows.Forms.Label();
            this.lblTournamentGameInfo = new System.Windows.Forms.Label();
            this.lblTournamentResults = new System.Windows.Forms.Label();
            this.ctrlStopTournament = new System.Windows.Forms.Button();
            this.ctrlPlayTournament = new System.Windows.Forms.Button();
            this.ctrlTournamentGrid = new System.Windows.Forms.DataGridView();
            this.ctrlTurnSpeed = new System.Windows.Forms.TextBox();
            this.lblTurnSpeed = new System.Windows.Forms.Label();
            this.ctrlShipSquares = new System.Windows.Forms.TextBox();
            this.lblShipSquares = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.ctrlSplit_Horizontal)).BeginInit();
            this.ctrlSplit_Horizontal.Panel1.SuspendLayout();
            this.ctrlSplit_Horizontal.Panel2.SuspendLayout();
            this.ctrlSplit_Horizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlSplit_Vertical)).BeginInit();
            this.ctrlSplit_Vertical.Panel1.SuspendLayout();
            this.ctrlSplit_Vertical.Panel2.SuspendLayout();
            this.ctrlSplit_Vertical.SuspendLayout();
            this.ctrlAIs_ContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlTournamentGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlTurnTimer
            // 
            this.ctrlTurnTimer.Enabled = true;
            this.ctrlTurnTimer.Interval = 50;
            this.ctrlTurnTimer.Tick += new System.EventHandler(this.ctrlTurnTimer_Tick);
            // 
            // ctrlSplit_Horizontal
            // 
            this.ctrlSplit_Horizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSplit_Horizontal.Location = new System.Drawing.Point(0, 0);
            this.ctrlSplit_Horizontal.Name = "ctrlSplit_Horizontal";
            this.ctrlSplit_Horizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ctrlSplit_Horizontal.Panel1
            // 
            this.ctrlSplit_Horizontal.Panel1.Controls.Add(this.ctrlSplit_Vertical);
            // 
            // ctrlSplit_Horizontal.Panel2
            // 
            this.ctrlSplit_Horizontal.Panel2.Controls.Add(this.lblLog);
            this.ctrlSplit_Horizontal.Panel2.Controls.Add(this.ctrlLog);
            this.ctrlSplit_Horizontal.Size = new System.Drawing.Size(1610, 766);
            this.ctrlSplit_Horizontal.SplitterDistance = 576;
            this.ctrlSplit_Horizontal.TabIndex = 9;
            // 
            // ctrlSplit_Vertical
            // 
            this.ctrlSplit_Vertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSplit_Vertical.Location = new System.Drawing.Point(0, 0);
            this.ctrlSplit_Vertical.Name = "ctrlSplit_Vertical";
            // 
            // ctrlSplit_Vertical.Panel1
            // 
            this.ctrlSplit_Vertical.Panel1.Controls.Add(this.ctrlAIs);
            this.ctrlSplit_Vertical.Panel1.Controls.Add(this.ctrlAIs_Load);
            this.ctrlSplit_Vertical.Panel1.Controls.Add(this.ctrlAIFolder);
            this.ctrlSplit_Vertical.Panel1.Controls.Add(this.lblAIList);
            // 
            // ctrlSplit_Vertical.Panel2
            // 
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblTournamentGameInfo);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblTournamentResults);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlStopTournament);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlPlayTournament);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlTournamentGrid);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlTurnSpeed);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblTurnSpeed);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlShipSquares);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblShipSquares);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblBoardSize);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlBoardSize);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblPlayer2);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.lblPlayer1);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlPlayTurn);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlStartGame);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlBoard2);
            this.ctrlSplit_Vertical.Panel2.Controls.Add(this.ctrlBoard1);
            this.ctrlSplit_Vertical.Size = new System.Drawing.Size(1610, 576);
            this.ctrlSplit_Vertical.SplitterDistance = 255;
            this.ctrlSplit_Vertical.TabIndex = 0;
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
            this.ctrlAIs.Size = new System.Drawing.Size(240, 508);
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
            this.ctrlAIs_Load.Location = new System.Drawing.Point(199, 29);
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
            this.ctrlAIFolder.Size = new System.Drawing.Size(181, 23);
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
            // lblTournamentGameInfo
            // 
            this.lblTournamentGameInfo.AutoSize = true;
            this.lblTournamentGameInfo.Location = new System.Drawing.Point(1015, 29);
            this.lblTournamentGameInfo.Name = "lblTournamentGameInfo";
            this.lblTournamentGameInfo.Size = new System.Drawing.Size(145, 15);
            this.lblTournamentGameInfo.TabIndex = 20;
            this.lblTournamentGameInfo.Text = "(Tournament not running)";
            // 
            // lblTournamentResults
            // 
            this.lblTournamentResults.AutoSize = true;
            this.lblTournamentResults.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTournamentResults.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTournamentResults.Location = new System.Drawing.Point(1015, 7);
            this.lblTournamentResults.Name = "lblTournamentResults";
            this.lblTournamentResults.Size = new System.Drawing.Size(128, 17);
            this.lblTournamentResults.TabIndex = 19;
            this.lblTournamentResults.Text = "Tournament results";
            // 
            // ctrlStopTournament
            // 
            this.ctrlStopTournament.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlStopTournament.Location = new System.Drawing.Point(1216, 535);
            this.ctrlStopTournament.Name = "ctrlStopTournament";
            this.ctrlStopTournament.Size = new System.Drawing.Size(128, 36);
            this.ctrlStopTournament.TabIndex = 18;
            this.ctrlStopTournament.Text = "Stop tournament";
            this.ctrlStopTournament.UseVisualStyleBackColor = true;
            this.ctrlStopTournament.Click += new System.EventHandler(this.ctrlStopTournament_Click);
            // 
            // ctrlPlayTournament
            // 
            this.ctrlPlayTournament.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlPlayTournament.Location = new System.Drawing.Point(1082, 535);
            this.ctrlPlayTournament.Name = "ctrlPlayTournament";
            this.ctrlPlayTournament.Size = new System.Drawing.Size(128, 36);
            this.ctrlPlayTournament.TabIndex = 17;
            this.ctrlPlayTournament.Text = "Play tournament";
            this.ctrlPlayTournament.UseVisualStyleBackColor = true;
            this.ctrlPlayTournament.Click += new System.EventHandler(this.ctrlPlayTournament_Click);
            // 
            // ctrlTournamentGrid
            // 
            this.ctrlTournamentGrid.AllowUserToAddRows = false;
            this.ctrlTournamentGrid.AllowUserToDeleteRows = false;
            this.ctrlTournamentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlTournamentGrid.Location = new System.Drawing.Point(1015, 47);
            this.ctrlTournamentGrid.Name = "ctrlTournamentGrid";
            this.ctrlTournamentGrid.ReadOnly = true;
            this.ctrlTournamentGrid.RowHeadersVisible = false;
            this.ctrlTournamentGrid.RowTemplate.Height = 25;
            this.ctrlTournamentGrid.Size = new System.Drawing.Size(329, 482);
            this.ctrlTournamentGrid.TabIndex = 16;
            // 
            // ctrlTurnSpeed
            // 
            this.ctrlTurnSpeed.Location = new System.Drawing.Point(198, 548);
            this.ctrlTurnSpeed.Name = "ctrlTurnSpeed";
            this.ctrlTurnSpeed.Size = new System.Drawing.Size(92, 23);
            this.ctrlTurnSpeed.TabIndex = 15;
            this.ctrlTurnSpeed.Text = "50";
            // 
            // lblTurnSpeed
            // 
            this.lblTurnSpeed.AutoSize = true;
            this.lblTurnSpeed.Location = new System.Drawing.Point(198, 532);
            this.lblTurnSpeed.Name = "lblTurnSpeed";
            this.lblTurnSpeed.Size = new System.Drawing.Size(92, 15);
            this.lblTurnSpeed.TabIndex = 14;
            this.lblTurnSpeed.Text = "Turn speed (ms)";
            // 
            // ctrlShipSquares
            // 
            this.ctrlShipSquares.Location = new System.Drawing.Point(100, 548);
            this.ctrlShipSquares.Name = "ctrlShipSquares";
            this.ctrlShipSquares.Size = new System.Drawing.Size(92, 23);
            this.ctrlShipSquares.TabIndex = 13;
            this.ctrlShipSquares.Text = "20";
            // 
            // lblShipSquares
            // 
            this.lblShipSquares.AutoSize = true;
            this.lblShipSquares.Location = new System.Drawing.Point(100, 530);
            this.lblShipSquares.Name = "lblShipSquares";
            this.lblShipSquares.Size = new System.Drawing.Size(73, 15);
            this.lblShipSquares.TabIndex = 12;
            this.lblShipSquares.Text = "Ship squares";
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
            this.lblPlayer2.ForeColor = System.Drawing.Color.Green;
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
            this.ctrlPlayTurn.Location = new System.Drawing.Point(405, 535);
            this.ctrlPlayTurn.Name = "ctrlPlayTurn";
            this.ctrlPlayTurn.Size = new System.Drawing.Size(98, 36);
            this.ctrlPlayTurn.TabIndex = 5;
            this.ctrlPlayTurn.Text = "Play turn";
            this.ctrlPlayTurn.UseVisualStyleBackColor = true;
            this.ctrlPlayTurn.Click += new System.EventHandler(this.ctrlPlayTurn_Click);
            // 
            // ctrlStartGame
            // 
            this.ctrlStartGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctrlStartGame.Location = new System.Drawing.Point(296, 535);
            this.ctrlStartGame.Name = "ctrlStartGame";
            this.ctrlStartGame.Size = new System.Drawing.Size(101, 36);
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
            this.ctrlBoard2.PlayerColor = System.Drawing.Color.Green;
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
            this.ctrlLog.Size = new System.Drawing.Size(1586, 154);
            this.ctrlLog.TabIndex = 3;
            // 
            // Form_Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1610, 766);
            this.Controls.Add(this.ctrlSplit_Horizontal);
            this.DoubleBuffered = true;
            this.Name = "Form_Battleship";
            this.Text = "Battleship coding challenge";
            this.ctrlSplit_Horizontal.Panel1.ResumeLayout(false);
            this.ctrlSplit_Horizontal.Panel2.ResumeLayout(false);
            this.ctrlSplit_Horizontal.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlSplit_Horizontal)).EndInit();
            this.ctrlSplit_Horizontal.ResumeLayout(false);
            this.ctrlSplit_Vertical.Panel1.ResumeLayout(false);
            this.ctrlSplit_Vertical.Panel1.PerformLayout();
            this.ctrlSplit_Vertical.Panel2.ResumeLayout(false);
            this.ctrlSplit_Vertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlSplit_Vertical)).EndInit();
            this.ctrlSplit_Vertical.ResumeLayout(false);
            this.ctrlAIs_ContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlTournamentGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer ctrlTurnTimer;
        private SplitContainer ctrlSplit_Horizontal;
        private SplitContainer ctrlSplit_Vertical;
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
        private TextBox ctrlShipSquares;
        private Label lblShipSquares;
        private Label lblTurnSpeed;
        private TextBox ctrlTurnSpeed;
        private DataGridView ctrlTournamentGrid;
        private Button ctrlStopTournament;
        private Button ctrlPlayTournament;
        private Label lblTournamentGameInfo;
        private Label lblTournamentResults;
    }
}