namespace Utility
{
    /// <summary>
    /// Extensions for linq / enumerables.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Returns true if the enumerable contains no elements, false otherwise.
        /// </summary>
        public static bool None<TSource>(this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// Returns true if the enumerable contains no elements which match the predicate, false otherwise.
        /// </summary>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }
    }
}
