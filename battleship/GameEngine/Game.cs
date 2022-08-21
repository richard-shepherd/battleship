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
            m_player1.startGame_SendMessage(boardSize, shipSquares, ai2_Name);
            m_player2.startGame_SendMessage(boardSize, shipSquares, ai1_Name);

            // We wait for responses, and set up the board for each player...
            GameUtils.waitForAIReponses(m_player1, m_player2, START_GAME_TIMEOUT, API.StartGame.EventName);
            m_player1.startGame_ProcessResponse();
            m_player2.startGame_ProcessResponse();
        }

        /// <summary>
        /// Plays one turn of the game.
        /// </summary>
        public void playTurn()
        {
            // Requests the players to fire their weapons, processes the responses and sends
            // a status update to the AIs...
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
        /// Requests the players to fire their weapons, processes the responses and sends
        /// a status update to the AIs.
        /// </summary>
        private void fireWeapons()
        {
            // We request each AI to fire its weapons, and wait for both responses...
            m_player1.fireWeapons_SendMessage();
            m_player2.fireWeapons_SendMessage();
            GameUtils.waitForAIReponses(m_player1, m_player2, TURN_TIMEOUT, API.FireWeapons.EventName);

            // We process the responses.
            // Note about the damage reports returned:
            // - Damage-report-1 includes shots by player-1 and damage taken by player-2
            // - Damage-report-2 includes shots by player-2 and damage taken by player-1
            var damageReport1 = m_player1.fireWeapons_ProcessResponse(m_player2);
            var damageReport2 = m_player2.fireWeapons_ProcessResponse(m_player1);

            // We send the post-firing status update.
            // Note that when we send the udpates below, the Player and Opponent data
            // is sent the opposite way around to each player.

            // To player 1...
            var statusUpdate = new API.StatusUpdate.Message();
            statusUpdate.Player = damageReport1;
            statusUpdate.Opponent = damageReport2;
            m_player1.AI.sendMessage(statusUpdate);

            // To player 2...
            statusUpdate.Player = damageReport2;
            statusUpdate.Opponent = damageReport1;
            m_player2.AI.sendMessage(statusUpdate);

            // We wait for the AIs to ACK the status update...
            GameUtils.waitForAIReponses(m_player1, m_player2, TURN_TIMEOUT, API.StatusUpdate.EventName);
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
