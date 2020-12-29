// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

// References:
// Patryk Gałach, How to implement Object Pooling in Unity
//		https://www.patrykgalach.com/2019/04/01/how-to-implement-object-pooling-in-unity/
// Jason Weimann, Object Pooling (in depth) - Game Programming Patterns in Unity & C#
//		https://www.youtube.com/watch?v=uxm4a0QnQ9E

using UnityEngine;

namespace Oni.Patterns
{
	/// <summary>
	/// Makes an object able to be stored in a pool
	/// </summary>
	public interface IPoolable
	{
		IObjectPool Origin { get; set; }
		void Return();
	}

	/// <summary>
	/// Generically pools the object that owns this component
	/// </summary>
	public class PoolableObject : MonoBehaviour, IPoolable
	{
		// Pool to return object
		public IObjectPool Origin { get; set; }

		// Add Set Up and Tear down functions in child class as necessary

		/// <summary> Returns instance to pool </summary>
		public virtual void Return()
		{
			Origin.Return(this);
		}
	}
}