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
                new RandomPlayer().run();
            }
            catch (Exception ex)
            {
                Logger.log(ex);
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Runs the main loop of the AI.
        /// </summary>
        private void run()
        {
            // We run the game loop, waiting for messages for the game engine and processing them...
            while(!m_shutdown)
            {
                var message = Console.ReadLine();
                var messageBase = Utils.fromJSON<API.MessageBase>(message);
                switch(messageBase.EventName)
                {
                    case "START_GAME":
                        onStartGame(message);
                        break;

                    case "SHUTDOWN":
                        onShutdown();
                        break;

                    default:
                        throw new Exception($"Unhandled EventName={messageBase.EventName}");
                }
            }
        }

        /// <summary>
        /// Serializes the message object to JSON and sends it to the game engine.
        /// </summary>
        private void sendMessage(object message)
        {
            var json = Utils.toJSON(message);
            Console.WriteLine(json);
        }

        /// <summary>
        /// Called when we receive the START_GAME message.
        /// </summary>
        private void onStartGame(string message)
        {
            // We deserialize the message and note the game properties...
            m_gameInfo = Utils.fromJSON<API.StartGame.Message>(message);

            // In response to the START_GAME message, we need to place our ships on the board...
            var response = new API.StartGame.AIResponse();
            foreach(var shipInfo in m_gameInfo.ShipInfos)
            {
                // We create a ship to place on the board and set its index...
                var shipPlacement = new API.StartGame.AIResponse.ShipPlacement();
                shipPlacement.ShipIndex = shipInfo.ShipIndex;

                // We decide its orientation...
                shipPlacement.Orientation = (m_rnd.NextDouble() > 0.5) ? API.Shared.OrientationEnum.HORIZONTAL : API.Shared.OrientationEnum.VERTICAL;

                // We assign its (1-based) top-left point...
                switch(shipPlacement.Orientation)
                {
                    case API.Shared.OrientationEnum.HORIZONTAL:
                        shipPlacement.TopLeft.X = m_rnd.Next(1, m_gameInfo.BoardSize.X - shipInfo.Size + 1);
                        shipPlacement.TopLeft.Y = m_rnd.Next(1, m_gameInfo.BoardSize.Y);
                        break;

                    case API.Shared.OrientationEnum.VERTICAL:
                        shipPlacement.TopLeft.X = m_rnd.Next(1, m_gameInfo.BoardSize.X);
                        shipPlacement.TopLeft.Y = m_rnd.Next(1, m_gameInfo.BoardSize.Y - shipInfo.Size + 1);
                        break;
                }

                // We add the ship to the response...
                response.ShipPlacements.Add(shipPlacement);
            }

            // We send the response...
            sendMessage(response);
        }

        /// <summary>
        /// Called when we receive the SHUTDOWN message.
        /// </summary>
        private void onShutdown()
        {
            // We send an ack, and note that we need to shut down...
            sendMessage(new API.Shutdown.AIResponse());
            m_shutdown = true;
        }

        #endregion

        #region Private data

        // Generates random numbers...
        private readonly Random m_rnd = new Random();

        // Set the true when we receive the SHUTDOWN message from the game engine.
        // This will make us break out of the main message loop.
        private bool m_shutdown = false;

        // The info received from the game engine at the start of the game. This includes the
        // size of the board and other info...
        private API.StartGame.Message m_gameInfo = null;

        #endregion
    }
}