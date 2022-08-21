namespace GameEngine
{
    /// <summary>
    /// Manages a mine.
    /// </summary>
    internal class Mine
    {
        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Mine(Board parentBoard, API.Shared.BoardSquareCoordinates boardPosition)
        {
            m_parentBoard = parentBoard;
            m_boardPosition = boardPosition;
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
