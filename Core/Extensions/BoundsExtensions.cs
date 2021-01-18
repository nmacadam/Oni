// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
	public static class BoundsExtensions
	{
		/// <summary>
		/// Create a new <see cref="Bounds"/> that contains both both of the original bounds 
		/// </summary>
		/// <param name="a">This <see cref="Bounds"/></param>
		/// <param name="b">The <see cref="Bounds"/> to combine with</param>
		/// <returns>The new <see cref="Bounds"/> containing both of the original</returns>
		public static Bounds Combine(this Bounds a, Bounds b)
		{
			Bounds output = new Bounds(a.center, a.extents * 2f);

			output.Encapsulate(b.center + new Vector3(b.extents.x, 0, 0));
			output.Encapsulate(b.center - new Vector3(b.extents.x, 0, 0));
			
			output.Encapsulate(b.center + new Vector3(0, b.extents.y, 0));
			output.Encapsulate(b.center - new Vector3(0, b.extents.y, 0));
			
			output.Encapsulate(b.center + new Vector3(0, 0, b.extents.z));
			output.Encapsulate(b.center - new Vector3(0, 0, b.extents.z));

			return output;
		}
	}
}