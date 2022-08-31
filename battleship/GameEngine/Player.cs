using Utility;

namespace GameEngine
{
    /// <summary>
    /// Manages one game player.
    /// </summary><remarks>
    /// Manages the AI for the player as well as:
    /// - The board with the player's ships (plus enemy mines and drones)
    /// - The shots available to the player.
    /// </remarks>
    public class Player
    {
        #region Properties

        /// <summary>
        /// Gets the AI process.
        /// </summary>
        public AIProcess AI => m_ai;

        /// <summary>
        /// Gets the board for the player.
        /// </summary>
        public Board Board => m_board;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Player(AIManager aiManager, int boardSize, int shipSquares, string aiName, string opponentAIName)
        {
            m_aiManager = aiManager;
            m_boardSize = boardSize;
            m_shipSquares = shipSquares;
            m_aiName = aiName;
            m_opponentAIName = opponentAIName;

            // We create the AI...
            m_ai = m_aiManager.createAIProcess(m_aiName);
        }

        /// <summary>
        /// Sends the START_GAME message to the AI, requesting initial ship placements.
        /// </summary>
        public void startGame_SendMessage()
        {
            var message = new API.StartGame.Message();
            message.BoardSize.X = m_boardSize;
            message.BoardSize.Y = m_boardSize;
            message.ShipSquares = m_shipSquares;
            message.OpponentAIName = m_opponentAIName;
            m_ai.sendMessage(message);
        }

        /// <summary>
        /// Processes the response from the AI to the START_GAME message, including setting up the
        /// board with the initial ship placements.
        /// </summary>
        public void startGame_ProcessResponse()
        {
            // We parse the response...
            var aiResponse = m_ai.getOutputAs<API.StartGame.AIResponse>();

            // We validate the ship placements...
            Logger.log($"Validating ship placement for {m_aiName}");
            var shipPlacementInfo = BoardUtils.validateShipPlacement(m_boardSize, m_shipSquares, aiResponse.ShipPlacements);
            if(!shipPlacementInfo.Valid)
            {
                throw new ShipPlacementValidationException($"{m_aiName}: invalid ship placement: {shipPlacementInfo.Message}");
            }

            // The ship placements are valid, so we set up the board...
            m_board = new Board(m_boardSize, aiResponse.ShipPlacements);
        }

        /// <summary>
        /// Sends the FIRE_WEAPONS message to the AI.
        /// </summary>
        public void fireWeapons_SendMessage()
        {
            // We update the number of shots of each type which are available - including
            // any accumulated unused shots from previous turns...
            m_shellsAvailable += additionShotsThisTurn(API.Shared.ShotTypeEnum.SHELL);
            m_minesAvailable += additionShotsThisTurn(API.Shared.ShotTypeEnum.MINE);
            m_dronesAvailable += additionShotsThisTurn(API.Shared.ShotTypeEnum.DRONE);

            // We let the AI know how many shots of each type are available and request it to fire its weapons...
            var message = new API.FireWeapons.Message();
            message.AvailableShells = (int)m_shellsAvailable;
            message.AvailableMines = (int)m_minesAvailable;
            message.AvailableDrones = (int)m_dronesAvailable;
            m_ai.sendMessage(message);
        }

        /// <summary>
        /// Processes the reponse from the AI to the FIRE_WEAPONS message.
        /// </summary><remarks>
        /// The DamageReport returned includes:
        /// - Shots fired by this player
        /// - Damage taken by the opponent
        /// </remarks>
        public API.StatusUpdate.Message.DamageReport fireWeapons_ProcessResponse(Player opponent)
        {
            // We parse the response from the AI...
            var aiResponse = m_ai.getOutputAs<API.FireWeapons.AIResponse>();

            // We clear the recent shells from the opponent's board.
            // (These are recorded so that we can draw them in the UI.)
            opponent.Board.ShelledSquares.Clear();

            // We process each shot and fill in the damage-report...
            var damageReport = new API.StatusUpdate.Message.DamageReport();
            foreach(var shot in aiResponse.Shots)
            {
                switch(shot.ShotType)
                {
                    case API.Shared.ShotTypeEnum.SHELL:
                        processShot_Shell(damageReport, opponent, shot);
                        break;

                    case API.Shared.ShotTypeEnum.MINE:
                        processShot_Mine(damageReport, opponent, shot);
                        break;

                    case API.Shared.ShotTypeEnum.DRONE:
                        processShot_Drone(damageReport, opponent, shot);
                        break;

                    default:
                        throw new Exception($"Unhandled shot-type: {shot.ShotType}");
                }
            }

            // We note the number of opponent's ships and the number remaining...
            damageReport.TotalShips = opponent.Board.ShipCount;
            damageReport.ShipsRemaining = opponent.Board.ActiveShipCount;

            return damageReport;
        }

        /// <summary>
        /// Sends the MOVE request to the AI.
        /// </summary>
        public void moveShips_SendMessage()
        {
            var message = new API.Move.Message();

            // We note where each ship is located and how much fuel it has...
            var ships = m_board.Ships;
            for (var i = 0; i < ships.Count; ++i)
            {
                var ship = ships[i];
                var shipInfo = new API.Move.Message.ShipInfo();
                shipInfo.Index = i;
                shipInfo.ShipPlacement = ship.ShipPlacement;
                shipInfo.Fuel = ship.Fuel;
                message.ShipInfos.Add(shipInfo);
            }

            m_ai.sendMessage(message);
        }

        /// <summary>
        /// Processes the response from the AI to the MOVE message.
        /// 
        /// Returns a damage report indicating if any ships have been hit by mines as a result
        /// of the movement.
        /// 
        /// NOTE: The damage report holds damage to this player's ships cause by the opponent's mines.
        /// </summary>
        public API.StatusUpdate.Message.DamageReport moveShips_ProcessResponse()
        {
            // We deserialize the response...
            var aiResponse = m_ai.getOutputAs<API.Move.AIResponse>();

            // We check that the movement requests are valid - eg, they do not use
            // too much fuel, or move ships into invalid positions...
            var fuelRequiredPerShip = new Dictionary<int, int>();
            var movementRequestInfo = BoardUtils.validateMovementRequests(m_board, m_aiName, aiResponse.MovementRequests, fuelRequiredPerShip);
            if (movementRequestInfo.Valid)
            {
                // The movement request is valid, so we apply the changes...
                foreach (var movementRequest in aiResponse.MovementRequests)
                {
                    var fuelUsed = fuelRequiredPerShip[movementRequest.Index];
                    m_board.moveShip(movementRequest.Index, movementRequest.ShipPlacement, fuelUsed);
                }
            }
            else
            {
                // The movement request was not valid. We log the message...
                Logger.log(movementRequestInfo.Message);
            }

            // We produce a damage report, as ship movements may have caused the ships to hit mines...
            return m_board.checkMines();
        }

        #endregion

        #region IDisposable implementation

        public virtual void Dispose()
        {
            if (IsDisposed) return;

            // We dispose (shut down) the AI...
            m_ai.Dispose();

            IsDisposed = true;
        }

        protected bool IsDisposed { get; private set; }

        #endregion

        #region Private functions

        /// <summary>
        /// Returns the number of additional shots accumulated this turn for the shot-type specified.
        /// </summary>
        private double additionShotsThisTurn(API.Shared.ShotTypeEnum shotType)
        {
            // We find the number of undamaged parts capable of firing the requested shot-type...
            var numShipParts = m_board.UndamagedParts.Where(x => x.ShotType == shotType).Count();

            // We find the cost to fire this shot...
            var cost = API.Shared.ShotCosts[shotType];

            return numShipParts / cost;
        }

        /// <summary>
        /// Processes a SHELL shot fired by the player.
        /// </summary>
        private void processShot_Shell(API.StatusUpdate.Message.DamageReport damageReport, Player opponent, API.FireWeapons.AIResponse.Shot shot)
        {
            // We confirm that we have enough shells available. If not, we ignore it. If so we remove it from the inventory...
            if (m_shellsAvailable < 1.0)
            {
                return;
            }
            m_shellsAvailable -= 1.0;

            // We record the shell on the opponent's board (so that it can be shown in the UI)...
            opponent.Board.ShelledSquares.Add(shot.TargetSquare);

            // We add the shot to the damage report...
            var shotInfo = new API.StatusUpdate.Message.ShotInfo();
            shotInfo.TargetSquare = shot.TargetSquare;
            damageReport.ShotInfos.Add(shotInfo);

            // We check if the shell hit an opponent ship...
            var hitStatus = BoardUtils.checkForHit(opponent.Board, shot.TargetSquare);
            var shotStatus = hitStatus.ShotStatus;
            shotInfo.ShotStatus = shotStatus;

            // If we hit a ship, we check if the whole ship has been destroyed...
            if(shotStatus == API.StatusUpdate.Message.ShotStatusEnum.HIT)
            {
                var ship = hitStatus.ShipPart.Ship;
                if (ship.IsDestroyed)
                {
                    damageReport.DestroyedShips.Add(ship.ShipType);
                }
            }
        }

        /// <summary>
        /// Processes a MINE laid by the player.
        /// </summary>
        private void processShot_Mine(API.StatusUpdate.Message.DamageReport damageReport, Player opponent, API.FireWeapons.AIResponse.Shot shot)
        {
            // We confirm that we have enough mines available. If not, we ignore it. If so we remove it from the inventory...
            if (m_minesAvailable < 1.0)
            {
                return;
            }
            m_minesAvailable -= 1.0;

            // We check if the mine has hit a ship.
            var hitStatus = BoardUtils.checkForHit(opponent.Board, shot.TargetSquare);
            var shotStatus = hitStatus.ShotStatus;
            if(shotStatus == API.StatusUpdate.Message.ShotStatusEnum.HIT)
            {
                // The mine hit a ship, so we add this to the damage report...
                var shotInfo = new API.StatusUpdate.Message.ShotInfo();
                shotInfo.TargetSquare = shot.TargetSquare;
                shotInfo.ShotStatus = shotStatus;
                damageReport.ShotInfos.Add(shotInfo);
                var ship = hitStatus.ShipPart.Ship;
                if (ship.IsDestroyed)
                {
                    damageReport.DestroyedShips.Add(ship.ShipType);
                }
            }
            else
            {
                // The mine did not hit a ship, so we add it to the opponent's board to 'lurk'
                // in case a ship moves onto its square...
                opponent.Board.addMine(shot.TargetSquare);
            }
        }

        /// <summary>
        /// Processes a DRONE placed by the player.
        /// </summary>
        private void processShot_Drone(API.StatusUpdate.Message.DamageReport damageReport, Player opponent, API.FireWeapons.AIResponse.Shot shot)
        {
            // We confirm that we have enough drones available. If not, we ignore it. If so we remove it from the inventory...
            if (m_dronesAvailable < 1.0)
            {
                return;
            }
            m_dronesAvailable -= 1.0;

            // We place the drone on the opponent's board...
            opponent.Board.addDrone(shot.TargetSquare);
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly AIManager m_aiManager;
        private readonly int m_boardSize;
        private readonly int m_shipSquares;
        private readonly string m_aiName;
        private readonly string m_opponentAIName;

        // The AI process...
        private readonly AIProcess m_ai;

        // The board, holding the player's ships and enemy mines and drones...
        private Board m_board;

        // Shots available. These can accumulate between turns if not all shots are taken...
        private double m_shellsAvailable = 0.0;
        private double m_minesAvailable = 0.0;
        private double m_dronesAvailable = 0.0;

        #endregion
    }
}
