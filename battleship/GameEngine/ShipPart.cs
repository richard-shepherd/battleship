namespace GameEngine
{
    /// <summary>
    /// Manages one part (board-square) for a ship.
    /// </summary>
    internal class ShipPart
    {
        #region Properties

        /// <summary>
        /// Gets the part's board position as an (int, int) tuple.
        /// </summary>
        public (int X, int Y) BoardPositionTuple => m_boardPosition.toTuple();

        /// <summary>
        /// Gets whether this ship-part is damaged.
        /// </summary>
        public bool IsDamaged => m_isDamaged;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShipPart(Ship parent)
        {
            m_parent = parent;
        }

        /// <summary>
        /// Sets (or updates) the 1-based position on the board where this ship-part is located.
        /// </summary>
        public void setBoardPosition(int x, int y)
        {
            m_boardPosition.X = x;
            m_boardPosition.Y = y;
        }

        #endregion

        #region Private data

        // The parent ship of which this is a part...
        private readonly Ship m_parent;

        // The 1-based position on the board of this ship-part...
        private readonly API.Shared.BoardSquareCoordinates m_boardPosition = new API.Shared.BoardSquareCoordinates();

        // Set to true once the ship-part has been hit and is damaged...
        private bool m_isDamaged = false;

        #endregion
    }
}
