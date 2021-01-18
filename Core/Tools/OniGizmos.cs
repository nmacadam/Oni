// Dream Frontier, Copyright (c) DARUMA WORKS, All rights reserved.
// Author: Nathan MacAdam

using UnityEngine;

namespace Oni
{
	/// <summary>
	/// Additional gizmos for debugging and visual info
	/// </summary>
	public static class OniGizmos
	{
		/// <summary>
		/// Draw a bounding box for the given <see cref="Bounds"/>
		/// </summary>
		/// <param name="bounds">The <see cref="Bounds"/> to draw a bounding box for</param>
		public static void DrawBounds(Bounds bounds)
		{
			// bottom points
			var p1 = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z) + bounds.center;
			var p2 = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z) + bounds.center;
			var p3 = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z) + bounds.center;
			var p4 = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z) + bounds.center;
			
			// top points
			var p5 = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z) + bounds.center;
			var p6 = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z) + bounds.center;
			var p7 = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z) + bounds.center;
			var p8 = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z) + bounds.center;

			// draw bottom
			Gizmos.DrawLine(p1, p2);
			Gizmos.DrawLine(p2, p3);
			Gizmos.DrawLine(p3, p4);
			Gizmos.DrawLine(p4, p1);

			// draw top
			Gizmos.DrawLine(p5, p6);
			Gizmos.DrawLine(p6, p7);
			Gizmos.DrawLine(p7, p8);
			Gizmos.DrawLine(p8, p5);

			// draw sides
			Gizmos.DrawLine(p1, p5);
			Gizmos.DrawLine(p2, p6);
			Gizmos.DrawLine(p3, p7);
			Gizmos.DrawLine(p4, p8);
		}
	}
}