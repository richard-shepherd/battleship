using GameEngine;
using Utility;

namespace UI
{
    /// <summary>
    /// Shows Battleshiup games.
    /// </summary>
    public partial class Form_Battleship : Form
    {
        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Form_Battleship()
        {
            InitializeComponent();

            // We select a 30x30 board by default...
            ctrlBoardSize.SelectedItem = ctrlBoardSize.Items[2];

            // We load AIs from the defauilt folder...
            ctrlAIs_Load_Click(null, null);
        }

        #endregion

        #region Form events

        /// <summary>
        /// Called when the Load AIs button is pressed.
        /// </summary>
        private void ctrlAIs_Load_Click(object sender, EventArgs e)
        {
            try
            {
                // We load AIs from the folder specified and show them in the list...
                m_aiManager = new AIManager(ctrlAIFolder.Text);
                ctrlAIs.Items.Clear();
                ctrlAIs.Items.AddRange(m_aiManager.AINames.ToArray());
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        /// <summary>
        /// Called when the Select All button is pressed.
        /// </summary>
        private void ctrlAIs_SelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                ctrlAIs.CheckAll(true);
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        /// <summary>
        /// Called when the Select None button is pressed.
        /// </summary>
        private void ctrlAIs_SelectNone_Click(object sender, EventArgs e)
        {
            try
            {
                ctrlAIs.CheckAll(false);
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        /// <summary>
        /// Called when the Start Game button is pressed.
        /// </summary>
        private void ctrlStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                // We dispose any existing game...
                cleanupGame();

                // We find the AIs to play.
                // - If two AIs are selected, we play them against each other.
                // - If one AI is selected, we play it against itself.
                if(ctrlAIs.CheckedItems.Count == 0 || ctrlAIs.CheckedItems.Count > 2)
                {
                    MessageBox.Show(this, "Please select one or two AIs from the list");
                    return;
                }

                // We find the first and second AI...
                var aiName1 = ctrlAIs.CheckedItems[0].ToString();
                var aiName2 = aiName1;
                if (ctrlAIs.CheckedItems.Count == 2)
                {
                    aiName2 = ctrlAIs.CheckedItems[1].ToString();
                }

                // We find the board size...
                var strBoardSize = ctrlBoardSize.SelectedItem.ToString();
                var tokens = strBoardSize.Split('x');
                var boardSize = Convert.ToInt32(tokens[0]);

                // We find the number of ship squares...
                var shipSquares = Convert.ToInt32(ctrlShipSquares.Text);

                // We set the turn speed for the timer...
                var turnSpeedMS = Convert.ToInt32(ctrlTurnSpeed.Text);
                if(turnSpeedMS > 0)
                {
                    ctrlTurnTimer.Enabled = true;
                    ctrlTurnTimer.Interval = turnSpeedMS;
                }
                else
                {
                    ctrlTurnTimer.Enabled = false;
                }

                // We start a new game...
                m_game = new Game(m_aiManager, aiName1, aiName2, boardSize, shipSquares);
                m_game.startGame();

                // We show the board and player info...
                showGame(m_game);
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        /// <summary>
        /// Called when the Play Turn button is pressed.
        /// </summary>
        private void ctrlPlayTurn_Click(object sender, EventArgs e)
        {
            try
            {
                playTurn();
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        /// <summary>
        /// Called when the turn timer ticks.
        /// </summary>
        private void ctrlTurnTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                playTurn();
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        /// <summary>
        /// Called when the Play Tournament button is pressed.
        /// </summary>
        private void ctrlPlayTournament_Click(object sender, EventArgs e)
        {
            try
            {
                // We set up the results...
                m_tournamentInfo = new TournamentInfo(m_aiManager.AINames);
                ctrlTournamentGrid.DataSource = m_tournamentInfo.AIInfos;

                // Will be set to true if the tournament should be stopped while it is playing...
                m_stopTournament = false;

                // For a tournament we play every AI against every other AI on various 
                // board configurations. We play a number of rounds of each of these
                // combinations.
                var boardSizes = new List<int> { 10, 20, 30, 40, 50, 80, 100, 200 };
                var shipSquaress = new List<int> { 20, 30, 40, 50, 100, 200, 300 };
                foreach(var aiName1 in m_aiManager.AINames)
                {
                    if (m_stopTournament) break;
                    foreach (var aiName2 in m_aiManager.AINames)
                    {
                        if (m_stopTournament) break;

                        // We do not play AIs against themselves...
                        if (aiName1 == aiName2)
                        {
                            continue;
                        }

                        // We play each combination of AIs on each board size and with different numbers 
                        // of ship squares...
                        foreach(var boardSize in boardSizes)
                        {
                            if (m_stopTournament) break;

                            foreach (var shipSquares in shipSquaress)
                            {
                                if (m_stopTournament) break;

                                // We limit the number of ship-squares depending on the size of the board,
                                // as smaller boards may not have enough room for all the ships...
                                if (shipSquares > boardSize * 2)
                                {
                                    continue;
                                }

                                // We play a game...
                                var game = new Game(m_aiManager, aiName1, aiName2, boardSize, shipSquares);
                                lblTournamentGameInfo.Text = $"{boardSize}x{boardSize}, {shipSquares}";
                                Application.DoEvents();
                                try
                                {
                                    game.startGame();
                                    var turnsPlayed = 0;
                                    while(game.GameStatus == Game.GameStatusEnum.PLAYING)
                                    {
                                        if (m_stopTournament) break;

                                        // We play a turn...
                                        game.playTurn();

                                        // We show the board every few turns...
                                        if(turnsPlayed % 50 == 0)
                                        {
                                            showGame(game);
                                            Application.DoEvents();
                                        }
                                        turnsPlayed++;
                                    }

                                    // The game has ended, so we update the results...
                                    m_tournamentInfo.updateAIInfo(aiName1, (game.WinningAIName == aiName1) ? 1 : 0, game.Player1.Board.UndamagedParts.Count());
                                    m_tournamentInfo.updateAIInfo(aiName2, (game.WinningAIName == aiName2) ? 1 : 0, game.Player2.Board.UndamagedParts.Count());

                                    // We show the results. This also allows the Stop Tournament button to be processed...
                                    showGame(game);
                                    Application.DoEvents();
                                }
                                catch (Exception ex)
                                {
                                    Logger.log(ex);
                                }
                                finally
                                {
                                    game.Dispose();
                                }
                            }
                        }
                    }
                }

                // We result the tournament game-info message...
                lblTournamentGameInfo.Text = "(Tournament not running)";
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }

        }

        /// <summary>
        /// Called when the Stop Tournament button is pressed.
        /// </summary>
        private void ctrlStopTournament_Click(object sender, EventArgs e)
        {
            try
            {
                m_stopTournament = true;
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Shows the player names and associated info.
        /// </summary>
        private void showPlayerInfo(Game game)
        {
            // Player 1...
            var player1 = $"Player 1: {game.Player1.AIName} ({game.Player1.Board.UndamagedParts.Count()})";
            if (game.WinningPlayerNumber == 1)
            {
                player1 += " WINNER!";
            }
            lblPlayer1.Text = player1;

            // Player 2...
            var player2 = $"Player 2: {game.Player2.AIName} ({game.Player2.Board.UndamagedParts.Count()})";
            if (game.WinningPlayerNumber == 2)
            {
                player2 += " WINNER!";
            }
            lblPlayer2.Text = player2;
        }

        /// <summary>
        /// Plays one turn of the game and shows the game state.
        /// </summary>
        private void playTurn()
        {
            // We check that we have an active game...
            if (m_game == null)
            {
                return;
            }

            // We check that the game is not over...
            if (m_game.GameStatus != Game.GameStatusEnum.PLAYING)
            {
                return;
            }

            // We play a turn...
            m_game.playTurn();

            // We show the boards and player info...
            showGame(m_game);
        }

        /// <summary>
        /// Shows the current status of the game.
        /// </summary>
        private void showGame(Game game)
        {
            // We show the boards...
            ctrlBoard1.showBoard(game.Player1.Board);
            ctrlBoard2.showBoard(game.Player2.Board);

            // We show the player names and info...
            showPlayerInfo(game);
        }

        /// <summary>
        /// Cleans up the current game.
        /// </summary>
        private void cleanupGame()
        {
            if (m_game != null)
            {
                m_game.Dispose();
                m_game = null;
            }
        }

        #endregion

        #region Private data

        // The AI manager...
        private AIManager m_aiManager = null;

        // The game currently being played...
        private Game m_game = null;

        // Holds tournament results...
        private TournamentInfo m_tournamentInfo = null;

        // Set to true to stop a tournament while it is playing...
        private bool m_stopTournament = false;

        #endregion
    }
}