namespace API
{
    /// <summary>
    /// Base class for messages - both from the game engine and from AI responses.
    /// </summary><remarks>
    /// In particular, the base message contains the EventName, to allow this to be
    /// parsed without decoding the whole more strongly-typed message.
    /// </remarks>
    public class MessageBase
    {
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; } = "";
    }
}
