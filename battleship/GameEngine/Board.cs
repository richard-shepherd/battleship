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
    public class Board
    {
        #region Properties

        /// <summary>
        /// Gets the board size.
        /// </summary>
        public int BoardSize => m_boardSize;

        /// <summary>
        /// Gets the list of ships on the board.
        /// </summary>
        public List<Ship> Ships => m_ships;

        /// <summary>
        /// Gets the collection of mines on the board.
        /// </summary>
        public IEnumerable<Mine> Mines => m_mineLocations.Values;

        /// <summary>
        /// Gets the collection of drones on the board.
        /// </summary>
        public IEnumerable<Drone> Drones => m_drones;

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

        /// <summary>
        /// Gets the locationsof shells fired to this board in the most recent turn.
        /// </summary>
        public HashSet<API.Shared.BoardSquareCoordinates> ShelledSquares { get; } = new HashSet<API.Shared.BoardSquareCoordinates>();

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Board(int boardSize, List<API.Shared.ShipPlacement> shipPlacements)
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
        /// Moves a ship.
        /// </summary>
        public void moveShip(int shipIndex, API.Shared.ShipPlacement newShipPlacement, int fuelUsed)
        {
            var ship = m_ships[shipIndex];
            ship.updateShipPlacement(newShipPlacement);
            ship.Fuel -= fuelUsed;
            updateShipPartLocations();
        }

        /// <summary>
        /// Checks if any ships have been hit by mines.
        /// </summary><remarks>
        /// In particular, this is checked after the MOVE phase to see if any moved ships have
        /// hit any lurking mines.
        /// </remarks>
        public API.StatusUpdate.Message.DamageReport checkMines()
        {
            var damageReport = new API.StatusUpdate.Message.DamageReport();

            // We check each mine.
            // Note: We loop over a copy of the mines, as we may remove mines from the underlying collection
            //       if we detect that they have hit a ship.
            var mines = new List<Mine>(m_mineLocations.Values);
            foreach(var mine in mines)
            {
                var hitStatus = BoardUtils.checkForHit(this, mine.BoardPosition);
                var shotStatus = hitStatus.ShotStatus;
                if (shotStatus == API.StatusUpdate.Message.ShotStatusEnum.HIT)
                {
                    // The mine hit a ship, so we add this to the damage report...
                    var shotInfo = new API.StatusUpdate.Message.ShotInfo();
                    shotInfo.TargetSquare = mine.BoardPosition;
                    shotInfo.ShotStatus = shotStatus;
                    damageReport.ShotInfos.Add(shotInfo);
                    var ship = hitStatus.ShipPart.Ship;
                    if (ship.IsDestroyed)
                    {
                        damageReport.DestroyedShips.Add(ship.ShipType);
                    }

                    // We remove the mine...
                    removeMine(mine);
                }
            }

            return damageReport;
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

        /// <summary>
        /// Returns a list of DroneReports for any drones which detect ships.
        /// </summary><remarks>
        /// Drones detect ships if they are directly N, S, E, W of the drone - or if the
        /// drone is directly over the ship.
        /// </remarks>
        public List<API.StatusUpdate.Message.DroneReport> getDroneReports()
        {
            // Perhaps there is a more efficient algorithm for this?
            // ie, without checking each drone against each ship part.

            // We check each drone...
            var droneReports = new List<API.StatusUpdate.Message.DroneReport>();
            foreach (var drone in m_drones)
            {
                var droneX = drone.BoardPosition.X;
                var droneY = drone.BoardPosition.Y;

                // We check each ship part...
                var directionsDetected = new HashSet<API.StatusUpdate.Message.DroneDetectionEnum>();
                foreach (var shipPart in m_shipPartLocations.Values)
                {
                    // Drones only detect undamaged ship-parts...
                    if(shipPart.IsDamaged)
                    {
                        continue;
                    }

                    // We check if the drone can see a ship part the N, S, E, W...
                    var shipPartX = shipPart.BoardPosition.X;
                    var shipPartY = shipPart.BoardPosition.Y;
                    if(shipPartX == droneX && shipPartY == droneY)
                    {
                        directionsDetected.Add(API.StatusUpdate.Message.DroneDetectionEnum.OVER_SHIP);
                    }
                    else if (shipPartX == droneX && shipPartY < droneY)
                    {
                        directionsDetected.Add(API.StatusUpdate.Message.DroneDetectionEnum.NORTH);
                    }
                    else if (shipPartX == droneX && shipPartY > droneY)
                    {
                        directionsDetected.Add(API.StatusUpdate.Message.DroneDetectionEnum.SOUTH);
                    }
                    else if (shipPartX < droneX && shipPartY == droneY)
                    {
                        directionsDetected.Add(API.StatusUpdate.Message.DroneDetectionEnum.WEST);
                    }
                    else if (shipPartX > droneX && shipPartY == droneY)
                    {
                        directionsDetected.Add(API.StatusUpdate.Message.DroneDetectionEnum.EAST);
                    }
                }

                // We only report if the drone detected something...
                if(directionsDetected.Any())
                {
                    var droneReport = new API.StatusUpdate.Message.DroneReport();
                    droneReport.DronePosition = drone.BoardPosition;
                    droneReport.DirectionsDetected.AddRange(directionsDetected);
                    droneReports.Add(droneReport);
                }
            }

            return droneReports;
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
