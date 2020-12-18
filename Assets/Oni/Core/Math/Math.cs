// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
    /// <summary>
	/// Math functions and utilities
	/// </summary>
    public static class Math
    {
        /*   Decimal Operations   */

        /// <summary>
        /// Remap a float value from range [low1, high1] to [low2, high2]
        /// </summary>
        public static float Remap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }

		/// <summary>
		/// Returns a random value in normal distribution for the given standard deviation and mean
		/// </summary>
		/// <param name="stddev">standard deviation of the distribution</param>
		/// <param name="mean">mean of the distribution</param>
		public static float RandomNormalDistribution(float stddev, float mean = 0)
		{
			float r1 = Random.Range(Mathf.Epsilon, 1f);
			float r2 = Random.Range(Mathf.Epsilon, 1f);

			float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(r1)) * Mathf.Sin(2.0f * Mathf.PI * r2);

			return mean + stddev * randStdNormal;
		}

		/// <summary>
		/// Returns a random value in a normal distribution in the given range
		/// </summary>
		/// <param name="min">minimum value (inclusive)</param>
		/// <param name="max">maximum value (inclusive)</param>
		public static float RandomGaussian(float min, float max)
		{
			float normalR = Mathf.Clamp(RandomNormalDistribution(.3333f), -1, 1);
			return Remap(normalR, -1, 1, min, max);
		}


        /*   Vector Operations   */

        /// <summary>
        /// Midpoint between two 2D points
        /// </summary>
        public static Vector2 Midpoint(Vector2 a, Vector2 b)
        {
            return new Vector2((a.x + b.x) / 2, (a.y + b.y) / 2);
        }

        /// <summary>
        /// Midpoint between two 3D points
        /// </summary>
        public static Vector3 Midpoint(Vector3 a, Vector3 b)
        {
            return new Vector3((a.x + b.x) / 2, (a.y + b.y) / 2, (a.z + b.z) / 2);
        }

        /// <summary>
        /// Quadratic 2D bezier curve
        /// </summary>
        /// <param name="start">Start position</param>
        /// <param name="end">End position</param>
        /// <param name="control">Curve control point</param>
        /// <param name="t">0-1 interpolation step</param>
        /// <returns>2D position on the curve at interpolation step t</returns>
        public static Vector2 QuadBezier(Vector2 start, Vector2 end, Vector2 control, float t)
        {
            return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
        }

        /// <summary>
        /// Quadratic 3D bezier curve
        /// </summary>
        /// <param name="start">Start position</param>
        /// <param name="end">End position</param>
        /// <param name="control">Curve control point</param>
        /// <param name="t">0-1 interpolation step</param>
        /// <returns>3D position on the curve at interpolation step t</returns>
        public static Vector3 QuadBezier(Vector3 start, Vector3 end, Vector3 control, float t)
        {
            return (((1 - t) * (1 - t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
        }

        /// <summary>
        /// Cubic 2D bezier curve
        /// </summary>
        /// <param name="start">Start position</param>
        /// <param name="end">End position</param>
        /// <param name="control1">Curve control point 1</param>
        /// <param name="control2">Curve control point 2</param>
        /// <param name="t">0-1 interpolation step</param>
        /// <returns>2D position on the curve at interpolation step t</returns>
        public static Vector2 CubicBezier(Vector2 start, Vector2 end, Vector2 control1, Vector2 control2, float t)
        {
            return (((-start + 3 * (control1 - control2) + end) * t + (3 * (start + control2) - 6 * control1)) * t + 3 * (control1 - start)) * t + start;
        }

        /// <summary>
        /// Cubic 3D bezier curve
        /// </summary>
        /// <param name="start">Start position</param>
        /// <param name="end">End position</param>
        /// <param name="control1">Curve control point 1</param>
        /// <param name="control2">Curve control point 2</param>
        /// <param name="t">0-1 interpolation step</param>
        /// <returns>3D position on the curve at interpolation step t</returns>
        public static Vector3 CubicBezier(Vector3 start, Vector3 end, Vector3 control1, Vector3 control2, float t)
        {
            return (((-start + 3 * (control1 - control2) + end) * t + (3 * (start + control2) - 6 * control1)) * t + 3 * (control1 - start)) * t + start;
        }
    }
}