namespace API
{
    /// <summary>
    /// Sent from the game engine to each AI at the start of the game.
    /// </summary>
    public class StartGame_FromGameEngine
    {
        /// <summary>
        /// Holds the size of the board.
        /// </summary>
        public class BoardSizeType
        {
            /// <summary>
            /// Gets or sets the size of the board in the x-axis.
            /// </summary>
            public int X { get; set; } = 100;

            /// <summary>
            /// Gets or sets the size of the board in the y-axis.
            /// </summary>
            public int Y { get; set; } = 100;
        }

        /// <summary>
        /// The name and size of a ship to be placed on the board.
        /// </summary>
        public class ShipInfo
        {
            /// <summary>
            /// Gets or sets the ship type, eg CARRIER, BATTLESHIP etc.
            /// </summary>
            public Shared.ShipTypeEnum ShipType { get; set; }

            /// <summary>
            /// Gets or sets the index for this ship. This can be used when identifying the ship for game actions.
            /// </summary>
            public int Index { get; set; } = 0;

            /// <summary>
            /// Gets or sets the size of the ship - the number of squares it occupies.
            /// </summary>
            public int Size { get; set; } = 0;
        }

        /// <summary>
        /// The cost to fire a shot.
        /// </summary>
        public class ShotCost
        {
            /// <summary>
            /// Gets or sets the shot type, eg SHELL, MINE etc.
            /// </summary>
            public Shared.ShotTypeEnum ShotType { get; set; }

            /// <summary>
            /// Gets or sets the cost of firing the shot.
            /// </summary>
            public int Cost { get; set; } = 1;
        }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; } = "START_GAME";

        /// <summary>
        /// Gets or sets the size of the board.
        /// </summary>
        public BoardSizeType BoardSize { get; set; } = new BoardSizeType();

        /// <summary>
        /// Gets or sets the list of ships which the AI should place on the board.
        /// </summary>
        public List<ShipInfo> ShipInfos { get; set; } = new List<ShipInfo>();

        /// <summary>
        /// Gets or sets the list of shot-costs, ie the cost to fire the various different types of shot.
        /// </summary>
        public List<ShotCost> ShotCosts { get; set; } = new List<ShotCost>();

        /// <summary>
        /// Gets or sets the amount of fuel available at the start of the game.
        /// </summary>
        public int Fuel { get; set; } = 100;
    }
}