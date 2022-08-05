namespace API
{
    /// <summary>
    /// The game-engine message and AI response for the START_GAME event.
    /// </summary>
    public class StartGame
    {
        #region Game engine message

        /// <summary>
        /// Sent from the game engine to each AI at the start of the game.
        /// </summary><remarks>
        /// Tells AIs about the size of the board and requests that they place ships
        /// on the board for the start of the game.
        /// </remarks>
        public class Message : MessageBase
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
                public int ShipIndex { get; set; } = 0;

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
            /// Constructor.
            /// </summary>
            public Message()
            {
                EventName = "START_GAME";
            }

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

        #endregion

        #region AI response

        /// <summary>
        /// Response from AIs to the START_GAME notification.
        /// </summary><remarks>
        /// The AI tells the game where where to place ships for the start
        /// of the game.
        /// </remarks>
        public class AIResponse : MessageBase
        {
            /// <summary>
            /// Initial game placement for one ship.
            /// </summary>
            public class ShipPlacement
            {
                /// <summary>
                /// Gets or sets the index of the ship to place.
                /// </summary>
                public int ShipIndex { get; set; } = 0;

                /// <summary>
                /// Gets or sets the 1-based coordinates of the top-left square where the ship should be placed.
                /// </summary>
                public Shared.BoardSquareCoordinates TopLeft { get; set; } = new Shared.BoardSquareCoordinates();

                /// <summary>
                /// Gets or sets the orientation of the ship, ie VERTICAL or HORIZONTAL
                /// </summary>
                public Shared.OrientationEnum Orientation { get; set; } = Shared.OrientationEnum.HORIZONTAL;
            }

            /// <summary>
            /// Constructor.
            /// </summary>
            public AIResponse()
            {
                EventName = "START_GAME";
            }

            /// <summary>
            /// Gets or sets the list of ship placements requested by the AI for the start of the game.
            /// </summary>
            public List<ShipPlacement> ShipPlacements { get; set; } = new List<ShipPlacement>();
        }

        #endregion
    }
}
