// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.Assertions;

namespace Oni.TestUtilities
{
	public static class GameObjectTestingExtensions
	{
		public static T GetComponentOrAssertFail<T>(this GameObject gameObject)  where T : Component
		{
			var component = gameObject.GetComponent<T>();

			Assert.IsNotNull(component, $"Failed to retrieve component of type {typeof(T).Name} from '{gameObject.name}'");

			return component;
		}
	}
}