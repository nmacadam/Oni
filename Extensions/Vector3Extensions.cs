// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Returns a copy of the given Vector3, replacing any old values with the ones provided
        /// </summary>
        /// <example>
        /// Vector3 v1 = Vector3(3, 5, 7);
        /// Vector3 v2 = v1.With(y : 1, z : 2);
        /// </example>
        /// <param name="vec">The calling Vector3 instance</param>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <param name="z">Z value</param>
        /// <returns></returns>
        public static Vector3 With(this Vector3 vec, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue) vec.x = x.Value;
            if (y.HasValue) vec.y = y.Value;
            if (z.HasValue) vec.z = z.Value;

            return vec;
        }

        /// <summary>
        /// Returns the direction to a position in 3D space
        /// </summary>
        /// <param name="source">The calling Vector3 instance that is the source position</param>
        /// <param name="destination">The destination position to find the position to</param>
        /// <returns>The direction to the destination position</returns>
        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            // Normalizes the difference between the destination vector and source vector
            return Vector3.Normalize(destination - source);
        }
    }
}