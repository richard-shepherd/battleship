using API;
using Utility;

namespace AI_Sample_RandomPlayer
{
    /// <summary>
    /// Battleship AI which plays more-or-less randomly.
    /// </summary>
    internal class RandomPlayer
    {
        #region Main

        /// <summary>
        /// Main.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                // We set up the logger...
                Logger.onMessageLogged += (sender, logArgs) =>
                {
                    Console.WriteLine(logArgs.Message);
                };

                // Creates and runs the AI. The run method runs a loop for one game
                // until the game is over...
                var ai = new RandomPlayer();
                ai.run();
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Constructor.
        /// </summary>
        private RandomPlayer()
        {
        }

        /// <summary>
        /// Runs the main loop of the AI.
        /// </summary>
        private void run()
        {
            // We wait for messages for the game engine, and process them...
            for(; ;)
            {
                var message = Console.ReadLine();
                var messageBase = Utils.fromJSON<MessageBase>(message);
            }
        }

        #endregion
    }
}