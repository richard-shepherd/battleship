using Utility;

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
        public Game(AIManager aiManager, string ai1_Name, string ai2_Name, int boardSize, int shipQuares)
        {
            m_aiManager = aiManager;
            m_ai1_Name = ai1_Name;
            m_ai2_Name = ai2_Name;
            m_boardSize = boardSize;
            m_shipQuares = shipQuares;

            // We create the two AIs and set up the boards...
            var startGameMessage = new API.StartGame.Message();
            startGameMessage.BoardSize.X = m_boardSize;
            startGameMessage.BoardSize.Y = m_boardSize;
            startGameMessage.ShipSquares = m_shipQuares;

            // We send the start-game message to AI1...
            m_ai1 = m_aiManager.createAIProcess(m_ai1_Name);
            startGameMessage.OpponentAIName = ai2_Name;
            m_ai1.sendMessage(startGameMessage);

            // We send the start-game message to AI2...
            m_ai2 = m_aiManager.createAIProcess(m_ai2_Name);
            startGameMessage.OpponentAIName = ai1_Name;
            m_ai2.sendMessage(startGameMessage);

            // We wait for responses...
            GameUtils.waitForAIReponses(m_ai1, m_ai2, START_GAME_TIMEOUT, startGameMessage.EventName);

            // We create the board for AI1...
            var ai1Response = m_ai1.getOutputAs<API.StartGame.AIResponse>();
            m_board1 = new Board(ai1Response.ShipPlacements);

            // We create the board for AI2...
            var ai2Response = m_ai2.getOutputAs<API.StartGame.AIResponse>();
            m_board2 = new Board(ai2Response.ShipPlacements);
        }

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (IsDisposed) return;

            // We clean up the AI processes...
            m_ai1.Dispose();
            m_ai2.Dispose();

            IsDisposed = true;
        }

        protected bool IsDisposed { get; private set; }

        #endregion

        #region Private data

        // Construction params...
        private readonly AIManager m_aiManager;
        private readonly string m_ai1_Name;
        private readonly string m_ai2_Name;
        private readonly int m_boardSize;
        private readonly int m_shipQuares;

        // The process and board for AI1...
        private readonly AIProcess m_ai1;
        private readonly Board m_board1;

        // The process and board for AI2...
        private readonly AIProcess m_ai2;
        private readonly Board m_board2;

        // Constants...
        private const int START_GAME_TIMEOUT = 30000;

        #endregion
    }
}
