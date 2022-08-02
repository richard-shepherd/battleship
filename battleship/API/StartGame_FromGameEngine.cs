namespace API
{
    /// <summary>
    /// Code representation of the JSON message sent from the game engine
    /// to each AI at the start of a new game.
    /// </summary>
    public class StartGame_FromGameEngine
    {
        /// <summary>
        /// Holds the size of the board/
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
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; } = "START_GAME";

        /// <summary>
        /// Gets or sets the size of the board.
        /// </summary>
        public BoardSizeType BoardSize { get; set; } = new BoardSizeType();




    }
}