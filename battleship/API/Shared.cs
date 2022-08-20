﻿namespace API
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
            MINELAYER
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

            /// <summary>
            /// Returns the coordinates as a tuple.
            /// </summary>
            public (int X, int Y) toTuple()
            {
                return (X, Y);
            }
        }

        /// <summary>
        /// Dictionary of ship-type enum to the size of each ship.
        /// </summary>
        public static Dictionary<ShipTypeEnum, int> ShipSizes { get; set; } = new Dictionary<ShipTypeEnum, int>
        {
            {ShipTypeEnum.CARRIER, 5 },
            {ShipTypeEnum.BATTLESHIP, 4 },
            {ShipTypeEnum.MINELAYER, 3 }
        };

        /// <summary>
        /// Dictionary of ship-type enum to the type of shot fired by the ship.
        /// </summary>
        public static Dictionary<ShipTypeEnum, ShotTypeEnum> ShipWeapons { get; set; } = new Dictionary<ShipTypeEnum, ShotTypeEnum>
        {
            {ShipTypeEnum.CARRIER, ShotTypeEnum.DRONE },
            {ShipTypeEnum.BATTLESHIP, ShotTypeEnum.SHELL },
            {ShipTypeEnum.MINELAYER, ShotTypeEnum.MINE }
        };

        /// <summary>
        /// Dictionary of shot-type to the cost of firing each type of shot.
        /// </summary><remarks>
        /// The costs refer to the number of undamaged ship-parts needed to take one shot of the specified type.
        /// For example, if you have 7 undamaged ship-parts (squares) capabale of firing drones, then you will
        /// accumulate 1.4 drones each turn. For example, you can fire one drone, and accumulate 0.4 drones for 
        /// subsequent turns.
        /// </remarks>
        public static Dictionary<ShotTypeEnum, double> ShotCosts = new Dictionary<ShotTypeEnum, double>
        {
            { ShotTypeEnum.SHELL, 1.0},
            { ShotTypeEnum.MINE, 5.0},
            { ShotTypeEnum.DRONE, 5.0}
        };
    }
}
