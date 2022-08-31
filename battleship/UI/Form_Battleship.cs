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
            m_aiManager = new AIManager(@"D:\code\battleship\AIs");
        }

        #endregion

        #region Form events

        /// <summary>
        /// Called when the Start Game button is pressed.
        /// </summary>
        private void ctrlStartGame_Click(object sender, EventArgs e)
        {
            try
            {
                // We dispose any existing game...
                cleanupGame();

                // We start a new game...
                m_game = new Game(m_aiManager, "AI_Sample_RandomPlayer", "AI_Sample_DroneHunter", 30, 40);
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
        private readonly AIManager m_aiManager;

        // The game currently being played...
        private Game m_game = null;

        #endregion
    }
}