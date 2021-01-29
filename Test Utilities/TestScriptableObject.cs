// Dream Frontier, Copyright (c) DARUMA WORKS, All rights reserved.
// Author: Nathan MacAdam

using UnityEngine;

namespace Oni.TestUtilities
{
    /// <summary>
    /// Creates a ScriptableObject with a component of the given type; is automatically destroyed after testing is complete
    /// </summary>
    /// <typeparam name="T">ScriptableObject type to generate</typeparam>
    public class TestScriptableObject<T> where T : ScriptableObject
	{
		private T _instance;
        public T Instance => _instance;

		public TestScriptableObject()
		{
			_instance = (T)ScriptableObject.CreateInstance(typeof(T));

			if (_instance is IInitializeForTest initialize)
			{
				initialize.Initialize();
			}
		}

		~TestScriptableObject()
		{
			ScriptableObject.DestroyImmediate(_instance);
		}
	}
}