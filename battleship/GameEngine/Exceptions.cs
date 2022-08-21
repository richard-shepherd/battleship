namespace GameEngine
{
    /// <summary>
    /// Exception thrown when ship placement validation fails.
    /// </summary>
    internal class ShipPlacementValidationException : Exception
    {
        public ShipPlacementValidationException(string message) : base(message)
        {
        }
    }
}
