using GameEngine;
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
                // We st up the logger...
                setupLogger();

                // Creates and runs the AI. The run method runs a loop for one game until the game is over...
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
        /// Redirects logged messages to a file.
        /// </summary>
        private static void setupLogger()
        {
            try
            {
                // We create the log folder...
                Directory.CreateDirectory("Logs");

                // We delete any old files...
                var cutoff = DateTime.Now - TimeSpan.FromMinutes(1);
                var logFiles = new DirectoryInfo("Logs").EnumerateFiles("*.log").ToList();
                foreach (var logFile in logFiles)
                {
                    if (logFile.LastWriteTime < cutoff)
                    {
                        logFile.Delete();
                    }
                }
            }
            catch (Exception)
            {
                // We do not do anything here. If we are running two versions of this AI,
                // they can conflict trying to set up the log folder. We ignore these conflicts.
            }

            // We log messages...
            var logFilePath = Path.Combine("Logs", $"RandomPlayer_{Environment.ProcessId}.log");
            Logger.onMessageLogged += (sender, logArgs) =>
            {
                try
                {
                    File.AppendAllText(logFilePath, $"{DateTime.Now:HH:mm:ss.fff}: {logArgs.Message}{Environment.NewLine}{Environment.NewLine}");
                }
                catch (Exception)
                {
                    // We ignore exceptions generated while logging. It may not be a good idea to try to log them in this case!
                }
            };
        }

        /// <summary>
        /// Runs the main loop of the AI.
        /// </summary>
        private void run()
        {
            // We run the game loop, waiting for messages for the game engine and processing them...
            while(!m_shutdown)
            {
                // We read the next game message from STDIN and log it...
                var message = Console.ReadLine();
                Logger.log($"RX->{message}");

                // We decode the message header to get the EventName and call an event-specific function
                // to decode and process the full message...
                var messageBase = Utils.fromJSON<API.MessageBase>(message);
                switch(messageBase.EventName)
                {
                    case "START_GAME":
                        onStartGame(message);
                        break;

                    case "FIRE_WEAPONS":
                        onFireWeapons(message);
                        break;

                    case "STATUS_UPDATE":
                        onStatusUpdate(message);
                        break;

                    case "MOVE":
                        onMove(message);
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
            Logger.log($"TX<-{json}");
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

            // We create a random layout of ships, and check that it is valid.
            // This AI only uses battleships and minelayers, as it does not process
            // the reports from drones...
            var shipPlacements = new List<API.Shared.ShipPlacement>();
            for(; ; )
            {
                // We create a random collection of battleships and minelayers...
                shipPlacements.Clear();
                var squaresRemaining = m_gameInfo.ShipSquares;
                var battleshipSize = API.Shared.ShipSizes[API.Shared.ShipTypeEnum.BATTLESHIP];
                var minelayerSize = API.Shared.ShipSizes[API.Shared.ShipTypeEnum.MINELAYER];
                while (squaresRemaining >= battleshipSize)
                {
                    if (squaresRemaining >= battleshipSize)
                    {
                        shipPlacements.Add(createShip(API.Shared.ShipTypeEnum.BATTLESHIP));
                        squaresRemaining -= battleshipSize;
                    }
                    if (squaresRemaining >= minelayerSize)
                    {
                        shipPlacements.Add(createShip(API.Shared.ShipTypeEnum.MINELAYER));
                        squaresRemaining -= minelayerSize;
                    }
                }

                // We check that the placement is valid...
                var shipPlacementInfo = BoardUtils.validateShipPlacement(m_gameInfo.BoardSize.X, m_gameInfo.ShipSquares, shipPlacements);
                if (shipPlacementInfo.Valid)
                {
                    // The ship placement is valid...
                    break;
                }
            }

            // We send the response...
            response.ShipPlacements = shipPlacements;
            sendMessage(response);
        }

        /// <summary>
        /// Called when we receive the FIRE_WEAPONS message.
        /// </summary>
        private void onFireWeapons(string message)
        {
            // We deserialize the message from the game. This tells us how many shots
            // of each type are available...
            var weaponsInfo = Utils.fromJSON<API.FireWeapons.Message>(message);

            // We fire each weapon into a random square on the board...
            var response = new API.FireWeapons.AIResponse();
            response.Shots.AddRange(createRandomShots(API.Shared.ShotTypeEnum.SHELL, weaponsInfo.AvailableShells));
            response.Shots.AddRange(createRandomShots(API.Shared.ShotTypeEnum.MINE, weaponsInfo.AvailableMines));
            response.Shots.AddRange(createRandomShots(API.Shared.ShotTypeEnum.DRONE, weaponsInfo.AvailableDrones));

            // We send the response...
            sendMessage(response);
        }

        /// <summary>
        /// Called when we receive the STATUS_UPDATE message.
        /// </summary>
        private void onStatusUpdate(string message)
        {
            // More sophisticated AIs might do something here with the status update.
            // For example, noting if any of their shots have hit the opponent and then
            // targetting the area around the hit. As this AI plays randomly it does not
            // do anything with this update.

            // We ACK the message...
            sendMessage(new API.StatusUpdate.AIResponse());
        }

        /// <summary>
        /// Creates a shot of the type specified, targetting a random square on the board.
        /// </summary>
        private List<API.FireWeapons.AIResponse.Shot> createRandomShots(API.Shared.ShotTypeEnum shotType, int numShots)
        {
            var shots = new List<API.FireWeapons.AIResponse.Shot>();
            for(var i=0; i<numShots; ++i)
            {
                var shot = new API.FireWeapons.AIResponse.Shot();
                shot.TargetSquare.X = m_rnd.Next(1, m_gameInfo.BoardSize.X + 1);
                shot.TargetSquare.Y = m_rnd.Next(1, m_gameInfo.BoardSize.Y + 1);
                shot.ShotType = shotType;
                shots.Add(shot);
            }
            return shots;
        }

        /// <summary>
        /// Creates a ship to be placed on the board at the start of the game.
        /// </summary>
        private API.Shared.ShipPlacement createShip(API.Shared.ShipTypeEnum shipType)
        {
            var shipPlacement = new API.Shared.ShipPlacement();
            shipPlacement.ShipType = shipType;
            shipPlacement.Orientation = (m_rnd.NextDouble() > 0.5) ? API.Shared.OrientationEnum.HORIZONTAL : API.Shared.OrientationEnum.VERTICAL;

            // We assign its (1-based) top-left point...
            var shipSize = API.Shared.ShipSizes[shipPlacement.ShipType];
            switch (shipPlacement.Orientation)
            {
                case API.Shared.OrientationEnum.HORIZONTAL:
                    shipPlacement.TopLeft.X = m_rnd.Next(1, m_gameInfo.BoardSize.X - shipSize + 1);
                    shipPlacement.TopLeft.Y = m_rnd.Next(1, m_gameInfo.BoardSize.Y);
                    break;

                case API.Shared.OrientationEnum.VERTICAL:
                    shipPlacement.TopLeft.X = m_rnd.Next(1, m_gameInfo.BoardSize.X);
                    shipPlacement.TopLeft.Y = m_rnd.Next(1, m_gameInfo.BoardSize.Y - shipSize + 1);
                    break;
            }

            return shipPlacement;
        }

        /// <summary>
        /// Called when we receive the MOVE message.
        /// </summary>
        private void onMove(string message)
        {
            // We deserialize the message from the game-engine. This tells us the current 
            // position of our ships and the fuel available for movement...
            var gameMessage = Utils.fromJSON<API.Move.Message>(message);

            // We move one of the ships...
            var response = new API.Move.AIResponse();

            // We choose a ship to move (though not every turn)...
            if(m_rnd.NextDouble() < 0.1)
            {
                var numShips = gameMessage.ShipInfos.Count;
                var shipIndex = m_rnd.Next(0, numShips);
                var shipInfo = gameMessage.ShipInfos[shipIndex];

                // We move the ship if it has enough fuel...
                if (shipInfo.Fuel >= 5)
                {
                    var movementRequest = new API.Move.AIResponse.MovementRequest();
                    movementRequest.Index = shipIndex;
                    movementRequest.ShipPlacement = moveShip(shipInfo.ShipPlacement);
                    response.MovementRequests.Add(movementRequest);
                }
            }

            sendMessage(response);
        }

        /// <summary>
        /// Returns a new location for a ship, moving it randomly.
        /// </summary>
        private API.Shared.ShipPlacement moveShip(API.Shared.ShipPlacement shipPlacement)
        {
            var newShipPlacement = Utils.clone(shipPlacement);
            var direction = m_rnd.Next(0, 4);
            switch(direction)
            {
                case 0:
                    // We move up...
                    newShipPlacement.TopLeft.Y -= 1;
                    if (newShipPlacement.TopLeft.Y < 1) newShipPlacement.TopLeft.Y = 1;
                    break;

                case 1:
                    // We move down...
                    var boardLimitY = m_gameInfo.BoardSize.Y - 5;
                    newShipPlacement.TopLeft.Y += 1;
                    if (newShipPlacement.TopLeft.Y > boardLimitY) newShipPlacement.TopLeft.Y = boardLimitY;
                    break;

                case 2:
                    // We move left...
                    newShipPlacement.TopLeft.X -= 1;
                    if (newShipPlacement.TopLeft.X < 1) newShipPlacement.TopLeft.X = 1;
                    break;

                case 3:
                    // We move right...
                    var boardLimitX = m_gameInfo.BoardSize.X - 5;
                    newShipPlacement.TopLeft.X += 1;
                    if (newShipPlacement.TopLeft.X > boardLimitX) newShipPlacement.TopLeft.X = boardLimitX;
                    break;

            }
            return newShipPlacement;
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