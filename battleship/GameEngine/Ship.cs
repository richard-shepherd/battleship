namespace GameEngine
{
    /// <summary>
    /// Holds details of one ship.
    /// </summary>
    internal class Ship
    {
        #region Properties

        /// <summary>
        /// Gets the collection of parts (squares) for the ship.
        /// </summary>
        public List<ShipPart> ShipParts => m_shipParts;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Ship(API.StartGame.AIResponse.ShipPlacement shipPlacement)
        {
            m_shipType = shipPlacement.ShipType;
            m_topLeft = shipPlacement.TopLeft;
            m_orientation = shipPlacement.Orientation;

            // We find the weapon type from the ship type...
            m_shotType = API.Shared.ShipWeapons[m_shipType];

            // We create the parts of the ship (one for each square the ship occupies) and set up their initial
            // positions on the board...
            m_shipSize = API.Shared.ShipSizes[m_shipType];
            for(var i=0; i < m_shipSize; ++i)
            {
                m_shipParts.Add(new ShipPart(this));
            }
            updatePartPositions();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Updates the positions of the ship-parts based on the location and orientation of the ship.
        /// </summary>
        private void updatePartPositions()
        {
            for (var i = 0; i < m_shipSize; ++i)
            {
                switch(m_orientation)
                {
                    case API.Shared.OrientationEnum.HORIZONTAL:
                        m_shipParts[i].setBoardPosition(m_topLeft.X + i, m_topLeft.Y);
                        break;

                    case API.Shared.OrientationEnum.VERTICAL:
                        m_shipParts[i].setBoardPosition(m_topLeft.X, m_topLeft.Y + i);
                        break;
                }
            }
        }

        #endregion

        #region Private data

        // The type of ship, and type of shots it fires...
        private readonly API.Shared.ShipTypeEnum m_shipType;
        private readonly API.Shared.ShotTypeEnum m_shotType;

        // The size of the ship...
        private readonly int m_shipSize;

        // The 1-based top-left point of the ship, and its orientation...
        private readonly API.Shared.BoardSquareCoordinates m_topLeft = new API.Shared.BoardSquareCoordinates();
        private API.Shared.OrientationEnum m_orientation;

        // The collection of parts making up the ship...
        private readonly List<ShipPart> m_shipParts = new List<ShipPart>();

        #endregion
    }
}
