namespace GameEngine
{
    /// <summary>
    /// Manages one game player.
    /// </summary><remarks>
    /// Manages the AI for the player as well as:
    /// - The board with the player's ships (plus enemy mines and drones)
    /// - The shots available to the player.
    /// </remarks>
    internal class Player
    {
        #region Properties

        /// <summary>
        /// Gets the AI process.
        /// </summary>
        public AIProcess AI => m_ai;

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor.
        /// </summary>
        public Player(AIManager aiManager, string aiName)
        {
            m_aiManager = aiManager;
            m_aiName = aiName;

            // We create the AI...
            m_ai = m_aiManager.createAIProcess(m_aiName);
        }

        /// <summary>
        /// Returns the ship-part at the 1-based (x, y) coordinates on the board.
        /// Returns null if there is no ship-part at the location.
        /// </summary>
        public ShipPart getShipPart(int x, int y)
        {
            return m_board.getShipPart(x, y);
        }

        /// <summary>
        /// Sends the START_GAME message to the AI, requesting initial ship placements.
        /// </summary>
        public void startGame_SendMessage(int boardSize, int shipSquares, string opponentName)
        {
            var message = new API.StartGame.Message();
            message.BoardSize.X = boardSize;
            message.BoardSize.Y = boardSize;
            message.ShipSquares = shipSquares;
            message.OpponentAIName = opponentName;
            m_ai.sendMessage(message);
        }

        /// <summary>
        /// Processes the response from the AI to the START_GAME message, including setting up the
        /// board with the initial ship placements.
        /// </summary>
        public void startGame_ProcessResponse()
        {
            var aiResponse = m_ai.getOutputAs<API.StartGame.AIResponse>();
            m_board = new Board(aiResponse.ShipPlacements);
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

            // We process each shot and fill in the damage-report...
            var damageReport = new API.StatusUpdate.Message.DamageReport();
            foreach(var shot in aiResponse.Shots)
            {
                switch(shot.ShotType)
                {
                    case API.Shared.ShotTypeEnum.SHELL:
                        processShot_Shell(damageReport, opponent, shot);
                        break;

                    default:
                        throw new Exception($"Unhandled shot-type: {shot.ShotType}");
                }
            }

            return damageReport;
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
            // We confirm that we have enough shells to fire this shot. If not, we ignore the shot...
            if(m_shellsAvailable < 1.0)
            {
                return;
            }

            // We remove the shell from our inventory...
            m_shellsAvailable -= 1.0;

            // We add the shot to the damage report...
            var shotInfo = new API.StatusUpdate.Message.ShotInfo();
            shotInfo.TargetSquare = shot.TargetSquare;
            damageReport.ShotInfos.Add(shotInfo);

            // We check if the shell hit an opponent ship...
            var shipPart = opponent.getShipPart(shot.TargetSquare.X, shot.TargetSquare.Y);
            if(shipPart == null)
            {
                // There was no ship at the target square...
                shotInfo.ShotStatus = API.StatusUpdate.Message.ShotStatusEnum.MISS;
                return;
            }

            // There is a ship-part at the target square. We check if we have hit an already damaged part...
            if(shipPart.IsDamaged)
            {
                // Hitting an already damaged part counts as a miss...
                shotInfo.ShotStatus = API.StatusUpdate.Message.ShotStatusEnum.MISS;
                return;
            }

            // We have hit a ship part...
            shotInfo.ShotStatus = API.StatusUpdate.Message.ShotStatusEnum.HIT;

            // We check if the whole ship has been destroyed...
            var ship = shipPart.Ship;
            if (ship.IsDestroyed)
            {
                damageReport.DestroyedShips.Add(ship.ShipType);
            }
        }

        #endregion

        #region Private data

        // Construction params...
        private readonly AIManager m_aiManager;
        private readonly string m_aiName;

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
