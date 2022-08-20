namespace GameEngine
{
    /// <summary>
    /// Holds the board for one player.
    /// </summary><remarks>
    /// The board holds one player's ships plus the positions of enemy mines and drones.
    /// 
    /// Sparse data
    /// -----------
    /// Boards can be of varying sizes, and some may very large, eg 1000x1000 or larger. The
    /// number of ships and items can be a lot smaller than this. So we do not represent the
    /// entire board grid. Instead we hold a sparse collection of information for squares
    /// which contain something. For example:
    ///   (34, 46) -> Ship[2].Part[3]
    ///   (13, 51) -> Mine
    /// </remarks>
    internal class Board
    {
        #region Properties

        /// <summary>
        /// Gets the collection of undamaged parts across all ships on the board.
        /// </summary>
        public IEnumerable<ShipPart> UndamagedParts => m_ships.SelectMany(x => x.ShipParts).Where(x => !x.IsDamaged);

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Board(List<API.StartGame.AIResponse.ShipPlacement> shipPlacements)
        {
            // We add ships to the board based on the AI's ship-placement requests...
            foreach(var shipPlacement in shipPlacements)
            {
                m_ships.Add(new Ship(shipPlacement));
            }
            updateShipPartLocations();
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Updates the collection of ship part locations.
        /// </summary>
        private void updateShipPartLocations()
        {
            m_shipPartLocations.Clear();
            var shipParts = m_ships.SelectMany(x => x.ShipParts);
            foreach(var shipPart in shipParts)
            {
                m_shipPartLocations[shipPart.BoardPositionTuple] = shipPart;
            }
        }

        #endregion

        #region Private data

        // The list of the AI's ships. The order (and indexes) in this list are the same as in the list
        // of ships supplied by the AI in response the the START_GAME message...
        private readonly List<Ship> m_ships = new List<Ship>();

        // The collection of the AI's ship-parts, keyed by their 1-based position on the board...
        private readonly Dictionary<(int X, int Y), ShipPart> m_shipPartLocations = new Dictionary<(int X, int Y), ShipPart>();

        #endregion
    }
}
