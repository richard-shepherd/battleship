namespace API
{
    /// <summary>
    /// The game-engine message and AI response for the MOVE event.
    /// </summary>
    public class Move
    {
        #region Event name

        // The event name sent with messages to and from the AIs...
        public static string EventName = "MOVE";

        #endregion

        #region Game engine message

        /// <summary>
        /// Sent from the game engine to each AI at the movement part of a turn.
        /// </summary>
        public class Message : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public Message()
            {
                EventName = Move.EventName;
            }

            /// <summary>
            /// Info about one ship and the fuel it has for movement.
            /// </summary>
            public class ShipInfo
            {
                /// <summary>
                /// Gets or sets the ship's index. 
                /// </summary>
                public int Index { get; set; } = 0;

                /// <summary>
                /// Gets or sets the placement of the ship before movement.
                /// </summary>
                public Shared.ShipPlacement ShipPlacement {get; set;} = new Shared.ShipPlacement();

                /// <summary>
                /// Gets or sets the fuel available for the ship.
                /// </summary>
                public int Fuel { get; set; } = 0;
            }

            /// <summary>
            /// Gets or sets the positions of the ships and the amount of fuel available before movement.
            /// </summary>
            public List<ShipInfo> ShipInfos { get; set; } = new List<ShipInfo>();
        }

        #endregion

        #region AI response

        /// <summary>
        /// Response from AIs to the MOVE event, giving details of any requested ship movements.
        /// </summary>
        public class AIResponse : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public AIResponse()
            {
                EventName = Move.EventName;
            }

            /// <summary>
            /// Request to move a ship.
            /// </summary><remarks>
            /// NOTE 1: The index specified must match the index specified in the game-engine message.
            /// NOTE 2: The ShipType in this object will be ignored (as you cannot change the type of ship when moving it).
            /// </remarks>
            public class MovementRequest
            {
                /// <summary>
                /// Gets or sets the ship's index. 
                /// </summary>
                public int Index { get; set; } = 0;

                /// <summary>
                /// Gets or sets the requested position of the ship.
                /// </summary>
                public Shared.ShipPlacement ShipPlacement { get; set; } = new Shared.ShipPlacement();
            }

            /// <summary>
            /// Gets or sets the collection of movement requests.
            /// </summary>
            public List<MovementRequest> MovementRequests { get; set; } = new List<MovementRequest>();
        }

        #endregion
    }
}
