// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
	public static class CameraExtensions
	{
		/// <summary>
		/// Returns whether the point is in the camera view frustum
		/// </summary>
		/// <remarks>
		/// This method does not take into account the visibility of the object in any way
		/// </remarks>
		/// <param name="camera">This camera</param>
		/// <param name="collider">The collider to look for</param>
		/// <returns>Whether the collider is in the camera view frustum</returns>
		public static bool InFrame(this Camera camera, Vector3 position)
		{
			Bounds bounds = new Bounds(position, Vector3.zero);
			return (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), bounds));
		}

		/// <summary>
		/// Returns whether the collider is in the camera view frustum
		/// </summary>
		/// <remarks>
		/// This method does not take into account the visibility of the object in any way
		/// </remarks>
		/// <param name="camera">This camera</param>
		/// <param name="collider">The collider to look for</param>
		/// <returns>Whether the collider is in the camera view frustum</returns>
		public static bool InFrame(this Camera camera, Collider collider)
		{
			return (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), collider.bounds));
		}
	}
}