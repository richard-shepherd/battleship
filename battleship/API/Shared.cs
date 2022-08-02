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
    }
}
