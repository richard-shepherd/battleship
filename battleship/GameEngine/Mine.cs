namespace GameEngine
{
    /// <summary>
    /// Manages a mine.
    /// </summary>
    public class Mine
    {
        #region Properties

        /// <summary>
        /// Gets the mine's position on the board.
        /// </summary>
        public API.Shared.BoardSquareCoordinates BoardPosition => m_boardPosition;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Mine(Board parentBoard, API.Shared.BoardSquareCoordinates boardPosition)
        {
            m_parentBoard = parentBoard;
            m_boardPosition = boardPosition;
        }

        /// <summary>
        /// Checks time-to-live and removes the mine from the board if it has expired.
        /// </summary>
        public void checkTTL()
        {
            m_turnsRemaining--;
            if(m_turnsRemaining <= 0)
            {
                m_parentBoard.removeMine(this);
            }
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly Board m_parentBoard;
        private readonly API.Shared.BoardSquareCoordinates m_boardPosition;

        // The remaining lifetime of the mine...
        private int m_turnsRemaining = 20;

        #endregion
    }
}
