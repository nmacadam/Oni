// Dream Frontier, Copyright (c) DARUMA WORKS, All rights reserved.
// Author: Nathan MacAdam

using UnityEngine;
using UnityEngine.Assertions;

namespace Oni.TestUtilities
{
    public class TestPrefab
    {
        private GameObject _gameObject;

        public GameObject Instance => _gameObject;

        public TestPrefab(string assetPath)
        {
            GameObject gameObjectInFile = Resources.Load(assetPath) as GameObject;
            Assert.IsNotNull(gameObjectInFile, $"Prefab '{assetPath}' was not found");

            _gameObject = GameObject.Instantiate(gameObjectInFile);
        }

        ~TestPrefab()
        {
            GameObject.DestroyImmediate(_gameObject);
        }
    }
}
