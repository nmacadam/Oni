// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

namespace Oni
{
    public static class RangeExtensions
    {
        /// <summary>
        /// Returns a random value with this range's min and max
        /// </summary>
        /// <param name="range">This range</param>
        /// <returns>A random float with this range's min and max</returns>
        public static float Random(this Range range)
        {
            return UnityEngine.Random.Range(range.Min, range.Max);
        }
    }
}