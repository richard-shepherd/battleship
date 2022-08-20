namespace GameEngine
{
    /// <summary>
    /// Manages one game player.
    /// </summary><remarks>
    /// Manages the AI for the player as well as:
    /// - The board with the player's ships (plus enemy mines and drones)
    /// - The shots available to the player.
    /// </remarks>
    internal class Player
    {
        #region Properties

        /// <summary>
        /// Gets the AI process.
        /// </summary>
        public AIProcess AI => m_ai;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Player(AIManager aiManager, string aiName)
        {
            m_aiManager = aiManager;
            m_aiName = aiName;

            // We create the AI...
            m_ai = m_aiManager.createAIProcess(m_aiName);
        }

        /// <summary>
        /// Sends the START_GAME message to the AI, requesting initial ship placements.
        /// </summary>
        public void startGame_sendMessage(int boardSize, int shipSquares, string opponentName)
        {
            var startGameMessage = new API.StartGame.Message();
            startGameMessage.BoardSize.X = boardSize;
            startGameMessage.BoardSize.Y = boardSize;
            startGameMessage.ShipSquares = shipSquares;
            startGameMessage.OpponentAIName = opponentName;
            m_ai.sendMessage(startGameMessage);
        }

        /// <summary>
        /// Processes the response from the AI to the START_GAME message, including setting up the
        /// board with the initial ship placements.
        /// </summary>
        public void startGame_processResponse()
        {
            var aiResponse = m_ai.getOutputAs<API.StartGame.AIResponse>();
            m_board = new Board(aiResponse.ShipPlacements);
        }

        /// <summary>
        /// Sends the FIRE_WEAPONS message to the AI.
        /// </summary>
        public void fireWeapons_sendMessage()
        {
            // We update the number of shots of each type which are available - including
            // any accumulated unused shots from previous turns...
            var undamagedParts = m_board.UndamagedParts;

        }

        /// <summary>
        /// Processes the reponse from the AI to the FIRE_WEAPONS message.
        /// </summary>
        public void fireWeapons_processResponse()
        {

        }

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (IsDisposed) return;

            // We dispose (shut down) the AI...
            m_ai.Dispose();

            IsDisposed = true;
        }

        protected bool IsDisposed { get; private set; }

        #endregion

        #region Private data

        // Construction params...
        private readonly AIManager m_aiManager;
        private readonly string m_aiName;

        // The AI process...
        private readonly AIProcess m_ai;

        // The board, holding the player's ships and enemy mines and drones...
        private Board m_board;

        // Shots available. These can accumulate between turns if not all shots are taken...
        private double m_shellsAvailable = 0.0;
        private double m_minesAvailable = 0.0;
        private double m_dronesAvailable = 0.0;

        #endregion
    }
}
