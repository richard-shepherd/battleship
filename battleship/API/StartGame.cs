namespace API
{
    /// <summary>
    /// The game-engine message and AI response for the START_GAME event.
    /// </summary>
    public class StartGame
    {
        #region Event name

        // The event name sent with messages to and from the AIs...
        public static string EventName = "START_GAME";

        #endregion

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
            /// Constructor.
            /// </summary>
            public Message()
            {
                EventName = StartGame.EventName;
            }

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
            /// Gets or sets the name of the opponent AI.
            /// </summary>
            public string OpponentAIName { get; set; } = "";

            /// <summary>
            /// Gets or sets the size of the board.
            /// </summary>
            public BoardSizeType BoardSize { get; set; } = new BoardSizeType();

            /// <summary>
            /// Gets or sets the number of board squares that the AI should fill with ships.
            /// </summary>
            public int ShipSquares { get; set; } = 20;
        }

        #endregion

        #region AI response

        /// <summary>
        /// Response from AIs to the START_GAME notification.
        /// </summary><remarks>
        /// The AI tells the game where where to place ships for the start of the game.
        /// </remarks>
        public class AIResponse : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public AIResponse()
            {
                EventName = StartGame.EventName;
            }

            /// <summary>
            /// Gets or sets the list of ship placements requested by the AI for the start of the game.
            /// </summary>
            public List<Shared.ShipPlacement> ShipPlacements { get; set; } = new List<Shared.ShipPlacement>();
        }

        #endregion
    }
}
