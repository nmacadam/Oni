// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni
{
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Gets a component from a GameObject if present, or adds one if missing
		/// </summary>
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			var component = gameObject.GetComponent<T>();

			if (component != null)
			{
				return component;
			}
			else
			{
				return gameObject.AddComponent<T>();
			}
		}

		/// <summary>
		/// Checks whether the GameObject has a component of the given type
		/// </summary>
		public static bool HasComponent<T>(this GameObject gameObject) where T : Component
		{
			return gameObject.GetComponent<T>() != null;
		}
	}
}