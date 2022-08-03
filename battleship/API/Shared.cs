namespace API
{
    /// <summary>
    /// Definitions shared across multiple API classes.
    /// </summary>
    public class Shared
    {
        /// <summary>
        /// Enum for ship types.
        /// </summary>
        public enum ShipTypeEnum
        {
            CARRIER,
            BATTLESHIP,
            DESTROYER,
            CRUISER,
            PATROL_BOAT
        }

        /// <summary>
        /// Enum for the various shot types.
        /// </summary>
        public enum ShotTypeEnum
        {
            SHELL,
            MINE,
            DRONE
        }

        /// <summary>
        /// Enum for ship orientation.
        /// </summary>
        public enum OrientationEnum
        {
            HORIZONTAL,
            VERTICAL
        }

        /// <summary>
        /// Coordinates for one square of the board.
        /// NOTE: Coordinates are 1-based.
        /// </summary>
        public class BoardSquareCoordinates
        {
            /// <summary>
            /// Gets or sets the 1-based X coordinate.
            /// </summary>
            public int X { get; set; } = 0;

            /// <summary>
            /// Gets or sets the 1-based Y coordinate.
            /// </summary>
            public int Y { get; set; } = 0;
        }
    }
}
