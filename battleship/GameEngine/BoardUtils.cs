using Utility;

namespace GameEngine
{
    /// <summary>
    /// Utility functions for the board.
    /// </summary>
    internal class BoardUtils
    {
        #region Public methods

        /// <summary>
        /// Validates ship placements, confirming that:
        /// - There are no more ships specified than the allowed ship-squares
        /// - Ships are placed within the board
        /// - Ships do not overlap
        /// Returns true if the ship placement is valid, false if not.
        /// </summary>
        public static bool validateShipPlacement(int boardSize, int shipSquares, List<API.StartGame.AIResponse.ShipPlacement> shipPlacements)
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
                        Logger.log("Ship placements overlap");
                        return false;
                    }

                    // Is the ship-part on the board?
                    if (boardPosition.X < 1 || boardPosition.X > boardSize
                        ||
                        boardPosition.Y < 1 || boardPosition.Y > boardSize)
                    {
                        Logger.log("Ship placement is not on the board");
                        return false;
                    }

                    // We note the ship-part location for checking against other ships...
                    shipPartLocations.Add(boardPosition);
                }
            }

            // We check that there are no more ship-squares used than was specified...
            if(shipPartLocations.Count > shipSquares)
            {
                Logger.log("Too many ships specified");
                return false;
            }

            return true;
        }

        #endregion
    }
}
