// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Returns a copy of the given Color, replacing any old values with the ones provided
        /// </summary>
        /// <example>
        /// Color c1 = new Color(1, 2, 3, 1);
        /// Color c2 = c1.With(g : 3, a : 0);
        /// </example>
        /// <param name="color">The calling Color instance</param>
        /// <param name="r">Red channel</param>
        /// <param name="g">Green channel</param>
        /// <param name="b">Blue channel</param>
        /// <param name="a">Alpha channel</param>
        /// <returns></returns>
        public static Color With(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            if (r.HasValue) color.r = r.Value;
            if (g.HasValue) color.g = g.Value;
            if (b.HasValue) color.b = b.Value;
            if (a.HasValue) color.a = a.Value;

            return color;
        }

        /// <summary>
        /// Returns the color as an RGB value represented by a Vector3
        /// </summary>
        /// <param name="color">The calling Color instance</param>
        /// <returns>Color as an RGB value represented by a Vector3</returns>
        public static Vector3 RGB(this Color color)
        {
            return new Vector3(color.r, color.g, color.b);
        }

        /// <summary>
        /// Returns the color as an RGBA value represented by a Vector4
        /// </summary>
        /// <param name="color">The calling Color instance</param>
        /// <returns>Color as an RGBA value represented by a Vector4</returns>
        public static Vector4 RGBA(this Color color)
        {
            return new Vector4(color.r, color.g, color.b, color.a);
        }

        /// <summary>
        /// Returns the color as an RGB value represented by a Vector3 with values from 0-255
        /// </summary>
        /// <param name="color">The calling Color instance</param>
        /// <returns>Color as an RGB value represented by a Vector3 with values from 0-255</returns>
        public static Vector3 RGB255(this Color color)
        {
            return color.RGB() * 255;
        }

        /// <summary>
        /// Returns the color as an RGBA value represented by a Vector4 with values from 0-255
        /// </summary>
        /// <param name="color">The calling Color instance</param>
        /// <returns>Color as an RGBA value represented by a Vector4 with values from 0-255</returns>
        public static Vector4 RGBA255(this Color color)
        {
            return color.RGBA() * 255;
        }

        /// <summary>
        /// Returns a string representing the hexadecimal value of the given color.
        /// </summary>
        /// <param name="color">The calling Color instance</param>
        /// <returns>Returns a string representing the hexadecimal value of the given color.</returns>
        public static string HexString(this Color color)
        {
            Vector3 c = color.RGB255();

            int r = Mathf.RoundToInt(c.x);
            int g = Mathf.RoundToInt(c.y);
            int b = Mathf.RoundToInt(c.z);

            return r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }
    
        /// <summary>
        /// Returns the given RGBA color as a Vector4
        /// </summary>
        public static Vector4 ToVector4(this Color color)
        {
            Vector4 vec;
            vec.x = color.r;
            vec.y = color.g;
            vec.z = color.b;
            vec.w = color.a;

            return vec;
        }
    }
}