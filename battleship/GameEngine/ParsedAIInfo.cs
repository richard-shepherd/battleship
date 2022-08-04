namespace GameEngine
{
    /// <summary>
    /// Parsed version of JSON AI.info files.
    /// </summary>
    internal class ParsedAIInfo
    {
        /// <summary>
        /// Gets or sets the executable to run for the AI.
        /// </summary>
        public string Executable { get; set; } = "";

        /// <summary>
        /// Gets or sets the command-line to use when running the AI.
        /// </summary>
        public string CommandLine { get; set; } = "";
    }
}
