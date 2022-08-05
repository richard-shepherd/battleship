namespace API
{
    /// <summary>
    /// The game-engine message and AI response for the SHUTDOWN event.
    /// </summary>
    public class Shutdown
    {
        #region Game engine message

        /// <summary>
        /// Sent from the game engine to each AI, telling then to shut down.
        /// </summary>
        public class Message : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public Message() 
            {
                EventName = "SHUTDOWN";
            }
        }

        #endregion

        #region AI response

        /// <summary>
        /// Response from AIs to the SHUTDOWN event.
        /// </summary><remarks>
        /// The AI should ack with this message when it has processed the SHUTDOWN event.
        /// </remarks>
        public class AIResponse : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public AIResponse()
            {
                EventName = "SHUTDOWN";
            }
        }

        #endregion
    }
}
