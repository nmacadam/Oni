// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    public static class Vector4Extensions
    {
        /// <summary>
        /// Returns a copy of the given Vector4, replacing any old values with the ones provided
        /// </summary>
        /// <example>
        /// Vector4 v1 = Vector4(3, 5, 7, 9);
        /// Vector4 v2 = v1.With(y : 1, w : 2);
        /// </example>
        /// <param name="vec">The calling Vector4 instance</param>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <param name="z">Z value</param>
        /// <param name="w">W value</param>
        /// <returns></returns>
        public static Vector4 With(this Vector4 vec, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            if (x.HasValue) vec.x = x.Value;
            if (y.HasValue) vec.y = y.Value;
            if (z.HasValue) vec.z = z.Value;
            if (w.HasValue) vec.w = w.Value;

            return vec;
        }

        /// <summary>
        /// Returns the direction to a position in 4D space
        /// </summary>
        /// <param name="source">The calling Vector4 instance that is the source position</param>
        /// <param name="destination">The destination position to find the position to</param>
        /// <returns>The direction to the destination position</returns>
        public static Vector4 DirectionTo(this Vector4 source, Vector4 destination)
        {
            // Normalizes the difference between the destination vector and source vector
            return Vector4.Normalize(destination - source);
        }

        /// <summary>
        /// Returns the given Vector4 as a RGBA color
        /// </summary>
        public static Color ToColor(this Vector4 source)
        {
            Color color;
            color.r = source.x;
            color.g = source.y;
            color.b = source.z;
            color.a = source.w;

            return color;
        }
    }
}