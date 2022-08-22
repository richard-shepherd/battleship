using Utility;

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

        /// <summary>
        /// Gets the ship placement.
        /// </summary>
        public API.Shared.ShipPlacement ShipPlacement => m_shipPlacement;

        /// <summary>
        /// Gets the fuel available for movement.
        /// </summary>
        public int Fuel => m_fuel;

        /// <summary>
        /// Gets the ship type - eg, CARRIER, BATTLESHIP etc.
        /// </summary>
        public API.Shared.ShipTypeEnum ShipType => m_shipPlacement.ShipType;

        /// <summary>
        /// Gets the type of shot fired by this ship.
        /// </summary>
        public API.Shared.ShotTypeEnum ShotType => m_shotType;

        /// <summary>
        /// Gets the collection of undamaged parts for this ship
        /// </summary>
        public IEnumerable<ShipPart> UndamagedParts => m_shipParts.Where(x => !x.IsDamaged);

        /// <summary>
        /// Gets whether the ship is destroyed - ie, if all of its parts have been hit.
        /// </summary>
        public bool IsDestroyed => UndamagedParts.None();

        /// <summary>
        /// Gets whether the ship can fire offensive weapons, ie shells or mines.
        /// </summary><remarks>
        /// This is used to help determine a particular type of draw - where both players have
        /// ships remaining, but where neither player can fire offensive weapons.
        /// </remarks>
        public bool HasOffensiveWeapons => m_shipParts.Any(x => x.HasOffensiveWeapons);

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Ship(API.Shared.ShipPlacement shipPlacement)
        {
            m_shipPlacement = shipPlacement;

            // We find the weapon type from the ship type...
            m_shotType = API.Shared.ShipWeapons[shipPlacement.ShipType];

            // We create the parts of the ship (one for each square the ship occupies) and set up their initial
            // positions on the board...
            m_shipSize = API.Shared.ShipSizes[shipPlacement.ShipType];
            for(var i=0; i < m_shipSize; ++i)
            {
                m_shipParts.Add(new ShipPart(this));
            }
            updatePartPositions();

            // We set the initial amount of fuel available. This depends on the ship size, as
            // when we move a ship we do this by calculating howmuch fuel it takes to move each
            // part to the new location...
            m_fuel = m_shipSize * 20;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Updates the positions of the ship-parts based on the location and orientation of the ship.
        /// </summary>
        private void updatePartPositions()
        {
            var orientation = m_shipPlacement.Orientation;
            var x = m_shipPlacement.TopLeft.X;
            var y = m_shipPlacement.TopLeft.Y;
            for (var i = 0; i < m_shipSize; ++i)
            {
                switch(orientation)
                {
                    case API.Shared.OrientationEnum.HORIZONTAL:
                        m_shipParts[i].setBoardPosition(x + i, y);
                        break;

                    case API.Shared.OrientationEnum.VERTICAL:
                        m_shipParts[i].setBoardPosition(x, y + i);
                        break;
                }
            }
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly API.Shared.ShipPlacement m_shipPlacement;
            
        // The type of shots fired by the ship...
        private readonly API.Shared.ShotTypeEnum m_shotType;

        // The size of the ship...
        private readonly int m_shipSize;

        // The collection of parts making up the ship...
        private readonly List<ShipPart> m_shipParts = new List<ShipPart>();

        // The amount of fuel available for movement...
        private int m_fuel = 0;

        #endregion
    }
}
