namespace API
{
    /// <summary>
    /// Ack message sent by AIs to game messages which do not need a more detailed response.
    /// </summary>
    public class Ack_AIResponse
    {
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; } = "ACK_AI_RESPONSE";
    }
}
