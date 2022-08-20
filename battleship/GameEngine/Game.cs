namespace GameEngine
{
    /// <summary>
    /// Manages a game of Battleship between two AIs.
    /// </summary>
    internal class Game
    {
        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Game(AIManager aiManager, string ai1_Name, string ai2_Name, int boardSize, int shipSquares)
        {
            m_aiManager = aiManager;
            m_ai1_Name = ai1_Name;
            m_ai2_Name = ai2_Name;
            m_boardSize = boardSize;
            m_shipSquares = shipSquares;

            // We create the two players, and send the START_GAME message to them...
            m_player1 = new Player(aiManager, ai1_Name);
            m_player2 = new Player(aiManager, ai2_Name);
            m_player1.startGame_sendMessage(boardSize, shipSquares, ai2_Name);
            m_player2.startGame_sendMessage(boardSize, shipSquares, ai1_Name);

            // We wait for responses, and set up the board for each player...
            GameUtils.waitForAIReponses(m_player1, m_player2, START_GAME_TIMEOUT, API.StartGame.EventName);
            m_player1.startGame_processResponse();
            m_player2.startGame_processResponse();
        }

        /// <summary>
        /// Plays one turn of the game.
        /// </summary>
        public void playTurn()
        {
            // Requests the players to fire their weapons, and processes the responses...
            fireWeapons();
        }

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (IsDisposed) return;

            // We clean up the players...
            m_player1.Dispose();
            m_player2.Dispose();

            IsDisposed = true;
        }

        protected bool IsDisposed { get; private set; }

        #endregion

        #region Private functions

        /// <summary>
        /// Requests the players to fire their weapons, and processes the responses.
        /// </summary>
        private void fireWeapons()
        {
            // We request each AI to fire its weapons, and wait for both responses...
            m_player1.fireWeapons_sendMessage();
            m_player2.fireWeapons_sendMessage();
            GameUtils.waitForAIReponses(m_player1, m_player2, TURN_TIMEOUT, API.FireWeapons.EventName);

            // We process the responses...
            m_player1.fireWeapons_processResponse();
            m_player2.fireWeapons_processResponse();
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly AIManager m_aiManager;
        private readonly string m_ai1_Name;
        private readonly string m_ai2_Name;
        private readonly int m_boardSize;
        private readonly int m_shipSquares;

        // The two players...
        private readonly Player m_player1;
        private readonly Player m_player2;

        // Constants...
        private const int START_GAME_TIMEOUT = 30000;
        private const int TURN_TIMEOUT = 1000;

        #endregion
    }
}
