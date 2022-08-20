namespace API
{
    /// <summary>
    /// The game-engine message and AI response for the FIRE_WEAPONS event.
    /// </summary>
    public class FireWeapons
    {
        #region Event name

        // The event name sent with messages to and from the AIs...
        public static string EventName = "FIRE_WEAPONS";

        #endregion

        #region Game engine message

        /// <summary>
        /// Sent from the game engine to each AI requesting the AI to fire its weapons.
        /// </summary>
        public class Message : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public Message()
            {
                EventName = FireWeapons.EventName;
            }

            /// <summary>
            /// Gets or sets the number of shells which can be fired this turn.
            /// </summary>
            public int AvailableShells { get; set; } = 0;

            /// <summary>
            /// Gets or sets the number of mines which can be fired this turn.
            /// </summary>
            public int AvailableMines { get; set; } = 0;

            /// <summary>
            /// Gets or sets the number of drones which can be fired this turn.
            /// </summary>
            public int AvailableDrones { get; set; } = 0;
        }

        #endregion

        #region AI response

        /// <summary>
        /// Response from AIs indicating which weapons it wishes to fire.
        /// </summary>
        public class AIResponse : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public AIResponse()
            {
                EventName = FireWeapons.EventName;
            }

            /// <summary>
            /// Defines one shot taken by the AI.
            /// </summary>
            public class Shot
            {
                /// <summary>
                /// Gets or sets the shot type, eg SHELL, MINE, DRONE.
                /// </summary>
                public Shared.ShotTypeEnum ShotType { get; set; }

                /// <summary>
                /// Gets or sets the 1-based square in the board being targetted.
                /// </summary>
                public Shared.BoardSquareCoordinates TargetSquare { get; set; } = new Shared.BoardSquareCoordinates();
            }

            /// <summary>
            /// Gets or sets the list of shots fired by the AI.
            /// </summary>
            public List<Shot> Shots { get; set; } = new List<Shot>();
        }

        #endregion
    }
}
