using Utility;

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
        public IEnumerable<ShipPart> UndamagedParts => m_ships.SelectMany(x => x.UndamagedParts);

        /// <summary>
        /// Gets the number of ships on the board.
        /// </summary>
        public int ShipCount => m_ships.Count;

        /// <summary>
        /// Gets the number of active ships.
        /// </summary><remarks>
        /// An active ship is one which is not destoyed, though it may have some damaged parts.
        /// </remarks>
        public int ActiveShipCount => m_ships.Where(x => !x.IsDestroyed).Count();

        /// <summary>
        /// Gets whether the fleet is destroyed - ie, all parts of all ships have been hit.
        /// </summary>
        public bool FleetIsDestroyed => UndamagedParts.None();

        /// <summary>
        /// Gets whether any of the ships has offensive weapons.
        /// </summary><remarks>
        /// This is used to help determine a particular type of draw - where both players have
        /// ships remaining, but where neither player can fire offensive weapons.
        /// </remarks>
        public bool FleetHasOffensiveWeapons => m_ships.Any(x => x.HasOffensiveWeapons);

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Board(int boardSize, List<API.StartGame.AIResponse.ShipPlacement> shipPlacements)
        {
            m_boardSize = boardSize;

            // We add ships to the board based on the AI's ship-placement requests...
            foreach(var shipPlacement in shipPlacements)
            {
                m_ships.Add(new Ship(shipPlacement));
            }
            updateShipPartLocations();
        }

        /// <summary>
        /// Returns the ship-part at the 1-based (x, y) coordinates on the board.
        /// Returns null if there is no ship-part at the location.
        /// </summary>
        public ShipPart getShipPart(int x, int y)
        {
            if(m_shipPartLocations.TryGetValue((x, y), out var shipPart))
            {
                return shipPart;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a mine to the board.
        /// </summary>
        public void addMine(API.Shared.BoardSquareCoordinates boardPosition)
        {
            m_mineLocations[boardPosition.toTuple()] = new Mine(this, boardPosition);
        }

        /// <summary>
        /// Adds a drone to the board.
        /// </summary>
        public void addDrone(API.Shared.BoardSquareCoordinates boardPosition)
        {
            m_drones.Add(new Drone(this, boardPosition));
        }

        /// <summary>
        /// Removes a mine.
        /// </summary>
        public void removeMine(Mine mine)
        {
            m_mineLocations.Remove(mine.BoardPosition.toTuple());
        }

        /// <summary>
        /// Removes a drone.
        /// </summary>
        public void removeDrone(Drone drone)
        {
            m_drones.Remove(drone);
        }

        /// <summary>
        /// Checks time-to-live for mines and drones and removes expired items from the board.
        /// </summary>
        public void checkTTL()
        {
            // Note that for both mines and drones we iterate over a copy of the collection
            // of objects, as checking TTL may result in removing the item from the underlying
            // collection.

            // Mines...
            var mines = new List<Mine>(m_mineLocations.Values);
            foreach(var mine in mines)
            {
                mine.checkTTL();
            }

            // Drones...
            var drones = new List<Drone>(m_drones);
            foreach(var drone in drones)
            {
                drone.checkTTL();
            }
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

        // Construction params...
        private readonly int m_boardSize;

        // The list of the AI's ships. The order (and indexes) in this list are the same as in the list
        // of ships supplied by the AI in response the the START_GAME message...
        private readonly List<Ship> m_ships = new List<Ship>();

        // The collection of the AI's ship-parts, keyed by their 1-based position on the board...
        private readonly Dictionary<(int X, int Y), ShipPart> m_shipPartLocations = new Dictionary<(int X, int Y), ShipPart>();

        // The collection of active mines, keyed by their 1-based position on the board.
        // Note: As this is a dictionary by location, this means that a newer mine placed in the same location as
        //       an existing mine with replace the previous one.
        private readonly Dictionary<(int X, int Y), Mine> m_mineLocations = new Dictionary<(int X, int Y), Mine>();

        // The collection of active drones...
        private readonly HashSet<Drone> m_drones = new HashSet<Drone>();

        #endregion
    }
}
