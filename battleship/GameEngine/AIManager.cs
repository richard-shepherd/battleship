namespace GameEngine
{
    /// <summary>
    /// Discovers the available AIs from the AIs folder and helps create them by name.
    /// </summary>
    public class AIManager
    {
        #region Properties

        /// <summary>
        /// Gets the list of AI names, ordered alphabetically.
        /// </summary>
        public IList<string> AINames => m_aiFolders.Keys.OrderBy(x => x).ToList();

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public AIManager(string folder)
        {
            // We find AIs folders. These are folders containing an AI.info file...
            foreach(var aiFolder in Directory.EnumerateDirectories(folder))
            {
                var aiName = Path.GetFileName(aiFolder);
                if(File.Exists(Path.Combine(folder, aiName, "AI.info")))
                {
                    m_aiFolders[aiName] = aiFolder;
                }
            }
        }

        /// <summary>
        /// Creates an AI process from an AI name.
        /// </summary>
        public AIProcess createAIProcess(string aiName)
        {
            if(m_aiFolders.TryGetValue(aiName, out var aiFolder))
            {
                return new AIProcess(aiName, aiFolder);
            }
            throw new Exception($"AI {aiName} could not be found");
        }

        #endregion

        #region Private data

        // Folders containing AIs, keyed by AI name...
        private readonly Dictionary<string, string> m_aiFolders = new Dictionary<string, string>();

        #endregion
    }
}
