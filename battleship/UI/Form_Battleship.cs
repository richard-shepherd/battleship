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

                // We start a new game...
                m_game = new Game(m_aiManager, aiName1, aiName2, 30, 40);
                m_game.startGame();

                // We show the player names...
                lblPlayer1.Text = $"Player 1: {m_game.Player1.AIName}";
                lblPlayer2.Text = $"Player 2: {m_game.Player2.AIName}";

                // We show the boards with the initial ship placement...
                ctrlBoard1.showBoard(m_game.Player1.Board);
                ctrlBoard2.showBoard(m_game.Player2.Board);
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

        #endregion

        #region Private functions

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

            // We show the boards...
            ctrlBoard1.showBoard(m_game.Player1.Board);
            ctrlBoard2.showBoard(m_game.Player2.Board);
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

        #endregion
    }
}