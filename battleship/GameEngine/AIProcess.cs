using API;
using System.Diagnostics;
using Utility;

namespace GameEngine
{
    /// <summary>
    /// Creates a process for an AI and manages communication with it.
    /// </summary><remarks>
    /// The AI is created by specifying its folder. This should contain a text file
    /// called AI.info which specifies how to run the process for the AI.
    /// </remarks>
    public class AIProcess : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets the AI's name.
        /// </summary>
        public string Name => m_name;

        /// <summary>
        /// Gets whether we have an output / response message from the AI.
        /// </summary>
        public bool HasOutput => (m_aiOutput != null);

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public AIProcess(string name, string folder)
        {
            m_name = name;
            m_folder = folder;

            // We load and parse the AI.info file...
            var strAIInfo = File.ReadAllText(Path.Combine(folder, "AI.info"));
            var aiInfo = Utils.fromJSON<ParsedAIInfo>(strAIInfo);

            // We create the AI process, including capturing its stdout...
            var startInfo = m_process.StartInfo;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.WorkingDirectory = Path.Combine(folder, aiInfo.WorkingDirectory);
            startInfo.FileName = Path.Combine(folder, aiInfo.WorkingDirectory, aiInfo.Executable);
            startInfo.Arguments = aiInfo.CommandLine;
            startInfo.CreateNoWindow = true;
            m_process.Start();
            m_process.OutputDataReceived += onOutputDataReceived; ;
            m_process.BeginOutputReadLine();
        }

        /// <summary>
        /// Converts the message to JSON and sends it to the AI.
        /// </summary>
        public void sendMessage(object message)
        {
            var json = Utils.toJSON(message);
            m_process.StandardInput.WriteLine(json);
        }

        /// <summary>
        /// Returns the AI output parsed as T.
        /// NOTE: Once read, this clears the output. So it can only be read once.
        /// </summary>
        public T getOutputAs<T>()
        {
            var output = m_aiOutput;
            m_aiOutput = null;
            return Utils.fromJSON<T>(output);
        }

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (IsDisposed) return;

            // We ask the AI to shut itself down...
            sendMessage(new Shutdown.Message());

            // We wait for the process to shut down...
            var hasExited = Utils.wait(() => m_process.HasExited, 5000);
            if (!hasExited)
            {
                // The process has not exited by the timeout, so we kill it...
                Logger.log($"AI {m_folder} has not shut down. Killing it.");
                m_process.Kill();
            }

            // We clean up the process...
            m_process.OutputDataReceived -= onOutputDataReceived;
            m_process.Dispose();

            IsDisposed = true;
        }

        protected bool IsDisposed { get; private set; }

        #endregion

        #region Private functions

        /// <summary>
        /// Called (on its own thread) when we receive a message from the AI via its stdout.
        /// </summary>
        private void onOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                m_aiOutput = e.Data;
            }
            catch(Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly string m_name;
        private readonly string m_folder;

        // The process we spawn to run the AI...
        private readonly Process m_process = new Process();

        // The most recent message received from the AI...
        private volatile string m_aiOutput = null;

        #endregion
    }
}
