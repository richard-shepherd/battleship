namespace GameEngine
{
    /// <summary>
    /// Holds the board for one player.
    /// </summary><remarks>
    /// The board holds one player's ships plus the positions of any enemy mines and drones.
    /// 
    /// Sparse data
    /// -----------
    /// Boards can be of varying sizes, and some may very large, eg 1000x1000 or larger. The
    /// number of ships and items can be a lot smaller than this. So we do not represent the
    /// entire board grid. Instead we hold a sparse collection of information for squares
    /// which contain something. For example:
    ///   (34, 46) -> Ship[2].Part[3]
    ///   (13, 51) -> Mine
    /// </remarks>
    internal class Board
    {
    }
}
