// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

// References:
// Patryk Gałach, How to implement Object Pooling in Unity
//		https://www.patrykgalach.com/2019/04/01/how-to-implement-object-pooling-in-unity/
// Jason Weimann, Object Pooling (in depth) - Game Programming Patterns in Unity & C#
//		https://www.youtube.com/watch?v=uxm4a0QnQ9E

using System.Collections.Generic;
using UnityEngine;

namespace Oni.Patterns
{
	/// <summary>
	/// Base interface for object pool used in poolable objects
	/// </summary>
	public interface IObjectPool
	{
		void Return(object instance);
	}

	/// <summary>
	/// Generic interface with more methods for object pooling
	/// </summary>
	public interface IObjectPool<T> : IObjectPool where T : IPoolable
	{
		T Get(bool setActive);
		void Return(T instance);
	}

	/// <summary>
	/// Implementation of the Object Pool pattern for MonoBehaviour based objects
	/// </summary>
	public class ObjectPool<T> : MonoBehaviour, IObjectPool<T>
		where T : Component, IPoolable
	{
		[Tooltip("Provide a cloneable instance to initialize the pool with (typically a prefab)")]
		[SerializeField] private T _clonableInstance = default;
		[Tooltip("How many instances should the pool start with?")]
		[SerializeField] private int _initialSize = 1;

		public T ClonableInstance => _clonableInstance;

		private Queue<T> _pool = new Queue<T>();

		private void Awake()
		{
			// Note: _clonableInstance is not added to the pool because it is likely a prefab
			
			if (_clonableInstance != null)
			{
				Add(_initialSize - _pool.Count);
			}
		}

		/// <summary> Retrieves an instance from the object pool </summary>
		public T Get(bool setActive = true)
		{
			if (_clonableInstance == null)
			{
				return null;
			}

			if (_pool.Count == 0)
			{
				Add(1);
			}

			var instance = _pool.Dequeue();
			instance.gameObject.SetActive(setActive);
			return instance;
		}

		/// <summary> Returns an instance to the object pool </summary>
		public void Return(T instance)
		{
			instance.gameObject.SetActive(false);
			instance.transform.SetParent(transform);
        	instance.transform.localPosition = Vector3.zero;
        	instance.transform.localScale = Vector3.one;
        	instance.transform.localEulerAngles = Vector3.one;

        	_pool.Enqueue(instance);
		}

		/// <summary> Returns an instance to the object pool </summary>
		/// <remarks> This method will fail if instance parameter does not match the pool type  </remarks>
        public void Return(object instance)
        {
            if (instance is T t)
			{
				Return(t);
			}
        }

		/// <summary> Increases the pool size </summary>
		public void Add(int quantity)
		{
			for (int i = 0; i < quantity; i++)
			{
				AddToPool(Instantiate(_clonableInstance, _clonableInstance.transform.parent));
			}
		}

		private void AddToPool(T instance)
		{
			instance.gameObject.SetActive(false);
			_pool.Enqueue(instance);
			instance.GetComponent<IPoolable>().Origin = this;
		}
    }
}