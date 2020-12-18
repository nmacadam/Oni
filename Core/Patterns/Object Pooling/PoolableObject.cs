﻿// Adapted from https://www.patrykgalach.com/2019/04/01/how-to-implement-object-pooling-in-unity/
// and https://www.youtube.com/watch?v=uxm4a0QnQ9E

using UnityEngine;

namespace Oni.Patterns
{
	/// <summary>
	/// Makes an object able to be stored in a pool
	/// </summary>
	public interface IPoolable
	{
		IObjectPool Origin { get; set; }
		void SetUp();
		void TearDown();
		void Return();
	}

	/// <summary>
	/// Generically pools the object that owns this component
	/// </summary>
	public class PoolableObject : MonoBehaviour, IPoolable
	{
		// Pool to return object
		public IObjectPool Origin { get; set; }

		/// <summary> Prepares instance for use </summary>
		public virtual void SetUp()
		{
			// ...
		}

		/// <summary> Resets instance to prepare for returning </summary>
		public virtual void TearDown()
		{
			// ...
		}

		/// <summary> Returns instance to pool </summary>
		public virtual void Return()
		{
			TearDown();
			Origin.Return(this);
		}
	}
}