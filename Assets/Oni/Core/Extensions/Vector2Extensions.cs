// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Returns a copy of the given Vector2, replacing any old values with the ones provided
        /// </summary>
        /// <example>
        /// Vector2 v1 = Vector2.zero;
        /// Vector2 v2 = v1.With(y : 1);
        /// </example>
        /// <param name="vec">The calling Vector2 instance</param>
        /// <param name="x">X value</param>
        /// <param name="y">Y value</param>
        /// <returns></returns>
        public static Vector2 With(this Vector2 vec, float? x = null, float? y = null)
        {
            if (x.HasValue) vec.x = x.Value;
            if (y.HasValue) vec.y = y.Value;

            return vec;
        }
    }
}