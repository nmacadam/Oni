// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System;
using UnityEngine;

namespace Oni.TestUtilities
{
    /// <summary>
    /// Creates a GameObject with a component of the given type; its GameObject is automatically destroyed at the end of the 
	/// TestComponent's lifecycle
    /// </summary>
	/// <remarks> 
	/// This component is meant for testing; it provides a simple way to create an instance of component that automatically 
	/// cleans up after itself
	/// </remarks>
    /// <typeparam name="T">Component type to generate</typeparam>
    public class TestComponent<T> where T : MonoBehaviour
    {
        private GameObject _gameObject;
		private T _instance;

        public T Instance => _instance;

        public TestComponent()
		{
			_gameObject = new GameObject();
			_instance = _gameObject.AddComponent<T>();

            if (_instance is IInitializeForTest initialize)
			{
				initialize.Initialize();
			}
		}

        public TestComponent(params Type[] extraComponents)
		{
			_gameObject = new GameObject();
			_instance = _gameObject.AddComponent<T>();

            if (_instance is IInitializeForTest initialize)
			{
				initialize.Initialize();
			}

            foreach (var component in extraComponents)
            {
                _gameObject.AddComponent(component);
            }
		}

		~TestComponent()
		{
			UnityEngine.Object.Destroy(_gameObject);
		}
    }
}
