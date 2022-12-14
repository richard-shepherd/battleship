using Utility;

namespace GameEngine
{
    /// <summary>
    /// Utility functions for the board.
    /// </summary>
    public class BoardUtils
    {
        #region Public methods

        /// <summary>
        /// Validates ship placements, confirming that:
        /// - There are no more ships specified than the allowed ship-squares
        /// - Ships are placed within the board
        /// - Ships do not overlap
        /// Returns true if the ship placement is valid, false if not.
        /// </summary>
        public static (bool Valid, string Message) validateShipPlacement(int boardSize, int shipSquares, List<API.Shared.ShipPlacement> shipPlacements)
        {
            var shipPartLocations = new HashSet<(int, int)>();

            // We create a 'dummy' ship for each requested ship-placement...
            foreach (var shipPlacement in shipPlacements)
            {
                var ship = new Ship(shipPlacement);

                // We check the location of each ship-part...
                foreach(var shipPart in ship.ShipParts)
                {
                    var boardPosition = shipPart.BoardPositionTuple;

                    // Is the part on a square already used by another ship?
                    if(shipPartLocations.Contains(boardPosition))
                    {
                        return (false, "Ship placements overlap");
                    }

                    // Is the ship-part on the board?
                    if (boardPosition.X < 1 || boardPosition.X > boardSize
                        ||
                        boardPosition.Y < 1 || boardPosition.Y > boardSize)
                    {
                        return (false, "Ship placement is not on the board");
                    }

                    // We note the ship-part location for checking against other ships...
                    shipPartLocations.Add(boardPosition);
                }
            }

            // We check that there are no more ship-squares used than was specified...
            if(shipPartLocations.Count > shipSquares)
            {
                return (false, "Too many ships specified");
            }

            return (true, "");
        }

        /// <summary>
        /// Validates a collection of movement requests.
        /// Returns true if they are valid, false if not.
        /// </summary>
        public static (bool Valid, string Message) validateMovementRequests(Board board, string aiName, List<API.Move.AIResponse.MovementRequest> movementRequests, Dictionary<int, int> fuelRequiredPerShip)
        {
            // If no movement requests were made, the request is valid...
            if(movementRequests.None())
            {
                return (true, "");
            }

            // We enrich the movement requests to include the ship-type, which may
            // not have been specified by the AI. This is needed during validation
            // as it determines the size of the ships...
            foreach (var movementRequest in movementRequests)
            {
                var ship = board.Ships[movementRequest.Index];
                movementRequest.ShipPlacement.ShipType = ship.ShipType;
            }

            // We validate the movement. This means checking that:
            // - Each ship has enough fuel for the movement
            // - Ship positions after movement are on the board
            // - Ship positions after movement do not overlap
            //
            // If any of these validations fails, we do not move the ships.

            // We check the fuel needed for each ship movement.
            // We note this in a dictionary of ship-index -> fuel-required, so that we can 
            // take the fuel from the ships laster if validation passes.
            foreach (var movementRequest in movementRequests)
            {
                var ship = board.Ships[movementRequest.Index];
                var fuelRequired = checkFuel(ship, movementRequest.ShipPlacement);
                if (fuelRequired == -1)
                {
                    // The ship does not have enough fuel for this movement request...
                    return (false, $"{aiName}: Not enough fuel for movement request");
                }
                else
                {
                    // The ship has enough fuel...
                    fuelRequiredPerShip[movementRequest.Index] = fuelRequired;
                }
            }

            // We have enough fuel to make each movement, so we check that the new ship positions
            // are valid. To do this, we get the collection of all ship-placements including 
            // existing ship positions and any that have moved...
            var shipPlacements = board.Ships.Select(x => x.ShipPlacement).ToList();
            foreach (var movementRequest in movementRequests)
            {
                shipPlacements[movementRequest.Index] = movementRequest.ShipPlacement;
            }
            var numShipSquares = board.Ships.SelectMany(x => x.ShipParts).Count();
            var shipPlacementInfo = BoardUtils.validateShipPlacement(board.BoardSize, numShipSquares, shipPlacements);
            if (!shipPlacementInfo.Valid)
            {
                // The new ship placements are not valid...
                var message = $"{aiName}: Ship placements not valid after movement request: {shipPlacementInfo.Message}";
                return (false, message);
            }

            // The movement request looks valid...
            return (true, "");
        }

        /// <summary>
        /// Checks that the ship has enough fuel to make the movement requested.
        /// 
        /// Returns the amount of fuel used if the move can be made, or -1 if the ship
        /// does not have enough fuel for the move.
        /// </summary><remarks>
        /// The amount of fuel used to move a ship is the amount of fuel needed to move each 
        /// ship-part to its new position. The amount of fuel used to move a ship-part is the
        /// difference in x-position plus the difference in y-position.
        /// </remarks>
        public static int checkFuel(Ship ship, API.Shared.ShipPlacement requestedShipPlacement)
        {
            // To find the positions of each ship-part after movement, we create a temporary
            // Ship in the new position...
            var movedShip = new Ship(requestedShipPlacement);

            // We see how far each ship-part has moved and use this to calculate
            // the fuel required for the move...
            var fuelRequired = 0;
            var numShipParts = ship.ShipParts.Count;
            for (var i = 0; i < numShipParts; ++i)
            {
                var shipPart = ship.ShipParts[i];
                var movedShipPart = movedShip.ShipParts[i];
                fuelRequired += Math.Abs(shipPart.BoardPosition.X - movedShipPart.BoardPosition.X);
                fuelRequired += Math.Abs(shipPart.BoardPosition.Y - movedShipPart.BoardPosition.Y);
            }

            // We check if the ship has enough fuel to make this move...
            if (ship.Fuel >= fuelRequired)
            {
                // The ship has enough fuel for this move...
                return fuelRequired;
            }
            else
            {
                // The ship does not have enough fuel for this move...
                return -1;
            }
        }

        /// <summary>
        /// Checks if the shot specified has hit one of the opponent's ships.
        /// Returns HIT or MISS, or HIT_ALREADY_DAMAGED_PART if the shot hits a part which has
        /// previously been hit. If the shot is a HIT, the ship-part is also returned.
        /// </summary><remarks>
        /// If the shot has hit a ship, the ship-part it hit is marked as damaged.
        /// </remarks>
        public static (API.StatusUpdate.Message.ShotStatusEnum ShotStatus, ShipPart ShipPart) checkForHit(Board board, API.Shared.BoardSquareCoordinates targetSquare)
        {
            // We check if the shell hit an opponent ship...
            var shipPart = board.getShipPart(targetSquare.X, targetSquare.Y);
            if (shipPart == null)
            {
                // There was no ship at the target square...
                return (API.StatusUpdate.Message.ShotStatusEnum.MISS, null);
            }

            // There is a ship-part at the target square. We check if we have hit an already damaged part...
            if (shipPart.IsDamaged)
            {
                return (API.StatusUpdate.Message.ShotStatusEnum.HIT_ALREADY_DAMAGED_PART, null);
            }

            // We have hit a ship part...
            shipPart.IsDamaged = true;
            return (API.StatusUpdate.Message.ShotStatusEnum.HIT, shipPart);
        }

        #endregion
    }
}
