namespace API
{
    /// <summary>
    /// The game-engine message and AI response for the FIRE_WEAPONS event.
    /// </summary>
    internal class FireWeapons
    {
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
                EventName = "FIRE_WEAPONS";
            }


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
                EventName = "FIRE_WEAPONS";
            }
        }

        #endregion
    }
}
