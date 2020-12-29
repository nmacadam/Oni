// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
	public static class TransformExtensions
	{
		/// <summary>
		/// Adds a list of GameObjects as children of this transform
		/// </summary>
		/// <param name="transform">This</param>
		/// <param name="children">List of GameObjects to child</param>
		public static void AddChildren(this Transform transform, GameObject[] children)
		{
			foreach (var child in children)
			{
				child.transform.parent = transform;
			}
		}
		
		/// <summary>
		/// Adds a list of Transforms as children of this transform
		/// </summary>
		/// <param name="transform">This</param>
		/// <param name="children">List of Transforms to child</param>
		public static void AddChildren(this Transform transform, Transform[] children)
		{
			foreach (var child in children)
			{
				child.parent = transform;
			}
		}

		/// <summary>
		/// Matches the elements of this transform to another
		/// </summary>
		/// <param name="transform">This transform</param>
		/// <param name="other">Transform to match</param>
		public static void MatchTransform(this Transform transform, Transform other)
		{
			transform.position = other.position;
			transform.rotation = other.rotation;
			transform.localScale = other.localScale;
		}
	}
}