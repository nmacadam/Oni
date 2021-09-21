namespace Oni
{
    public static class RangeIntExtensions
    {
        /// <summary>
        /// Returns a random value with this range's min (inclusive) and max (exclusive)
        /// </summary>
        /// <param name="range">This range</param>
        /// <returns>A random integer with this range's min and max</returns>
        public static float Random(this RangeInt range)
        {
            return UnityEngine.Random.Range(range.Min, range.Max);
        }
    }
}