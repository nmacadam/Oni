// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Oni.TestUtilities
{
    /// <summary>
    /// Creates a GameObject that is automatically destroyed at the end of the 
	/// TestGameObject's lifecycle
    /// </summary>
	/// <remarks> 
	/// This component is meant for testing; it provides a simple way to create an instance of GameObject that automatically 
	/// cleans up after itself
	/// </remarks>
    public class TestGameObject
    {
        private GameObject _gameObject;
        public GameObject Instance => _gameObject;

        public TestGameObject()
		{
			_gameObject = new GameObject();
		}

		~TestGameObject()
		{
			UnityEngine.Object.Destroy(_gameObject);
		}
    }
}
