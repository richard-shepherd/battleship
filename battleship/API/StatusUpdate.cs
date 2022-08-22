namespace API
{
    /// <summary>
    /// Sent to AIs after the FIRE_WEAPONS and MOVE phases of the turn.
    /// 
    /// Provides info about shots made and any hits. (After the MOVE phase, there can 
    /// be hits from mines.) Provides an update on the status of each of the AI's ships
    /// and reports from any drones they have launched.
    /// </summary>
    public class StatusUpdate
    {
        #region Event name

        // The event name sent with messages to and from the AIs...
        public static string EventName = "STATUS_UPDATE";

        #endregion

        #region Game engine message

        /// <summary>
        /// Sent from the game engine to each AI, providing a status update after the FIRE_WEAPONS and MOVE phases.
        /// </summary>
        public class Message : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public Message()
            {
                EventName = StatusUpdate.EventName;
            }

            /// <summary>
            /// Enum for whether shots have hit or missed.
            /// </summary>
            public enum ShotStatusEnum
            {
                HIT,
                HIT_ALREADY_DAMAGED_PART,
                MISS
            }

            /// <summary>
            /// Info about one shot taken.
            /// </summary>
            public class ShotInfo
            {
                /// <summary>
                /// Gets or sets the targetted square.
                /// </summary>
                public Shared.BoardSquareCoordinates TargetSquare { get; set; } = new Shared.BoardSquareCoordinates();

                /// <summary>
                /// Gets or sets whether the shot hit or missed.
                /// </summary>
                public ShotStatusEnum ShotStatus { get; set; } = ShotStatusEnum.MISS;
            }

            /// <summary>
            /// Damage caused by shells and mines.
            /// </summary>
            public class DamageReport
            {
                /// <summary>
                /// Gets or sets the collection of shots taken and whether they hit or missed.
                /// </summary><remarks>
                /// - All SHELL shots are included. 
                /// - MINE shots are reported only if they hit an opponent. Otherwise they lurk unseen.
                /// </remarks>
                public List<ShotInfo> ShotInfos { get; set; } = new List<ShotInfo>();

                /// <summary>
                /// Gets or sets the list of ship-types which have been destroyed since the previous status update.
                /// </summary>
                public List<Shared.ShipTypeEnum> DestroyedShips { get; set; } = new List<Shared.ShipTypeEnum>();

                /// <summary>
                /// Gets or sets the total number of ships.
                /// </summary>
                public int TotalShips { get; set; } = 0;

                /// <summary>
                /// Gets or sets the number of active ships remaining.
                /// </summary>
                public int ShipsRemaining { get; set; } = 0;
            }

            /// <summary>
            /// Enum for the direction (or directions) where a drone detects oppoonent ships.
            /// </summary>
            public enum DroneDetectionEnum
            {
                OVER_SHIP,
                NORTH,
                SOUTH,
                EAST,
                WEST
            }

            /// <summary>
            /// Report of opponent ships detected by a drone.
            /// </summary>
            public class DroneReport
            {
                /// <summary>
                /// Gets or sets the board position of the drone.
                /// </summary>
                public Shared.BoardSquareCoordinates DronePosition { get; set; } = new Shared.BoardSquareCoordinates();

                /// <summary>
                /// Gets or sets the collection of directions relative to the drone where opponent 
                /// ships have been detected.
                /// </summary>
                public List<DroneDetectionEnum> DirectionsDetected { get; set; } = new List<DroneDetectionEnum>();
            }

            /// <summary>
            /// Gets or sets the name of the previous event.
            /// </summary><remarks>
            /// There can be more than one status update in a turn - for example, after the FIRE_WEAPONS event
            /// and after the MOVE event. The name of the previous event is provided here to provide some context
            /// for the data provided with the update.
            /// </remarks>
            public string PreviousEventName { get; set; } = "";

            /// <summary>
            /// Gets or sets the report of damage caused by the player.
            /// </summary><remarks>
            /// - ShotInfos are shots made by the player
            /// - DestroyedShips, TotalShips and ShipsRemaining refer to the opponent's ships
            /// </remarks>
            public DamageReport Player { get; set; } = new DamageReport();

            /// <summary>
            /// Gets or sets the report of damage caused by the opponent.
            /// </summary><remarks>
            /// - ShotInfos are shots made by the opponent
            /// - DestroyedShips, TotalShips and ShipsRemaining refer to the player's ships
            /// </remarks>
            public DamageReport Opponent { get; set; } = new DamageReport();

            /// <summary>
            /// Gets or sets reports from drones which have detected opponent ships.
            /// </summary><remarks>
            /// NOTE 1: Only drones which have detected opponent ships are reported.
            /// NOTE 2: Drones only report directions in which undamaged ship-parts are detected.
            /// </remarks>
            public List<DroneReport> DroneReports { get; set; } = new List<DroneReport>();
        }

        #endregion

        #region AI response

        /// <summary>
        /// Response from AIs to the STATUS_UPDATE event.
        /// </summary><remarks>
        /// The AI should ack with this message when it has processed the STATUS_UPDATE event.
        /// </remarks>
        public class AIResponse : MessageBase
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public AIResponse()
            {
                EventName = StatusUpdate.EventName;
            }
        }

        #endregion
    }
}
