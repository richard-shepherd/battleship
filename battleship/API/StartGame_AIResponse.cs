namespace API
{
    /// <summary>
    /// Response from AIs to the START_GAME notification.
    /// </summary><remarks>
    /// The AI tells the game where where to place ships for the start
    /// of the game.
    /// </remarks>
    public class StartGame_AIResponse
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
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; } = "START_GAME_AI_RESPONSE";

        /// <summary>
        /// Gets or sets the list of ship placements requested by the AI for the start of the game.
        /// </summary>
        public List<ShipPlacement> ShipPlacements { get; set; } = new List<ShipPlacement>();
    }
}
